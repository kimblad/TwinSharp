namespace TwinSharpControls
{
    partial class EtherCatHealthDashboard
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            groupBoxMaster = new GroupBox();
            lblError = new Label();
            lblFrameCount = new Label();
            lblSlaveCount = new Label();
            lblMasterDevState = new Label();
            lblMasterState = new Label();
            lblMasterNetId = new Label();
            lblMasterName = new Label();
            comboBoxMasters = new ComboBox();
            labelSelectMaster = new Label();
            dataGridViewSlaves = new DataGridView();
            groupBoxControls = new GroupBox();
            btnRequestState = new Button();
            btnClearFrames = new Button();
            btnClearCRC = new Button();
            btnRefresh = new Button();
            timerUpdate = new System.Windows.Forms.Timer(components);
            groupBoxMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSlaves).BeginInit();
            groupBoxControls.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(620, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(180, 20);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "EtherCAT Health Dashboard";
            //
            // groupBoxMaster
            // 
            groupBoxMaster.Controls.Add(lblError);
            groupBoxMaster.Controls.Add(lblFrameCount);
            groupBoxMaster.Controls.Add(lblSlaveCount);
            groupBoxMaster.Controls.Add(lblMasterDevState);
            groupBoxMaster.Controls.Add(lblMasterState);
            groupBoxMaster.Controls.Add(lblMasterNetId);
            groupBoxMaster.Controls.Add(lblMasterName);
            groupBoxMaster.Controls.Add(comboBoxMasters);
            groupBoxMaster.Controls.Add(labelSelectMaster);
            groupBoxMaster.Dock = DockStyle.Top;
            groupBoxMaster.Location = new Point(0, 0);
            groupBoxMaster.Name = "groupBoxMaster";
            groupBoxMaster.Size = new Size(800, 130);
            groupBoxMaster.TabIndex = 0;
            groupBoxMaster.TabStop = false;
            groupBoxMaster.Text = "EtherCAT Master Information";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(420, 25);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 15);
            lblError.TabIndex = 8;
            lblError.Text = "Error";
            lblError.Visible = false;
            // 
            // lblFrameCount
            // 
            lblFrameCount.AutoSize = true;
            lblFrameCount.Location = new Point(600, 85);
            lblFrameCount.Name = "lblFrameCount";
            lblFrameCount.Size = new Size(56, 15);
            lblFrameCount.TabIndex = 7;
            lblFrameCount.Text = "Frames: -";
            // 
            // lblSlaveCount
            // 
            lblSlaveCount.AutoSize = true;
            lblSlaveCount.Location = new Point(600, 60);
            lblSlaveCount.Name = "lblSlaveCount";
            lblSlaveCount.Size = new Size(50, 15);
            lblSlaveCount.TabIndex = 6;
            lblSlaveCount.Text = "Slaves: -";
            // 
            // lblMasterDevState
            // 
            lblMasterDevState.AutoSize = true;
            lblMasterDevState.BorderStyle = BorderStyle.FixedSingle;
            lblMasterDevState.Location = new Point(420, 78);
            lblMasterDevState.Name = "lblMasterDevState";
            lblMasterDevState.Padding = new Padding(5);
            lblMasterDevState.Size = new Size(94, 27);
            lblMasterDevState.TabIndex = 5;
            lblMasterDevState.Text = "Device State: -";
            // 
            // lblMasterState
            // 
            lblMasterState.AutoSize = true;
            lblMasterState.BorderStyle = BorderStyle.FixedSingle;
            lblMasterState.Location = new Point(420, 48);
            lblMasterState.Name = "lblMasterState";
            lblMasterState.Padding = new Padding(5);
            lblMasterState.Size = new Size(56, 27);
            lblMasterState.TabIndex = 4;
            lblMasterState.Text = "State: -";
            // 
            // lblMasterNetId
            // 
            lblMasterNetId.AutoSize = true;
            lblMasterNetId.Location = new Point(15, 85);
            lblMasterNetId.Name = "lblMasterNetId";
            lblMasterNetId.Size = new Size(47, 15);
            lblMasterNetId.TabIndex = 3;
            lblMasterNetId.Text = "NetId: -";
            // 
            // lblMasterName
            // 
            lblMasterName.AutoSize = true;
            lblMasterName.Location = new Point(15, 65);
            lblMasterName.Name = "lblMasterName";
            lblMasterName.Size = new Size(54, 15);
            lblMasterName.TabIndex = 2;
            lblMasterName.Text = "Master: -";
            // 
            // comboBoxMasters
            // 
            comboBoxMasters.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMasters.FormattingEnabled = true;
            comboBoxMasters.Location = new Point(110, 25);
            comboBoxMasters.Name = "comboBoxMasters";
            comboBoxMasters.Size = new Size(280, 23);
            comboBoxMasters.TabIndex = 1;
            comboBoxMasters.SelectedIndexChanged += comboBoxMasters_SelectedIndexChanged;
            // 
            // labelSelectMaster
            // 
            labelSelectMaster.AutoSize = true;
            labelSelectMaster.Location = new Point(15, 28);
            labelSelectMaster.Name = "labelSelectMaster";
            labelSelectMaster.Size = new Size(80, 15);
            labelSelectMaster.TabIndex = 0;
            labelSelectMaster.Text = "Select Master:";
            // 
            // dataGridViewSlaves
            // 
            dataGridViewSlaves.AllowUserToAddRows = false;
            dataGridViewSlaves.AllowUserToDeleteRows = false;
            dataGridViewSlaves.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewSlaves.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSlaves.Location = new Point(0, 136);
            dataGridViewSlaves.Name = "dataGridViewSlaves";
            dataGridViewSlaves.ReadOnly = true;
            dataGridViewSlaves.RowHeadersWidth = 51;
            dataGridViewSlaves.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSlaves.Size = new Size(800, 264);
            dataGridViewSlaves.TabIndex = 1;
            // 
            // groupBoxControls
            // 
            groupBoxControls.Controls.Add(btnRequestState);
            groupBoxControls.Controls.Add(btnClearFrames);
            groupBoxControls.Controls.Add(btnClearCRC);
            groupBoxControls.Controls.Add(btnRefresh);
            groupBoxControls.Dock = DockStyle.Bottom;
            groupBoxControls.Location = new Point(0, 406);
            groupBoxControls.Name = "groupBoxControls";
            groupBoxControls.Size = new Size(800, 60);
            groupBoxControls.TabIndex = 2;
            groupBoxControls.TabStop = false;
            groupBoxControls.Text = "Controls";
            // 
            // btnRequestState
            // 
            btnRequestState.Location = new Point(330, 22);
            btnRequestState.Name = "btnRequestState";
            btnRequestState.Size = new Size(100, 28);
            btnRequestState.TabIndex = 3;
            btnRequestState.Text = "Request State";
            btnRequestState.UseVisualStyleBackColor = true;
            btnRequestState.Click += btnRequestState_Click;
            // 
            // btnClearFrames
            // 
            btnClearFrames.Location = new Point(224, 22);
            btnClearFrames.Name = "btnClearFrames";
            btnClearFrames.Size = new Size(100, 28);
            btnClearFrames.TabIndex = 2;
            btnClearFrames.Text = "Clear Frames";
            btnClearFrames.UseVisualStyleBackColor = true;
            btnClearFrames.Click += btnClearFrames_Click;
            // 
            // btnClearCRC
            // 
            btnClearCRC.Location = new Point(118, 22);
            btnClearCRC.Name = "btnClearCRC";
            btnClearCRC.Size = new Size(100, 28);
            btnClearCRC.TabIndex = 1;
            btnClearCRC.Text = "Clear CRC";
            btnClearCRC.UseVisualStyleBackColor = true;
            btnClearCRC.Click += btnClearCRC_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(12, 22);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // timerUpdate
            // 
            timerUpdate.Enabled = true;
            timerUpdate.Interval = 1000;
            timerUpdate.Tick += timerUpdate_Tick;
            // 
            // EtherCatHealthDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitle);
            Controls.Add(groupBoxControls);
            Controls.Add(dataGridViewSlaves);
            Controls.Add(groupBoxMaster);
            Name = "EtherCatHealthDashboard";
            Size = new Size(800, 466);
            Load += EtherCatHealthDashboard_Load;
            groupBoxMaster.ResumeLayout(false);
            groupBoxMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSlaves).EndInit();
            groupBoxControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private GroupBox groupBoxMaster;
        private Label labelSelectMaster;
        private ComboBox comboBoxMasters;
        private Label lblMasterName;
        private Label lblMasterNetId;
        private Label lblMasterState;
        private Label lblMasterDevState;
        private Label lblSlaveCount;
        private Label lblFrameCount;
        private DataGridView dataGridViewSlaves;
        private GroupBox groupBoxControls;
        private Button btnRefresh;
        private Button btnClearCRC;
        private Button btnClearFrames;
        private Button btnRequestState;
        private System.Windows.Forms.Timer timerUpdate;
        private Label lblError;
    }
}
