namespace TwinSharpControls
{
    partial class NcOnline
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
            label2 = new Label();
            txtBoxLag = new TextBox();
            label3 = new Label();
            txtBoxActualVelocity = new TextBox();
            label4 = new Label();
            txtBoxSetpointVelocity = new TextBox();
            label5 = new Label();
            txtBoxError = new TextBox();
            label6 = new Label();
            txtBoxOutput = new TextBox();
            label7 = new Label();
            txtBoxOverride = new TextBox();
            groupBoxLogicalStatus = new GroupBox();
            chkBoxMovingBw = new CheckBox();
            chkBoxMovingFw = new CheckBox();
            chkBoxNotMoving = new CheckBox();
            chkBoxHasJob = new CheckBox();
            chkBoxCalibrated = new CheckBox();
            chkBoxReady = new CheckBox();
            groupBoxPhysicalStatus = new GroupBox();
            chkBoxInPosRange = new CheckBox();
            chkBoxInTargetPos = new CheckBox();
            chkBoxCoupledMode = new CheckBox();
            groupBoxEnabling = new GroupBox();
            btnSetEnabling = new Button();
            chkBoxFeedBwEnabled = new CheckBox();
            chkBoxFeedFwEnabled = new CheckBox();
            chkBoxControllerEnabled = new CheckBox();
            txtBoxSetKvFactor = new TextBox();
            label8 = new Label();
            btnDownloadKvFactor = new Button();
            btnDownloadRefVelocity = new Button();
            label9 = new Label();
            txtBoxSetRefVelocity = new TextBox();
            label10 = new Label();
            txtBoxSetTargetVelocity = new TextBox();
            label11 = new Label();
            txtBoxSetTargetPosition = new TextBox();
            btnJogNegativeFast = new NcButton();
            btnJogNegativeSlow = new NcButton();
            btnJogPositiveSlow = new NcButton();
            btnJogPositiveFast = new NcButton();
            btnStart = new NcButton();
            btnStop = new NcButton();
            btnReset = new NcButton();
            btnReference = new NcButton();
            btnDownloadTargetPosition = new Button();
            ncAxisHeader1 = new NcAxisHeader();
            groupBoxLogicalStatus.SuspendLayout();
            groupBoxPhysicalStatus.SuspendLayout();
            groupBoxEnabling.SuspendLayout();
            SuspendLayout();
            // 
            // timerUpdate
            // 
            timerUpdate.Enabled = true;
            timerUpdate.Tick += timerUpdate_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 62);
            label2.Name = "label2";
            label2.Size = new Size(137, 15);
            label2.TabIndex = 4;
            label2.Text = "Lag Distance (min/max):";
            // 
            // txtBoxLag
            // 
            txtBoxLag.Location = new Point(18, 80);
            txtBoxLag.Name = "txtBoxLag";
            txtBoxLag.ReadOnly = true;
            txtBoxLag.Size = new Size(150, 23);
            txtBoxLag.TabIndex = 3;
            txtBoxLag.TabStop = false;
            txtBoxLag.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(174, 62);
            label3.Name = "label3";
            label3.Size = new Size(88, 15);
            label3.TabIndex = 6;
            label3.Text = "Actual Velocity:";
            // 
            // txtBoxActualVelocity
            // 
            txtBoxActualVelocity.Location = new Point(174, 80);
            txtBoxActualVelocity.Name = "txtBoxActualVelocity";
            txtBoxActualVelocity.ReadOnly = true;
            txtBoxActualVelocity.Size = new Size(150, 23);
            txtBoxActualVelocity.TabIndex = 5;
            txtBoxActualVelocity.TabStop = false;
            txtBoxActualVelocity.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(330, 62);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 8;
            label4.Text = "Setpoint Velocity:";
            // 
            // txtBoxSetpointVelocity
            // 
            txtBoxSetpointVelocity.Location = new Point(330, 80);
            txtBoxSetpointVelocity.Name = "txtBoxSetpointVelocity";
            txtBoxSetpointVelocity.ReadOnly = true;
            txtBoxSetpointVelocity.Size = new Size(150, 23);
            txtBoxSetpointVelocity.TabIndex = 7;
            txtBoxSetpointVelocity.TabStop = false;
            txtBoxSetpointVelocity.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(330, 106);
            label5.Name = "label5";
            label5.Size = new Size(35, 15);
            label5.TabIndex = 14;
            label5.Text = "Error:";
            // 
            // txtBoxError
            // 
            txtBoxError.Location = new Point(330, 124);
            txtBoxError.Name = "txtBoxError";
            txtBoxError.ReadOnly = true;
            txtBoxError.Size = new Size(150, 23);
            txtBoxError.TabIndex = 13;
            txtBoxError.TabStop = false;
            txtBoxError.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(174, 106);
            label6.Name = "label6";
            label6.Size = new Size(121, 15);
            label6.TabIndex = 12;
            label6.Text = "Total/Control Output:";
            // 
            // txtBoxOutput
            // 
            txtBoxOutput.Location = new Point(174, 124);
            txtBoxOutput.Name = "txtBoxOutput";
            txtBoxOutput.ReadOnly = true;
            txtBoxOutput.Size = new Size(150, 23);
            txtBoxOutput.TabIndex = 11;
            txtBoxOutput.TabStop = false;
            txtBoxOutput.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(18, 106);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 10;
            label7.Text = "Override:";
            // 
            // txtBoxOverride
            // 
            txtBoxOverride.Location = new Point(18, 124);
            txtBoxOverride.Name = "txtBoxOverride";
            txtBoxOverride.ReadOnly = true;
            txtBoxOverride.Size = new Size(150, 23);
            txtBoxOverride.TabIndex = 9;
            txtBoxOverride.TabStop = false;
            txtBoxOverride.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBoxLogicalStatus
            // 
            groupBoxLogicalStatus.Controls.Add(chkBoxMovingBw);
            groupBoxLogicalStatus.Controls.Add(chkBoxMovingFw);
            groupBoxLogicalStatus.Controls.Add(chkBoxNotMoving);
            groupBoxLogicalStatus.Controls.Add(chkBoxHasJob);
            groupBoxLogicalStatus.Controls.Add(chkBoxCalibrated);
            groupBoxLogicalStatus.Controls.Add(chkBoxReady);
            groupBoxLogicalStatus.Location = new Point(18, 153);
            groupBoxLogicalStatus.Name = "groupBoxLogicalStatus";
            groupBoxLogicalStatus.Size = new Size(184, 100);
            groupBoxLogicalStatus.TabIndex = 15;
            groupBoxLogicalStatus.TabStop = false;
            groupBoxLogicalStatus.Text = "Status (log.)";
            // 
            // chkBoxMovingBw
            // 
            chkBoxMovingBw.AutoSize = true;
            chkBoxMovingBw.Location = new Point(92, 72);
            chkBoxMovingBw.Name = "chkBoxMovingBw";
            chkBoxMovingBw.Size = new Size(86, 19);
            chkBoxMovingBw.TabIndex = 5;
            chkBoxMovingBw.TabStop = false;
            chkBoxMovingBw.Text = "Moving Bw";
            chkBoxMovingBw.UseVisualStyleBackColor = true;
            // 
            // chkBoxMovingFw
            // 
            chkBoxMovingFw.AutoSize = true;
            chkBoxMovingFw.Location = new Point(92, 47);
            chkBoxMovingFw.Name = "chkBoxMovingFw";
            chkBoxMovingFw.Size = new Size(85, 19);
            chkBoxMovingFw.TabIndex = 4;
            chkBoxMovingFw.TabStop = false;
            chkBoxMovingFw.Text = "Moving Fw";
            chkBoxMovingFw.UseVisualStyleBackColor = true;
            // 
            // chkBoxNotMoving
            // 
            chkBoxNotMoving.AutoSize = true;
            chkBoxNotMoving.Location = new Point(92, 22);
            chkBoxNotMoving.Name = "chkBoxNotMoving";
            chkBoxNotMoving.Size = new Size(90, 19);
            chkBoxNotMoving.TabIndex = 3;
            chkBoxNotMoving.TabStop = false;
            chkBoxNotMoving.Text = "Not Moving";
            chkBoxNotMoving.UseVisualStyleBackColor = true;
            // 
            // chkBoxHasJob
            // 
            chkBoxHasJob.AutoSize = true;
            chkBoxHasJob.Location = new Point(6, 72);
            chkBoxHasJob.Name = "chkBoxHasJob";
            chkBoxHasJob.Size = new Size(67, 19);
            chkBoxHasJob.TabIndex = 2;
            chkBoxHasJob.TabStop = false;
            chkBoxHasJob.Text = "Has Job";
            chkBoxHasJob.UseVisualStyleBackColor = true;
            // 
            // chkBoxCalibrated
            // 
            chkBoxCalibrated.AutoSize = true;
            chkBoxCalibrated.Location = new Point(6, 47);
            chkBoxCalibrated.Name = "chkBoxCalibrated";
            chkBoxCalibrated.Size = new Size(80, 19);
            chkBoxCalibrated.TabIndex = 1;
            chkBoxCalibrated.TabStop = false;
            chkBoxCalibrated.Text = "Calibrated";
            chkBoxCalibrated.UseVisualStyleBackColor = true;
            // 
            // chkBoxReady
            // 
            chkBoxReady.AutoSize = true;
            chkBoxReady.Location = new Point(6, 22);
            chkBoxReady.Name = "chkBoxReady";
            chkBoxReady.Size = new Size(58, 19);
            chkBoxReady.TabIndex = 0;
            chkBoxReady.TabStop = false;
            chkBoxReady.Text = "Ready";
            chkBoxReady.UseVisualStyleBackColor = true;
            // 
            // groupBoxPhysicalStatus
            // 
            groupBoxPhysicalStatus.Controls.Add(chkBoxInPosRange);
            groupBoxPhysicalStatus.Controls.Add(chkBoxInTargetPos);
            groupBoxPhysicalStatus.Controls.Add(chkBoxCoupledMode);
            groupBoxPhysicalStatus.Location = new Point(208, 153);
            groupBoxPhysicalStatus.Name = "groupBoxPhysicalStatus";
            groupBoxPhysicalStatus.Size = new Size(116, 100);
            groupBoxPhysicalStatus.TabIndex = 16;
            groupBoxPhysicalStatus.TabStop = false;
            groupBoxPhysicalStatus.Text = "Status (phys.)";
            // 
            // chkBoxInPosRange
            // 
            chkBoxInPosRange.AutoSize = true;
            chkBoxInPosRange.Location = new Point(6, 72);
            chkBoxInPosRange.Name = "chkBoxInPosRange";
            chkBoxInPosRange.Size = new Size(97, 19);
            chkBoxInPosRange.TabIndex = 2;
            chkBoxInPosRange.TabStop = false;
            chkBoxInPosRange.Text = "In Pos. Range";
            chkBoxInPosRange.UseVisualStyleBackColor = true;
            // 
            // chkBoxInTargetPos
            // 
            chkBoxInTargetPos.AutoSize = true;
            chkBoxInTargetPos.Location = new Point(6, 47);
            chkBoxInTargetPos.Name = "chkBoxInTargetPos";
            chkBoxInTargetPos.Size = new Size(96, 19);
            chkBoxInTargetPos.TabIndex = 1;
            chkBoxInTargetPos.TabStop = false;
            chkBoxInTargetPos.Text = "In Target Pos.";
            chkBoxInTargetPos.UseVisualStyleBackColor = true;
            // 
            // chkBoxCoupledMode
            // 
            chkBoxCoupledMode.AutoSize = true;
            chkBoxCoupledMode.Location = new Point(6, 22);
            chkBoxCoupledMode.Name = "chkBoxCoupledMode";
            chkBoxCoupledMode.Size = new Size(105, 19);
            chkBoxCoupledMode.TabIndex = 0;
            chkBoxCoupledMode.TabStop = false;
            chkBoxCoupledMode.Text = "Coupled Mode";
            chkBoxCoupledMode.UseVisualStyleBackColor = true;
            // 
            // groupBoxEnabling
            // 
            groupBoxEnabling.Controls.Add(btnSetEnabling);
            groupBoxEnabling.Controls.Add(chkBoxFeedBwEnabled);
            groupBoxEnabling.Controls.Add(chkBoxFeedFwEnabled);
            groupBoxEnabling.Controls.Add(chkBoxControllerEnabled);
            groupBoxEnabling.Location = new Point(330, 153);
            groupBoxEnabling.Name = "groupBoxEnabling";
            groupBoxEnabling.Size = new Size(150, 100);
            groupBoxEnabling.TabIndex = 0;
            groupBoxEnabling.TabStop = false;
            groupBoxEnabling.Text = "Enabling";
            // 
            // btnSetEnabling
            // 
            btnSetEnabling.Location = new Point(91, 19);
            btnSetEnabling.Name = "btnSetEnabling";
            btnSetEnabling.Size = new Size(53, 23);
            btnSetEnabling.TabIndex = 0;
            btnSetEnabling.Text = "Set";
            btnSetEnabling.UseVisualStyleBackColor = true;
            btnSetEnabling.Click += btnSetEnabling_Click;
            // 
            // chkBoxFeedBwEnabled
            // 
            chkBoxFeedBwEnabled.AutoSize = true;
            chkBoxFeedBwEnabled.Location = new Point(6, 72);
            chkBoxFeedBwEnabled.Name = "chkBoxFeedBwEnabled";
            chkBoxFeedBwEnabled.Size = new Size(70, 19);
            chkBoxFeedBwEnabled.TabIndex = 2;
            chkBoxFeedBwEnabled.TabStop = false;
            chkBoxFeedBwEnabled.Text = "Feed Bw";
            chkBoxFeedBwEnabled.UseVisualStyleBackColor = true;
            // 
            // chkBoxFeedFwEnabled
            // 
            chkBoxFeedFwEnabled.AutoSize = true;
            chkBoxFeedFwEnabled.Location = new Point(6, 47);
            chkBoxFeedFwEnabled.Name = "chkBoxFeedFwEnabled";
            chkBoxFeedFwEnabled.Size = new Size(69, 19);
            chkBoxFeedFwEnabled.TabIndex = 1;
            chkBoxFeedFwEnabled.TabStop = false;
            chkBoxFeedFwEnabled.Text = "Feed Fw";
            chkBoxFeedFwEnabled.UseVisualStyleBackColor = true;
            // 
            // chkBoxControllerEnabled
            // 
            chkBoxControllerEnabled.AutoSize = true;
            chkBoxControllerEnabled.Location = new Point(6, 22);
            chkBoxControllerEnabled.Name = "chkBoxControllerEnabled";
            chkBoxControllerEnabled.Size = new Size(79, 19);
            chkBoxControllerEnabled.TabIndex = 0;
            chkBoxControllerEnabled.TabStop = false;
            chkBoxControllerEnabled.Text = "Controller";
            chkBoxControllerEnabled.UseVisualStyleBackColor = true;
            // 
            // txtBoxSetKvFactor
            // 
            txtBoxSetKvFactor.Location = new Point(18, 284);
            txtBoxSetKvFactor.Name = "txtBoxSetKvFactor";
            txtBoxSetKvFactor.Size = new Size(184, 23);
            txtBoxSetKvFactor.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(18, 266);
            label8.Margin = new Padding(3, 10, 3, 0);
            label8.Name = "label8";
            label8.Size = new Size(118, 15);
            label8.TabIndex = 20;
            label8.Text = "Controller KV-Factor:";
            // 
            // btnDownloadKvFactor
            // 
            btnDownloadKvFactor.Location = new Point(208, 284);
            btnDownloadKvFactor.Name = "btnDownloadKvFactor";
            btnDownloadKvFactor.Size = new Size(23, 23);
            btnDownloadKvFactor.TabIndex = 2;
            btnDownloadKvFactor.Text = "↓";
            btnDownloadKvFactor.UseVisualStyleBackColor = true;
            btnDownloadKvFactor.Click += btnDownloadKvFactor_Click;
            // 
            // btnDownloadRefVelocity
            // 
            btnDownloadRefVelocity.Location = new Point(457, 283);
            btnDownloadRefVelocity.Name = "btnDownloadRefVelocity";
            btnDownloadRefVelocity.Size = new Size(23, 23);
            btnDownloadRefVelocity.TabIndex = 4;
            btnDownloadRefVelocity.Text = "↓";
            btnDownloadRefVelocity.UseVisualStyleBackColor = true;
            btnDownloadRefVelocity.Click += btnDownloadRefVelocity_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(267, 265);
            label9.Margin = new Padding(3, 10, 3, 0);
            label9.Name = "label9";
            label9.Size = new Size(106, 15);
            label9.TabIndex = 23;
            label9.Text = "Reference Velocity:";
            // 
            // txtBoxSetRefVelocity
            // 
            txtBoxSetRefVelocity.Location = new Point(267, 283);
            txtBoxSetRefVelocity.Name = "txtBoxSetRefVelocity";
            txtBoxSetRefVelocity.Size = new Size(184, 23);
            txtBoxSetRefVelocity.TabIndex = 3;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(267, 312);
            label10.Margin = new Padding(3, 3, 3, 0);
            label10.Name = "label10";
            label10.Size = new Size(86, 15);
            label10.TabIndex = 29;
            label10.Text = "Target Velocity:";
            // 
            // txtBoxSetTargetVelocity
            // 
            txtBoxSetTargetVelocity.Location = new Point(267, 330);
            txtBoxSetTargetVelocity.Name = "txtBoxSetTargetVelocity";
            txtBoxSetTargetVelocity.Size = new Size(184, 23);
            txtBoxSetTargetVelocity.TabIndex = 7;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(18, 313);
            label11.Margin = new Padding(3, 3, 3, 0);
            label11.Name = "label11";
            label11.Size = new Size(88, 15);
            label11.TabIndex = 26;
            label11.Text = "Target Position:";
            // 
            // txtBoxSetTargetPosition
            // 
            txtBoxSetTargetPosition.Location = new Point(18, 331);
            txtBoxSetTargetPosition.Name = "txtBoxSetTargetPosition";
            txtBoxSetTargetPosition.Size = new Size(184, 23);
            txtBoxSetTargetPosition.TabIndex = 5;
            // 
            // btnJogNegativeFast
            // 
            btnJogNegativeFast.BackColorGradient1 = Color.Orange;
            btnJogNegativeFast.BackColorGradient2 = Color.DarkOrange;
            btnJogNegativeFast.BlinkEnabled = false;
            btnJogNegativeFast.DrawAsDown = false;
            btnJogNegativeFast.Font = new Font("Bahnschrift", 12F);
            btnJogNegativeFast.Location = new Point(18, 367);
            btnJogNegativeFast.Margin = new Padding(1, 10, 1, 2);
            btnJogNegativeFast.Name = "btnJogNegativeFast";
            btnJogNegativeFast.RoundBottomRadius = 5;
            btnJogNegativeFast.RoundTopRadius = 5;
            btnJogNegativeFast.Size = new Size(52, 52);
            btnJogNegativeFast.TabIndex = 8;
            btnJogNegativeFast.TabStop = false;
            btnJogNegativeFast.Text = "--\r\nF1";
            btnJogNegativeFast.TextAlign = ContentAlignment.MiddleCenter;
            btnJogNegativeFast.TextColor = Color.FromArgb(240, 240, 239);
            btnJogNegativeFast.MouseDown += btnJogNegativeFast_MouseDown;
            btnJogNegativeFast.MouseUp += btnJog_MouseUp;
            // 
            // btnJogNegativeSlow
            // 
            btnJogNegativeSlow.BackColorGradient1 = Color.Orange;
            btnJogNegativeSlow.BackColorGradient2 = Color.DarkOrange;
            btnJogNegativeSlow.BlinkEnabled = false;
            btnJogNegativeSlow.DrawAsDown = false;
            btnJogNegativeSlow.Font = new Font("Bahnschrift", 12F);
            btnJogNegativeSlow.Location = new Point(72, 367);
            btnJogNegativeSlow.Margin = new Padding(1, 10, 1, 2);
            btnJogNegativeSlow.Name = "btnJogNegativeSlow";
            btnJogNegativeSlow.RoundBottomRadius = 5;
            btnJogNegativeSlow.RoundTopRadius = 5;
            btnJogNegativeSlow.Size = new Size(52, 52);
            btnJogNegativeSlow.TabIndex = 9;
            btnJogNegativeSlow.TabStop = false;
            btnJogNegativeSlow.Text = "-\r\nF2\r\n";
            btnJogNegativeSlow.TextAlign = ContentAlignment.MiddleCenter;
            btnJogNegativeSlow.TextColor = Color.FromArgb(240, 240, 239);
            btnJogNegativeSlow.MouseDown += btnJogNegativeSlow_MouseDown;
            btnJogNegativeSlow.MouseUp += btnJog_MouseUp;
            // 
            // btnJogPositiveSlow
            // 
            btnJogPositiveSlow.BackColorGradient1 = Color.Orange;
            btnJogPositiveSlow.BackColorGradient2 = Color.DarkOrange;
            btnJogPositiveSlow.BlinkEnabled = false;
            btnJogPositiveSlow.DrawAsDown = false;
            btnJogPositiveSlow.Font = new Font("Bahnschrift", 12F);
            btnJogPositiveSlow.Location = new Point(126, 367);
            btnJogPositiveSlow.Margin = new Padding(1, 10, 1, 2);
            btnJogPositiveSlow.Name = "btnJogPositiveSlow";
            btnJogPositiveSlow.RoundBottomRadius = 5;
            btnJogPositiveSlow.RoundTopRadius = 5;
            btnJogPositiveSlow.Size = new Size(52, 52);
            btnJogPositiveSlow.TabIndex = 10;
            btnJogPositiveSlow.TabStop = false;
            btnJogPositiveSlow.Text = "+\r\nF3";
            btnJogPositiveSlow.TextAlign = ContentAlignment.MiddleCenter;
            btnJogPositiveSlow.TextColor = Color.FromArgb(240, 240, 239);
            btnJogPositiveSlow.MouseDown += btnJogPositiveSlow_MouseDown;
            btnJogPositiveSlow.MouseUp += btnJog_MouseUp;
            // 
            // btnJogPositiveFast
            // 
            btnJogPositiveFast.BackColorGradient1 = Color.Orange;
            btnJogPositiveFast.BackColorGradient2 = Color.DarkOrange;
            btnJogPositiveFast.BlinkEnabled = false;
            btnJogPositiveFast.DrawAsDown = false;
            btnJogPositiveFast.Font = new Font("Bahnschrift", 12F);
            btnJogPositiveFast.Location = new Point(180, 367);
            btnJogPositiveFast.Margin = new Padding(1, 10, 1, 2);
            btnJogPositiveFast.Name = "btnJogPositiveFast";
            btnJogPositiveFast.RoundBottomRadius = 5;
            btnJogPositiveFast.RoundTopRadius = 5;
            btnJogPositiveFast.Size = new Size(52, 52);
            btnJogPositiveFast.TabIndex = 11;
            btnJogPositiveFast.TabStop = false;
            btnJogPositiveFast.Text = "++\r\nF4";
            btnJogPositiveFast.TextAlign = ContentAlignment.MiddleCenter;
            btnJogPositiveFast.TextColor = Color.FromArgb(240, 240, 239);
            btnJogPositiveFast.MouseDown += btnJogPositiveFast_MouseDown;
            btnJogPositiveFast.MouseUp += btnJog_MouseUp;
            // 
            // btnStart
            // 
            btnStart.BackColorGradient1 = Color.LimeGreen;
            btnStart.BackColorGradient2 = Color.ForestGreen;
            btnStart.BlinkEnabled = false;
            btnStart.DrawAsDown = false;
            btnStart.Font = new Font("Bahnschrift", 12F);
            btnStart.Location = new Point(234, 367);
            btnStart.Margin = new Padding(1, 10, 1, 2);
            btnStart.Name = "btnStart";
            btnStart.RoundBottomRadius = 5;
            btnStart.RoundTopRadius = 5;
            btnStart.Size = new Size(52, 52);
            btnStart.TabIndex = 12;
            btnStart.TabStop = false;
            btnStart.Text = "Start\r\nF5\r\n";
            btnStart.TextAlign = ContentAlignment.MiddleCenter;
            btnStart.TextColor = Color.FromArgb(240, 240, 239);
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColorGradient1 = Color.Tomato;
            btnStop.BackColorGradient2 = Color.Red;
            btnStop.BlinkEnabled = false;
            btnStop.DrawAsDown = false;
            btnStop.Font = new Font("Bahnschrift", 12F);
            btnStop.Location = new Point(288, 367);
            btnStop.Margin = new Padding(1, 10, 1, 2);
            btnStop.Name = "btnStop";
            btnStop.RoundBottomRadius = 5;
            btnStop.RoundTopRadius = 5;
            btnStop.Size = new Size(52, 52);
            btnStop.TabIndex = 13;
            btnStop.TabStop = false;
            btnStop.Text = "Stop\r\nF6";
            btnStop.TextAlign = ContentAlignment.MiddleCenter;
            btnStop.TextColor = Color.FromArgb(240, 240, 239);
            btnStop.MouseClick += btnStop_MouseClick;
            // 
            // btnReset
            // 
            btnReset.BackColorGradient1 = Color.DodgerBlue;
            btnReset.BackColorGradient2 = Color.SteelBlue;
            btnReset.BlinkEnabled = false;
            btnReset.DrawAsDown = false;
            btnReset.Font = new Font("Bahnschrift", 12F);
            btnReset.Location = new Point(374, 367);
            btnReset.Margin = new Padding(1, 10, 1, 2);
            btnReset.Name = "btnReset";
            btnReset.RoundBottomRadius = 5;
            btnReset.RoundTopRadius = 5;
            btnReset.Size = new Size(52, 52);
            btnReset.TabIndex = 14;
            btnReset.TabStop = false;
            btnReset.Text = "Reset\r\nF8";
            btnReset.TextAlign = ContentAlignment.MiddleCenter;
            btnReset.TextColor = Color.FromArgb(240, 240, 239);
            btnReset.Click += btnReset_Click;
            // 
            // btnReference
            // 
            btnReference.BackColorGradient1 = Color.DodgerBlue;
            btnReference.BackColorGradient2 = Color.SteelBlue;
            btnReference.BlinkEnabled = false;
            btnReference.DrawAsDown = false;
            btnReference.Font = new Font("Bahnschrift", 12F);
            btnReference.Location = new Point(428, 367);
            btnReference.Margin = new Padding(1, 10, 1, 2);
            btnReference.Name = "btnReference";
            btnReference.RoundBottomRadius = 5;
            btnReference.RoundTopRadius = 5;
            btnReference.Size = new Size(52, 52);
            btnReference.TabIndex = 15;
            btnReference.TabStop = false;
            btnReference.Text = "Ref.\r\nF9";
            btnReference.TextAlign = ContentAlignment.MiddleCenter;
            btnReference.TextColor = Color.FromArgb(240, 240, 239);
            btnReference.Click += btnReference_Click;
            // 
            // btnDownloadTargetPosition
            // 
            btnDownloadTargetPosition.Location = new Point(208, 331);
            btnDownloadTargetPosition.Name = "btnDownloadTargetPosition";
            btnDownloadTargetPosition.Size = new Size(23, 23);
            btnDownloadTargetPosition.TabIndex = 6;
            btnDownloadTargetPosition.Text = "↓";
            btnDownloadTargetPosition.UseVisualStyleBackColor = true;
            btnDownloadTargetPosition.Click += btnDownloadTargetPosition_Click;
            // 
            // ncAxisHeader1
            // 
            ncAxisHeader1.Location = new Point(18, 3);
            ncAxisHeader1.Name = "ncAxisHeader1";
            ncAxisHeader1.Size = new Size(464, 48);
            ncAxisHeader1.TabIndex = 30;
            // 
            // NcOnline
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ncAxisHeader1);
            Controls.Add(btnDownloadTargetPosition);
            Controls.Add(btnReference);
            Controls.Add(btnReset);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(btnJogPositiveFast);
            Controls.Add(btnJogPositiveSlow);
            Controls.Add(btnJogNegativeSlow);
            Controls.Add(btnJogNegativeFast);
            Controls.Add(label10);
            Controls.Add(txtBoxSetTargetVelocity);
            Controls.Add(label11);
            Controls.Add(txtBoxSetTargetPosition);
            Controls.Add(btnDownloadRefVelocity);
            Controls.Add(label9);
            Controls.Add(txtBoxSetRefVelocity);
            Controls.Add(btnDownloadKvFactor);
            Controls.Add(label8);
            Controls.Add(txtBoxSetKvFactor);
            Controls.Add(groupBoxEnabling);
            Controls.Add(groupBoxPhysicalStatus);
            Controls.Add(groupBoxLogicalStatus);
            Controls.Add(label5);
            Controls.Add(txtBoxError);
            Controls.Add(label6);
            Controls.Add(txtBoxOutput);
            Controls.Add(label7);
            Controls.Add(txtBoxOverride);
            Controls.Add(label4);
            Controls.Add(txtBoxSetpointVelocity);
            Controls.Add(label3);
            Controls.Add(txtBoxActualVelocity);
            Controls.Add(label2);
            Controls.Add(txtBoxLag);
            Name = "NcOnline";
            Size = new Size(498, 427);
            groupBoxLogicalStatus.ResumeLayout(false);
            groupBoxLogicalStatus.PerformLayout();
            groupBoxPhysicalStatus.ResumeLayout(false);
            groupBoxPhysicalStatus.PerformLayout();
            groupBoxEnabling.ResumeLayout(false);
            groupBoxEnabling.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timerUpdate;
        private Label label2;
        private TextBox txtBoxLag;
        private Label label3;
        private TextBox txtBoxActualVelocity;
        private Label label4;
        private TextBox txtBoxSetpointVelocity;
        private Label label5;
        private TextBox txtBoxError;
        private Label label6;
        private TextBox txtBoxOutput;
        private Label label7;
        private TextBox txtBoxOverride;
        private GroupBox groupBoxLogicalStatus;
        private CheckBox chkBoxCalibrated;
        private CheckBox chkBoxReady;
        private CheckBox chkBoxMovingBw;
        private CheckBox chkBoxMovingFw;
        private CheckBox chkBoxNotMoving;
        private CheckBox chkBoxHasJob;
        private GroupBox groupBoxPhysicalStatus;
        private CheckBox chkBoxInPosRange;
        private CheckBox chkBoxInTargetPos;
        private CheckBox chkBoxCoupledMode;
        private GroupBox groupBoxEnabling;
        private CheckBox chkBoxFeedBwEnabled;
        private CheckBox chkBoxFeedFwEnabled;
        private CheckBox chkBoxControllerEnabled;
        private Button btnSetEnabling;
        private TextBox txtBoxSetKvFactor;
        private Label label8;
        private Button btnDownloadKvFactor;
        private Button btnDownloadRefVelocity;
        private Label label9;
        private TextBox txtBoxSetRefVelocity;
        private Label label10;
        private TextBox txtBoxSetTargetVelocity;
        private Label label11;
        private TextBox txtBoxSetTargetPosition;
        private NcButton btnJogNegativeFast;
        private NcButton btnJogNegativeSlow;
        private NcButton btnJogPositiveSlow;
        private NcButton btnJogPositiveFast;
        private NcButton btnStart;
        private NcButton btnStop;
        private NcButton btnReset;
        private NcButton btnReference;
        private Button btnDownloadTargetPosition;
        private NcAxisHeader ncAxisHeader1;
    }
}
