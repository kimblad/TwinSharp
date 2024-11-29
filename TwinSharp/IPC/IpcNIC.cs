using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IpcNIC class provides an interface to interact with network interface card (NIC) settings 
    /// through the TwinCAT ADS protocol. It allows reading and writing of various NIC properties 
    /// such as MAC address, IPv4 address, subnet mask, DHCP status, default gateway, DNS servers, 
    /// and virtual device name. The class handles specific behaviors for different operating systems 
    /// like Windows, WinCE, TC/BSD, and TC/RTOS.
    /// </summary>
    public class IpcNIC
    {
        internal const ushort ModuleType = 0x0002;

        readonly AdsClient client;
        readonly uint subIndex;

        internal IpcNIC(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        /// <summary>
        /// MAC address of the card.
        /// </summary>
        public string MACAddress
        {
            get => client.ReadString(0xF302, subIndex + 01, 80);
        }

        /// <summary>
        /// With WinCE a reboot may be required in order to obtain a correct value. Without a reboot WinCE may still supply the previous value!
        /// </summary>
        public string IPv4Address
        {
            get => client.ReadString(0xF302, subIndex + 02, 80);
            set => client.WriteAny(0xF302, subIndex + 02, value);
        }

        /// <summary>
        /// With WinCE a reboot may be required in order to obtain a correct value. Without a reboot WinCE may still supply the previous value!
        /// </summary>
        public string IPv4SubNetMask
        {
            get => client.ReadString(0xF302, subIndex + 03, 80);
            set => client.WriteAny(0xF302, subIndex + 03, value);
        }

        /// <summary>
        /// DHCP active.
        /// </summary>
        public bool DHCP
        {
            get => client.ReadAny<bool>(0xF302, subIndex + 04);
            set => client.WriteAny(0xF302, subIndex + 04, value);
        }

        /// <summary>
        /// With WinCE a reboot may be required in order to obtain a correct value. Without a reboot WinCE may still supply the previous value!
        /// WinCE: depending on the DHCP status, a "Read" operation has the return value "DefaultGateway" or "DhcpDefaultGateway".
        /// </summary>
        public string IPv4DefaultGateway
        {
            get => client.ReadString(0xF302, subIndex + 05, 80);
            set => client.WriteAny(0xF302, subIndex + 05, value);
        }

        /// <summary>
        /// Not for WinCE.
        /// </summary>
        public string IPv4DNSServers
        {
            get => client.ReadString(0xF302, subIndex + 06, 80);
            set => client.WriteAny(0xF302, subIndex + 06, value);
        }

        /// <summary>
        /// Only for Windows.
        /// </summary>
        public string VirtualDeviceName
        {
            get => client.ReadString(0xF302, subIndex + 07, 80);
            set => client.WriteAny(0xF302, subIndex + 07, value);
        }

        /// <summary>
        /// Only for TC/BSD and TC/RTOS
        /// </summary>
        public string IPv4DNSServersActive
        {
            get
            {
                try
                {
                    return client.ReadString(0xF302, subIndex + 08, 80);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not TC/BSD or TC/RTOS
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                        return "";
                    else
                        throw;
                }
            }
        }
    }
}
