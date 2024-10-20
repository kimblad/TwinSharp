using TwinCAT.Ads;

namespace TwinSharp
{
    public class Encoder
    {
        public readonly EncoderFunctions Functions;
        public readonly EncoderParameters Parameters;
        public readonly EncoderState State;

        internal Encoder(AdsClient client, uint id)
        {
            Functions = new EncoderFunctions(client, id);
            Parameters = new EncoderParameters(client, id);
            State = new EncoderState(client, id);
        }
    }
}
