namespace TwinSharpControls
{
    partial class NcFunctions
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
            timerUpdate = new System.Windows.Forms.Timer(components);
            groupBoxLogicalStatus = new GroupBox();
            lblLastTime = new Label();
            txtBoxLastTime = new TextBox();
            btnStop = new Button();
            btnStart = new Button();
            label3 = new Label();
            chkBoxEnableJerk = new CheckBox();
            chkBoxEnableDeacceleration = new CheckBox();
            chkBoxEnableAcceleration = new CheckBox();
            txtBoxJerk = new TextBox();
            txtBoxDeacceleration = new TextBox();
            txtBoxAcceleration = new TextBox();
            txtBoxTargetVelocity = new TextBox();
            label2 = new Label();
            comboBoxStartMode = new ComboBox();
            label1 = new Label();
            txtBoxTargetPosition = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            groupBoxRawDriveOutput = new GroupBox();
            btnStopRawDriveOutput = new Button();
            btnStartRawDriveOutput = new Button();
            lblOutputValue = new Label();
            comboBoxRawDriveOutputMode = new ComboBox();
            lblOutputMode = new Label();
            txtBoxRawDriveOutputValue = new TextBox();
            groupBoxSetActualPosition = new GroupBox();
            btnSetActualPosition = new Button();
            comboBoxSetActualPositionType = new ComboBox();
            txtBoxSetActualPositionValue = new TextBox();
            groupBoxSetTargetPosition = new GroupBox();
            btnSetTargetPosition = new Button();
            comboBoxSetTargetPositionType = new ComboBox();
            txtBoxSetTargetPositionValue = new TextBox();
            ncAxisHeader1 = new NcAxisHeader();
            groupBoxLogicalStatus.SuspendLayout();
            groupBoxRawDriveOutput.SuspendLayout();
            groupBoxSetActualPosition.SuspendLayout();
            groupBoxSetTargetPosition.SuspendLayout();
            SuspendLayout();
            // 
            // timerUpdate
            // 
            timerUpdate.Enabled = true;
            timerUpdate.Tick += timerUpdate_Tick;
            // 
            // groupBoxLogicalStatus
            // 
            groupBoxLogicalStatus.Controls.Add(lblLastTime);
            groupBoxLogicalStatus.Controls.Add(txtBoxLastTime);
            groupBoxLogicalStatus.Controls.Add(btnStop);
            groupBoxLogicalStatus.Controls.Add(btnStart);
            groupBoxLogicalStatus.Controls.Add(label3);
            groupBoxLogicalStatus.Controls.Add(chkBoxEnableJerk);
            groupBoxLogicalStatus.Controls.Add(chkBoxEnableDeacceleration);
            groupBoxLogicalStatus.Controls.Add(chkBoxEnableAcceleration);
            groupBoxLogicalStatus.Controls.Add(txtBoxJerk);
            groupBoxLogicalStatus.Controls.Add(txtBoxDeacceleration);
            groupBoxLogicalStatus.Controls.Add(txtBoxAcceleration);
            groupBoxLogicalStatus.Controls.Add(txtBoxTargetVelocity);
            groupBoxLogicalStatus.Controls.Add(label2);
            groupBoxLogicalStatus.Controls.Add(comboBoxStartMode);
            groupBoxLogicalStatus.Controls.Add(label1);
            groupBoxLogicalStatus.Controls.Add(txtBoxTargetPosition);
            groupBoxLogicalStatus.Controls.Add(label4);
            groupBoxLogicalStatus.Controls.Add(label5);
            groupBoxLogicalStatus.Controls.Add(label6);
            groupBoxLogicalStatus.Location = new Point(20, 57);
            groupBoxLogicalStatus.Name = "groupBoxLogicalStatus";
            groupBoxLogicalStatus.Size = new Size(462, 205);
            groupBoxLogicalStatus.TabIndex = 0;
            groupBoxLogicalStatus.TabStop = false;
            groupBoxLogicalStatus.Text = "Extended Start";
            // 
            // lblLastTime
            // 
            lblLastTime.AutoSize = true;
            lblLastTime.Location = new Point(380, 148);
            lblLastTime.Name = "lblLastTime";
            lblLastTime.Size = new Size(76, 15);
            lblLastTime.TabIndex = 34;
            lblLastTime.Text = "Last Time: (s)";
            // 
            // txtBoxLastTime
            // 
            txtBoxLastTime.Location = new Point(381, 166);
            txtBoxLastTime.Name = "txtBoxLastTime";
            txtBoxLastTime.ReadOnly = true;
            txtBoxLastTime.Size = new Size(75, 23);
            txtBoxLastTime.TabIndex = 33;
            txtBoxLastTime.TabStop = false;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(380, 46);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 23);
            btnStop.TabIndex = 10;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(380, 22);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 9;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 82);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 30;
            label3.Text = "Target Velocity:";
            // 
            // chkBoxEnableJerk
            // 
            chkBoxEnableJerk.AutoSize = true;
            chkBoxEnableJerk.Location = new Point(6, 168);
            chkBoxEnableJerk.Name = "chkBoxEnableJerk";
            chkBoxEnableJerk.Size = new Size(49, 19);
            chkBoxEnableJerk.TabIndex = 7;
            chkBoxEnableJerk.Text = "Jerk:";
            chkBoxEnableJerk.UseVisualStyleBackColor = true;
            chkBoxEnableJerk.CheckedChanged += chkBoxEnableJerk_CheckedChanged;
            // 
            // chkBoxEnableDeacceleration
            // 
            chkBoxEnableDeacceleration.AutoSize = true;
            chkBoxEnableDeacceleration.Location = new Point(6, 139);
            chkBoxEnableDeacceleration.Name = "chkBoxEnableDeacceleration";
            chkBoxEnableDeacceleration.Size = new Size(95, 19);
            chkBoxEnableDeacceleration.TabIndex = 5;
            chkBoxEnableDeacceleration.Text = "Deceleration:";
            chkBoxEnableDeacceleration.UseVisualStyleBackColor = true;
            chkBoxEnableDeacceleration.CheckedChanged += chkBoxEnableDeacceleration_CheckedChanged;
            // 
            // chkBoxEnableAcceleration
            // 
            chkBoxEnableAcceleration.AutoSize = true;
            chkBoxEnableAcceleration.Location = new Point(6, 110);
            chkBoxEnableAcceleration.Name = "chkBoxEnableAcceleration";
            chkBoxEnableAcceleration.Size = new Size(95, 19);
            chkBoxEnableAcceleration.TabIndex = 3;
            chkBoxEnableAcceleration.Text = "Acceleration:";
            chkBoxEnableAcceleration.UseVisualStyleBackColor = true;
            chkBoxEnableAcceleration.CheckedChanged += chkBoxEnableAcceleration_CheckedChanged;
            // 
            // txtBoxJerk
            // 
            txtBoxJerk.Enabled = false;
            txtBoxJerk.Location = new Point(185, 167);
            txtBoxJerk.Name = "txtBoxJerk";
            txtBoxJerk.Size = new Size(130, 23);
            txtBoxJerk.TabIndex = 8;
            // 
            // txtBoxDeacceleration
            // 
            txtBoxDeacceleration.Enabled = false;
            txtBoxDeacceleration.Location = new Point(185, 138);
            txtBoxDeacceleration.Name = "txtBoxDeacceleration";
            txtBoxDeacceleration.Size = new Size(130, 23);
            txtBoxDeacceleration.TabIndex = 6;
            // 
            // txtBoxAcceleration
            // 
            txtBoxAcceleration.Enabled = false;
            txtBoxAcceleration.Location = new Point(185, 109);
            txtBoxAcceleration.Name = "txtBoxAcceleration";
            txtBoxAcceleration.Size = new Size(130, 23);
            txtBoxAcceleration.TabIndex = 4;
            // 
            // txtBoxTargetVelocity
            // 
            txtBoxTargetVelocity.Location = new Point(185, 80);
            txtBoxTargetVelocity.Name = "txtBoxTargetVelocity";
            txtBoxTargetVelocity.Size = new Size(130, 23);
            txtBoxTargetVelocity.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 53);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 22;
            label2.Text = "Target Position:";
            // 
            // comboBoxStartMode
            // 
            comboBoxStartMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStartMode.FormattingEnabled = true;
            comboBoxStartMode.Location = new Point(185, 22);
            comboBoxStartMode.Name = "comboBoxStartMode";
            comboBoxStartMode.Size = new Size(130, 23);
            comboBoxStartMode.TabIndex = 0;
            comboBoxStartMode.SelectedIndexChanged += comboBoxStartMode_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 20;
            label1.Text = "Start Mode:";
            // 
            // txtBoxTargetPosition
            // 
            txtBoxTargetPosition.Location = new Point(185, 51);
            txtBoxTargetPosition.Name = "txtBoxTargetPosition";
            txtBoxTargetPosition.Size = new Size(130, 23);
            txtBoxTargetPosition.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 112);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 35;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 141);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 36;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 170);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 37;
            // 
            // groupBoxRawDriveOutput
            // 
            groupBoxRawDriveOutput.Controls.Add(btnStopRawDriveOutput);
            groupBoxRawDriveOutput.Controls.Add(btnStartRawDriveOutput);
            groupBoxRawDriveOutput.Controls.Add(lblOutputValue);
            groupBoxRawDriveOutput.Controls.Add(comboBoxRawDriveOutputMode);
            groupBoxRawDriveOutput.Controls.Add(lblOutputMode);
            groupBoxRawDriveOutput.Controls.Add(txtBoxRawDriveOutputValue);
            groupBoxRawDriveOutput.Location = new Point(20, 268);
            groupBoxRawDriveOutput.Name = "groupBoxRawDriveOutput";
            groupBoxRawDriveOutput.Size = new Size(462, 89);
            groupBoxRawDriveOutput.TabIndex = 1;
            groupBoxRawDriveOutput.TabStop = false;
            groupBoxRawDriveOutput.Text = "Raw Drive Output:";
            // 
            // btnStopRawDriveOutput
            // 
            btnStopRawDriveOutput.Location = new Point(381, 46);
            btnStopRawDriveOutput.Name = "btnStopRawDriveOutput";
            btnStopRawDriveOutput.Size = new Size(75, 23);
            btnStopRawDriveOutput.TabIndex = 3;
            btnStopRawDriveOutput.Text = "Stop";
            btnStopRawDriveOutput.UseVisualStyleBackColor = true;
            btnStopRawDriveOutput.Click += btnStopRawDriveOutput_Click;
            // 
            // btnStartRawDriveOutput
            // 
            btnStartRawDriveOutput.Location = new Point(381, 22);
            btnStartRawDriveOutput.Name = "btnStartRawDriveOutput";
            btnStartRawDriveOutput.Size = new Size(75, 23);
            btnStartRawDriveOutput.TabIndex = 2;
            btnStartRawDriveOutput.Text = "Start";
            btnStartRawDriveOutput.UseVisualStyleBackColor = true;
            btnStartRawDriveOutput.Click += btnStartRawDriveOutput_Click;
            // 
            // lblOutputValue
            // 
            lblOutputValue.AutoSize = true;
            lblOutputValue.Location = new Point(6, 54);
            lblOutputValue.Name = "lblOutputValue";
            lblOutputValue.Size = new Size(79, 15);
            lblOutputValue.TabIndex = 36;
            lblOutputValue.Text = "Output Value:";
            // 
            // comboBoxRawDriveOutputMode
            // 
            comboBoxRawDriveOutputMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRawDriveOutputMode.FormattingEnabled = true;
            comboBoxRawDriveOutputMode.Location = new Point(185, 22);
            comboBoxRawDriveOutputMode.Name = "comboBoxRawDriveOutputMode";
            comboBoxRawDriveOutputMode.Size = new Size(130, 23);
            comboBoxRawDriveOutputMode.TabIndex = 0;
            // 
            // lblOutputMode
            // 
            lblOutputMode.AutoSize = true;
            lblOutputMode.Location = new Point(6, 24);
            lblOutputMode.Name = "lblOutputMode";
            lblOutputMode.Size = new Size(82, 15);
            lblOutputMode.TabIndex = 34;
            lblOutputMode.Text = "Output Mode:";
            // 
            // txtBoxRawDriveOutputValue
            // 
            txtBoxRawDriveOutputValue.Location = new Point(185, 51);
            txtBoxRawDriveOutputValue.Name = "txtBoxRawDriveOutputValue";
            txtBoxRawDriveOutputValue.Size = new Size(130, 23);
            txtBoxRawDriveOutputValue.TabIndex = 1;
            // 
            // groupBoxSetActualPosition
            // 
            groupBoxSetActualPosition.Controls.Add(btnSetActualPosition);
            groupBoxSetActualPosition.Controls.Add(comboBoxSetActualPositionType);
            groupBoxSetActualPosition.Controls.Add(txtBoxSetActualPositionValue);
            groupBoxSetActualPosition.Location = new Point(20, 363);
            groupBoxSetActualPosition.Name = "groupBoxSetActualPosition";
            groupBoxSetActualPosition.Size = new Size(462, 60);
            groupBoxSetActualPosition.TabIndex = 2;
            groupBoxSetActualPosition.TabStop = false;
            groupBoxSetActualPosition.Text = "Set Actual Position:";
            // 
            // btnSetActualPosition
            // 
            btnSetActualPosition.Location = new Point(381, 22);
            btnSetActualPosition.Name = "btnSetActualPosition";
            btnSetActualPosition.Size = new Size(75, 23);
            btnSetActualPosition.TabIndex = 2;
            btnSetActualPosition.Text = "Set";
            btnSetActualPosition.UseVisualStyleBackColor = true;
            btnSetActualPosition.Click += btnSetActualPosition_Click;
            // 
            // comboBoxSetActualPositionType
            // 
            comboBoxSetActualPositionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSetActualPositionType.FormattingEnabled = true;
            comboBoxSetActualPositionType.Location = new Point(6, 22);
            comboBoxSetActualPositionType.Name = "comboBoxSetActualPositionType";
            comboBoxSetActualPositionType.Size = new Size(121, 23);
            comboBoxSetActualPositionType.TabIndex = 0;
            // 
            // txtBoxSetActualPositionValue
            // 
            txtBoxSetActualPositionValue.Location = new Point(185, 22);
            txtBoxSetActualPositionValue.Name = "txtBoxSetActualPositionValue";
            txtBoxSetActualPositionValue.Size = new Size(130, 23);
            txtBoxSetActualPositionValue.TabIndex = 1;
            // 
            // groupBoxSetTargetPosition
            // 
            groupBoxSetTargetPosition.Controls.Add(btnSetTargetPosition);
            groupBoxSetTargetPosition.Controls.Add(comboBoxSetTargetPositionType);
            groupBoxSetTargetPosition.Controls.Add(txtBoxSetTargetPositionValue);
            groupBoxSetTargetPosition.Location = new Point(20, 429);
            groupBoxSetTargetPosition.Name = "groupBoxSetTargetPosition";
            groupBoxSetTargetPosition.Size = new Size(462, 60);
            groupBoxSetTargetPosition.TabIndex = 3;
            groupBoxSetTargetPosition.TabStop = false;
            groupBoxSetTargetPosition.Text = "Set Target Position:";
            // 
            // btnSetTargetPosition
            // 
            btnSetTargetPosition.Location = new Point(381, 22);
            btnSetTargetPosition.Name = "btnSetTargetPosition";
            btnSetTargetPosition.Size = new Size(75, 23);
            btnSetTargetPosition.TabIndex = 2;
            btnSetTargetPosition.Text = "Set";
            btnSetTargetPosition.UseVisualStyleBackColor = true;
            btnSetTargetPosition.Click += btnSetTargetPosition_Click;
            // 
            // comboBoxSetTargetPositionType
            // 
            comboBoxSetTargetPositionType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSetTargetPositionType.FormattingEnabled = true;
            comboBoxSetTargetPositionType.Location = new Point(6, 22);
            comboBoxSetTargetPositionType.Name = "comboBoxSetTargetPositionType";
            comboBoxSetTargetPositionType.Size = new Size(121, 23);
            comboBoxSetTargetPositionType.TabIndex = 0;
            // 
            // txtBoxSetTargetPositionValue
            // 
            txtBoxSetTargetPositionValue.Location = new Point(185, 22);
            txtBoxSetTargetPositionValue.Name = "txtBoxSetTargetPositionValue";
            txtBoxSetTargetPositionValue.Size = new Size(130, 23);
            txtBoxSetTargetPositionValue.TabIndex = 1;
            // 
            // ncAxisHeader1
            // 
            ncAxisHeader1.Location = new Point(18, 3);
            ncAxisHeader1.Name = "ncAxisHeader1";
            ncAxisHeader1.Size = new Size(464, 48);
            ncAxisHeader1.TabIndex = 4;
            // 
            // NcFunctions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ncAxisHeader1);
            Controls.Add(groupBoxSetTargetPosition);
            Controls.Add(groupBoxSetActualPosition);
            Controls.Add(groupBoxRawDriveOutput);
            Controls.Add(groupBoxLogicalStatus);
            Name = "NcFunctions";
            Size = new Size(498, 513);
            groupBoxLogicalStatus.ResumeLayout(false);
            groupBoxLogicalStatus.PerformLayout();
            groupBoxRawDriveOutput.ResumeLayout(false);
            groupBoxRawDriveOutput.PerformLayout();
            groupBoxSetActualPosition.ResumeLayout(false);
            groupBoxSetActualPosition.PerformLayout();
            groupBoxSetTargetPosition.ResumeLayout(false);
            groupBoxSetTargetPosition.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerUpdate;
        private GroupBox groupBoxLogicalStatus;
        private TextBox txtBoxTargetPosition;
        private Label label1;
        private TextBox txtBoxJerk;
        private TextBox txtBoxDeacceleration;
        private TextBox txtBoxAcceleration;
        private TextBox txtBoxTargetVelocity;
        private Label label2;
        private ComboBox comboBoxStartMode;
        private CheckBox chkBoxEnableAcceleration;
        private CheckBox chkBoxEnableJerk;
        private CheckBox chkBoxEnableDeacceleration;
        private Label lblLastTime;
        private TextBox txtBoxLastTime;
        private Button btnStop;
        private Button btnStart;
        private Label label3;
        private GroupBox groupBoxRawDriveOutput;
        private Button btnStopRawDriveOutput;
        private Button btnStartRawDriveOutput;
        private Label lblOutputValue;
        private ComboBox comboBoxRawDriveOutputMode;
        private Label lblOutputMode;
        private TextBox txtBoxRawDriveOutputValue;
        private GroupBox groupBoxSetActualPosition;
        private Button btnSetActualPosition;
        private ComboBox comboBoxSetActualPositionType;
        private TextBox txtBoxSetActualPositionValue;
        private GroupBox groupBoxSetTargetPosition;
        private Button btnSetTargetPosition;
        private ComboBox comboBoxSetTargetPositionType;
        private TextBox txtBoxSetTargetPositionValue;
        private Label label4;
        private Label label5;
        private Label label6;
        private NcAxisHeader ncAxisHeader1;
    }
}
