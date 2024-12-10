using TwinCAT.Ads;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The operator can start machining at what is called the continuation position at any point in the program. After a program is interrupted (e.g. tool breakage), this is a quick method to reactivate machining at the point of interruption.
    /// The continuation position can be defined using a number of different block search types(file offset, block counter, block number, etc.). 
    /// </summary>
    public class BlockSearch
    {
        readonly AdsClient comClient;
        readonly uint indexGroup;

        internal BlockSearch(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;
            indexGroup =  0x120100 + (uint)channelNumber;
        }

        /// <summary>
        /// The continuation position can be defined using a number of different block search types (file offset, block counter, block number, etc.)
        /// </summary>
        public BlockSearchType BlockSearchType
        {
            get => (BlockSearchType)comClient.ReadAny<ushort>(indexGroup, 0x61);
            set => comClient.WriteAny(indexGroup, 0x49, (ushort)value);
        }

        /// <summary>
        /// Defines the block number at which point actual machining is to continue.
        /// </summary>
        public uint BlockNumberToFind
        {
            get => comClient.ReadAny<uint>(indexGroup, 0x77);
            set => comClient.WriteAny(indexGroup, 0x5F, value);
        }

        /// <summary>
        /// This object defines that resumption of motion on the contour occurs automatically.
        /// </summary>
        public bool AutomaticResumption
        {
            get => comClient.ReadAny<bool>(indexGroup, 0x63);
            set => comClient.WriteAny(indexGroup, 0x4B, value);
        }

        /// <summary>
        /// This object defines whether resumption of motion on the contour should occur directly without any operator input.
        /// </summary>
        public bool NoStopOnResumption
        {
            get => comClient.ReadAny<bool>(indexGroup, 0x7A);
            set => comClient.WriteAny(indexGroup, 0x79, value);
        }


        /// <summary>
        /// This object defines the maximum deviation of the axes between actual position and continuation position when machining is resumed after block search. If resumption of motion on the contour is automatic, the maximum path deviation is not considered since the exact continuation position has already been reached. (Default= 0)
        /// Unit [0.1 µm] 
        /// </summary>
        public uint MaxPathDeviation
        {
            get => comClient.ReadAny<uint>(indexGroup, 0x64);
            set => comClient.WriteAny(indexGroup, 0x4C, value);
        }

        /// <summary>
        /// The file offset defines a jump to a known position in the NC program. The program part before the jump point is not evaluated. Processing starts at the jump point as for a program shortened by file offset. This object defines the file offset. (Default value= 0)
        /// </summary>
        public int ProgramStartAsOfFileOffset
        {
            set => comClient.WriteAny(indexGroup, 0x11, value);
        }

        /// <summary>
        /// This object defines an automatic breakpoint by specifying the distance from program start.
        /// Unit [0.1 µm] 
        /// </summary>
        public double BreakPoint
        {
            get => comClient.ReadAny<double>(indexGroup, 0x7C);
            set => comClient.WriteAny(indexGroup, 0x7B, value);
        }

        /// <summary>
        /// This object defines the distance from program start or #DISTANCE PROG START CLEAR at which machining is actually supposed to start.
        /// Unit [0.1 µm] 
        /// </summary>
        public double CoveredDistanceFromProgramStart
        {
            get => comClient.ReadAny<double>(indexGroup, 0x45);
            set => comClient.WriteAny(indexGroup, 0x44, value);
        }

        /// <summary>
        /// This object defines the distance covered in the NC block in per mil at which machining is actually supposed to continue. The first part of the block in the block search is then executed without motion and only the remaining part is executed with moved axes. Value range: [0.0 to 1000.0]; default value= 0.0
        /// Unit [0.1%] 
        /// </summary>
        public double CoveredDistanceMotionBlockInPerMill
        {
            get => comClient.ReadAny<double>(indexGroup, 0x62);
            set => comClient.WriteAny(indexGroup, 0x4A, value);
        }
    }

    /// <summary>
    /// The block search type defines the method used to define the continuation position in the NC program.
    /// </summary>
    public enum BlockSearchType
    {
        /// <summary> No block search. Normal operation. </summary>
        NoBlockSearch = 0,

        /// <summary> Continuation position and end position by file offset. </summary>
        FileOffset = 1,

        /// <summary> Continuation position by block counter. </summary>
        BlockCounter = 3,

        /// <summary> Continuation position by block number and program name. </summary>
        BlockNumber = 4,

        /// <summary> 
        /// Continuation position at program end 
        /// This special block search type is used in particular in job planning on a simulation system for a rapid test of NC programs. 
        /// </summary>
        ProgramEnd = 5
    }
}