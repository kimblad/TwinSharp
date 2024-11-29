using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IpcCpu class provides methods to interact with the CPU of a device via ADS (Automation Device Specification).
    /// It allows reading the CPU frequency, current CPU usage percentage, and current CPU temperature in Celsius.
    /// </summary>
    public class IpcCpu
    {
        internal const ushort ModuleType = 0x000B;

        readonly AdsClient client;
        readonly uint subIndex;

        internal IpcCpu(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        /// <summary>
        /// CPU frequency (constant)
        /// </summary>
        public uint Frequency
        {
            get => client.ReadAny<uint>(0xF302, subIndex + 01);
        }

        /// <summary>
        /// Current CPU Usage (%)
        /// </summary>
        public ushort UsagePercent
        {
            get => client.ReadAny<ushort>(0xF302, subIndex + 02);
        }

        /// <summary>
        /// Current CPU Temperature (°C). Requires BIOS API.
        /// </summary>
        public short TemperatureCelsius
        {
            get => client.ReadAny<short>(0xF302, subIndex + 03);
        }
    }
}
