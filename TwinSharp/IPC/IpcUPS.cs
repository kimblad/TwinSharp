using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IpcUps class provides an interface to interact with an Uninterruptible Power Supply (UPS) device
    /// using the TwinCAT ADS protocol. It allows reading various properties of the UPS such as model, vendor name,
    /// version, revision, build, serial number, power status, communication status, battery status, battery capacity,
    /// battery runtime, persistent power fail count, power fail counter, fan error, no battery status, battery replace date,
    /// and interval service status. The class uses an AdsClient to communicate with the UPS device.
    /// </summary>
    public class IpcUps
    {
        internal const ushort ModuleType = 0x001E;

        readonly AdsClient client;
        readonly uint subIndex;

        internal IpcUps(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        /// <summary>
        /// UPS Model.
        /// </summary>
        public string UPSModel => client.ReadString(0xF302, subIndex + 01, 80);

        /// <summary>
        /// Vendor name.
        /// </summary>
        public string VendorName => client.ReadString(0xF302, subIndex + 02, 80);

        /// <summary>
        /// Version.
        /// </summary>
        public byte Version => client.ReadAny<byte>(0xF302, subIndex + 03);

        /// <summary>
        /// Revision.
        /// </summary>
        public byte Revision => client.ReadAny<byte>(0xF302, subIndex + 04);

        /// <summary>
        /// Build.
        /// </summary>
        public ushort Build => client.ReadAny<ushort>(0xF302, subIndex + 05);

        /// <summary>
        /// Serial number.
        /// </summary>
        public string SerialNumber => client.ReadString(0xF302, subIndex + 06, 80);

        /// <summary>
        /// Power status.
        /// </summary>
        public byte PowerStatus => client.ReadAny<byte>(0xF302, subIndex + 07);

        /// <summary>
        /// Communication status.
        /// </summary>
        public byte CommunicationStatus => client.ReadAny<byte>(0xF302, subIndex + 08);

        /// <summary>
        /// Battery status.
        /// </summary>
        public byte BatteryStatus => client.ReadAny<byte>(0xF302, subIndex + 09);

        /// <summary>
        /// Battery capacity in percent.
        /// </summary>
        public byte BatteryCapacityPercent => client.ReadAny<byte>(0xF302, subIndex + 10);

        /// <summary>
        /// Battery runtime in seconds.
        /// </summary>
        public uint BatteryRuntimeSeconds => client.ReadAny<uint>(0xF302, subIndex + 11);

        /// <summary>
        /// Persistent Power Fail Count
        /// </summary>
        public bool PersistentPowerFailCount => client.ReadAny<bool>(0xF302, subIndex + 12);

        /// <summary>
        /// Power fail counter.
        /// </summary>
        public uint PowerFailCounter => client.ReadAny<uint>(0xF302, subIndex + 13);

        /// <summary>
        /// Fan error.
        /// </summary>
        public bool FanError => client.ReadAny<bool>(0xF302, subIndex + 14);

        /// <summary>
        /// No battery.
        /// </summary>
        public bool NoBattery => client.ReadAny<bool>(0xF302, subIndex + 15);

        /// <summary>
        /// Date of the last battery change.
        /// </summary>
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

        /// <summary>
        /// Interval Service Status indicates whether the configured service interval has elapsed.
        /// </summary>
        public bool IntervalServiceStatus => client.ReadAny<bool>(0xF302, subIndex + 17);
    }
}
