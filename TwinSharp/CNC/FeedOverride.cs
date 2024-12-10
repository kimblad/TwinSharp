using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Provides properties to read and control the feed override of a channel.
    /// </summary>
    public class FeedOverride
    {

        readonly AdsClient comClient;
        readonly uint indexGroup;
        internal FeedOverride(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;
            indexGroup = 0x120100 + (uint)channelNumber;

            Adresses = new FeedOverrideAdresses(indexGroup);
        }

        /// <summary>
        /// Gets or sets the commanded feed override.
        /// </summary>
        public ushort CommandedFeedOverride
        {
            set => comClient.WriteAny(indexGroup, 0x09, value);
            get => comClient.ReadAny<ushort>(indexGroup, 0x0A);
        }

        /// <summary>
        /// Gets the actual feed override.
        /// </summary>
        public ushort ActualFeedOverride
        {
            get => comClient.ReadAny<ushort>(indexGroup, Adresses.ActualFeedOverride);
        }

        /// <summary>
        /// Provides the adresses of the feed override variables. So users can them to for example add ADS device notifications.
        /// </summary>
        public FeedOverrideAdresses Adresses
        {
            get;
            private set;
        }
    }


    /// <summary>
    /// Contains the adresses of the feed override variables. Can be used to add device notifications, sum read commands etc.
    /// </summary>
    public class FeedOverrideAdresses
    {
        readonly uint indexGroup;
        internal FeedOverrideAdresses(uint indexGroup)
        {
            this.indexGroup = indexGroup;
        }

        /// <summary> Index group for all properties. </summary>
        public uint IndexGroup => indexGroup;

        /// <summary> Actual feed override. </summary>
        public uint ActualFeedOverride => 0x0C;
    }
}