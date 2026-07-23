using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Represents a CNC axis and provides access to its status, external commanding, and dynamic position limitation.
    /// This class allows interaction with the axis to read its state, command movements, and set position limits.
    /// </summary>
    public class CncAxis
    {
        readonly uint Number;


        readonly AdsClient plcClient;

        internal CncAxis(uint number, AdsClient plcClient, AdsClient comClient)
        {
            this.Number = number;
            this.plcClient = plcClient;

            Status = new AxisStatus(number, comClient);
            ExternalAxisCommanding = new ExternalAxisCommanding(number, plcClient);
            DynamicPositionLimitation = new DynamicPositionLimitation(number, plcClient);
            DistanceControl = new DistanceControl(number, plcClient);
            AxisCoupling    = new AxisCoupling(number, plcClient);
            ControlCommands = new AxisControlCommands(number, plcClient);
        }

        /// <summary>
        /// Registers the PLC as present on this axis. Must be set to TRUE before the CNC
        /// will accept HLI commands for the axis. Writes to gpAx[n]^.head.plc_present_w.
        /// </summary>
        public bool PlcPresent
        {
            set
            {
                uint handle = plcClient.CreateVariableHandle(
                    $"HLI_Global_Variables.gpAx[{Number - 1}]^.head.plc_present_w");
                plcClient.WriteAny(handle, value);
                plcClient.DeleteVariableHandle(handle);
            }
        }

        /// <summary>
        /// Gets the AxisStatus instance which provides access to various properties and methods to read and manipulate the state of the axis,
        /// including position, velocity, acceleration, torque, and error codes.
        /// </summary>
        public AxisStatus Status { get; private set; }

        /// <summary>
        /// Gets the ExternalAxisCommanding instance which allows specifying additive velocity or position command values.
        /// </summary>
        public ExternalAxisCommanding ExternalAxisCommanding { get; private set; }

        /// <summary>
        /// Gets the DynamicPositionLimitation instance which allows setting and monitoring dynamic position limits for the axis.
        /// </summary>
        public DynamicPositionLimitation DynamicPositionLimitation { get; private set; }

        /// <summary>
        /// Gets the DistanceControl instance for TF5200 height/distance control on this axis.
        /// Allows enabling, commanding (activate/freeze/deactivate), and monitoring the distance control HLI interface.
        /// </summary>
        public DistanceControl DistanceControl { get; private set; }

        /// <summary>
        /// Gets the AxisCoupling instance for HLI axis coupling (electronic gearing) on this axis.
        /// Allows activating and deactivating coupling to one or more source axes.
        /// </summary>
        public AxisCoupling AxisCoupling { get; private set; }

        /// <summary>
        /// Gets the AxisControlCommands instance for the axis control commands of the HLI lr_mc_control
        /// control unit (drive on, controller enable, feed enable, referencing, probing, park, ...).
        /// </summary>
        public AxisControlCommands ControlCommands { get; private set; }






        /// <summary>
        /// Returns a string representation of the axis.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Axis {Number}";
        }
    }
}