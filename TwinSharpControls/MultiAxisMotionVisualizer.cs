using TwinCAT.Ads;
using TwinSharp;
using TwinSharp.NC;

namespace TwinSharpControls
{
    public partial class MultiAxisMotionVisualizer : UserControl
    {
        private NC? nc;
        private System.Windows.Forms.Timer? updateTimer;
        private Axis[]? axes;
        private int updateInterval = 100; // 100ms default

        public MultiAxisMotionVisualizer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the target AmsNetId.
        /// </summary>
        public AmsNetId? Target
        {
            set
            {
                StopAutoRefresh();

                if (value == null)
                {
                    nc = null;
                    axes = null;
                }
                else
                {
                    try
                    {
                        nc = new NC(value);
                        axes = nc.Axes;
                        UpdateAxisGrid();
                        StartAutoRefresh();
                        lblError.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = $"Error connecting to NC: {ex.Message}";
                        lblError.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Auto-refresh enabled.
        /// </summary>
        public bool AutoRefresh { get; set; } = true;

        /// <summary>
        /// Update interval in milliseconds.
        /// </summary>
        public int UpdateInterval
        {
            get => updateInterval;
            set
            {
                updateInterval = value;
                if (updateTimer != null)
                {
                    updateTimer.Interval = value;
                }
            }
        }

        private void StartAutoRefresh()
        {
            if (!AutoRefresh)
                return;

            if (updateTimer == null)
            {
                updateTimer = new System.Windows.Forms.Timer();
                updateTimer.Tick += UpdateTimer_Tick;
            }

            updateTimer.Interval = updateInterval;
            updateTimer.Start();
        }

        private void StopAutoRefresh()
        {
            if (updateTimer != null)
            {
                updateTimer.Stop();
            }
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateAxisGrid();
        }

        private void UpdateAxisGrid()
        {
            if (axes == null || axes.Length == 0)
                return;

            try
            {
                // Save selected index
                int selectedIndex = -1;
                if (dataGridViewAxes.SelectedRows.Count > 0)
                    selectedIndex = dataGridViewAxes.SelectedRows[0].Index;

                dataGridViewAxes.Rows.Clear();

                foreach (var axis in axes)
                {
                    var online = axis.State.OnlineData;
                    var status = online.AxisStatusDWord;

                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridViewAxes);
                    row.Tag = axis;

                    // Axis Name
                    row.Cells[0].Value = axis.Parameters.Name;

                    // Actual Position
                    row.Cells[1].Value = FormatPosition(online.ActualPosition);

                    // Set Position
                    row.Cells[2].Value = FormatPosition(online.SetPosition);

                    // Lag Error
                    row.Cells[3].Value = FormatPosition(online.FollowingErrorPosition);

                    // Actual Velocity
                    row.Cells[4].Value = FormatVelocity(online.ActualVelocity);

                    // Set Velocity
                    row.Cells[5].Value = FormatVelocity(online.SetVelocity);

                    // Status flags
                    row.Cells[6].Value = GetStatusIndicator(status, StateDWordFlags.ReadyForOperation);
                    row.Cells[7].Value = GetStatusIndicator(status, StateDWordFlags.Homed);
                    row.Cells[8].Value = GetStatusIndicator(status, StateDWordFlags.NotMoving);
                    row.Cells[9].Value = GetStatusIndicator(status, StateDWordFlags.InTargetPosition);
                    row.Cells[10].Value = (online.ErrorState != 0) ? "ERR" : "";

                    // Color code based on status
                    if (online.ErrorState != 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else if ((status & StateDWordFlags.ReadyForOperation) != 0)
                    {
                        if ((status & StateDWordFlags.InTargetPosition) != 0)
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if ((status & StateDWordFlags.NotMoving) == 0)
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        else
                            row.DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }

                    dataGridViewAxes.Rows.Add(row);
                }

                // Restore selection
                if (selectedIndex >= 0 && selectedIndex < dataGridViewAxes.Rows.Count)
                {
                    dataGridViewAxes.Rows[selectedIndex].Selected = true;
                }

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Update error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private string FormatPosition(double position)
        {
            return $"{position:F3}";
        }

        private string FormatVelocity(double velocity)
        {
            return $"{velocity:F2}";
        }

        private string GetStatusIndicator(StateDWordFlags status, StateDWordFlags flag)
        {
            return (status & flag) != 0 ? "✓" : "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateAxisGrid();
        }

        private void dataGridViewAxes_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAxes.SelectedRows.Count > 0)
            {
                var row = dataGridViewAxes.SelectedRows[0];
                if (row.Tag is Axis axis)
                {
                    UpdateDetailPanel(axis);
                }
            }
        }

        private void UpdateDetailPanel(Axis axis)
        {
            try
            {
                var online = axis.State.OnlineData;
                var status = online.AxisStatusDWord;

                lblDetailName.Text = $"Axis: {axis.Parameters.Name}";
                lblActualPos.Text = $"Actual Position: {online.ActualPosition:F6}";
                lblSetPos.Text = $"Set Position: {online.SetPosition:F6}";
                lblActualVel.Text = $"Actual Velocity: {online.ActualVelocity:F3}";
                lblSetVel.Text = $"Set Velocity: {online.SetVelocity:F3}";
                lblActualAcc.Text = $"Actual Acceleration: {online.ActualAcceleration:F2}";
                lblSetAcc.Text = $"Set Acceleration: {online.SetAcceleration:F2}";
                lblLagError.Text = $"Lag Error: {online.FollowingErrorPosition:F6}";
                lblTorque.Text = $"Torque: {online.ActualTorque:F2}%";

                // Status text
                var statusText = new List<string>();
                if ((status & StateDWordFlags.ReadyForOperation) != 0) statusText.Add("Ready");
                if ((status & StateDWordFlags.Homed) != 0) statusText.Add("Homed");
                if ((status & StateDWordFlags.NotMoving) != 0) statusText.Add("Stopped");
                else statusText.Add("Moving");
                if ((status & StateDWordFlags.InTargetPosition) != 0) statusText.Add("InTarget");
                if ((status & StateDWordFlags.HasJob) != 0) statusText.Add("HasJob");
                if ((status & StateDWordFlags.PositiveDirection) != 0) statusText.Add("Dir+");
                if ((status & StateDWordFlags.NegativeDirection) != 0) statusText.Add("Dir-");
                if ((status & StateDWordFlags.HomingBusy) != 0) statusText.Add("Homing");
                if (online.ErrorState != 0) statusText.Add($"ERROR: 0x{online.ErrorState:X}");

                lblDetailStatus.Text = $"Status: {string.Join(", ", statusText)}";

                // Update position bar
                DrawPositionBar(axis, online);

                groupBoxDetail.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Detail error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void DrawPositionBar(Axis axis, NCAXISSTATE_ONLINESTRUCT online)
        {
            try
            {
                var bitmap = new Bitmap(panelPositionBar.Width, panelPositionBar.Height);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);

                    var actualPos = online.ActualPosition;
                    var setPos = online.SetPosition;
                    var lagError = online.FollowingErrorPosition;

                    // Auto-range based on actual and set positions
                    var minPos = Math.Min(actualPos, setPos) - Math.Abs(lagError) - 10;
                    var maxPos = Math.Max(actualPos, setPos) + Math.Abs(lagError) + 10;
                    var range = maxPos - minPos;

                    if (range <= 0)
                        range = 100; // Default range

                    // Calculate positions on bar
                    var barWidth = panelPositionBar.Width - 60;
                    var barHeight = 30;
                    var barX = 30;
                    var barY = 10;

                    // Draw axis range bar
                    g.DrawRectangle(Pens.Black, barX, barY, barWidth, barHeight);

                    // Draw set position indicator
                    var setX = barX + (int)((setPos - minPos) / range * barWidth);
                    using (var pen = new Pen(Color.Blue, 2))
                    {
                        g.DrawLine(pen, setX, barY - 5, setX, barY + barHeight + 5);
                    }
                    g.DrawString("Set", this.Font, Brushes.Blue, setX - 10, barY + barHeight + 8);

                    // Draw actual position indicator
                    var actX = barX + (int)((actualPos - minPos) / range * barWidth);
                    using (var pen = new Pen(Color.Green, 3))
                    {
                        g.DrawLine(pen, actX, barY, actX, barY + barHeight);
                    }
                    g.DrawString("Act", this.Font, Brushes.Green, actX - 10, barY - 18);

                    // Draw lag error if significant
                    if (Math.Abs(lagError) > range * 0.01)
                    {
                        var errorStart = setX;
                        var errorEnd = actX;
                        using (var pen = new Pen(Color.Red, 1))
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            g.DrawLine(pen, errorStart, barY + barHeight / 2, errorEnd, barY + barHeight / 2);
                        }
                    }

                    // Draw labels
                    g.DrawString($"{minPos:F2}", this.Font, Brushes.Black, barX - 25, barY + barHeight + 5);
                    g.DrawString($"{maxPos:F2}", this.Font, Brushes.Black, barX + barWidth - 15, barY + barHeight + 5);
                }

                panelPositionBar.BackgroundImage?.Dispose();
                panelPositionBar.BackgroundImage = bitmap;
            }
            catch
            {
                // Ignore drawing errors
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAutoRefresh();
                updateTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
