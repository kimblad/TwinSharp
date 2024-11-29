using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class Channel
    {
        public readonly ChannelParameters Parameters;
        public readonly ChannelState State;
        public readonly ChannelFunctions Functions;
        public readonly ChannelCyclicProcessData CyclicProcessData;


        public Channel(AdsClient client, uint id)
        {
            Parameters = new ChannelParameters(client, id);
            State = new ChannelState(client, id);
            Functions = new ChannelFunctions(client, id);
            CyclicProcessData = new ChannelCyclicProcessData(client, id);
        }
    }

}