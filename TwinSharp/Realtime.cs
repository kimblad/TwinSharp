using TwinCAT.Ads;

namespace TwinSharp
{
    public class Realtime
    {
        readonly AdsClient client;
        internal Realtime()
        {
            client = new AdsClient();
            client.Connect(AmsPort.R0_Realtime);
        }

        public AdsErrorCode SetSharedCores(uint sharedCores)
        {
            var oldSettings = ReadCpuSettings();

            if (sharedCores == uint.MaxValue)
            {
                // 0xffffffff means RESET -> no isolated cores, all shared.
                if (oldSettings.nNonWinCPUs == 0)
                {
                    //Requested shared core configuration already active, no change applied.
                    return AdsErrorCode.DeviceExists;
                }
            }
            else
            {
                // All other values mean limit the number of shared cores -> use remaining cores as isolated.
                if (sharedCores == oldSettings.nWinCPUs)
                {
                    //Requested shared core configuration already active, no change applied.;
                    return AdsErrorCode.DeviceExists;
                }
            }

            // We have to apply changes to the current core configuration. For this we
            // have to talk to a different AdsDevice, the system service.

            using (AdsClient client = new AdsClient())
            {
                client.Connect(AmsPort.R3_CTRLPROG);
                client.WriteAny(1200, 0, sharedCores);
            }

            return AdsErrorCode.NoError;
        }



        public RTimeCpuSettings ReadCpuSettings()
        {
            var client = new AdsClient();
            client.Connect(AmsPort.R0_Realtime);

            var settings = new RTimeCpuSettings();

            var readBytes = new byte[64];
            var memoryToReadInto = new Memory<byte>(readBytes);

            int bytesRead = client.Read(0x01, 0xd, memoryToReadInto);

            //Create a memory stream and binary reader to read the bytes
            var ms = new MemoryStream(readBytes);
            var br = new BinaryReader(ms);

            settings.nWinCPUs = br.ReadUInt32();
            settings.nNonWinCPUs = br.ReadUInt32();
            settings.affinityMask = br.ReadUInt64();
            settings.nRtCpus = br.ReadUInt32();
            settings.nCpuType = br.ReadUInt32();
            settings.nCpuFamily = br.ReadUInt32();
            settings.nCpuFreq = br.ReadUInt32();

            return settings;
        }

        public RTimeCpuLatency ReadLatency()
        {
            using (AdsClient client = new AdsClient())
            {
                client.Connect(AmsPort.R0_Realtime);

                var info = new RTimeCpuLatency();

                var readBytes = new byte[24];
                var memoryToReadInto = new Memory<byte>(readBytes);

                int bytesRead = client.Read(0x01, 0x2, memoryToReadInto);

                //Create a memory stream and binary reader to read the bytes
                var ms = new MemoryStream(readBytes);
                var br = new BinaryReader(ms);

                info.current = br.ReadUInt32();
                info.maximum = br.ReadUInt32();
                info.limit = br.ReadUInt32();

                return info;
            }
        }

        /// <summary>
        /// Gets the current CPU usage of a TwinCAT system. Corresponds to the function block TC_CpuUsage. This function corresponds to the display of CPU usage in the TwinCAT system menu under the real-time settings.
        /// </summary>
        public uint CpuUsage
        {
            get => client.ReadAny<uint>(0x1, 0x6);
        }
    }
}
