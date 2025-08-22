using TwinSharp.NC;
using static TwinSharpControls.HelperFunctions;

namespace TwinSharpControls
{
    public partial class NcAxisHeader : UserControl
    {
        public NcAxisHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called from timer from controls that use this user control.
        /// </summary>
        public void UpdateHeader(NCAXISSTATE_ONLINESTRUCT data)
        {
            txtBoxActualPosition.Text = FormatDouble(data.ActualPosition, 4);
            txtBoxSetPointPosition.Text = FormatDouble(data.SetPosition);

            UpdateBarberPole(data);
        }

        private void UpdateBarberPole(NCAXISSTATE_ONLINESTRUCT data)
        {
            //Barber pole can move at different speeds.
            //Calculate a speed 1-100 based on the controller output.
            //If axis is not moving, speed will be set to 0.

            //percent is 0.0 to 1.0
            double percent = data.TotalOutputPercent;
            percent *= 100.0;

            //Use absolute portion of the output.
            //Change it depedning on the direction of the axis.

            percent = Math.Abs(percent);
            double speed = Math.Clamp(percent, 10.0, 100.0);


            if (data.AxisStatusDWord.HasFlag(StateDWordFlags.PositiveDirection))
                barberPole1.Speed = speed;
            else if (data.AxisStatusDWord.HasFlag(StateDWordFlags.NegativeDirection))
                barberPole1.Speed = -speed;
            else
                barberPole1.Speed = 0.0;


            barberPole1.UpdatePole();
        }
    }
}
