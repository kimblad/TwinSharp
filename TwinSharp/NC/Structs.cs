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
        public double ActualPosition;
        public double ActualModuloPosition;
        public double SetPosition;
        public double SetModuloPosition;
        public double ActualVelocity;
        public double SetVelocity;
        public int VelocityOverride;
        public double FollowingErrorPosition;
        public double FollowingErrorPeakMinimum;
        public double FollowingErrorPeakMaximum;
        public double ControllerOutputPercent;
        public double TotalOutputPercent;
        public int AxisStatusDWord;
        public int AxisControlDWord;
        public int SlaveCouplingState;
        public int ControlLoopIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 144)]
        public byte[] FillUp;
    }
}
