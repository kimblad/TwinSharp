using System.Globalization;
using System.Text;
using TwinCAT.Ads;
using TwinCAT.TypeSystem;

namespace TwinSharpControls
{
    public partial class DataLogger : UserControl
    {
        private AmsNetId? targetNetId;
        private AdsClient? adsClient;
        private List<LogChannel> channels = new List<LogChannel>();
        private List<DataPoint> dataPoints = new List<DataPoint>();
        private bool isLogging = false;
        private DateTime loggingStartTime;
        private int maxDataPoints = 1000; // Keep last 1000 points

        public DataLogger()
        {
            InitializeComponent();
            chartPanel.Paint += ChartPanel_Paint;
        }

        /// <summary>
        /// Set the target AmsNetId to connect to.
        /// </summary>
        public AmsNetId? Target
        {
            get => targetNetId;
            set
            {
                targetNetId = value;
                ConnectToTarget();
            }
        }

        /// <summary>
        /// Sampling interval in milliseconds.
        /// </summary>
        public int SamplingInterval
        {
            get => timerSample.Interval;
            set => timerSample.Interval = Math.Max(10, value); // Minimum 10ms
        }

        private void ConnectToTarget()
        {
            try
            {
                adsClient?.Dispose();
                adsClient = null;

                if (targetNetId == null)
                {
                    lblStatus.Text = "Status: Not connected";
                    return;
                }

                adsClient = new AdsClient();
                adsClient.Connect(targetNetId, 851); // Connect to PLC runtime by default

                lblStatus.Text = $"Status: Connected to {targetNetId}";
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Connection error: {ex.Message}";
                lblError.Visible = true;
                lblStatus.Text = "Status: Connection failed";
            }
        }

        private void btnBrowseSymbol_Click(object? sender, EventArgs e)
        {
            if (targetNetId == null)
            {
                MessageBox.Show("Please select a target first.", "No Target", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var dialog = new SymbolBrowserDialog(targetNetId);
                if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedSymbolPath))
                {
                    txtSymbolName.Text = dialog.SelectedSymbolPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening symbol browser: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            var symbolName = txtSymbolName.Text.Trim();
            if (string.IsNullOrEmpty(symbolName))
            {
                MessageBox.Show("Please enter a symbol name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (adsClient == null)
            {
                MessageBox.Show("Not connected to target.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Try to create a symbol handle
                uint handle = adsClient.CreateVariableHandle(symbolName);

                var channel = new LogChannel
                {
                    Name = symbolName,
                    Handle = handle,
                    Color = GetNextColor(),
                    Enabled = true
                };

                channels.Add(channel);
                UpdateChannelList();

                txtSymbolName.Clear();
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error adding channel: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void btnRemoveChannel_Click(object sender, EventArgs e)
        {
            if (listBoxChannels.SelectedIndex >= 0)
            {
                var channel = channels[listBoxChannels.SelectedIndex];

                try
                {
                    adsClient?.DeleteVariableHandle(channel.Handle);
                }
                catch { }

                channels.RemoveAt(listBoxChannels.SelectedIndex);
                UpdateChannelList();
            }
        }

        private void UpdateChannelList()
        {
            listBoxChannels.Items.Clear();
            foreach (var channel in channels)
            {
                var statusText = channel.Enabled ? "✓" : "✗";
                listBoxChannels.Items.Add($"{statusText} {channel.Name}");
            }
        }

        private Color GetNextColor()
        {
            Color[] colors = new[]
            {
                Color.Blue, Color.Red, Color.Green, Color.Orange,
                Color.Purple, Color.Brown, Color.Cyan, Color.Magenta
            };
            return colors[channels.Count % colors.Length];
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (channels.Count == 0)
            {
                MessageBox.Show("Add at least one channel before starting.", "No Channels", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isLogging = true;
            loggingStartTime = DateTime.Now;
            timerSample.Start();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;

            lblStatus.Text = "Status: Logging...";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isLogging = false;
            timerSample.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;

            lblStatus.Text = $"Status: Stopped ({dataPoints.Count} samples)";
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (timerSample.Enabled)
            {
                timerSample.Stop();
                btnPause.Text = "Resume";
                lblStatus.Text = "Status: Paused";
            }
            else
            {
                timerSample.Start();
                btnPause.Text = "Pause";
                lblStatus.Text = "Status: Logging...";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataPoints.Clear();
            chartPanel.Invalidate();
            lblStatus.Text = "Status: Cleared";
            lblSampleCount.Text = "Samples: 0";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataPoints.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                DefaultExt = "csv",
                FileName = $"DataLog_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(sfd.FileName);
            }
        }

        private void ExportToCsv(string filePath)
        {
            try
            {
                var sb = new StringBuilder();

                // Header
                sb.Append("Timestamp,Time (s)");
                foreach (var channel in channels)
                {
                    sb.Append($",{channel.Name}");
                }
                sb.AppendLine();

                // Data rows
                foreach (var point in dataPoints)
                {
                    sb.Append($"{point.Timestamp:yyyy-MM-dd HH:mm:ss.fff},{point.TimeSeconds:F3}");
                    foreach (var channel in channels)
                    {
                        var value = point.Values.ContainsKey(channel.Name) ? point.Values[channel.Name] : double.NaN;
                        sb.Append($",{value}");
                    }
                    sb.AppendLine();
                }

                File.WriteAllText(filePath, sb.ToString());
                MessageBox.Show($"Data exported successfully to:\n{filePath}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerSample_Tick(object sender, EventArgs e)
        {
            if (!isLogging || adsClient == null || channels.Count == 0)
                return;

            try
            {
                var point = new DataPoint
                {
                    Timestamp = DateTime.Now,
                    TimeSeconds = (DateTime.Now - loggingStartTime).TotalSeconds,
                    Values = new Dictionary<string, double>()
                };

                foreach (var channel in channels.Where(c => c.Enabled))
                {
                    try
                    {
                        var value = adsClient.ReadAny(channel.Handle, typeof(double));
                        point.Values[channel.Name] = Convert.ToDouble(value);
                    }
                    catch
                    {
                        point.Values[channel.Name] = double.NaN;
                    }
                }

                dataPoints.Add(point);

                // Limit data points
                if (dataPoints.Count > maxDataPoints)
                {
                    dataPoints.RemoveAt(0);
                }

                lblSampleCount.Text = $"Samples: {dataPoints.Count}";
                chartPanel.Invalidate();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Sampling error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void ChartPanel_Paint(object sender, PaintEventArgs e)
        {
            if (dataPoints.Count == 0 || channels.Count == 0)
            {
                e.Graphics.Clear(Color.White);
                e.Graphics.DrawString("No data to display", Font, Brushes.Gray, 10, 10);
                return;
            }

            var g = e.Graphics;
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Drawing area
            var marginLeft = 60;
            var marginRight = 20;
            var marginTop = 20;
            var marginBottom = 40;
            var chartWidth = chartPanel.Width - marginLeft - marginRight;
            var chartHeight = chartPanel.Height - marginTop - marginBottom;

            if (chartWidth <= 0 || chartHeight <= 0)
                return;

            // Draw axes
            g.DrawLine(Pens.Black, marginLeft, marginTop, marginLeft, marginTop + chartHeight);
            g.DrawLine(Pens.Black, marginLeft, marginTop + chartHeight, marginLeft + chartWidth, marginTop + chartHeight);

            // Find min/max values across all channels
            var allValues = dataPoints.SelectMany(p => p.Values.Values.Where(v => !double.IsNaN(v))).ToList();
            if (allValues.Count == 0)
                return;

            var minValue = allValues.Min();
            var maxValue = allValues.Max();
            var valueRange = maxValue - minValue;
            if (valueRange == 0) valueRange = 1;

            var minTime = dataPoints.First().TimeSeconds;
            var maxTime = dataPoints.Last().TimeSeconds;
            var timeRange = maxTime - minTime;
            if (timeRange == 0) timeRange = 1;

            // Draw grid
            using var gridPen = new Pen(Color.LightGray);
            for (int i = 0; i <= 10; i++)
            {
                var y = marginTop + (chartHeight * i / 10);
                g.DrawLine(gridPen, marginLeft, y, marginLeft + chartWidth, y);

                var value = maxValue - (valueRange * i / 10);
                g.DrawString(value.ToString("F2"), Font, Brushes.Black, 5, y - 7);
            }

            // Draw data for each channel
            foreach (var channel in channels.Where(c => c.Enabled))
            {
                using var pen = new Pen(channel.Color, 2);
                PointF? lastPoint = null;

                foreach (var point in dataPoints)
                {
                    if (!point.Values.ContainsKey(channel.Name) || double.IsNaN(point.Values[channel.Name]))
                        continue;

                    var value = point.Values[channel.Name];
                    var x = marginLeft + (float)((point.TimeSeconds - minTime) / timeRange * chartWidth);
                    var y = marginTop + chartHeight - (float)((value - minValue) / valueRange * chartHeight);

                    if (lastPoint.HasValue)
                    {
                        g.DrawLine(pen, lastPoint.Value, new PointF(x, y));
                    }

                    lastPoint = new PointF(x, y);
                }
            }

            // Draw time labels
            for (int i = 0; i <= 5; i++)
            {
                var x = marginLeft + (chartWidth * i / 5);
                var time = minTime + (timeRange * i / 5);
                g.DrawString(time.ToString("F1") + "s", Font, Brushes.Black, x - 15, marginTop + chartHeight + 5);
            }

            // Draw legend
            var legendY = 5;
            foreach (var channel in channels.Where(c => c.Enabled))
            {
                using var brush = new SolidBrush(channel.Color);
                g.FillRectangle(brush, chartWidth + marginLeft - 100, legendY, 15, 10);
                g.DrawString(channel.Name, Font, Brushes.Black, chartWidth + marginLeft - 80, legendY - 2);
                legendY += 20;
            }
        }

        private void numericSampleRate_ValueChanged(object sender, EventArgs e)
        {
            SamplingInterval = (int)numericSampleRate.Value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timerSample.Stop();

                if (adsClient != null)
                {
                    foreach (var channel in channels)
                    {
                        try
                        {
                            adsClient.DeleteVariableHandle(channel.Handle);
                        }
                        catch { }
                    }
                    adsClient.Dispose();
                }

                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class LogChannel
    {
        public string Name { get; set; } = "";
        public uint Handle { get; set; }
        public Color Color { get; set; }
        public bool Enabled { get; set; }
    }

    public class DataPoint
    {
        public DateTime Timestamp { get; set; }
        public double TimeSeconds { get; set; }
        public Dictionary<string, double> Values { get; set; } = new Dictionary<string, double>();
    }
}
