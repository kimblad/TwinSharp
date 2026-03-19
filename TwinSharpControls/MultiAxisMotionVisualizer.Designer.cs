namespace TwinSharpControls
{
    partial class MultiAxisMotionVisualizer
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
            dataGridViewAxes = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colActualPos = new DataGridViewTextBoxColumn();
            colSetPos = new DataGridViewTextBoxColumn();
            colLagError = new DataGridViewTextBoxColumn();
            colActualVel = new DataGridViewTextBoxColumn();
            colSetVel = new DataGridViewTextBoxColumn();
            colReady = new DataGridViewTextBoxColumn();
            colHomed = new DataGridViewTextBoxColumn();
            colStopped = new DataGridViewTextBoxColumn();
            colInTarget = new DataGridViewTextBoxColumn();
            colError = new DataGridViewTextBoxColumn();
            groupBoxDetail = new GroupBox();
            panelPositionBar = new Panel();
            lblTorque = new Label();
            lblLagError = new Label();
            lblSetAcc = new Label();
            lblActualAcc = new Label();
            lblSetVel = new Label();
            lblActualVel = new Label();
            lblSetPos = new Label();
            lblActualPos = new Label();
            lblDetailStatus = new Label();
            lblDetailName = new Label();
            btnRefresh = new Button();
            lblError = new Label();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAxes).BeginInit();
            groupBoxDetail.SuspendLayout();
            SuspendLayout();
            //
            // dataGridViewAxes
            //
            dataGridViewAxes.AllowUserToAddRows = false;
            dataGridViewAxes.AllowUserToDeleteRows = false;
            dataGridViewAxes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewAxes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAxes.Columns.AddRange(new DataGridViewColumn[] { colName, colActualPos, colSetPos, colLagError, colActualVel, colSetVel, colReady, colHomed, colStopped, colInTarget, colError });
            dataGridViewAxes.Location = new Point(10, 45);
            dataGridViewAxes.MultiSelect = false;
            dataGridViewAxes.Name = "dataGridViewAxes";
            dataGridViewAxes.ReadOnly = true;
            dataGridViewAxes.RowHeadersVisible = false;
            dataGridViewAxes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAxes.Size = new Size(960, 250);
            dataGridViewAxes.TabIndex = 0;
            dataGridViewAxes.SelectionChanged += dataGridViewAxes_SelectionChanged;
            //
            // colName
            //
            colName.HeaderText = "Axis Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Width = 150;
            //
            // colActualPos
            //
            colActualPos.HeaderText = "Actual Pos";
            colActualPos.Name = "colActualPos";
            colActualPos.ReadOnly = true;
            colActualPos.Width = 100;
            //
            // colSetPos
            //
            colSetPos.HeaderText = "Set Pos";
            colSetPos.Name = "colSetPos";
            colSetPos.ReadOnly = true;
            colSetPos.Width = 100;
            //
            // colLagError
            //
            colLagError.HeaderText = "Lag Error";
            colLagError.Name = "colLagError";
            colLagError.ReadOnly = true;
            colLagError.Width = 80;
            //
            // colActualVel
            //
            colActualVel.HeaderText = "Actual Vel";
            colActualVel.Name = "colActualVel";
            colActualVel.ReadOnly = true;
            colActualVel.Width = 90;
            //
            // colSetVel
            //
            colSetVel.HeaderText = "Set Vel";
            colSetVel.Name = "colSetVel";
            colSetVel.ReadOnly = true;
            colSetVel.Width = 90;
            //
            // colReady
            //
            colReady.HeaderText = "Rdy";
            colReady.Name = "colReady";
            colReady.ReadOnly = true;
            colReady.Width = 40;
            //
            // colHomed
            //
            colHomed.HeaderText = "Hmd";
            colHomed.Name = "colHomed";
            colHomed.ReadOnly = true;
            colHomed.Width = 40;
            //
            // colStopped
            //
            colStopped.HeaderText = "Stp";
            colStopped.Name = "colStopped";
            colStopped.ReadOnly = true;
            colStopped.Width = 40;
            //
            // colInTarget
            //
            colInTarget.HeaderText = "Trg";
            colInTarget.Name = "colInTarget";
            colInTarget.ReadOnly = true;
            colInTarget.Width = 40;
            //
            // colError
            //
            colError.HeaderText = "Err";
            colError.Name = "colError";
            colError.ReadOnly = true;
            colError.Width = 50;
            //
            // groupBoxDetail
            //
            groupBoxDetail.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxDetail.Controls.Add(panelPositionBar);
            groupBoxDetail.Controls.Add(lblTorque);
            groupBoxDetail.Controls.Add(lblLagError);
            groupBoxDetail.Controls.Add(lblSetAcc);
            groupBoxDetail.Controls.Add(lblActualAcc);
            groupBoxDetail.Controls.Add(lblSetVel);
            groupBoxDetail.Controls.Add(lblActualVel);
            groupBoxDetail.Controls.Add(lblSetPos);
            groupBoxDetail.Controls.Add(lblActualPos);
            groupBoxDetail.Controls.Add(lblDetailStatus);
            groupBoxDetail.Controls.Add(lblDetailName);
            groupBoxDetail.Location = new Point(10, 305);
            groupBoxDetail.Name = "groupBoxDetail";
            groupBoxDetail.Size = new Size(960, 230);
            groupBoxDetail.TabIndex = 1;
            groupBoxDetail.TabStop = false;
            groupBoxDetail.Text = "Axis Details";
            groupBoxDetail.Visible = false;
            //
            // panelPositionBar
            //
            panelPositionBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelPositionBar.BorderStyle = BorderStyle.FixedSingle;
            panelPositionBar.Location = new Point(15, 150);
            panelPositionBar.Name = "panelPositionBar";
            panelPositionBar.Size = new Size(930, 70);
            panelPositionBar.TabIndex = 10;
            //
            // lblTorque
            //
            lblTorque.AutoSize = true;
            lblTorque.Location = new Point(500, 120);
            lblTorque.Name = "lblTorque";
            lblTorque.Size = new Size(80, 15);
            lblTorque.TabIndex = 9;
            lblTorque.Text = "Torque: 0.00%";
            //
            // lblLagError
            //
            lblLagError.AutoSize = true;
            lblLagError.Location = new Point(500, 95);
            lblLagError.Name = "lblLagError";
            lblLagError.Size = new Size(110, 15);
            lblLagError.TabIndex = 8;
            lblLagError.Text = "Lag Error: 0.000000";
            //
            // lblSetAcc
            //
            lblSetAcc.AutoSize = true;
            lblSetAcc.Location = new Point(260, 120);
            lblSetAcc.Name = "lblSetAcc";
            lblSetAcc.Size = new Size(135, 15);
            lblSetAcc.TabIndex = 7;
            lblSetAcc.Text = "Set Acceleration: 0.00";
            //
            // lblActualAcc
            //
            lblActualAcc.AutoSize = true;
            lblActualAcc.Location = new Point(15, 120);
            lblActualAcc.Name = "lblActualAcc";
            lblActualAcc.Size = new Size(165, 15);
            lblActualAcc.TabIndex = 6;
            lblActualAcc.Text = "Actual Acceleration: 0.00";
            //
            // lblSetVel
            //
            lblSetVel.AutoSize = true;
            lblSetVel.Location = new Point(260, 95);
            lblSetVel.Name = "lblSetVel";
            lblSetVel.Size = new Size(105, 15);
            lblSetVel.TabIndex = 5;
            lblSetVel.Text = "Set Velocity: 0.000";
            //
            // lblActualVel
            //
            lblActualVel.AutoSize = true;
            lblActualVel.Location = new Point(15, 95);
            lblActualVel.Name = "lblActualVel";
            lblActualVel.Size = new Size(135, 15);
            lblActualVel.TabIndex = 4;
            lblActualVel.Text = "Actual Velocity: 0.000";
            //
            // lblSetPos
            //
            lblSetPos.AutoSize = true;
            lblSetPos.Location = new Point(260, 70);
            lblSetPos.Name = "lblSetPos";
            lblSetPos.Size = new Size(125, 15);
            lblSetPos.TabIndex = 3;
            lblSetPos.Text = "Set Position: 0.000000";
            //
            // lblActualPos
            //
            lblActualPos.AutoSize = true;
            lblActualPos.Location = new Point(15, 70);
            lblActualPos.Name = "lblActualPos";
            lblActualPos.Size = new Size(155, 15);
            lblActualPos.TabIndex = 2;
            lblActualPos.Text = "Actual Position: 0.000000";
            //
            // lblDetailStatus
            //
            lblDetailStatus.AutoSize = true;
            lblDetailStatus.Location = new Point(15, 45);
            lblDetailStatus.Name = "lblDetailStatus";
            lblDetailStatus.Size = new Size(77, 15);
            lblDetailStatus.TabIndex = 1;
            lblDetailStatus.Text = "Status: Ready";
            //
            // lblDetailName
            //
            lblDetailName.AutoSize = true;
            lblDetailName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetailName.Location = new Point(15, 20);
            lblDetailName.Name = "lblDetailName";
            lblDetailName.Size = new Size(45, 19);
            lblDetailName.TabIndex = 0;
            lblDetailName.Text = "Axis:";
            //
            // btnRefresh
            //
            btnRefresh.Location = new Point(10, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(120, 16);
            lblError.Name = "lblError";
            lblError.Size = new Size(32, 15);
            lblError.TabIndex = 3;
            lblError.Text = "Error";
            lblError.Visible = false;
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(770, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 20);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Multi-Axis Motion Visualizer";
            //
            // MultiAxisMotionVisualizer
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitle);
            Controls.Add(lblError);
            Controls.Add(btnRefresh);
            Controls.Add(groupBoxDetail);
            Controls.Add(dataGridViewAxes);
            Name = "MultiAxisMotionVisualizer";
            Size = new Size(980, 545);
            ((System.ComponentModel.ISupportInitialize)dataGridViewAxes).EndInit();
            groupBoxDetail.ResumeLayout(false);
            groupBoxDetail.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewAxes;
        private GroupBox groupBoxDetail;
        private Button btnRefresh;
        private Label lblError;
        private Label lblDetailName;
        private Label lblDetailStatus;
        private Label lblActualPos;
        private Label lblSetPos;
        private Label lblActualVel;
        private Label lblSetVel;
        private Label lblActualAcc;
        private Label lblSetAcc;
        private Label lblLagError;
        private Label lblTorque;
        private Panel panelPositionBar;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colActualPos;
        private DataGridViewTextBoxColumn colSetPos;
        private DataGridViewTextBoxColumn colLagError;
        private DataGridViewTextBoxColumn colActualVel;
        private DataGridViewTextBoxColumn colSetVel;
        private DataGridViewTextBoxColumn colReady;
        private DataGridViewTextBoxColumn colHomed;
        private DataGridViewTextBoxColumn colStopped;
        private DataGridViewTextBoxColumn colInTarget;
        private DataGridViewTextBoxColumn colError;
        private Label lblTitle;
    }
}
