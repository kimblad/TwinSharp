using TwinCAT.Ads;
using TwinSharp;
using TwinSharp.IPC;

namespace TwinSharpControls
{
    public partial class IpcSystemMonitor : UserControl
    {
        private IPC? ipc;
        private AmsNetId? targetNetId;

        public IpcSystemMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the target AmsNetId to monitor.
        /// </summary>
        public AmsNetId? Target
        {
            get => targetNetId;
            set
            {
                targetNetId = value;
                LoadIpc();
            }
        }

        /// <summary>
        /// Update interval in milliseconds for refreshing data.
        /// </summary>
        public int UpdateInterval
        {
            get => timerUpdate.Interval;
            set => timerUpdate.Interval = value;
        }

        /// <summary>
        /// Enable or disable auto-refresh.
        /// </summary>
        public bool AutoRefresh
        {
            get => timerUpdate.Enabled;
            set => timerUpdate.Enabled = value;
        }

        private void LoadIpc()
        {
            try
            {
                if (targetNetId == null)
                {
                    ipc?.Dispose();
                    ipc = null;
                    ClearAllData();
                    return;
                }

                ipc?.Dispose();
                ipc = new IPC(targetNetId);

                lblError.Visible = false;
                RefreshData();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error connecting to IPC: {ex.Message}";
                lblError.Visible = true;
                ClearAllData();
            }
        }

        private void RefreshData()
        {
            if (ipc == null)
                return;

            try
            {
                UpdateCpuInfo();
                UpdateMemoryInfo();
                UpdateMainBoardInfo();
                UpdateFans();
                UpdateUps();
                UpdateTwinCatInfo();
                UpdateOperatingSystemInfo();

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void UpdateCpuInfo()
        {
            if (ipc?.Cpu == null)
            {
                groupBoxCpu.Visible = false;
                return;
            }

            groupBoxCpu.Visible = true;

            try
            {
                var temp = ipc.Cpu.TemperatureCelsius;
                lblCpuTemp.Text = $"Temperature: {temp}°C";
                lblCpuTemp.ForeColor = temp > 80 ? Color.Red : (temp > 60 ? Color.Orange : Color.Black);

                var usage = ipc.Cpu.UsagePercent;
                lblCpuUsage.Text = $"Usage: {usage}%";
                progressBarCpu.Value = Math.Min(100, Math.Max(0, (int)usage));

                var freq = ipc.Cpu.Frequency;
                lblCpuFreq.Text = $"Frequency: {freq} MHz";
            }
            catch
            {
                groupBoxCpu.Visible = false;
            }
        }

        private void UpdateMemoryInfo()
        {
            if (ipc?.Memory == null)
            {
                groupBoxMemory.Visible = false;
                return;
            }

            groupBoxMemory.Visible = true;

            try
            {
                var allocated = ipc.Memory.ProgramMemoryAllocated;
                var available = ipc.Memory.ProgramMemoryAvailable;
                var total = allocated + available;

                if (total > 0)
                {
                    var usedPercent = (int)((allocated * 100) / total);
                    progressBarMemory.Value = Math.Min(100, usedPercent);
                    lblMemoryUsage.Text = $"Used: {FormatBytes(allocated)} / {FormatBytes(total)} ({usedPercent}%)";
                }
            }
            catch
            {
                groupBoxMemory.Visible = false;
            }
        }

        private void UpdateMainBoardInfo()
        {
            if (ipc?.MainBoard == null)
            {
                groupBoxMainBoard.Visible = false;
                return;
            }

            groupBoxMainBoard.Visible = true;

            try
            {
                lblMbType.Text = $"Type: {ipc.MainBoard.Type}";
                lblMbSerial.Text = $"Serial: {ipc.MainBoard.SerialNumber}";

                var temp = ipc.MainBoard.TemperatureCelsius;
                lblMbTemp.Text = $"Temperature: {temp}°C";
                lblMbTemp.ForeColor = temp > 70 ? Color.Red : (temp > 50 ? Color.Orange : Color.Black);

                lblMbBootCount.Text = $"Boot Count: {ipc.MainBoard.BootCount}";

                var uptimeMinutes = ipc.MainBoard.OperatingTimeMinutes;
                var uptime = TimeSpan.FromMinutes(uptimeMinutes);
                lblMbUptime.Text = $"Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m";

                lblMbBios.Text = $"BIOS: {ipc.MainBoard.BiosVersion}";
            }
            catch
            {
                groupBoxMainBoard.Visible = false;
            }
        }

        private void UpdateFans()
        {
            if (ipc?.Fans == null || ipc.Fans.Length == 0)
            {
                groupBoxFans.Visible = false;
                return;
            }

            groupBoxFans.Visible = true;

            try
            {
                var fanText = "";
                for (int i = 0; i < ipc.Fans.Length; i++)
                {
                    var rpm = ipc.Fans[i].FanSpeedRPM;
                    fanText += $"Fan {i + 1}: {rpm} RPM";
                    if (i < ipc.Fans.Length - 1)
                        fanText += "\n";
                }
                lblFans.Text = fanText;
            }
            catch
            {
                groupBoxFans.Visible = false;
            }
        }

        private void UpdateUps()
        {
            if (ipc?.UPS == null)
            {
                groupBoxUps.Visible = false;
                return;
            }

            groupBoxUps.Visible = true;

            try
            {
                lblUpsModel.Text = $"Model: {ipc.UPS.UPSModel}";

                var capacity = ipc.UPS.BatteryCapacityPercent;
                lblUpsBattery.Text = $"Battery: {capacity}%";
                progressBarUpsBattery.Value = Math.Min(100, Math.Max(0, (int)capacity));
                progressBarUpsBattery.ForeColor = capacity < 20 ? Color.Red : (capacity < 50 ? Color.Orange : Color.Green);

                var runtime = TimeSpan.FromSeconds(ipc.UPS.BatteryRuntimeSeconds);
                lblUpsRuntime.Text = $"Runtime: {runtime.Hours}h {runtime.Minutes}m";

                lblUpsPowerFails.Text = $"Power Fails: {ipc.UPS.PowerFailCounter}";

                var powerStatus = ipc.UPS.PowerStatus == 1 ? "On Battery" : "AC Power";
                lblUpsPowerStatus.Text = $"Status: {powerStatus}";
                lblUpsPowerStatus.ForeColor = ipc.UPS.PowerStatus == 1 ? Color.Red : Color.Green;
            }
            catch
            {
                groupBoxUps.Visible = false;
            }
        }

        private void UpdateTwinCatInfo()
        {
            if (ipc?.TwinCAT == null)
            {
                groupBoxTwinCat.Visible = false;
                return;
            }

            groupBoxTwinCat.Visible = true;

            try
            {
                var version = $"{ipc.TwinCAT.MajorVersion}.{ipc.TwinCAT.MinorVersion}.{ipc.TwinCAT.BuildNumber}";
                lblTcVersion.Text = $"Version: {version}";
                lblTcSystemId.Text = $"System ID: {ipc.TwinCAT.SystemID}";
                lblTcAmsNetId.Text = $"AMS Net ID: {ipc.TwinCAT.AmsNetID}";

                var status = ipc.TwinCAT.Status;
                var statusText = status switch
                {
                    1 => "Config",
                    2 => "Stop",
                    4 => "Run",
                    _ => $"Unknown ({status})"
                };
                lblTcStatus.Text = $"Status: {statusText}";
                lblTcStatus.ForeColor = status == 4 ? Color.Green : (status == 2 ? Color.Red : Color.Orange);
            }
            catch
            {
                groupBoxTwinCat.Visible = false;
            }
        }

        private void UpdateOperatingSystemInfo()
        {
            if (ipc?.OperatingSystem == null)
            {
                groupBoxOs.Visible = false;
                return;
            }

            groupBoxOs.Visible = true;

            try
            {
                var version = $"{ipc.OperatingSystem.MajorVersion}.{ipc.OperatingSystem.MinorVersion}.{ipc.OperatingSystem.BuildNumber}";
                lblOsVersion.Text = $"OS: {version}";
                lblOsServicePack.Text = $"CSD Version: {ipc.OperatingSystem.CSDVersion}";

                var uptime = TimeSpan.FromSeconds(ipc.OperatingSystem.UpTimeSeconds);
                // Add uptime display if we have space
            }
            catch
            {
                groupBoxOs.Visible = false;
            }
        }

        private void ClearAllData()
        {
            groupBoxCpu.Visible = false;
            groupBoxMemory.Visible = false;
            groupBoxMainBoard.Visible = false;
            groupBoxFans.Visible = false;
            groupBoxUps.Visible = false;
            groupBoxTwinCat.Visible = false;
            groupBoxOs.Visible = false;
        }

        private string FormatBytes(ulong bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            double dblBytes = bytes;

            while (dblBytes >= 1024 && i < suffixes.Length - 1)
            {
                dblBytes /= 1024;
                i++;
            }

            return $"{dblBytes:0.##} {suffixes[i]}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void IpcSystemMonitor_Load(object sender, EventArgs e)
        {
            // Initial setup
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ipc?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
