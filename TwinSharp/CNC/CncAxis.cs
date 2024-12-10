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


        internal CncAxis(uint number, AdsClient plcClient, AdsClient comClient)
        {
            this.Number = number;
            
            Status = new AxisStatus(number, comClient);
            ExternalAxisCommanding = new ExternalAxisCommanding(number, plcClient);
            DynamicPositionLimitation = new DynamicPositionLimitation(number, plcClient);
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
        /// Returns a string representation of the axis.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Axis {Number}";
        }
    }
}