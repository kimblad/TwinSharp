using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// Represents the operating system running on the IPC.
    /// Provides properties to retrieve OS version information, build number, CSD version, and system uptime.
    /// </summary>
    public class IpcOperatingSystem
    {
        internal const ushort ModuleType = 0x0018;

        readonly AdsClient client;
        readonly uint subIndexTable1;
        readonly uint subIndexTable2;

        internal IpcOperatingSystem(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
            subIndexTable2 = (uint)(mdpId << 20) | 0x80020000; //Table 0x8nn2, just add the desired subIndex later.
        }

        /// <summary>
        /// OS Major Version
        /// </summary>
        public uint MajorVersion
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 0x01);
        }

        /// <summary>
        /// OS Minor Version
        /// </summary>
        public uint MinorVersion
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 0x02);
        }

        /// <summary>
        /// OS Build Number
        /// </summary>
        public uint BuildNumber
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 0x03);
        }

        /// <summary>
        /// OS CSD Version
        /// </summary>
        public string CSDVersion
        {
            //TODO: This throws an exception. Find out why.
            get => ""; // client.ReadString(0xF302, subIndexTable1 + 0x04, 16);
        }

        /// <summary>
        /// Uptime in seconds
        /// </summary>
        public ulong UpTimeSeconds
        {
            get => client.ReadAny<ulong>(0xF302, subIndexTable2 + 0x01);
        }
    }
}
