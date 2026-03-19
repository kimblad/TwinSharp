namespace TwinSharpControls
{
    partial class PlcRuntimeMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            groupBoxState = new GroupBox();
            lblPlcState = new Label();
            lblStateLabel = new Label();
            groupBoxControl = new GroupBox();
            btnReset = new Button();
            btnStop = new Button();
            btnStart = new Button();
            groupBoxInfo = new GroupBox();
            chkKeepOutputsOnBP = new CheckBox();
            lblObjId = new Label();
            lblOnlineChanges = new Label();
            lblTaskCount = new Label();
            lblAdsPort = new Label();
            lblAppTimestamp = new Label();
            lblAppName = new Label();
            lblProjectName = new Label();
            groupBoxStatus = new GroupBox();
            lblBSOD = new Label();
            lblLicensesPending = new Label();
            lblShutdownInProgress = new Label();
            lblBootDataStatus = new Label();
            btnRefresh = new Button();
            lblError = new Label();
            groupBoxState.SuspendLayout();
            groupBoxControl.SuspendLayout();
            groupBoxInfo.SuspendLayout();
            groupBoxStatus.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(800, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(160, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "PLC Runtime Monitor";
            //
            // groupBoxState
            //
            groupBoxState.Controls.Add(lblPlcState);
            groupBoxState.Controls.Add(lblStateLabel);
            groupBoxState.Location = new Point(15, 45);
            groupBoxState.Name = "groupBoxState";
            groupBoxState.Size = new Size(300, 100);
            groupBoxState.TabIndex = 1;
            groupBoxState.TabStop = false;
            groupBoxState.Text = "PLC State";
            //
            // lblPlcState
            //
            lblPlcState.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblPlcState.Location = new Point(15, 25);
            lblPlcState.Name = "lblPlcState";
            lblPlcState.Size = new Size(270, 60);
            lblPlcState.TabIndex = 0;
            lblPlcState.Text = "UNKNOWN";
            lblPlcState.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblStateLabel
            //
            lblStateLabel.AutoSize = true;
            lblStateLabel.Location = new Point(15, 25);
            lblStateLabel.Name = "lblStateLabel";
            lblStateLabel.Size = new Size(0, 15);
            lblStateLabel.TabIndex = 1;
            //
            // groupBoxControl
            //
            groupBoxControl.Controls.Add(btnReset);
            groupBoxControl.Controls.Add(btnStop);
            groupBoxControl.Controls.Add(btnStart);
            groupBoxControl.Location = new Point(330, 45);
            groupBoxControl.Name = "groupBoxControl";
            groupBoxControl.Size = new Size(300, 100);
            groupBoxControl.TabIndex = 2;
            groupBoxControl.TabStop = false;
            groupBoxControl.Text = "Control";
            //
            // btnReset
            //
            btnReset.Location = new Point(205, 35);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(80, 40);
            btnReset.TabIndex = 2;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            //
            // btnStop
            //
            btnStop.Location = new Point(110, 35);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(80, 40);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            //
            // btnStart
            //
            btnStart.Location = new Point(15, 35);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(80, 40);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            //
            // groupBoxInfo
            //
            groupBoxInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxInfo.Controls.Add(chkKeepOutputsOnBP);
            groupBoxInfo.Controls.Add(lblObjId);
            groupBoxInfo.Controls.Add(lblOnlineChanges);
            groupBoxInfo.Controls.Add(lblTaskCount);
            groupBoxInfo.Controls.Add(lblAdsPort);
            groupBoxInfo.Controls.Add(lblAppTimestamp);
            groupBoxInfo.Controls.Add(lblAppName);
            groupBoxInfo.Controls.Add(lblProjectName);
            groupBoxInfo.Location = new Point(15, 160);
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.Size = new Size(945, 180);
            groupBoxInfo.TabIndex = 3;
            groupBoxInfo.TabStop = false;
            groupBoxInfo.Text = "Application Information";
            //
            // chkKeepOutputsOnBP
            //
            chkKeepOutputsOnBP.AutoSize = true;
            chkKeepOutputsOnBP.Location = new Point(15, 145);
            chkKeepOutputsOnBP.Name = "chkKeepOutputsOnBP";
            chkKeepOutputsOnBP.Size = new Size(211, 19);
            chkKeepOutputsOnBP.TabIndex = 7;
            chkKeepOutputsOnBP.Text = "Keep Outputs on Breakpoint";
            chkKeepOutputsOnBP.UseVisualStyleBackColor = true;
            chkKeepOutputsOnBP.CheckedChanged += chkKeepOutputsOnBP_CheckedChanged;
            //
            // lblObjId
            //
            lblObjId.AutoSize = true;
            lblObjId.Location = new Point(350, 65);
            lblObjId.Name = "lblObjId";
            lblObjId.Size = new Size(93, 15);
            lblObjId.TabIndex = 6;
            lblObjId.Text = "Object ID: 0x0000";
            //
            // lblOnlineChanges
            //
            lblOnlineChanges.AutoSize = true;
            lblOnlineChanges.Location = new Point(15, 115);
            lblOnlineChanges.Name = "lblOnlineChanges";
            lblOnlineChanges.Size = new Size(107, 15);
            lblOnlineChanges.TabIndex = 5;
            lblOnlineChanges.Text = "Online Changes: 0";
            //
            // lblTaskCount
            //
            lblTaskCount.AutoSize = true;
            lblTaskCount.Location = new Point(15, 90);
            lblTaskCount.Name = "lblTaskCount";
            lblTaskCount.Size = new Size(51, 15);
            lblTaskCount.TabIndex = 4;
            lblTaskCount.Text = "Tasks: 0";
            //
            // lblAdsPort
            //
            lblAdsPort.AutoSize = true;
            lblAdsPort.Location = new Point(15, 65);
            lblAdsPort.Name = "lblAdsPort";
            lblAdsPort.Size = new Size(73, 15);
            lblAdsPort.TabIndex = 3;
            lblAdsPort.Text = "ADS Port: 851";
            //
            // lblAppTimestamp
            //
            lblAppTimestamp.AutoSize = true;
            lblAppTimestamp.Location = new Point(350, 40);
            lblAppTimestamp.Name = "lblAppTimestamp";
            lblAppTimestamp.Size = new Size(142, 15);
            lblAppTimestamp.TabIndex = 2;
            lblAppTimestamp.Text = "Compiled: Unknown";
            //
            // lblAppName
            //
            lblAppName.AutoSize = true;
            lblAppName.Location = new Point(350, 20);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(104, 15);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "Application: Unknown";
            //
            // lblProjectName
            //
            lblProjectName.AutoSize = true;
            lblProjectName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProjectName.Location = new Point(15, 25);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(132, 19);
            lblProjectName.TabIndex = 0;
            lblProjectName.Text = "Project: Unknown";
            //
            // groupBoxStatus
            //
            groupBoxStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxStatus.Controls.Add(lblBSOD);
            groupBoxStatus.Controls.Add(lblLicensesPending);
            groupBoxStatus.Controls.Add(lblShutdownInProgress);
            groupBoxStatus.Controls.Add(lblBootDataStatus);
            groupBoxStatus.Location = new Point(15, 355);
            groupBoxStatus.Name = "groupBoxStatus";
            groupBoxStatus.Size = new Size(945, 120);
            groupBoxStatus.TabIndex = 4;
            groupBoxStatus.TabStop = false;
            groupBoxStatus.Text = "Status";
            //
            // lblBSOD
            //
            lblBSOD.AutoSize = true;
            lblBSOD.Location = new Point(15, 85);
            lblBSOD.Name = "lblBSOD";
            lblBSOD.Size = new Size(104, 15);
            lblBSOD.TabIndex = 3;
            lblBSOD.Text = "BSOD Occurred: No";
            //
            // lblLicensesPending
            //
            lblLicensesPending.AutoSize = true;
            lblLicensesPending.Location = new Point(15, 65);
            lblLicensesPending.Name = "lblLicensesPending";
            lblLicensesPending.Size = new Size(114, 15);
            lblLicensesPending.TabIndex = 2;
            lblLicensesPending.Text = "Licenses Pending: No";
            //
            // lblShutdownInProgress
            //
            lblShutdownInProgress.AutoSize = true;
            lblShutdownInProgress.Location = new Point(15, 45);
            lblShutdownInProgress.Name = "lblShutdownInProgress";
            lblShutdownInProgress.Size = new Size(150, 15);
            lblShutdownInProgress.TabIndex = 1;
            lblShutdownInProgress.Text = "Shutdown In Progress: No";
            //
            // lblBootDataStatus
            //
            lblBootDataStatus.AutoSize = true;
            lblBootDataStatus.Location = new Point(15, 25);
            lblBootDataStatus.Name = "lblBootDataStatus";
            lblBootDataStatus.Size = new Size(115, 15);
            lblBootDataStatus.TabIndex = 0;
            lblBootDataStatus.Text = "Boot Data: Unknown";
            //
            // btnRefresh
            //
            btnRefresh.Location = new Point(15, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(125, 16);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 15);
            lblError.TabIndex = 6;
            lblError.Text = "Error";
            lblError.Visible = false;
            //
            // PlcRuntimeMonitor
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblError);
            Controls.Add(btnRefresh);
            Controls.Add(groupBoxStatus);
            Controls.Add(groupBoxInfo);
            Controls.Add(groupBoxControl);
            Controls.Add(groupBoxState);
            Controls.Add(lblTitle);
            Name = "PlcRuntimeMonitor";
            Size = new Size(980, 545);
            groupBoxState.ResumeLayout(false);
            groupBoxState.PerformLayout();
            groupBoxControl.ResumeLayout(false);
            groupBoxInfo.ResumeLayout(false);
            groupBoxInfo.PerformLayout();
            groupBoxStatus.ResumeLayout(false);
            groupBoxStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox groupBoxState;
        private Label lblPlcState;
        private Label lblStateLabel;
        private GroupBox groupBoxControl;
        private Button btnStart;
        private Button btnStop;
        private Button btnReset;
        private GroupBox groupBoxInfo;
        private Label lblProjectName;
        private Label lblAppName;
        private Label lblAppTimestamp;
        private Label lblAdsPort;
        private Label lblTaskCount;
        private Label lblOnlineChanges;
        private Label lblObjId;
        private CheckBox chkKeepOutputsOnBP;
        private GroupBox groupBoxStatus;
        private Label lblBootDataStatus;
        private Label lblShutdownInProgress;
        private Label lblLicensesPending;
        private Label lblBSOD;
        private Button btnRefresh;
        private Label lblError;
    }
}
