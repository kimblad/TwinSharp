using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IpcDisplayDevice class provides an interface to interact with a display device
    /// connected via TwinCAT ADS. It allows reading and writing various properties of the
    /// display such as active display mode, display mode description, primary display status,
    /// COM port, version, brightness, and light enabled status. It also provides a method to
    /// save the brightness setting persistently across power cycles.
    /// </summary>
    public class IpcDisplayDevice
    {
        internal const ushort ModuleType = 0x0013;

        readonly AdsClient client;
        readonly uint subIndexTable1;
        readonly uint subIndexTable2;
        readonly uint subIndexTable3;
        readonly uint subIndexTable4;
        readonly uint subIndexTable5;

        internal IpcDisplayDevice(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
            subIndexTable2 = (uint)(mdpId << 20) | 0x80020000; //Table 0x8nn2, just add the desired subIndex later.
            subIndexTable3 = (uint)(mdpId << 20) | 0x80030000; //Table 0x8nn3, just add the desired subIndex later.
            subIndexTable4 = (uint)(mdpId << 20) | 0xB0000000; //Table 0xBnn0, just add the desired subIndex later.
            subIndexTable5 = (uint)(mdpId << 20) | 0xB0010000; //Table 0xBnn1, just add the desired subIndex later.
        }

        /// <summary>
        /// ID of active display mode
        /// </summary>
        public byte ActiveDisplayModeID
        {
            get => client.ReadAny<byte>(0xF302, subIndexTable1 + 01);
            set => client.WriteAny(0xF302, subIndexTable1 + 01, value);
        }

        /// <summary>
        /// Description of active display mode
        /// </summary>
        public string DisplayModeDescription
        {
            get
            {
                ushort length = client.ReadAny<ushort>(0xF302, subIndexTable2 + 0);
                return client.ReadString(0xF302, subIndexTable2 + 01, length);
            }
        }

        /// <summary>
        /// True if this display is the primary display
        /// </summary>
        public bool IsPrimaryDisplay
        {
            get => client.ReadAny<bool>(0xF302, subIndexTable3 + 01);
        }

        /// <summary>
        /// Windows Embedded Standard (WES): e.g. "Com4"
        /// Windows CE: under Windows CE, the Com Port must end with a colon, e.g. "COM4:"
        /// </summary>
        public string ComPort
        {
            get
            {
                //Identification of the com port(s) is started by setting any value to this other subIndex.If the call is successful, the field 0x8nn3 subindex 2 is set.If the call fails, an error code is issued.
                client.WriteAny(0xF302, subIndexTable4, true);

                return client.ReadString(0xF302, subIndexTable3 + 02, 80);
            }
            set
            {
                client.WriteAny(0xF302, subIndexTable3 + 02, value);
            }
        }

        /// <summary>
        /// Version of the display device
        /// </summary>
        public uint Version
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable3 + 03);
        }

        /// <summary>
        /// Valid values: 20-100 (20 lowest brightness, 100 maximum brightness)
        /// </summary>
        public uint Brightness
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable3 + 04);
            set => client.WriteAny(0xF302, subIndexTable3 + 04, value);
        }

        /// <summary>
        /// Valid values: TRUE = background light ON, FALSE = background light OFF
        /// </summary>
        public bool LightEnabled
        {
            get => client.ReadAny<bool>(0xF302, subIndexTable3 + 05);
            set => client.WriteAny(0xF302, subIndexTable3 + 05, value);
        }

        /// <summary>
        /// Save brightness persistent between power cycles.
        /// </summary>
        public void SaveBrightnessPersistent()
        {
            client.WriteAny(0xF302, subIndexTable5, true);
        }
    }
}
