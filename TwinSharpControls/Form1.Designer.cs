namespace TwinSharpControls
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxTargets = new ComboBox();
            comboBoxAxes = new ComboBox();
            lblTarget = new Label();
            lblAxis = new Label();
            ncAxis1 = new NcAxis();
            tabControl1 = new TabControl();
            tabPageAxis = new TabPage();
            tabPageEtherCAT = new TabPage();
            etherCatHealthDashboard1 = new EtherCatHealthDashboard();
            tabPageIPC = new TabPage();
            ipcSystemMonitor1 = new IpcSystemMonitor();
            tabPageDataLogger = new TabPage();
            dataLogger1 = new DataLogger();
            tabPageMotionVisualizer = new TabPage();
            multiAxisMotionVisualizer1 = new MultiAxisMotionVisualizer();
            tabPagePlcRuntime = new TabPage();
            plcRuntimeMonitor1 = new PlcRuntimeMonitor();
            tabPageSymbolBrowser = new TabPage();
            plcSymbolBrowser1 = new PlcSymbolBrowser();
            tabControl1.SuspendLayout();
            tabPageAxis.SuspendLayout();
            tabPageEtherCAT.SuspendLayout();
            tabPageIPC.SuspendLayout();
            tabPageDataLogger.SuspendLayout();
            tabPageMotionVisualizer.SuspendLayout();
            tabPagePlcRuntime.SuspendLayout();
            tabPageSymbolBrowser.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxTargets
            // 
            comboBoxTargets.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTargets.FormattingEnabled = true;
            comboBoxTargets.Location = new Point(111, 12);
            comboBoxTargets.Name = "comboBoxTargets";
            comboBoxTargets.Size = new Size(350, 23);
            comboBoxTargets.TabIndex = 1;
            comboBoxTargets.SelectedIndexChanged += comboBoxTargets_SelectedIndexChanged;
            // 
            // comboBoxAxes
            // 
            comboBoxAxes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAxes.FormattingEnabled = true;
            comboBoxAxes.Location = new Point(50, 10);
            comboBoxAxes.Name = "comboBoxAxes";
            comboBoxAxes.Size = new Size(200, 23);
            comboBoxAxes.TabIndex = 2;
            comboBoxAxes.SelectedIndexChanged += comboBoxAxes_SelectedIndexChanged;
            // 
            // lblTarget
            // 
            lblTarget.AutoSize = true;
            lblTarget.Location = new Point(12, 15);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(93, 15);
            lblTarget.TabIndex = 4;
            lblTarget.Text = "TwinCAT Target:";
            // 
            // lblAxis
            // 
            lblAxis.AutoSize = true;
            lblAxis.Location = new Point(10, 13);
            lblAxis.Name = "lblAxis";
            lblAxis.Size = new Size(31, 15);
            lblAxis.TabIndex = 5;
            lblAxis.Text = "Axis:";
            // 
            // ncAxis1
            // 
            ncAxis1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ncAxis1.Axis = null;
            ncAxis1.Location = new Point(6, 40);
            ncAxis1.Name = "ncAxis1";
            ncAxis1.Size = new Size(956, 497);
            ncAxis1.TabIndex = 6;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageAxis);
            tabControl1.Controls.Add(tabPageEtherCAT);
            tabControl1.Controls.Add(tabPageIPC);
            tabControl1.Controls.Add(tabPageDataLogger);
            tabControl1.Controls.Add(tabPageMotionVisualizer);
            tabControl1.Controls.Add(tabPagePlcRuntime);
            tabControl1.Controls.Add(tabPageSymbolBrowser);
            tabControl1.Location = new Point(12, 45);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(976, 571);
            tabControl1.TabIndex = 7;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageAxis
            // 
            tabPageAxis.Controls.Add(ncAxis1);
            tabPageAxis.Controls.Add(lblAxis);
            tabPageAxis.Controls.Add(comboBoxAxes);
            tabPageAxis.Location = new Point(4, 24);
            tabPageAxis.Name = "tabPageAxis";
            tabPageAxis.Padding = new Padding(3);
            tabPageAxis.Size = new Size(968, 543);
            tabPageAxis.TabIndex = 0;
            tabPageAxis.Text = "NC Axis Demo";
            tabPageAxis.UseVisualStyleBackColor = true;
            // 
            // tabPageEtherCAT
            // 
            tabPageEtherCAT.Controls.Add(etherCatHealthDashboard1);
            tabPageEtherCAT.Location = new Point(4, 24);
            tabPageEtherCAT.Name = "tabPageEtherCAT";
            tabPageEtherCAT.Padding = new Padding(3);
            tabPageEtherCAT.Size = new Size(968, 543);
            tabPageEtherCAT.TabIndex = 1;
            tabPageEtherCAT.Text = "EtherCAT Health Dashboard";
            tabPageEtherCAT.UseVisualStyleBackColor = true;
            // 
            // etherCatHealthDashboard1
            // 
            etherCatHealthDashboard1.AutoRefresh = false;
            etherCatHealthDashboard1.Dock = DockStyle.Fill;
            etherCatHealthDashboard1.Location = new Point(3, 3);
            etherCatHealthDashboard1.Name = "etherCatHealthDashboard1";
            etherCatHealthDashboard1.Size = new Size(962, 537);
            etherCatHealthDashboard1.System = null;
            etherCatHealthDashboard1.TabIndex = 0;
            etherCatHealthDashboard1.UpdateInterval = 1000;
            // 
            // tabPageIPC
            // 
            tabPageIPC.Controls.Add(ipcSystemMonitor1);
            tabPageIPC.Location = new Point(4, 24);
            tabPageIPC.Name = "tabPageIPC";
            tabPageIPC.Padding = new Padding(3);
            tabPageIPC.Size = new Size(968, 543);
            tabPageIPC.TabIndex = 2;
            tabPageIPC.Text = "IPC System Monitor";
            tabPageIPC.UseVisualStyleBackColor = true;
            // 
            // ipcSystemMonitor1
            // 
            ipcSystemMonitor1.AutoRefresh = false;
            ipcSystemMonitor1.AutoScroll = true;
            ipcSystemMonitor1.Dock = DockStyle.Fill;
            ipcSystemMonitor1.Location = new Point(3, 3);
            ipcSystemMonitor1.Name = "ipcSystemMonitor1";
            ipcSystemMonitor1.Size = new Size(962, 537);
            ipcSystemMonitor1.TabIndex = 0;
            ipcSystemMonitor1.Target = null;
            ipcSystemMonitor1.UpdateInterval = 2000;
            // 
            // tabPageDataLogger
            // 
            tabPageDataLogger.Controls.Add(dataLogger1);
            tabPageDataLogger.Location = new Point(4, 24);
            tabPageDataLogger.Name = "tabPageDataLogger";
            tabPageDataLogger.Padding = new Padding(3);
            tabPageDataLogger.Size = new Size(968, 543);
            tabPageDataLogger.TabIndex = 3;
            tabPageDataLogger.Text = "Data Logger";
            tabPageDataLogger.UseVisualStyleBackColor = true;
            // 
            // dataLogger1
            // 
            dataLogger1.Dock = DockStyle.Fill;
            dataLogger1.Location = new Point(3, 3);
            dataLogger1.Name = "dataLogger1";
            dataLogger1.SamplingInterval = 100;
            dataLogger1.Size = new Size(962, 537);
            dataLogger1.TabIndex = 0;
            dataLogger1.Target = null;
            //
            // tabPageMotionVisualizer
            // 
            tabPageMotionVisualizer.Controls.Add(multiAxisMotionVisualizer1);
            tabPageMotionVisualizer.Location = new Point(4, 24);
            tabPageMotionVisualizer.Name = "tabPageMotionVisualizer";
            tabPageMotionVisualizer.Padding = new Padding(3);
            tabPageMotionVisualizer.Size = new Size(968, 543);
            tabPageMotionVisualizer.TabIndex = 5;
            tabPageMotionVisualizer.Text = "Multi-Axis Motion Visualizer";
            tabPageMotionVisualizer.UseVisualStyleBackColor = true;
            // 
            // multiAxisMotionVisualizer1
            // 
            multiAxisMotionVisualizer1.AutoRefresh = false;
            multiAxisMotionVisualizer1.Dock = DockStyle.Fill;
            multiAxisMotionVisualizer1.Location = new Point(3, 3);
            multiAxisMotionVisualizer1.Name = "multiAxisMotionVisualizer1";
            multiAxisMotionVisualizer1.Size = new Size(962, 537);
            multiAxisMotionVisualizer1.TabIndex = 0;
            multiAxisMotionVisualizer1.UpdateInterval = 100;
            // 
            // tabPagePlcRuntime
            // 
            tabPagePlcRuntime.Controls.Add(plcRuntimeMonitor1);
            tabPagePlcRuntime.Location = new Point(4, 24);
            tabPagePlcRuntime.Name = "tabPagePlcRuntime";
            tabPagePlcRuntime.Padding = new Padding(3);
            tabPagePlcRuntime.Size = new Size(968, 543);
            tabPagePlcRuntime.TabIndex = 6;
            tabPagePlcRuntime.Text = "PLC Runtime Monitor";
            tabPagePlcRuntime.UseVisualStyleBackColor = true;
            // 
            // plcRuntimeMonitor1
            // 
            plcRuntimeMonitor1.AutoRefresh = false;
            plcRuntimeMonitor1.Dock = DockStyle.Fill;
            plcRuntimeMonitor1.Location = new Point(3, 3);
            plcRuntimeMonitor1.Name = "plcRuntimeMonitor1";
            plcRuntimeMonitor1.Size = new Size(962, 537);
            plcRuntimeMonitor1.TabIndex = 0;
            plcRuntimeMonitor1.UpdateInterval = 500;
            // 
            // tabPageSymbolBrowser
            // 
            tabPageSymbolBrowser.Controls.Add(plcSymbolBrowser1);
            tabPageSymbolBrowser.Location = new Point(4, 24);
            tabPageSymbolBrowser.Name = "tabPageSymbolBrowser";
            tabPageSymbolBrowser.Padding = new Padding(3);
            tabPageSymbolBrowser.Size = new Size(968, 543);
            tabPageSymbolBrowser.TabIndex = 7;
            tabPageSymbolBrowser.Text = "PLC Symbol Browser";
            tabPageSymbolBrowser.UseVisualStyleBackColor = true;
            // 
            // plcSymbolBrowser1
            // 
            plcSymbolBrowser1.Dock = DockStyle.Fill;
            plcSymbolBrowser1.Location = new Point(3, 3);
            plcSymbolBrowser1.Name = "plcSymbolBrowser1";
            plcSymbolBrowser1.Port = 851;
            plcSymbolBrowser1.Size = new Size(962, 537);
            plcSymbolBrowser1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 628);
            Controls.Add(lblTarget);
            Controls.Add(comboBoxTargets);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "TwinSharp Demo";
            tabControl1.ResumeLayout(false);
            tabPageAxis.ResumeLayout(false);
            tabPageAxis.PerformLayout();
            tabPageEtherCAT.ResumeLayout(false);
            tabPageIPC.ResumeLayout(false);
            tabPageDataLogger.ResumeLayout(false);
            tabPageMotionVisualizer.ResumeLayout(false);
            tabPagePlcRuntime.ResumeLayout(false);
            tabPageSymbolBrowser.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxTargets;
        private ComboBox comboBoxAxes;
        private Label lblTarget;
        private Label lblAxis;
        private NcAxis ncAxis1;
        private TabControl tabControl1;
        private TabPage tabPageAxis;
        private TabPage tabPageEtherCAT;
        private EtherCatHealthDashboard etherCatHealthDashboard1;
        private TabPage tabPageIPC;
        private IpcSystemMonitor ipcSystemMonitor1;
        private TabPage tabPageDataLogger;
        private DataLogger dataLogger1;
        private TabPage tabPageMotionVisualizer;
        private MultiAxisMotionVisualizer multiAxisMotionVisualizer1;
        private TabPage tabPagePlcRuntime;
        private PlcRuntimeMonitor plcRuntimeMonitor1;
        private TabPage tabPageSymbolBrowser;
        private PlcSymbolBrowser plcSymbolBrowser1;
    }
}