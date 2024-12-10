using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// With the incremental specification of motion commands (streaming), the CAD/CAM system or the PLC stipulates the next path segment to be travelled (or even several segments).
    /// In this way, motion information not previously specified can still be modified until shortly before entering the command.
    /// </summary>
    public class DataStreaming
    {
        readonly AdsClient comClient;
        readonly uint indexGroup;
        readonly uint indexOffset;
        internal DataStreaming(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;

            indexGroup = 0x120100 + (uint)channelNumber;
            indexOffset = 0x90;
        }


        /// <summary>
        /// This COM interface object can write the data stream with incremental NC commands.
        /// One complete NC line must always be written.Several NC lines may also be written jointly in one write access.
        /// Each NC line must be terminated by a carriage return (ASCII value = 13) and line feed(ASCII value = 10).
        /// </summary>
        /// <param name="ncLines"></param>
        public void Write(string ncLines)
        {
            comClient.WriteAny(indexGroup, indexOffset, ncLines);
        }


    }
}