using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IpcTime class provides methods to interact with time settings on the IPC.
    /// It allows getting and setting various time-related properties such as SNTP server address, 
    /// SNTP refresh interval, seconds since 1970, textual date-time representation, timezone, and time offset.
    /// </summary>
    public class IpcTime
    {
        internal const ushort ModuleType = 0x0003;

        readonly AdsClient client;
        readonly uint subIndexTable1;

        internal IpcTime(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        /// <summary>
        /// Name or IP Address of the timeserver
        /// "NoSync" = No synchronization
        /// "NT5DS" = Use domain hierarchy settings(Win32 only – no WinCE)
        /// May contain the following flags: See "NtpServer" msdn(Win32 only – no WinCE)
        /// The system must be rebooted in order for the changes to take effect.
        /// </summary>
        public string SNTPServer
        {
            get => client.ReadString(0xF302, subIndexTable1 + 01, 80);
            set => client.WriteAny(0xF302, subIndexTable1 + 01, value);
        }

        /// <summary>
        /// SNTP Refresh1 in Seconds
        /// On WindowsCE lowest allowed value is 5 Seconds.
        /// The system must be rebooted in order for the changes to take effect.
        /// </summary>
        public uint SNTPRefreshInSeconds
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 02);
            set => client.WriteAny(0xF302, subIndexTable1 + 02, value);
        }

        /// <summary>
        /// Seconds since midnight January 1, 1970 (local time)
        /// </summary>
        public uint SecondsSince1970
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 03);
            set => client.WriteAny(0xF302, subIndexTable1 + 03, value);
        }

        /// <summary>
        /// Textual DateTime presentation(local time)
        /// (ISO 8601) YYYY-MM-DDThh:mm:ss.sTZD
        /// </summary>
        public string TextualDateTime
        {
            get => client.ReadString(0xF302, subIndexTable1 + 04, 80);
            set => client.WriteAny(0xF302, subIndexTable1 + 04, value);
        }

        /// <summary>
        /// Timezone - Zero based index of currently active timezone as listed in object 0x8nn2.Sub indizes in Oject 0x8nn2 are one based.To lookup timezone information you need to query sub idx @ "this value"+1
        /// Not for TC/RTOS
        /// </summary>
        public ushort Timezone
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1 + 05);
            set => client.WriteAny(0xF302, subIndexTable1 + 05, value);
        }

        /// <summary>
        /// Time Offset – offset in seconds of the current local time relative to the coordinated universal time (UTC)
        /// (supports only steps of 15 minutes = 900 seconds)
        /// only for TC/RTOS
        /// </summary>
        public int TimeOffset
        {
            get
            {
                try
                {
                    return client.ReadAny<int>(0xF302, subIndexTable1 + 06);
                }
                catch (AdsErrorException adsExc)
                {
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                        return 0;

                    throw;
                }
            }
            set
            {
                client.WriteAny(0xF302, subIndexTable1 + 06, value);
            }
        }
    }
}

