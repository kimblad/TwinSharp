using System.Runtime.InteropServices;

namespace TwinSharp.NC
{

    /// <summary>
    /// AXIS ONLINE STRUCTURE (NC/CNC)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct NCAXISSTATE_ONLINESTRUCT
    {
        /// <summary> Axis error state. </summary>
        public int ErrorState;

        /// <summary> Reserved for future use. </summary>
        int Reserved1;
        
        /// <summary> Axis Actual position </summary>
        public double ActualPosition;
        
        /// <summary> Axis actual modulo position </summary>
        public double ActualModuloPosition;
        
        /// <summary> Axis set position. </summary>
        public double SetPosition;
        
        /// <summary> Axis modulo set position. </summary>
        public double SetModuloPosition;

        /// <summary> Optional: Actual velocity. e.g. mm/s </summary>
        public double ActualVelocity;

        /// <summary> Set velocity. e.g. mm/s </summary>
        public double SetVelocity;

        /// <summary> Velocity override (1000000 == 100%) </summary>
        public int VelocityOverride;
        
        /// <summary> Reserved for future use. </summary>
        int Reserved2;

        /// <summary> Lag error position. e.g. mm </summary>
        public double FollowingErrorPosition;

        /// <summary> PeakHold value for max. neg. position lag (pos.) e.g. mm </summary>
        public double FollowingErrorPeakMinimum;

        /// <summary> Peak hold value for max. pos. position lag (pos.) e.g. mm </summary>
        public double FollowingErrorPeakMaximum;

        /// <summary> Controller output in percent </summary>
        public double ControllerOutputPercent;

        /// <summary> Total output in percent </summary>
        public double TotalOutputPercent;

        /// <summary> Axis state double word </summary>
        public StateDWordFlags AxisStatusDWord;

        /// <summary> Axis control double word </summary>
        public NCTOPLC_AXIS_REF_OPMODE AxisControlDWord;

        /// <summary> Slave coupling state (state) </summary>
        public CoupleState SlaveCouplingState;

        /// <summary> Axis control loop index </summary>
        public int ControlLoopIndex;

        /// <summary> Actual acceleration. e.g. mm/s^2 </summary>
        public double ActualAcceleration;

        /// <summary> Set acceleration. e.g. mm/s^2 </summary>
        public double SetAcceleration;

        /// <summary> Set jerk (new from TwinCAT 3.1 B4013). e.g. mm/s^3 </summary>
        public double SetJerk;

        /// <summary> Set torque or set force. Symbol "SetTorque". e.g. 100% = 1000 </summary>
        public double SetTorque;

        /// <summary> Actual torque or actual force (new from TwinCAT 3.1 B4013). e.g. 100% = 1000 </summary>
        public double ActualTorque;

        /// <summary> Set torque change or set force change (time derivative of the set torque or set force) (from TwinCAT 3.1 B4024.2). e.g. %/s </summary>
        public double SetTorqueChange;

        /// <summary> Additive set torque or additive set force ("TorqueOffset") (from TwinCAT 3.1 B4024.2). e.g. 100% = 1000 </summary>
        public double TorqueOffset;

        /// <summary> Reserved for future use. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] FillUp;
    }
}
