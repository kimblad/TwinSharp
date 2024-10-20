using TwinCAT.Ads;

namespace TwinSharp
{
    public class IpcMemory
    {
        public const ushort ModuleType = 0x000C;

        AdsClient client;
        readonly uint subIndex;

        internal IpcMemory(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }



        public ulong ProgramMemoryAllocated
        {
            get
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndex + 0x01);
                }
                catch (AdsErrorException adsExc)
                {
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    { 
                        // On computers with more than 4 GB of RAM the subindex 01 "Program Memory Allocated" will return Not Supported.
                        // Use subindex 06 instead.
                        return client.ReadAny<ulong>(0xF302, subIndex + 0x06);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public ulong ProgramMemoryAvailable
        {
            get
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndex + 0x02);
                }
                catch (AdsErrorException adsExc)
                {
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                    {
                        // On computers with more than 4 GB of RAM the subindex 02 "Program Memory Allocated" will return Not Supported.
                        // Use subindex 07 instead.
                        return client.ReadAny<ulong>(0xF302, subIndex + 0x07);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// Storage Memory Allocated.
        /// Only for WindowsCE.
        /// </summary>
        public uint StorageMemoryAllocated
        {
            get
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndex + 0x03);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not a WindowsCE device.
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                        return 0;
                    else
                        throw;
                }
            }
        }

        /// <summary>
        /// Storage Memory Available.
        /// Only for WindowsCE.
        /// </summary>
        public uint StorageMemoryAvailable
        {
            get 
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndex + 0x04);
                }
                catch (AdsErrorException adsExc)
                {
                    //Not a WindowsCE device.
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                        return 0;
                    else
                        throw;
                }
            }
        }

        /// <summary>
        /// Memory division.
        /// Only for WindowsCE.
        /// </summary>
        public uint MemoryDivision
        {
            get 
            {
                try
                {
                    return client.ReadAny<uint>(0xF302, subIndex + 0x05);
                } 
                catch (AdsErrorException adsExc)
                {
                    //Not a WindowsCE device.
                    if ((uint)adsExc.ErrorCode == (uint)ExtendedAdsErrorCodes.NotSupported)
                        return 0;
                    else
                        throw;
                }
            }
        }
    }
}
