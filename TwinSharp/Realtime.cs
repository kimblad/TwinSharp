using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// The Realtime class provides methods to interact with the TwinCAT systems real-time settings.
    /// It allows setting shared cores configuration, reading CPU settings, reading CPU latency,
    /// and getting the current CPU usage. It uses the AdsClient to communicate with the TwinCAT system.
    /// </summary>
    public class Realtime
    {
        readonly AdsClient client;
        readonly AmsNetId target;
        internal Realtime(AmsNetId target)
        {
            this.target = target;
            client = new AdsClient();
            client.Connect(target, AmsPort.R0_Realtime);
        }

        /// <summary>
        /// Sets the shared cores configuration for the TwinCAT system.
        /// </summary>
        /// <param name="sharedCores"></param>
        /// <returns></returns>
        public AdsErrorCode SetSharedCores(uint sharedCores)
        {
            var oldSettings = ReadCpuSettings();

            if (sharedCores == uint.MaxValue)
            {
                // 0xffffffff means RESET -> no isolated cores, all shared.
                if (oldSettings.NonWinCPUs == 0)
                {
                    //Requested shared core configuration already active, no change applied.
                    return AdsErrorCode.DeviceExists;
                }
            }
            else
            {
                // All other values mean limit the number of shared cores -> use remaining cores as isolated.
                if (sharedCores == oldSettings.WinCPUs)
                {
                    //Requested shared core configuration already active, no change applied.;
                    return AdsErrorCode.DeviceExists;
                }
            }

            // We have to apply changes to the current core configuration. For this we
            // have to talk to a different AdsDevice, the system service.

            using (var client = new AdsClient())
            {
                client.Connect(AmsPort.R3_CTRLPROG);
                client.WriteAny(1200, 0, sharedCores);
            }

            return AdsErrorCode.NoError;
        }


        /// <summary>
        /// Reads the CPU settings of the TwinCAT system.
        /// </summary>
        /// <returns></returns>
        public RTimeCpuSettings ReadCpuSettings()
        {
            var client = new AdsClient();
            client.Connect(target, AmsPort.R0_Realtime);

            var settings = new RTimeCpuSettings();

            var readBytes = new byte[64];
            var memoryToReadInto = new Memory<byte>(readBytes);

            int bytesRead = client.Read(0x01, 0xd, memoryToReadInto);

            //Create a memory stream and binary reader to read the bytes
            var ms = new MemoryStream(readBytes);
            var br = new BinaryReader(ms);

            settings.WinCPUs = br.ReadUInt32();
            settings.NonWinCPUs = br.ReadUInt32();
            settings.AffinityMask = br.ReadUInt64();
            settings.RtCpus = br.ReadUInt32();
            settings.CpuType = br.ReadUInt32();
            settings.CpuFamily = br.ReadUInt32();
            settings.CpuFreq = br.ReadUInt32();

            return settings;
        }

        /// <summary>
        /// Reads the CPU latency of the TwinCAT system.
        /// </summary>
        /// <returns></returns>
        public RTimeCpuLatency ReadLatency()
        {
            using var client = new AdsClient();
            client.Connect(target, AmsPort.R0_Realtime);

            var info = new RTimeCpuLatency();

            var readBytes = new byte[24];
            var memoryToReadInto = new Memory<byte>(readBytes);

            int bytesRead = client.Read(0x01, 0x2, memoryToReadInto);

            //Create a memory stream and binary reader to read the bytes
            var ms = new MemoryStream(readBytes);
            var br = new BinaryReader(ms);

            info.Current = br.ReadUInt32();
            info.Maximum = br.ReadUInt32();
            info.Limit = br.ReadUInt32();

            return info;
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
