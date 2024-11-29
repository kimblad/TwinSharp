using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IpcTwinCAT class provides an interface to interact with a TwinCAT system using an AdsClient.
    /// It allows reading various system properties such as version, status, and configuration details.
    /// The class handles specific TwinCAT system attributes and provides methods to read these attributes
    /// from the TwinCAT system using ADS (Automation Device Specification) protocol.
    /// </summary>
    public class IpcTwinCAT
    {
        internal const ushort ModuleType = 0x0008;

        AdsClient client;
        readonly uint subIndexTable1;
        readonly uint subIndexTable2;

        internal IpcTwinCAT(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
            subIndexTable2 = (uint)(mdpId << 20) | 0x80020000; //Table 0x8nn2, just add the desired subIndex later.
        }

        /// <summary>
        /// Length of the structure
        /// </summary>
        public ushort Length
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1);
        }

        /// <summary>
        /// TwinCAT major version.
        /// </summary>
        public ushort MajorVersion
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1 + 01);
        }

        /// <summary>
        /// TwinCAT minor version.
        /// </summary>
        public ushort MinorVersion
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1 + 02);
        }

        /// <summary>
        /// TwinCAT build number.
        /// </summary>
        public ushort BuildNumber
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1 + 03);
        }

        /// <summary>
        /// AMS Net ID of the TwinCAT system. A restart of the computer is required in order to make a change to the Ams Net ID.
        /// </summary>
        public string AmsNetID
        {
            get => client.ReadString(0xF302, subIndexTable1 + 04, 80);
        }

        /// <summary>
        /// Only for TwinCAT 2.
        /// </summary>
        public uint RegLevel
        {
            get
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndexTable1 + 05);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not TC2
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    {
                        return 0;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Status of the TwinCAT system.
        /// </summary>
        public ushort Status
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1 + 06);
        }

        /// <summary>
        /// Only for WindowsCE
        /// </summary>
        public ushort RunAsDevice
        {
            get
            {
                try
                {
                    return client.ReadAny<ushort>(0xF302, subIndexTable1 + 07);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not TC2
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    {
                        return 0;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Only for WindowsCE
        /// </summary>
        public ushort ShowTargetVisu
        {
            get
            {
                try
                {
                    return client.ReadAny<ushort>(0xF302, subIndexTable1 + 08);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not TC2
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    {
                        return 0;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Only for WindowsCE
        /// </summary>
        public uint LogFileSize
        {
            get
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndexTable1 + 09);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not TC2
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    {
                        return 0;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Only for WindowsCE
        /// </summary>
        public string LogFilePath
        {
            get
            {
                try
                {
                    return client.ReadString(0xF302, subIndexTable1 + 10, 80);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not TC2
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    {
                        return "Not supported.";
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// TwinCAT System ID
        /// </summary>
        public string SystemID
        {
            get => client.ReadString(0xF302, subIndexTable1 + 11, 80);
        }

        /// <summary>
        /// TwinCAT Revision
        /// </summary>
        public ushort Revision
        {
            get => client.ReadAny<ushort>(0xF302, subIndexTable1 + 12);
        }

        /// <summary>
        /// Seconds since last TwinCAT status change
        /// </summary>
        public ulong SecondsSinceLastStatusChange
        {
            get => client.ReadAny<ulong>(0xF302, subIndexTable1 + 13);
        }

    }
}
