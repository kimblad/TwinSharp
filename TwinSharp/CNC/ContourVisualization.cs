using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The controller can supply the axis positions for the graphic display of machine movements and visualise them by means of a user program or in the graphic user interface. 
    /// </summary>
    public class ContourVisualization
    {
        readonly AdsClient comClient;
        readonly uint group;
        internal ContourVisualization(AdsClient comClient, int channelID)
        {
            this.comClient = comClient;
            group = 0x120100 + (uint)channelID;
        }

        /// <summary>
        /// Select nominal contour visualisation
        /// 0x0000 ISG_STANDARD Normal mode
        /// 0x0002 SOLLKON Nominal contour visualisation
        /// 0x0004 ON_LINE Online-Visu
        /// 0x0008 SYNCHK Syntax check
        /// </summary>
        public ChannelMode ExecutionMode
        {
            get => (ChannelMode)comClient.ReadAny<uint>(group, 0x40);
            set => comClient.WriteAny(group, 0x3F, (uint)value);
        }

        /// <summary>
        /// Output grid for nominal contour visualisation for linear blocks(G00/G01) in [0.1 µm]
        /// </summary>
        public uint OutputGridSize
        {
            get => comClient.ReadAny<uint>(group, 0x89);
            set => comClient.WriteAny(group, 0x8A, value);
        }

        /// <summary>
        /// Maximum relative path error in [0.1%] for nominal contour visualisation of circles or polynomials
        /// </summary>
        public double MaxRelativePathError
        {
            set => comClient.WriteAny(group, 0x8B, value);
        }

        /// <summary>
        /// Maximum absolute path error in [0.1 µm] for nominal contour visualisation of circles and polynomials
        /// </summary>
        public double MaxAbsolutePathError
        {
            set => comClient.WriteAny(group, 0x8C, value);
        }

        /// <summary>
        /// Data record from channel-specific output buffer (FIFO).
        /// </summary>
        public byte[] DataRecordChannelFIFO
        {
            get => comClient.ReadAny<byte[]>(group, 0x2000);
        }

        /// <summary>
        /// Number of data records in the channel-specific output FIFO
        /// </summary>
        public uint DataRecordCountChannelFIFO
        {
            get => comClient.ReadAny<uint>(group, 0x2001);
        }

        /// <summary>
        /// Get data record from global output FIFO.
        /// </summary>
        /// <param name="bufferToFill"></param>
        /// <returns></returns>
        public int GetDataRecordFromGlobalFIFO(Memory<byte> bufferToFill)
        {
            return comClient.Read(group, 0x2002, bufferToFill);
        }

        /// <summary>
        /// Number of data records in the global output FIFO
        /// </summary>
        public uint DataRecordCountGlobalFIFO
        {
            get => comClient.ReadAny<uint>(group, 0x2003);
        }
    }
}