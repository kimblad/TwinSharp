using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents a channel in the TwinCAT NC system.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="client">The ADS client connected to the target.</param>
        /// <param name="id">The channel ID.</param>
        public Channel(AdsClient client, uint id)
        {
            Parameters = new ChannelParameters(client, id);
            State = new ChannelState(client, id);
            Functions = new ChannelFunctions(client, id);
            CyclicProcessData = new ChannelCyclicProcessData(client, id);
        }

        /// <summary>
        /// Gets the channel parameters.
        /// </summary>
        public ChannelParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the channel state.
        /// </summary>
        public ChannelState State { get; private set; }

        /// <summary>
        /// Gets the channel functions.
        /// </summary>
        public ChannelFunctions Functions { get; private set; }

        /// <summary>
        /// Gets the cyclic process data of the channel.
        /// </summary>
        public ChannelCyclicProcessData CyclicProcessData { get; private set; }
    }

}
