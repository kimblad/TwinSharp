namespace TwinSharpControls
{
    partial class IpcSystemMonitor
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
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            groupBoxCpu = new GroupBox();
            lblCpuFreq = new Label();
            progressBarCpu = new ProgressBar();
            lblCpuUsage = new Label();
            lblCpuTemp = new Label();
            groupBoxMemory = new GroupBox();
            lblMemoryUsage = new Label();
            progressBarMemory = new ProgressBar();
            groupBoxMainBoard = new GroupBox();
            lblMbBios = new Label();
            lblMbUptime = new Label();
            lblMbBootCount = new Label();
            lblMbTemp = new Label();
            lblMbSerial = new Label();
            lblMbType = new Label();
            groupBoxFans = new GroupBox();
            lblFans = new Label();
            groupBoxUps = new GroupBox();
            lblUpsPowerStatus = new Label();
            lblUpsPowerFails = new Label();
            lblUpsRuntime = new Label();
            progressBarUpsBattery = new ProgressBar();
            lblUpsBattery = new Label();
            lblUpsModel = new Label();
            groupBoxTwinCat = new GroupBox();
            lblTcStatus = new Label();
            lblTcAmsNetId = new Label();
            lblTcSystemId = new Label();
            lblTcVersion = new Label();
            groupBoxOs = new GroupBox();
            lblOsServicePack = new Label();
            lblOsVersion = new Label();
            btnRefresh = new Button();
            timerUpdate = new System.Windows.Forms.Timer(components);
            lblError = new Label();
            groupBoxCpu.SuspendLayout();
            groupBoxMemory.SuspendLayout();
            groupBoxMainBoard.SuspendLayout();
            groupBoxFans.SuspendLayout();
            groupBoxUps.SuspendLayout();
            groupBoxTwinCat.SuspendLayout();
            groupBoxOs.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(680, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(120, 20);
            lblTitle.TabIndex = 8;
            lblTitle.Text = "IPC System Monitor";
            //
            // groupBoxCpu
            //
            groupBoxCpu.Controls.Add(lblCpuFreq);
            groupBoxCpu.Controls.Add(progressBarCpu);
            groupBoxCpu.Controls.Add(lblCpuUsage);
            groupBoxCpu.Controls.Add(lblCpuTemp);
            groupBoxCpu.Location = new Point(12, 40);
            groupBoxCpu.Name = "groupBoxCpu";
            groupBoxCpu.Size = new Size(380, 130);
            groupBoxCpu.TabIndex = 0;
            groupBoxCpu.TabStop = false;
            groupBoxCpu.Text = "CPU";
            //
            // lblCpuFreq
            //
            lblCpuFreq.AutoSize = true;
            lblCpuFreq.Location = new Point(15, 95);
            lblCpuFreq.Name = "lblCpuFreq";
            lblCpuFreq.Size = new Size(100, 15);
            lblCpuFreq.TabIndex = 3;
            lblCpuFreq.Text = "Frequency: - MHz";
            //
            // progressBarCpu
            //
            progressBarCpu.Location = new Point(15, 60);
            progressBarCpu.Name = "progressBarCpu";
            progressBarCpu.Size = new Size(350, 23);
            progressBarCpu.TabIndex = 2;
            //
            // lblCpuUsage
            //
            lblCpuUsage.AutoSize = true;
            lblCpuUsage.Location = new Point(15, 42);
            lblCpuUsage.Name = "lblCpuUsage";
            lblCpuUsage.Size = new Size(60, 15);
            lblCpuUsage.TabIndex = 1;
            lblCpuUsage.Text = "Usage: -%";
            //
            // lblCpuTemp
            //
            lblCpuTemp.AutoSize = true;
            lblCpuTemp.Location = new Point(15, 22);
            lblCpuTemp.Name = "lblCpuTemp";
            lblCpuTemp.Size = new Size(95, 15);
            lblCpuTemp.TabIndex = 0;
            lblCpuTemp.Text = "Temperature: -°C";
            //
            // groupBoxMemory
            //
            groupBoxMemory.Controls.Add(lblMemoryUsage);
            groupBoxMemory.Controls.Add(progressBarMemory);
            groupBoxMemory.Location = new Point(408, 40);
            groupBoxMemory.Name = "groupBoxMemory";
            groupBoxMemory.Size = new Size(380, 130);
            groupBoxMemory.TabIndex = 1;
            groupBoxMemory.TabStop = false;
            groupBoxMemory.Text = "Memory";
            //
            // lblMemoryUsage
            //
            lblMemoryUsage.AutoSize = true;
            lblMemoryUsage.Location = new Point(15, 22);
            lblMemoryUsage.Name = "lblMemoryUsage";
            lblMemoryUsage.Size = new Size(120, 15);
            lblMemoryUsage.TabIndex = 1;
            lblMemoryUsage.Text = "Used: - / - (-%))";
            //
            // progressBarMemory
            //
            progressBarMemory.Location = new Point(15, 50);
            progressBarMemory.Name = "progressBarMemory";
            progressBarMemory.Size = new Size(350, 23);
            progressBarMemory.TabIndex = 0;
            //
            // groupBoxMainBoard
            //
            groupBoxMainBoard.Controls.Add(lblMbBios);
            groupBoxMainBoard.Controls.Add(lblMbUptime);
            groupBoxMainBoard.Controls.Add(lblMbBootCount);
            groupBoxMainBoard.Controls.Add(lblMbTemp);
            groupBoxMainBoard.Controls.Add(lblMbSerial);
            groupBoxMainBoard.Controls.Add(lblMbType);
            groupBoxMainBoard.Location = new Point(12, 185);
            groupBoxMainBoard.Name = "groupBoxMainBoard";
            groupBoxMainBoard.Size = new Size(380, 180);
            groupBoxMainBoard.TabIndex = 2;
            groupBoxMainBoard.TabStop = false;
            groupBoxMainBoard.Text = "Main Board";
            //
            // lblMbBios
            //
            lblMbBios.AutoSize = true;
            lblMbBios.Location = new Point(15, 142);
            lblMbBios.Name = "lblMbBios";
            lblMbBios.Size = new Size(45, 15);
            lblMbBios.TabIndex = 5;
            lblMbBios.Text = "BIOS: -";
            //
            // lblMbUptime
            //
            lblMbUptime.AutoSize = true;
            lblMbUptime.Location = new Point(15, 118);
            lblMbUptime.Name = "lblMbUptime";
            lblMbUptime.Size = new Size(65, 15);
            lblMbUptime.TabIndex = 4;
            lblMbUptime.Text = "Uptime: -";
            //
            // lblMbBootCount
            //
            lblMbBootCount.AutoSize = true;
            lblMbBootCount.Location = new Point(15, 94);
            lblMbBootCount.Name = "lblMbBootCount";
            lblMbBootCount.Size = new Size(82, 15);
            lblMbBootCount.TabIndex = 3;
            lblMbBootCount.Text = "Boot Count: -";
            //
            // lblMbTemp
            //
            lblMbTemp.AutoSize = true;
            lblMbTemp.Location = new Point(15, 70);
            lblMbTemp.Name = "lblMbTemp";
            lblMbTemp.Size = new Size(95, 15);
            lblMbTemp.TabIndex = 2;
            lblMbTemp.Text = "Temperature: -°C";
            //
            // lblMbSerial
            //
            lblMbSerial.AutoSize = true;
            lblMbSerial.Location = new Point(15, 46);
            lblMbSerial.Name = "lblMbSerial";
            lblMbSerial.Size = new Size(50, 15);
            lblMbSerial.TabIndex = 1;
            lblMbSerial.Text = "Serial: -";
            //
            // lblMbType
            //
            lblMbType.AutoSize = true;
            lblMbType.Location = new Point(15, 22);
            lblMbType.Name = "lblMbType";
            lblMbType.Size = new Size(44, 15);
            lblMbType.TabIndex = 0;
            lblMbType.Text = "Type: -";
            //
            // groupBoxFans
            //
            groupBoxFans.Controls.Add(lblFans);
            groupBoxFans.Location = new Point(408, 185);
            groupBoxFans.Name = "groupBoxFans";
            groupBoxFans.Size = new Size(380, 100);
            groupBoxFans.TabIndex = 3;
            groupBoxFans.TabStop = false;
            groupBoxFans.Text = "Fans";
            //
            // lblFans
            //
            lblFans.AutoSize = true;
            lblFans.Location = new Point(15, 22);
            lblFans.Name = "lblFans";
            lblFans.Size = new Size(65, 15);
            lblFans.TabIndex = 0;
            lblFans.Text = "No fans detected";
            //
            // groupBoxUps
            //
            groupBoxUps.Controls.Add(lblUpsPowerStatus);
            groupBoxUps.Controls.Add(lblUpsPowerFails);
            groupBoxUps.Controls.Add(lblUpsRuntime);
            groupBoxUps.Controls.Add(progressBarUpsBattery);
            groupBoxUps.Controls.Add(lblUpsBattery);
            groupBoxUps.Controls.Add(lblUpsModel);
            groupBoxUps.Location = new Point(12, 380);
            groupBoxUps.Name = "groupBoxUps";
            groupBoxUps.Size = new Size(380, 180);
            groupBoxUps.TabIndex = 4;
            groupBoxUps.TabStop = false;
            groupBoxUps.Text = "UPS";
            //
            // lblUpsPowerStatus
            //
            lblUpsPowerStatus.AutoSize = true;
            lblUpsPowerStatus.Location = new Point(15, 142);
            lblUpsPowerStatus.Name = "lblUpsPowerStatus";
            lblUpsPowerStatus.Size = new Size(90, 15);
            lblUpsPowerStatus.TabIndex = 5;
            lblUpsPowerStatus.Text = "Status: AC Power";
            //
            // lblUpsPowerFails
            //
            lblUpsPowerFails.AutoSize = true;
            lblUpsPowerFails.Location = new Point(15, 118);
            lblUpsPowerFails.Name = "lblUpsPowerFails";
            lblUpsPowerFails.Size = new Size(85, 15);
            lblUpsPowerFails.TabIndex = 4;
            lblUpsPowerFails.Text = "Power Fails: -";
            //
            // lblUpsRuntime
            //
            lblUpsRuntime.AutoSize = true;
            lblUpsRuntime.Location = new Point(15, 94);
            lblUpsRuntime.Name = "lblUpsRuntime";
            lblUpsRuntime.Size = new Size(65, 15);
            lblUpsRuntime.TabIndex = 3;
            lblUpsRuntime.Text = "Runtime: -";
            //
            // progressBarUpsBattery
            //
            progressBarUpsBattery.Location = new Point(15, 60);
            progressBarUpsBattery.Name = "progressBarUpsBattery";
            progressBarUpsBattery.Size = new Size(350, 23);
            progressBarUpsBattery.TabIndex = 2;
            //
            // lblUpsBattery
            //
            lblUpsBattery.AutoSize = true;
            lblUpsBattery.Location = new Point(15, 42);
            lblUpsBattery.Name = "lblUpsBattery";
            lblUpsBattery.Size = new Size(60, 15);
            lblUpsBattery.TabIndex = 1;
            lblUpsBattery.Text = "Battery: -%";
            //
            // lblUpsModel
            //
            lblUpsModel.AutoSize = true;
            lblUpsModel.Location = new Point(15, 22);
            lblUpsModel.Name = "lblUpsModel";
            lblUpsModel.Size = new Size(54, 15);
            lblUpsModel.TabIndex = 0;
            lblUpsModel.Text = "Model: -";
            //
            // groupBoxTwinCat
            //
            groupBoxTwinCat.Controls.Add(lblTcStatus);
            groupBoxTwinCat.Controls.Add(lblTcAmsNetId);
            groupBoxTwinCat.Controls.Add(lblTcSystemId);
            groupBoxTwinCat.Controls.Add(lblTcVersion);
            groupBoxTwinCat.Location = new Point(408, 300);
            groupBoxTwinCat.Name = "groupBoxTwinCat";
            groupBoxTwinCat.Size = new Size(380, 135);
            groupBoxTwinCat.TabIndex = 5;
            groupBoxTwinCat.TabStop = false;
            groupBoxTwinCat.Text = "TwinCAT";
            //
            // lblTcStatus
            //
            lblTcStatus.AutoSize = true;
            lblTcStatus.Location = new Point(15, 94);
            lblTcStatus.Name = "lblTcStatus";
            lblTcStatus.Size = new Size(53, 15);
            lblTcStatus.TabIndex = 3;
            lblTcStatus.Text = "Status: -";
            //
            // lblTcAmsNetId
            //
            lblTcAmsNetId.AutoSize = true;
            lblTcAmsNetId.Location = new Point(15, 70);
            lblTcAmsNetId.Name = "lblTcAmsNetId";
            lblTcAmsNetId.Size = new Size(90, 15);
            lblTcAmsNetId.TabIndex = 2;
            lblTcAmsNetId.Text = "AMS Net ID: -";
            //
            // lblTcSystemId
            //
            lblTcSystemId.AutoSize = true;
            lblTcSystemId.Location = new Point(15, 46);
            lblTcSystemId.Name = "lblTcSystemId";
            lblTcSystemId.Size = new Size(75, 15);
            lblTcSystemId.TabIndex = 1;
            lblTcSystemId.Text = "System ID: -";
            //
            // lblTcVersion
            //
            lblTcVersion.AutoSize = true;
            lblTcVersion.Location = new Point(15, 22);
            lblTcVersion.Name = "lblTcVersion";
            lblTcVersion.Size = new Size(60, 15);
            lblTcVersion.TabIndex = 0;
            lblTcVersion.Text = "Version: -";
            //
            // groupBoxOs
            //
            groupBoxOs.Controls.Add(lblOsServicePack);
            groupBoxOs.Controls.Add(lblOsVersion);
            groupBoxOs.Location = new Point(408, 450);
            groupBoxOs.Name = "groupBoxOs";
            groupBoxOs.Size = new Size(380, 110);
            groupBoxOs.TabIndex = 6;
            groupBoxOs.TabStop = false;
            groupBoxOs.Text = "Operating System";
            //
            // lblOsServicePack
            //
            lblOsServicePack.AutoSize = true;
            lblOsServicePack.Location = new Point(15, 46);
            lblOsServicePack.Name = "lblOsServicePack";
            lblOsServicePack.Size = new Size(95, 15);
            lblOsServicePack.TabIndex = 1;
            lblOsServicePack.Text = "Service Pack: -";
            //
            // lblOsVersion
            //
            lblOsVersion.AutoSize = true;
            lblOsVersion.Location = new Point(15, 22);
            lblOsVersion.Name = "lblOsVersion";
            lblOsVersion.Size = new Size(37, 15);
            lblOsVersion.TabIndex = 0;
            lblOsVersion.Text = "OS: -";
            //
            // btnRefresh
            //
            btnRefresh.Location = new Point(12, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // timerUpdate
            //
            timerUpdate.Enabled = true;
            timerUpdate.Interval = 2000;
            timerUpdate.Tick += timerUpdate_Tick;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(130, 12);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 15);
            lblError.TabIndex = 8;
            lblError.Text = "Error";
            lblError.Visible = false;
            //
            // IpcSystemMonitor
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lblTitle);
            Controls.Add(lblError);
            Controls.Add(btnRefresh);
            Controls.Add(groupBoxOs);
            Controls.Add(groupBoxTwinCat);
            Controls.Add(groupBoxUps);
            Controls.Add(groupBoxFans);
            Controls.Add(groupBoxMainBoard);
            Controls.Add(groupBoxMemory);
            Controls.Add(groupBoxCpu);
            Name = "IpcSystemMonitor";
            Size = new Size(800, 575);
            Load += IpcSystemMonitor_Load;
            groupBoxCpu.ResumeLayout(false);
            groupBoxCpu.PerformLayout();
            groupBoxMemory.ResumeLayout(false);
            groupBoxMemory.PerformLayout();
            groupBoxMainBoard.ResumeLayout(false);
            groupBoxMainBoard.PerformLayout();
            groupBoxFans.ResumeLayout(false);
            groupBoxFans.PerformLayout();
            groupBoxUps.ResumeLayout(false);
            groupBoxUps.PerformLayout();
            groupBoxTwinCat.ResumeLayout(false);
            groupBoxTwinCat.PerformLayout();
            groupBoxOs.ResumeLayout(false);
            groupBoxOs.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox groupBoxCpu;
        private Label lblCpuTemp;
        private Label lblCpuUsage;
        private ProgressBar progressBarCpu;
        private Label lblCpuFreq;
        private GroupBox groupBoxMemory;
        private ProgressBar progressBarMemory;
        private Label lblMemoryUsage;
        private GroupBox groupBoxMainBoard;
        private Label lblMbType;
        private Label lblMbSerial;
        private Label lblMbTemp;
        private Label lblMbBootCount;
        private Label lblMbUptime;
        private Label lblMbBios;
        private GroupBox groupBoxFans;
        private Label lblFans;
        private GroupBox groupBoxUps;
        private Label lblUpsModel;
        private Label lblUpsBattery;
        private ProgressBar progressBarUpsBattery;
        private Label lblUpsRuntime;
        private Label lblUpsPowerFails;
        private Label lblUpsPowerStatus;
        private GroupBox groupBoxTwinCat;
        private Label lblTcVersion;
        private Label lblTcSystemId;
        private Label lblTcAmsNetId;
        private Label lblTcStatus;
        private GroupBox groupBoxOs;
        private Label lblOsVersion;
        private Label lblOsServicePack;
        private Button btnRefresh;
        private System.Windows.Forms.Timer timerUpdate;
        private Label lblError;
    }
}
