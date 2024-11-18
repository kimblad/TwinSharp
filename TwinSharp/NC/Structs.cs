using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TwinSharp.NC
{

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct NCAXISSTATE_ONLINESTRUCT
    {
        public int ErrorState;
        int Reserved1;
        public double ActualPosition;
        public double ActualModuloPosition;
        public double SetPosition;
        public double SetModuloPosition;
        public double ActualVelocity;
        public double SetVelocity;
        public int VelocityOverride;
        int Reserved2;
        public double FollowingErrorPosition;
        public double FollowingErrorPeakMinimum;
        public double FollowingErrorPeakMaximum;
        public double ControllerOutputPercent;
        public double TotalOutputPercent;
        public StateDWordFlags AxisStatusDWord;
        public ControlDWordFlags AxisControlDWord;
        public CoupleState SlaveCouplingState;
        public int ControlLoopIndex;
        public double ActualAcceleration;
        public double SetAcceleration;
        public double SetJerk;
        public double SetTorque;
        public double ActualTorque;
        public double SetTorqueChange;
        public double TorqueOffset;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] FillUp;
    }
}
