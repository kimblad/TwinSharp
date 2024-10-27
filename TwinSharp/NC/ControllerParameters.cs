using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class ControllerParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal ControllerParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x6000 + id;
        }

        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

        public ControllerType Type
        {
            get => (ControllerType)client.ReadAny<uint>(indexGroup, 0x03);
        }


    }
}
