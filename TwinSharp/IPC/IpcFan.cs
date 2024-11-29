using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// Each fan for which information is available is represented by a dedicated MDP module instance (not all devices support this).
    /// </summary>
    public class IpcFan
    {
        internal const ushort ModuleType = 0x001B;

        readonly AdsClient client;
        readonly uint subIndex;

        internal IpcFan(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        /// <summary>
        /// Fan speed (rpm)
        /// </summary>
        public short FanSpeedRPM
        {
            get => client.ReadAny<short>(0xF302, subIndex + 01);
        }
    }
}