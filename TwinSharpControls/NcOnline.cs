using System.Globalization;
using TwinSharp.NC;

namespace TwinSharpControls
{
    public partial class NcOnline : UserControl
    {
        Axis? axis;
        public NcOnline()
        {
            InitializeComponent();

            //In order for a TextBox that is ReadOnly to obey forecolor, we must explicitly set backcolor once.
            //We will change the forecolor if the axis is in error state.
            txtBoxError.BackColor = txtBoxError.BackColor;
        }

        /// <summary>
        /// Set the axis to be displayed in the control.
        /// </summary>
        public Axis? Axis
        {
            get => axis;
            set
            {
                axis = value;
                InitEditableTextBoxes();
            }
        }


        /// <summary>
        /// New data is fetched from axis at this interval. In milliseconds.
        /// </summary>
        public int UpdateInterval
        {
            get => timerUpdate.Interval;
            set => timerUpdate.Interval = value;
        }

        private void InitEditableTextBoxes()
        {
            if (axis == null)
                return;

            if (axis.Controllers.Length > 0)
                txtBoxSetKvFactor.Text = FormatDouble(axis.Controllers[0].Parameters.ProportionalGainKpOrKv, 2);


            txtBoxSetRefVelocity.Text = FormatDouble(axis.Parameters.RefVelocityAtRefOutput, 1);
        }


        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            var data = axis.State.OnlineData;

             ncAxisHeader1.UpdateHeader(data);


            txtBoxLag.Text = $"{FormatDouble(axis.State.LagErrorPosition)} ({FormatDouble(axis.State.LagErrorPeakMinimum, 3)}, {FormatDouble(axis.State.LagErrorPeakMaximum, 3)})";
            txtBoxActualVelocity.Text = FormatDouble(data.ActualVelocity);
            txtBoxSetpointVelocity.Text = FormatDouble(data.SetVelocity);

            txtBoxOverride.Text = FormatDouble(data.VelocityOverride / 10_000.0);
            txtBoxOutput.Text = $"{FormatDouble(data.TotalOutputPercent * 100.0, 2)} / {FormatDouble(data.ControllerOutputPercent * 100.0, 2)}";
            txtBoxError.Text = $"{data.ErrorState.ToString()} (0x{data.ErrorState.ToString("X")})";
            txtBoxError.ForeColor = data.ErrorState != 0 ? Color.Red : SystemColors.ControlText;

            chkBoxReady.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.ReadyForOperation);
            chkBoxCalibrated.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.Homed); ;
            chkBoxHasJob.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.HasJob);

            chkBoxNotMoving.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.NotMoving);
            chkBoxMovingFw.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.PositiveDirection);
            chkBoxMovingBw.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.NegativeDirection);


            chkBoxCoupledMode.Checked = data.SlaveCouplingState != CoupleState.Single;
            chkBoxInTargetPos.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.InTargetPosition);
            chkBoxInPosRange.Checked = data.AxisStatusDWord.HasFlag(StateDWordFlags.InPositionArea);

            chkBoxControllerEnabled.Checked = axis.CyclicProcessData.ControllerEnable;
            chkBoxFeedFwEnabled.Checked = axis.CyclicProcessData.FeedEnablePlus;
            chkBoxFeedBwEnabled.Checked = axis.CyclicProcessData.FeedEnableMinus;
        }




        internal static string FormatDouble(double value)
        {
            return value.ToString("0.0000", CultureInfo.InvariantCulture);
        }

        internal static string FormatDouble(double value, int decimalPlaces)
        {
            return value.ToString("0." + Zeroes(decimalPlaces), CultureInfo.InvariantCulture);
        }

        internal static string Zeroes(int num)
        {
            return new string('0', num);
        }

        private void btnSetEnabling_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            bool controllerChecked = axis.CyclicProcessData.ControllerEnable;
            bool feedFwChecked = axis.CyclicProcessData.FeedEnablePlus;
            bool feedBwChecked = axis.CyclicProcessData.FeedEnableMinus;
            string feedOverride = (axis.CyclicProcessData.VelocityOverride / 10_000.0).ToString("0.0", CultureInfo.InvariantCulture);

            using var form = CreateSetEnablingForm(controllerChecked, feedFwChecked, feedBwChecked, feedOverride);
            var res = form.ShowDialog();


            if (res != DialogResult.OK)
                return; //User cancelled

            //Get the checkboxes from the recently shown form and apply the values to the axis.
            if (form.Controls["chkBoxController"] is CheckBox chkBoxController)
                axis.CyclicProcessData.ControllerEnable = chkBoxController.Checked;

            if (form.Controls["chkBoxFeedFw"] is CheckBox chkBoxFeedFw)
                axis.CyclicProcessData.FeedEnablePlus = chkBoxFeedFw.Checked;

            if (form.Controls["chkBoxFeedBw"] is CheckBox chkBoxFeedBw)
                axis.CyclicProcessData.FeedEnableMinus = chkBoxFeedBw.Checked;

            if (form.Controls["textbox"] is TextBox textbox)
            {
                if (double.TryParse(textbox.Text, CultureInfo.InvariantCulture, out var value))
                {
                    if (value >= 0 && value <= 100)
                        axis.CyclicProcessData.VelocityOverride = (uint)(value * 10_000);
                    else
                        MessageBox.Show("Invalid feed override value. Enter a value between 0 and 100.");
                }
                else
                    MessageBox.Show("Invalid feed override value. Enter a value between 0 and 100.");
            }
        }

        private Form CreateSetEnablingForm(bool controllerChecked, bool feedFwChecked, bool feedBwChecked, string feedOverrideText)
        {
            var form = new Form
            {
                Size = new Size(225, 175),
                Text = "Set Enabling",
                MinimizeBox = false,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterParent,
                KeyPreview = true,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var chkBoxController = new CheckBox { Name = "chkBoxController", Text = "Controller", Location = new Point(6, 6), Checked = controllerChecked };
            var chkBoxFeedFw = new CheckBox { Name = "chkBoxFeedFw", Text = "Feed Fw", Location = new Point(6, 31), Checked = feedFwChecked };
            var chkBoxFeedBw = new CheckBox { Name = "chkBoxFeedBw", Text = "Feed Bw", Location = new Point(6, 56), Checked = feedBwChecked };

            var label = new Label() { Text = "Override [%]:", Location = new Point(6, 85) };
            var textbox = new TextBox() { Name = "textbox", Text = feedOverrideText, Location = new Point(6, 105) };

            var btnOk = new Button() { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(120, 6) };
            var btnCancel = new Button() { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(120, 31) };
            var btnAll = new Button() { Text = "All", DialogResult = DialogResult.OK, Location = new Point(120, 105) };

            btnAll.Click += (s, e) =>
            {
                chkBoxController.Checked = true;
                chkBoxFeedFw.Checked = true;
                chkBoxFeedBw.Checked = true;
                textbox.Text = "100";
            };

            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    form.DialogResult = DialogResult.Cancel;
                else if (e.KeyCode == Keys.Enter)
                    form.DialogResult = DialogResult.OK;
            };

            form.Controls.AddRange([chkBoxController, chkBoxFeedFw, chkBoxFeedBw, label, textbox, btnOk, btnCancel, btnAll]);

            return form;
        }

        private void btnDownloadKvFactor_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            if (axis.Controllers.Length == 0)
                return;

            if (double.TryParse(txtBoxSetKvFactor.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                axis.Controllers[0].Parameters.ProportionalGainKpOrKv = value;
        }

        private void btnJogNegativeFast_MouseDown(object sender, MouseEventArgs e)
        {
            StartJog(GroupAxisStartType.JogNegativeFast);
        }

        private void btnJogNegativeSlow_MouseDown(object sender, MouseEventArgs e)
        {
            StartJog(GroupAxisStartType.JogNegativeSlow);
        }


        private void btnJogPositiveSlow_MouseDown(object sender, MouseEventArgs e)
        {
            StartJog(GroupAxisStartType.JogPositiveSlow);
        }

        private void btnJogPositiveFast_MouseDown(object sender, MouseEventArgs e)
        {
            StartJog(GroupAxisStartType.JogPositiveFast);
        }

        private bool StartJog(GroupAxisStartType jogType)
        {
            if (axis == null)
                return false;
            try
            {
                axis.Functions.StandardAxisStart(jogType, 0, 0);
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not jog: " + exc.Message);
                return false;
            }
        }

        private void btnJog_MouseUp(object sender, MouseEventArgs e)
        {
            if (axis == null)
                return;

            axis.Functions.Stop();
        }


        bool keyIsDown;
        int WM_KEYDOWN = 0x100;
        int WM_KEYUP = 0x101;

        protected override bool ProcessKeyPreview(ref Message m)
        {
            //User controls don't get key events by default. So we do some raw key interception here.
            var keys = (Keys)m.WParam;

            bool keyDown = m.Msg == WM_KEYDOWN;

            if (keyDown && keyIsDown) //Ignore key repeats
                return base.ProcessKeyPreview(ref m);


            bool keyUp = m.Msg == WM_KEYUP;

            if (keyUp)
                return HandleKeyUp(ref m, keys);

            if (keyDown)
                return HandleKeyDown(ref m, keys);

            return base.ProcessKeyPreview(ref m);
        }

        private bool HandleKeyUp(ref Message msg, Keys keyData)
        {
            //Don't draw button as down anymore
            switch (keyData)
            {
                case Keys.F1:
                    axis?.Functions.Stop();
                    btnJogNegativeFast.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F2:
                    axis?.Functions.Stop();
                    btnJogNegativeSlow.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F3:
                    axis?.Functions.Stop();
                    btnJogPositiveSlow.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F4:
                    axis?.Functions.Stop();
                    btnJogPositiveFast.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F5:
                    StartAxis();
                    btnStart.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F6:
                    btnStop.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F8:
                    btnReset.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                case Keys.F9:
                    ReferenceAxis();
                    btnReference.DrawAsDown = false;
                    keyIsDown = false;
                    return true;
                default:
                    break;
            }


            return base.ProcessKeyPreview(ref msg);
        }

        private bool HandleKeyDown(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                if (StartJog(GroupAxisStartType.JogNegativeFast))
                {
                    btnJogNegativeFast.DrawAsDown = true;
                    keyIsDown = true;
                }

                return true;
            }
            else if (keyData == Keys.F2)
            {
                if (StartJog(GroupAxisStartType.JogNegativeSlow))
                {
                    btnJogNegativeSlow.DrawAsDown = true;
                    keyIsDown = true;
                }

                return true;
            }
            else if (keyData == Keys.F3)
            {
                if (StartJog(GroupAxisStartType.JogPositiveSlow))
                {
                    btnJogPositiveSlow.DrawAsDown = true;
                    keyIsDown = true;
                }

                return true;
            }
            else if (keyData == Keys.F4)
            {
                if (StartJog(GroupAxisStartType.JogPositiveFast))
                {
                    btnJogPositiveFast.DrawAsDown = true;
                    keyIsDown = true;
                }

                return true;
            }
            else if (keyData == Keys.F5)
            {
                //Start handled in key up.
                btnStart.DrawAsDown = true;
                keyIsDown = true;
                return true;
            }
            else if (keyData == Keys.F6)
            {
                StopAxis();
                btnStop.DrawAsDown = true;
                keyIsDown = true;
                return true;
            }
            else if (keyData == Keys.F8)
            {
                ResetAxis();
                btnReset.DrawAsDown = true;
                keyIsDown = true;
                return true;
            }
            else if (keyData == Keys.F9)
            {
                //Reference started in key up.
                btnReference.DrawAsDown = true;
                keyIsDown = true;
                return true;
            }

            return base.ProcessKeyPreview(ref msg);
        }

        private void btnStop_MouseClick(object sender, MouseEventArgs e)
        {
            StopAxis();
        }

        private void StopAxis()
        {
            axis?.Functions.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAxis();
        }

        private void ResetAxis()
        {
            axis?.Functions.Reset();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartAxis();
        }

        private bool StartAxis()
        {
            try
            {
                if (!double.TryParse(txtBoxSetTargetPosition.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var position))
                {
                    txtBoxSetTargetPosition.SelectAll();
                    txtBoxSetTargetPosition.Focus();
                    MessageBox.Show("Invalid target position.");
                    return false;
                }

                if (!double.TryParse(txtBoxSetTargetVelocity.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var velocity))
                {
                    txtBoxSetTargetVelocity.SelectAll();
                    txtBoxSetTargetVelocity.Focus();
                    MessageBox.Show("Invalid target velocity.");
                    return false;
                }


                axis?.Functions.StandardAxisStart(GroupAxisStartType.AbsoluteStart, position, velocity);
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Axis start not possible: " + exc.Message);
                return false;
            }
        }

        private void btnDownloadTargetPosition_Click(object sender, EventArgs e)
        {
            //Used to adjust target position. Axis must already be running.
            if (axis == null)
                return;

            if (!double.TryParse(txtBoxSetTargetPosition.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var position))
            {
                txtBoxSetTargetPosition.SelectAll();
                txtBoxSetTargetPosition.Focus();
                MessageBox.Show("Invalid target position.");
                return;
            }

            try
            {
                axis.Functions.NewEndPositionAxis(EndPositionType.AbsolutePosition, position);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not update target position: " + exc.Message);
            }
        }

        private void btnReference_Click(object sender, EventArgs e)
        {
            ReferenceAxis();
        }

        private void ReferenceAxis()
        {
            axis?.Functions.ReferenceAxis();
        }

        private void btnDownloadRefVelocity_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;


            if (!double.TryParse(txtBoxSetRefVelocity.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var refVelocity))
            {
                txtBoxSetRefVelocity.SelectAll();
                txtBoxSetRefVelocity.Focus();
                MessageBox.Show("Invalid reference velocity.");
                return;
            }


            axis.Parameters.RefVelocityAtRefOutput = refVelocity;
        }
    }
}
