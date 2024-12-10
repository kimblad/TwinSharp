using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The ChannelCyclicProcessData class provides methods to interact with the cyclic process data of a channel in a TwinCAT system.
    /// It allows reading and writing of speed override values for both the channel axis and the spindle.
    /// </summary>
    public class ChannelCyclicProcessData
    {
        private readonly AdsClient client;
        private readonly uint indexGroup;

        internal ChannelCyclicProcessData(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x2300 + id;
        }

        /// <summary>
        /// Speed override channel (Axis in the Channel).
        /// 1000000 = 100%
        /// </summary>
        public uint SpeedOverrideChannel
        {
            get => client.ReadAny<uint>(indexGroup, 0x02);
            set => client.WriteAny(indexGroup, 0x02, value);
        }

        /// <summary>
        /// Speed override spindle.
        /// 1000000 = 100%
        /// </summary>
        public uint SpeedOverrideSpindle
        {
            get => client.ReadAny<uint>(indexGroup, 0x03);
            set => client.WriteAny(indexGroup, 0x03, value);
        }
    }
}