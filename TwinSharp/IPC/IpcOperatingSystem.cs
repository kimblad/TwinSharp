using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    public class IpcOperatingSystem
    {
        public const ushort ModuleType = 0x0018;

        AdsClient client;
        readonly uint subIndexTable1;
        readonly uint subIndexTable2;

        internal IpcOperatingSystem(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
            subIndexTable2 = (uint)(mdpId << 20) | 0x80020000; //Table 0x8nn2, just add the desired subIndex later.
        }

        public uint MajorVersion
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 0x01);
        }

        public uint MinorVersion
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 0x02);
        }

        public uint BuildNumber
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 0x03);
        }

        public string CSDVersion
        {
            //ToDo: This throws an exception. Find out why.
            get => ""; // client.ReadString(0xF302, subIndexTable1 + 0x04, 16);
        }

        public ulong UpTimeSeconds
        {
            get => client.ReadAny<ulong>(0xF302, subIndexTable2 + 0x01);
        }
    }
}
