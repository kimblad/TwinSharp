namespace TwinSharpControls
{
    partial class DataLogger
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
            groupBoxChannels = new GroupBox();
            btnBrowseSymbol = new Button();
            btnRemoveChannel = new Button();
            btnAddChannel = new Button();
            listBoxChannels = new ListBox();
            txtSymbolName = new TextBox();
            labelSymbolName = new Label();
            chartPanel = new Panel();
            groupBoxControls = new GroupBox();
            lblSampleCount = new Label();
            btnExport = new Button();
            btnClear = new Button();
            btnPause = new Button();
            btnStop = new Button();
            btnStart = new Button();
            lblStatus = new Label();
            timerSample = new System.Windows.Forms.Timer(components);
            lblError = new Label();
            numericSampleRate = new NumericUpDown();
            labelSampleRate = new Label();
            groupBoxChannels.SuspendLayout();
            groupBoxControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericSampleRate).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(710, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(90, 20);
            lblTitle.TabIndex = 7;
            lblTitle.Text = "Data Logger";
            //
            // groupBoxChannels
            //
            groupBoxChannels.Controls.Add(btnBrowseSymbol);
            groupBoxChannels.Controls.Add(btnRemoveChannel);
            groupBoxChannels.Controls.Add(btnAddChannel);
            groupBoxChannels.Controls.Add(listBoxChannels);
            groupBoxChannels.Controls.Add(txtSymbolName);
            groupBoxChannels.Controls.Add(labelSymbolName);
            groupBoxChannels.Location = new Point(12, 12);
            groupBoxChannels.Name = "groupBoxChannels";
            groupBoxChannels.Size = new Size(300, 250);
            groupBoxChannels.TabIndex = 0;
            groupBoxChannels.TabStop = false;
            groupBoxChannels.Text = "Data Channels";
            //
            // btnBrowseSymbol
            //
            btnBrowseSymbol.Location = new Point(255, 40);
            btnBrowseSymbol.Name = "btnBrowseSymbol";
            btnBrowseSymbol.Size = new Size(35, 23);
            btnBrowseSymbol.TabIndex = 5;
            btnBrowseSymbol.Text = "...";
            btnBrowseSymbol.UseVisualStyleBackColor = true;
            btnBrowseSymbol.Click += btnBrowseSymbol_Click;
            //
            // btnRemoveChannel
            //
            btnRemoveChannel.Location = new Point(155, 215);
            btnRemoveChannel.Name = "btnRemoveChannel";
            btnRemoveChannel.Size = new Size(135, 25);
            btnRemoveChannel.TabIndex = 4;
            btnRemoveChannel.Text = "Remove Selected";
            btnRemoveChannel.UseVisualStyleBackColor = true;
            btnRemoveChannel.Click += btnRemoveChannel_Click;
            //
            // btnAddChannel
            //
            btnAddChannel.Location = new Point(10, 215);
            btnAddChannel.Name = "btnAddChannel";
            btnAddChannel.Size = new Size(135, 25);
            btnAddChannel.TabIndex = 3;
            btnAddChannel.Text = "Add Channel";
            btnAddChannel.UseVisualStyleBackColor = true;
            btnAddChannel.Click += btnAddChannel_Click;
            //
            // listBoxChannels
            //
            listBoxChannels.FormattingEnabled = true;
            listBoxChannels.ItemHeight = 15;
            listBoxChannels.Location = new Point(10, 75);
            listBoxChannels.Name = "listBoxChannels";
            listBoxChannels.Size = new Size(280, 124);
            listBoxChannels.TabIndex = 2;
            //
            // txtSymbolName
            //
            txtSymbolName.Location = new Point(10, 40);
            txtSymbolName.Name = "txtSymbolName";
            txtSymbolName.PlaceholderText = "e.g., MAIN.Counter, GVL.Temperature";
            txtSymbolName.Size = new Size(239, 23);
            txtSymbolName.TabIndex = 1;
            //
            // labelSymbolName
            //
            labelSymbolName.AutoSize = true;
            labelSymbolName.Location = new Point(10, 22);
            labelSymbolName.Name = "labelSymbolName";
            labelSymbolName.Size = new Size(85, 15);
            labelSymbolName.TabIndex = 0;
            labelSymbolName.Text = "Symbol Name:";
            //
            // chartPanel
            //
            chartPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartPanel.BorderStyle = BorderStyle.FixedSingle;
            chartPanel.Location = new Point(325, 40);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(663, 492);
            chartPanel.TabIndex = 1;
            //
            // groupBoxControls
            //
            groupBoxControls.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxControls.Controls.Add(lblSampleCount);
            groupBoxControls.Controls.Add(btnExport);
            groupBoxControls.Controls.Add(btnClear);
            groupBoxControls.Controls.Add(btnPause);
            groupBoxControls.Controls.Add(btnStop);
            groupBoxControls.Controls.Add(btnStart);
            groupBoxControls.Location = new Point(12, 415);
            groupBoxControls.Name = "groupBoxControls";
            groupBoxControls.Size = new Size(300, 117);
            groupBoxControls.TabIndex = 2;
            groupBoxControls.TabStop = false;
            groupBoxControls.Text = "Controls";
            //
            // lblSampleCount
            //
            lblSampleCount.AutoSize = true;
            lblSampleCount.Location = new Point(10, 90);
            lblSampleCount.Name = "lblSampleCount";
            lblSampleCount.Size = new Size(67, 15);
            lblSampleCount.TabIndex = 5;
            lblSampleCount.Text = "Samples: 0";
            //
            // btnExport
            //
            btnExport.Location = new Point(155, 55);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(135, 28);
            btnExport.TabIndex = 4;
            btnExport.Text = "Export to CSV";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            //
            // btnClear
            //
            btnClear.Location = new Point(10, 55);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(135, 28);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear Data";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            //
            // btnPause
            //
            btnPause.Enabled = false;
            btnPause.Location = new Point(155, 22);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(65, 28);
            btnPause.TabIndex = 2;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            //
            // btnStop
            //
            btnStop.Enabled = false;
            btnStop.Location = new Point(225, 22);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(65, 28);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            //
            // btnStart
            //
            btnStart.Location = new Point(10, 22);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(135, 28);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start Logging";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            //
            // lblStatus
            //
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(325, 540);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(102, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Status: Idle";
            //
            // timerSample
            //
            timerSample.Interval = 100;
            timerSample.Tick += timerSample_Tick;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(12, 542);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 15);
            lblError.TabIndex = 4;
            lblError.Text = "Error";
            lblError.Visible = false;
            //
            // numericSampleRate
            //
            numericSampleRate.Location = new Point(22, 300);
            numericSampleRate.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericSampleRate.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericSampleRate.Name = "numericSampleRate";
            numericSampleRate.Size = new Size(120, 23);
            numericSampleRate.TabIndex = 5;
            numericSampleRate.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericSampleRate.ValueChanged += numericSampleRate_ValueChanged;
            //
            // labelSampleRate
            //
            labelSampleRate.AutoSize = true;
            labelSampleRate.Location = new Point(22, 280);
            labelSampleRate.Name = "labelSampleRate";
            labelSampleRate.Size = new Size(130, 15);
            labelSampleRate.TabIndex = 6;
            labelSampleRate.Text = "Sample Rate (ms):";
            //
            // DataLogger
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitle);
            Controls.Add(labelSampleRate);
            Controls.Add(numericSampleRate);
            Controls.Add(lblError);
            Controls.Add(lblStatus);
            Controls.Add(groupBoxControls);
            Controls.Add(chartPanel);
            Controls.Add(groupBoxChannels);
            Name = "DataLogger";
            Size = new Size(1000, 565);
            groupBoxChannels.ResumeLayout(false);
            groupBoxChannels.PerformLayout();
            groupBoxControls.ResumeLayout(false);
            groupBoxControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericSampleRate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox groupBoxChannels;
        private Button btnBrowseSymbol;
        private TextBox txtSymbolName;
        private Label labelSymbolName;
        private ListBox listBoxChannels;
        private Button btnAddChannel;
        private Button btnRemoveChannel;
        private Panel chartPanel;
        private GroupBox groupBoxControls;
        private Button btnStart;
        private Button btnStop;
        private Button btnPause;
        private Button btnClear;
        private Button btnExport;
        private Label lblStatus;
        private System.Windows.Forms.Timer timerSample;
        private Label lblSampleCount;
        private Label lblError;
        private NumericUpDown numericSampleRate;
        private Label labelSampleRate;
    }
}
