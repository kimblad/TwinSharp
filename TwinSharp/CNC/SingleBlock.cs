using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class SingleBlock
    {
        readonly AdsClient comClient;
        readonly uint indexGroup;
        public SingleBlock(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;

            indexGroup = 0x120100 + (uint)channelNumber;
        }

        public bool Enabled
        {
            get
            {
                return comClient.ReadAny<bool>(indexGroup, 0x3);
            }
            set
            {
                comClient.WriteAny(indexGroup, 0x1, value);
            }
        }


    }
}