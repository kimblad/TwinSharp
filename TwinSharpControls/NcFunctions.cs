using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using TwinSharp;
using TwinSharp.NC;

namespace TwinSharpControls
{
    public partial class NcFunctions : UserControl
    {
        Axis? axis;
        public NcFunctions()
        {
            InitializeComponent();
            PopulateComboBoxes();

        }

        private void PopulateComboBoxes()
        {
            comboBoxStartMode.Items.AddRange(StartModeComboBoxItem.AllItems);
            comboBoxStartMode.SelectedIndex = 0;

            comboBoxRawDriveOutputMode.Items.AddRange(RawDriveOutputModeComboBoxItem.AllItems);
            comboBoxRawDriveOutputMode.SelectedIndex = 0;

            comboBoxSetActualPositionType.Items.AddRange(SetActualPositionComboBoxItem.AllItems);
            comboBoxSetActualPositionType.SelectedIndex = 0;

            comboBoxSetTargetPositionType.Items.AddRange(SetTargetPositionComboBoxItem.AllItems);
            comboBoxSetTargetPositionType.SelectedIndex = 0;
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


        }


        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            var data = axis.State.OnlineData;

            ncAxisHeader1.UpdateHeader(data);

            txtBoxLastTime.Text = FormatDouble(axis.State.PositioningTimeLastMotionCommand, 3);
        }





        private string FormatDouble(double value)
        {
            return value.ToString("0.0000", CultureInfo.InvariantCulture);
        }

        private string FormatDouble(double value, int decimalPlaces)
        {
            return value.ToString("0." + Zeroes(decimalPlaces), CultureInfo.InvariantCulture);
        }

        private string Zeroes(int num)
        {
            return new string('0', num);
        }

        private void chkBoxEnableAcceleration_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxAcceleration.Enabled = chkBoxEnableAcceleration.Checked;
        }

        private void chkBoxEnableDeacceleration_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxDeacceleration.Enabled = chkBoxEnableDeacceleration.Checked;
        }

        private void chkBoxEnableJerk_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxJerk.Enabled = chkBoxEnableJerk.Checked;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            axis?.Functions.Stop();
        }


        #region Enums and ComboBoxItems
        internal enum StartMode
        {
            Absolute,
            Relative,
            EndlessPos,
            EndlessNeg,
            Modulo,
            ModuloShortestWay,
            ModuloPosDirection,
            ModuloNegDirection,
            JogPos,
            JogNeg,
            PlusOne,
            PlusTenth,
            PlusHundredth,
            PlusThousandth,
            MinusOne,
            MinusTenth,
            MinusHundredth,
            MinusThousandth,
            ReversingSequence,
            StartStopSequence,
            VeloStepSequence,
            SinusSequenceBode,
            SinusOscillation,
        }


        internal class StartModeComboBoxItem
        {
            internal static StartModeComboBoxItem[] AllItems =
            [
                new StartModeComboBoxItem(StartMode.Absolute, "Absolute", GroupAxisStartType.AbsoluteStart),
                new StartModeComboBoxItem(StartMode.Relative, "Relative", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.EndlessPos, "Endless +", GroupAxisStartType.ContinousStartPositive),
                new StartModeComboBoxItem(StartMode.EndlessNeg, "Endless -", GroupAxisStartType.ContinousStartNegative),
                new StartModeComboBoxItem(StartMode.Modulo, "Modulo", GroupAxisStartType.ModuloStartOLD),
                new StartModeComboBoxItem(StartMode.ModuloShortestWay, "Modulo shortest way", GroupAxisStartType.ModuloStartShortestDistance),
                new StartModeComboBoxItem(StartMode.ModuloPosDirection, "Modulo plus direct.", GroupAxisStartType.ModuloStartPositiveDirection),
                new StartModeComboBoxItem(StartMode.ModuloNegDirection, "Modulo minus direct.", GroupAxisStartType.ModuloStartNegativeDirection),
                new StartModeComboBoxItem(StartMode.JogPos, "Jog +", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.JogNeg, "Jog -", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.PlusOne, "+ 1", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.PlusTenth, "+ 0.1", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.PlusHundredth, "+ 0.01", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.PlusThousandth, "+ 0.001", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.MinusOne, "- 1", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.MinusTenth, "- 0.1", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.MinusHundredth, "- 0.01", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.MinusThousandth, "- 0.001", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.ReversingSequence, "Reversing sequence", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.StartStopSequence, "Start/Stop sequence", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.VeloStepSequence, "Velo step sequence", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.SinusSequenceBode, "Sinus sequence (Bode)", GroupAxisStartType.RelativeStart),
                new StartModeComboBoxItem(StartMode.SinusOscillation, "Sinus oscillation", GroupAxisStartType.RelativeStart)
            ];



            internal StartMode StartModeIdentifier;
            internal string Text;
            internal GroupAxisStartType GroupStartMode;
            internal StartModeComboBoxItem(StartMode identifier, string text, GroupAxisStartType groupStart)
            {
                StartModeIdentifier = identifier;
                Text = text;
                GroupStartMode = groupStart;
            }

            public override string ToString()
            {
                return Text;
            }
        }


        internal class RawDriveOutputModeComboBoxItem
        {
            internal static RawDriveOutputModeComboBoxItem[] AllItems =
            [
                new RawDriveOutputModeComboBoxItem(DriveOutputStartType.Percent, "Percent"),
                new RawDriveOutputModeComboBoxItem(DriveOutputStartType.Velocity, "Velocity"),
            ];


            internal DriveOutputStartType OutputMode;
            internal string Text;
            internal RawDriveOutputModeComboBoxItem(DriveOutputStartType mode, string text)
            {
                OutputMode = mode;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }



        internal class SetActualPositionComboBoxItem
        {
            internal static SetActualPositionComboBoxItem[] AllItems =
            [
                new SetActualPositionComboBoxItem(ActualPositionType.AbsolutePosition, "Absolute"),
                new SetActualPositionComboBoxItem(ActualPositionType.RelativePosition, "Relative"),
            ];


            internal ActualPositionType SetMode;
            internal string Text;
            internal SetActualPositionComboBoxItem(ActualPositionType mode, string text)
            {
                SetMode = mode;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }




        internal class SetTargetPositionComboBoxItem
        {
            internal static SetTargetPositionComboBoxItem[] AllItems =
            [
                new SetTargetPositionComboBoxItem(EndPositionType.AbsolutePosition, "Absolute"),
                new SetTargetPositionComboBoxItem(EndPositionType.RelativePosition, "Relative"),
                new SetTargetPositionComboBoxItem(EndPositionType.ContinousPositionPositive, "Endless +"),
                new SetTargetPositionComboBoxItem(EndPositionType.ContinousPositionNegative, "Endless -"),
                new SetTargetPositionComboBoxItem(EndPositionType.ModuloPosition, "Modulo"),
            ];


            internal EndPositionType SetMode;
            internal string Text;
            internal SetTargetPositionComboBoxItem(EndPositionType mode, string text)
            {
                SetMode = mode;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        #endregion

        private void btnSetTargetPosition_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            if (!double.TryParse(txtBoxSetTargetPositionValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var targetPosition))
            {
                txtBoxSetTargetPositionValue.SelectAll();
                txtBoxSetTargetPositionValue.Focus();
                MessageBox.Show("Invalid target position.");
                return;
            }

            if (comboBoxSetTargetPositionType.SelectedItem is SetTargetPositionComboBoxItem item)
            {
                try
                {
                    axis.Functions.NewEndPositionAxis(item.SetMode, targetPosition);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Can not set new target position. " + exc.Message);
                }
            }
        }

        private void btnSetActualPosition_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            if (!double.TryParse(txtBoxSetActualPositionValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var actualPosition))
            {
                txtBoxSetActualPositionValue.SelectAll();
                txtBoxSetActualPositionValue.Focus();
                MessageBox.Show("Invalid new actual position.");
                return;
            }

            if (comboBoxSetActualPositionType.SelectedItem is SetActualPositionComboBoxItem item)
            {
                try
                {
                    axis.Functions.SetActualPositionOnTheFly(item.SetMode, 1, actualPosition);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Can not set new target position. " + exc.Message);
                }
            }
        }

        private void btnStartRawDriveOutput_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            if (!double.TryParse(txtBoxRawDriveOutputValue.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var wantedOutput))
            {
                txtBoxRawDriveOutputValue.SelectAll();
                txtBoxRawDriveOutputValue.Focus();
                MessageBox.Show("Invalid raw output value.");
                return;
            }

            if (comboBoxRawDriveOutputMode.SelectedItem is RawDriveOutputModeComboBoxItem item)
            {
                try
                {
                    axis.Functions.ChangeDriveOutput(item.OutputMode, wantedOutput);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Can not set raw output. " + exc.Message);
                }
            }
        }

        private void btnStopRawDriveOutput_Click(object sender, EventArgs e)
        {
            axis?.Functions.StopDriveOutput();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (axis == null)
                return;

            if (comboBoxStartMode.SelectedItem is not StartModeComboBoxItem item)
                return;

            switch (item.StartModeIdentifier)
            {
                case StartMode.Absolute:
                case StartMode.Relative:
                case StartMode.EndlessPos:
                case StartMode.EndlessNeg:
                case StartMode.Modulo:
                case StartMode.ModuloShortestWay:
                case StartMode.ModuloPosDirection:
                case StartMode.ModuloNegDirection:
                case StartMode.JogPos:
                case StartMode.JogNeg:
                case StartMode.PlusOne:
                case StartMode.PlusTenth:
                case StartMode.PlusHundredth:
                case StartMode.PlusThousandth:
                case StartMode.MinusOne:
                case StartMode.MinusTenth:
                case StartMode.MinusHundredth:
                case StartMode.MinusThousandth:
                    StartNormal(item);
                    break;
                case StartMode.ReversingSequence:
                    StartReversingSequence();
                    break;
                case StartMode.StartStopSequence:
                    StartStartStopSequence();
                    break;
                case StartMode.VeloStepSequence:
                    StartVeloStepSequence();
                    break;
                case StartMode.SinusSequenceBode:
                    StartSinusBodeSequence();
                    break;
                case StartMode.SinusOscillation:
                    StartSinusOscillationSequence();
                    break;
            }
        }

        private void StartReversingSequence()
        {
            if (!TryParseTextBox(txtBoxTargetPosition, out double targetPosition1))
                return;

            if (!TryParseTextBox(txtBoxTargetVelocity, out double targetVelocity))
                return;

            if (!TryParseTextBox(txtBoxAcceleration, out double targetPosition2))
                return;

            if (!TryParseTextBox(txtBoxDeacceleration, out double idleSeconds))
                return;

            try
            {
                axis?.Functions.StartReversingOperation(GroupAxisStartType.AbsoluteStart, targetPosition1, targetPosition2, targetVelocity, idleSeconds);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not start. " + exc.Message);
            }
        }

        private void StartStartStopSequence()
        {
            if (!TryParseTextBox(txtBoxTargetPosition, out double targetPosition1))
                return;

            if (!TryParseTextBox(txtBoxTargetVelocity, out double targetVelocity))
                return;


            if (!TryParseTextBox(txtBoxDeacceleration, out double idleSeconds))
                return;

            try
            {
                axis?.Functions.StartReversingOperation(GroupAxisStartType.RelativeStart, targetPosition1, 0.0, targetVelocity, idleSeconds);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not start. " + exc.Message);
            }
        }
        private void StartVeloStepSequence()
        {
            if (!TryParseTextBox(txtBoxTargetPosition, out double targetVelocity1))
                return;

            if (!TryParseTextBox(txtBoxTargetVelocity, out double targetVelocity2))
                return;

            if (!TryParseTextBox(txtBoxAcceleration, out double drivingSeconds))
                return;

            if (!TryParseTextBox(txtBoxDeacceleration, out double idleSeconds))
                return;

            if (!TryParseTextBox(txtBoxJerk, out double noOfCycles))
                return;

            uint noOfCyclesInt = (uint)noOfCycles;

            try
            {
                axis?.Functions.StartReversingOperationVelocityJumps(GroupAxisStartType.AbsoluteStart, targetVelocity1, targetVelocity2, drivingSeconds, idleSeconds, noOfCyclesInt);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not start. " + exc.Message);
            }
        }

        private void StartSinusBodeSequence()
        {
            if (!TryParseTextBox(txtBoxTargetPosition, out double startFrequency))
                return;

            if (!TryParseTextBox(txtBoxTargetVelocity, out double stopFrequency))
                return;

            if (!TryParseTextBox(txtBoxAcceleration, out double frequencySteps))
                return;

            if (!TryParseTextBox(txtBoxDeacceleration, out double baseVelocity))
                return;

            if (!TryParseTextBox(txtBoxJerk, out double feedConstant))
                return;

            uint frequencyStepsInt = (uint)frequencySteps;

            const double baseFrequency = 1.953125;
            const double startAmplitude = 0.0;
            const double stepDuration = 2.048;

            try
            {
                //Using the same values as TwinCAT uses for base frequency, start amplitude and step duration.
                axis?.Functions.StartSinusOscillationSequence(baseVelocity, baseFrequency, startAmplitude, feedConstant, startFrequency, stopFrequency, stepDuration, frequencyStepsInt);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not start. " + exc.Message);
            }
        }

        private void StartSinusOscillationSequence()
        {
            if (!TryParseTextBox(txtBoxTargetPosition, out double startFrequency))
                return;

            if (!TryParseTextBox(txtBoxTargetVelocity, out double velocityAmplitude))
                return;

            if (!TryParseTextBox(txtBoxDeacceleration, out double drivingTimeSeconds))
                return;

            //TwinCAT uses the same default values as here when the same function is called.
            const double baseAmplitude = 2.5;
            const double baseFrequency = 1.953125;
            const double feedConstant = 10.0;
            const double stopFrequency = 500.0;
            const uint stepCycles = 0; //Infinite

            try
            {
                axis?.Functions.StartSinusOscillationSequence(baseAmplitude, baseFrequency, velocityAmplitude, feedConstant, startFrequency, stopFrequency, drivingTimeSeconds, stepCycles);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not start. " + exc.Message);
            }
        }


        private void StartNormal(StartModeComboBoxItem item)
        {
            double targetPosition;

            //Modify target position based on selected mode.
            switch (item.StartModeIdentifier)
            {
                case StartMode.JogPos:
                    targetPosition = 5.0;
                    break;
                case StartMode.JogNeg:
                    targetPosition = -5.0;
                    break;
                case StartMode.PlusOne:
                    targetPosition = 1.0;
                    break;
                case StartMode.PlusTenth:
                    targetPosition = 0.1;
                    break;
                case StartMode.PlusHundredth:
                    targetPosition = 0.01;
                    break;
                case StartMode.PlusThousandth:
                    targetPosition = 0.001;
                    break;
                case StartMode.MinusOne:
                    targetPosition = -1.0;
                    break;
                case StartMode.MinusTenth:
                    targetPosition = -0.1;
                    break;
                case StartMode.MinusHundredth:
                    targetPosition = -0.01;
                    break;
                case StartMode.MinusThousandth:
                    targetPosition = -0.001;
                    break;
                default:
                    if (!TryParseTextBox(txtBoxTargetPosition, out targetPosition))
                        return;
                    break;
            }




            if(!TryParseTextBox(txtBoxTargetVelocity, out double requiredVelocity))
                return;

            double acceleration = 0.0;
            if (chkBoxEnableAcceleration.Checked)
            {
                if (!TryParseTextBox(txtBoxAcceleration, out acceleration))
                    return;
            }

            double deceleration = 0.0;
            if (chkBoxEnableDeacceleration.Checked)
            {
                if (!TryParseTextBox(txtBoxDeacceleration, out deceleration))
                    return;
            }

            double jerk = 0.0;
            if (chkBoxEnableJerk.Checked)
            {
                if (!TryParseTextBox(txtBoxJerk, out jerk))
                    return;
            }

            try
            {
                axis?.Functions.ExtendedAxisStart(item.GroupStartMode, targetPosition, requiredVelocity, acceleration, deceleration, jerk);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not start. " + exc.Message);
            }
        }

        private bool TryParseTextBox(TextBox textBox, out double value)
        {
            if (!double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                textBox.SelectAll();
                textBox.Focus();
                MessageBox.Show("Invalid value.");
                return false;
            }
            return true;
        }



        private void comboBoxStartMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxStartMode.SelectedItem is StartModeComboBoxItem item)
                PossiblyReArrangeUI(item.StartModeIdentifier);
        }

        private void PossiblyReArrangeUI(StartMode startModeIdentifier)
        {
            switch (startModeIdentifier)
            {
                case StartMode.Absolute:
                case StartMode.Relative:
                case StartMode.EndlessPos:
                case StartMode.EndlessNeg:
                case StartMode.Modulo:
                case StartMode.ModuloShortestWay:
                case StartMode.ModuloPosDirection:
                case StartMode.ModuloNegDirection:
                case StartMode.JogPos:
                case StartMode.JogNeg:
                case StartMode.PlusOne:
                case StartMode.PlusTenth:
                case StartMode.PlusHundredth:
                case StartMode.PlusThousandth:
                case StartMode.MinusOne:
                case StartMode.MinusTenth:
                case StartMode.MinusHundredth:
                case StartMode.MinusThousandth:
                    ReArrangeToStandardUI();
                    break;
                case StartMode.ReversingSequence:
                    ReArrangeToReversingSequenceUI();
                    break;
                case StartMode.StartStopSequence:
                    ReArrangeToStartStopUI();
                    break;
                case StartMode.VeloStepSequence:
                    ReArrangeToVeloStepSequenceUI();
                    break;
                case StartMode.SinusSequenceBode:
                    ReArrangeToSinusBodeUI();
                    break;
                case StartMode.SinusOscillation:
                    ReArrangeToSinusOscillationUI();
                    break;
            }
        }

        private void ReArrangeToReversingSequenceUI()
        {
            chkBoxEnableAcceleration.Visible = false;
            chkBoxEnableDeacceleration.Visible = false;
            chkBoxEnableJerk.Visible = false;

            label2.Text = "Target position 1:";
            label3.Text = "Target Velocity:";
            label4.Text = "Target position 2:";
            label5.Text = "Idle Time:";
            label6.Text = "";

            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = false;

            txtBoxAcceleration.Visible = true;
            txtBoxDeacceleration.Visible = true;
            txtBoxJerk.Visible = false;

            txtBoxAcceleration.Enabled = true;
            txtBoxDeacceleration.Enabled = true;
        }

        private void ReArrangeToStartStopUI()
        {
            chkBoxEnableAcceleration.Visible = false;
            chkBoxEnableDeacceleration.Visible = false;
            chkBoxEnableJerk.Visible = false;

            label2.Text = "Target position 1:";
            label3.Text = "Target Velocity:";
            label4.Text = "";
            label5.Text = "Idle Time:";
            label6.Text = "";

            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = false;
            label5.Visible = true;
            label6.Visible = false;

            txtBoxAcceleration.Visible = false;
            txtBoxDeacceleration.Visible = true;
            txtBoxJerk.Visible = false;

            txtBoxAcceleration.Enabled = false;
            txtBoxDeacceleration.Enabled = true;
            txtBoxJerk.Enabled = false;
        }

        private void ReArrangeToVeloStepSequenceUI()
        {
            chkBoxEnableAcceleration.Visible = false;
            chkBoxEnableDeacceleration.Visible = false;
            chkBoxEnableJerk.Visible = false;

            label2.Text = "Target Velocty 1:";
            label3.Text = "Target Velocity 2:";
            label4.Text = "Driving Time:";
            label5.Text = "Idle Time:";
            label6.Text = "No Of Cycles:";

            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            txtBoxAcceleration.Visible = true;
            txtBoxDeacceleration.Visible = true;
            txtBoxJerk.Visible = true;

            txtBoxAcceleration.Enabled = true;
            txtBoxDeacceleration.Enabled = true;
            txtBoxJerk.Enabled = true;
        }

        private void ReArrangeToSinusBodeUI()
        {
            chkBoxEnableAcceleration.Visible = false;
            chkBoxEnableDeacceleration.Visible = false;
            chkBoxEnableJerk.Visible = false;

            label2.Text = "Start Frequency:";
            label3.Text = "Stop Frequency:";
            label4.Text = "Frequency Steps:";
            label5.Text = "Sinus Base Velocity:";
            label6.Text = "Feed Constant Motor:";

            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            txtBoxAcceleration.Visible = true;
            txtBoxDeacceleration.Visible = true;
            txtBoxJerk.Visible = true;

            txtBoxAcceleration.Enabled = true;
            txtBoxDeacceleration.Enabled = true;
            txtBoxJerk.Enabled = true;
        }

        private void ReArrangeToSinusOscillationUI()
        {
            chkBoxEnableAcceleration.Visible = false;
            chkBoxEnableDeacceleration.Visible = false;
            chkBoxEnableJerk.Visible = false;

            label2.Text = "Frequency:";
            label3.Text = "Sinus Velocity Amplitude:";
            label4.Text = "";
            label5.Text = "Driving time:";
            label6.Text = "";

            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = false;
            label5.Visible = true;
            label6.Visible = false;

            txtBoxAcceleration.Visible = false;
            txtBoxDeacceleration.Visible = true;
            txtBoxJerk.Visible = false;

            txtBoxAcceleration.Enabled = false;
            txtBoxDeacceleration.Enabled = true;
            txtBoxJerk.Enabled = false;
        }

        private void ReArrangeToStandardUI()
        {
            chkBoxEnableAcceleration.Visible = true;
            chkBoxEnableDeacceleration.Visible = true;
            chkBoxEnableJerk.Visible = true;

            label2.Text = "Target Position:";
            label3.Text = "Target Velocity:";


            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            txtBoxAcceleration.Visible = true;
            txtBoxDeacceleration.Visible = true;
            txtBoxJerk.Visible = true;

            txtBoxAcceleration.Enabled = chkBoxEnableAcceleration.Checked;
            txtBoxDeacceleration.Enabled = chkBoxEnableDeacceleration.Checked;
            txtBoxJerk.Enabled = chkBoxEnableJerk.Checked;
        }
    }
}
