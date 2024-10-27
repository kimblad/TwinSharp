using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    public class IpcCpu
    {
        public const ushort ModuleType = 0x000B;

        AdsClient client;
        readonly uint subIndex;

        internal IpcCpu(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        public uint Frequency
        {
            get => client.ReadAny<uint>(0xF302, subIndex + 01);
        }

        public ushort UsagePercent
        {
            get => client.ReadAny<ushort>(0xF302, subIndex + 02);
        }

        /// <summary>
        /// Requires BIOS API
        /// </summary>
        public short TemperatureCelsius
        {
            get => client.ReadAny<short>(0xF302, subIndex + 03);
        }
    }
}
