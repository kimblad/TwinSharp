using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    public class IpcUPS
    {
        public const ushort ModuleType = 0x001E;

        readonly AdsClient client;
        readonly uint subIndex;

        internal IpcUPS(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        public string UPSModel => client.ReadString(0xF302, subIndex + 01, 80);
        public string VendorName => client.ReadString(0xF302, subIndex + 02, 80);
        public byte Version => client.ReadAny<byte>(0xF302, subIndex + 03);
        public byte Revision => client.ReadAny<byte>(0xF302, subIndex + 04);
        public ushort Build => client.ReadAny<ushort>(0xF302, subIndex + 05);
        public string SerialNumber => client.ReadString(0xF302, subIndex + 06, 80);
        public byte PowerStatus => client.ReadAny<byte>(0xF302, subIndex + 07);
        public byte CommunicationStatus => client.ReadAny<byte>(0xF302, subIndex + 08);
        public byte BatteryStatus => client.ReadAny<byte>(0xF302, subIndex + 09);
        public byte BatteryCapacityPercent => client.ReadAny<byte>(0xF302, subIndex + 10);
        public uint BatteryRuntimeSeconds => client.ReadAny<uint>(0xF302, subIndex + 11);
        public bool PersistentPowerFailCount => client.ReadAny<bool>(0xF302, subIndex + 12);
        public uint PowerFailCounter => client.ReadAny<uint>(0xF302, subIndex + 13);
        public bool FanError => client.ReadAny<bool>(0xF302, subIndex + 14);
        public bool NoBattery => client.ReadAny<bool>(0xF302, subIndex + 15);
        public string BatteryReplaceDate
        {
            get
            {
                try
                {
                    return client.ReadString(0xF302, subIndex + 16, 80);
                }
                catch (AdsErrorException adsExc)
                {
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                        return "Not supported";

                    throw;
                }
            }
        }
        public bool IntervalServiceStatus => client.ReadAny<bool>(0xF302, subIndex + 17);
    }
}
