using TwinCAT.Ads;
using TwinSharp.PLC;

namespace TwinSharpControls
{
    public partial class PlcRuntimeMonitor : UserControl
    {
        private AdsClient? client;
        private PLC? plc;
        private System.Windows.Forms.Timer? updateTimer;
        private int updateInterval = 500; // 500ms default

        public PlcRuntimeMonitor()
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
                Disconnect();

                if (value == null)
                {
                    plc = null;
                }
                else
                {
                    try
                    {
                        client = new AdsClient();
                        client.Connect(value, AmsPort.PlcRuntime_851);
                        plc = new PLC(client);
                        UpdateDisplay();
                        StartAutoRefresh();
                        lblError.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = $"Error connecting to PLC: {ex.Message}";
                        lblError.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Auto-refresh enabled.
        /// </summary>
        public bool AutoRefresh
        {
            get => updateTimer != null && updateTimer.Enabled;
            set
            {
                if (value)
                    StartAutoRefresh();
                else
                    StopAutoRefresh();
            }
        }

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
            if (plc == null)
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
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (plc == null || client == null)
                return;

            try
            {
                // Read PLC state
                var state = client.ReadState();
                UpdatePlcState(state.AdsState);

                // Read app info
                var appInfo = plc.AppInfo;

                lblProjectName.Text = $"Project: {appInfo.ProjectName}";
                lblAppName.Text = $"Application: {appInfo.AppName}";
                lblAppTimestamp.Text = $"Compiled: {appInfo.AppTimestamp:yyyy-MM-dd HH:mm:ss}";
                lblAdsPort.Text = $"ADS Port: {appInfo.AdsPort}";
                lblTaskCount.Text = $"Tasks: {appInfo.TaskCnt}";
                lblOnlineChanges.Text = $"Online Changes: {appInfo.OnlineChangeCnt}";
                lblObjId.Text = $"Object ID: 0x{appInfo.ObjId:X16}";

                // Keep outputs on breakpoint checkbox
                chkKeepOutputsOnBP.CheckedChanged -= chkKeepOutputsOnBP_CheckedChanged;
                chkKeepOutputsOnBP.Checked = appInfo.KeepOutputsOnBP;
                chkKeepOutputsOnBP.CheckedChanged += chkKeepOutputsOnBP_CheckedChanged;

                // Boot data status
                if (appInfo.BootDataLoaded)
                {
                    lblBootDataStatus.Text = "Boot Data: Loaded";
                    lblBootDataStatus.ForeColor = Color.Green;
                }
                else if (appInfo.OldBootData)
                {
                    lblBootDataStatus.Text = "Boot Data: Old (Backup)";
                    lblBootDataStatus.ForeColor = Color.Orange;
                }
                else
                {
                    lblBootDataStatus.Text = "Boot Data: Not Loaded";
                    lblBootDataStatus.ForeColor = Color.Red;
                }

                // Shutdown in progress
                if (appInfo.ShutdownInProgress)
                {
                    lblShutdownInProgress.Text = "Shutdown In Progress: Yes";
                    lblShutdownInProgress.ForeColor = Color.Red;
                    lblShutdownInProgress.Font = new Font(lblShutdownInProgress.Font, FontStyle.Bold);
                }
                else
                {
                    lblShutdownInProgress.Text = "Shutdown In Progress: No";
                    lblShutdownInProgress.ForeColor = Color.Green;
                    lblShutdownInProgress.Font = new Font(lblShutdownInProgress.Font, FontStyle.Regular);
                }

                // Licenses pending
                if (appInfo.LicensesPending)
                {
                    lblLicensesPending.Text = "Licenses Pending: Yes";
                    lblLicensesPending.ForeColor = Color.Orange;
                    lblLicensesPending.Font = new Font(lblLicensesPending.Font, FontStyle.Bold);
                }
                else
                {
                    lblLicensesPending.Text = "Licenses Pending: No";
                    lblLicensesPending.ForeColor = Color.Green;
                    lblLicensesPending.Font = new Font(lblLicensesPending.Font, FontStyle.Regular);
                }

                // BSOD occurred
                if (appInfo.BSODOccured)
                {
                    lblBSOD.Text = "BSOD Occurred: Yes";
                    lblBSOD.ForeColor = Color.Red;
                    lblBSOD.Font = new Font(lblBSOD.Font, FontStyle.Bold);
                }
                else
                {
                    lblBSOD.Text = "BSOD Occurred: No";
                    lblBSOD.ForeColor = Color.Green;
                    lblBSOD.Font = new Font(lblBSOD.Font, FontStyle.Regular);
                }

                // Update button states
                btnStart.Enabled = state.AdsState != AdsState.Run;
                btnStop.Enabled = state.AdsState == AdsState.Run;
                btnReset.Enabled = true;

                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Update error: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void UpdatePlcState(AdsState state)
        {
            string stateText;
            Color stateColor;

            switch (state)
            {
                case AdsState.Run:
                    stateText = "RUNNING";
                    stateColor = Color.Green;
                    break;
                case AdsState.Stop:
                    stateText = "STOPPED";
                    stateColor = Color.Orange;
                    break;
                case AdsState.Reset:
                    stateText = "RESET";
                    stateColor = Color.Gray;
                    break;
                case AdsState.Config:
                    stateText = "CONFIG";
                    stateColor = Color.Blue;
                    break;
                case AdsState.Invalid:
                    stateText = "INVALID";
                    stateColor = Color.Red;
                    break;
                default:
                    stateText = state.ToString().ToUpper();
                    stateColor = Color.Black;
                    break;
            }

            lblPlcState.Text = stateText;
            lblPlcState.ForeColor = stateColor;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (plc == null)
                return;

            try
            {
                plc.Start();
                UpdateDisplay();
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Start failed: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (plc == null)
                return;

            try
            {
                plc.Stop();
                UpdateDisplay();
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Stop failed: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (plc == null)
                return;

            try
            {
                plc.Reset();
                UpdateDisplay();
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Reset failed: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void chkKeepOutputsOnBP_CheckedChanged(object? sender, EventArgs e)
        {
            if (plc == null)
                return;

            try
            {
                plc.AppInfo.KeepOutputsOnBP = chkKeepOutputsOnBP.Checked;
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Failed to set KeepOutputsOnBP: {ex.Message}";
                lblError.Visible = true;

                // Revert checkbox to actual state
                chkKeepOutputsOnBP.CheckedChanged -= chkKeepOutputsOnBP_CheckedChanged;
                chkKeepOutputsOnBP.Checked = !chkKeepOutputsOnBP.Checked;
                chkKeepOutputsOnBP.CheckedChanged += chkKeepOutputsOnBP_CheckedChanged;
            }
        }

        private void Disconnect()
        {
            if (client != null)
            {
                try
                {
                    client.Disconnect();
                    client.Dispose();
                }
                catch { }
                client = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAutoRefresh();
                updateTimer?.Dispose();
                Disconnect();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
