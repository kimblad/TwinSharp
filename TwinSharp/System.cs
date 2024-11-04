using TwinCAT.Ads;

namespace TwinSharp
{
    public class System
    {
        public Realtime Realtime;
        public License License;
        public FileSystem FileSystem;


        public System(AmsNetId target)
        {
            Realtime = new Realtime();
            License = new License();
            FileSystem = new FileSystem(target);
        }

        public EtherCatMaster[] ListEcatMasters()
        {
            const uint IOADS_IGR_IODEVICESTATE_BASE = 0x5000;
            const uint IOADS_IOF_READDEVIDS = 0x1;
            const uint IOADS_IOF_READDEVNAME = 0x1;
            const uint IOADS_IOF_READDEVCOUNT = 0x2;
            const uint IOADS_IOF_READDEVNETID = 0x5;
            const uint IOADS_IOF_READDEVTYPE = 0x7;


            using (var client = new AdsClient())
            {
                client.Connect(AmsPort.R0_IO);


                uint deviceCount = client.ReadAny<uint>(IOADS_IGR_IODEVICESTATE_BASE, IOADS_IOF_READDEVCOUNT);
                var etherCatMasters = new EtherCatMaster[deviceCount];

                //Get the device IDs
                // the first element of the array is set to devCount,
                // so the actual device Ids start at index 1

                Memory<byte> buffer = new byte[(deviceCount + 1) * sizeof(ushort)];
                client.Read(IOADS_IGR_IODEVICESTATE_BASE, IOADS_IOF_READDEVIDS, buffer);

                var ms = new MemoryStream(buffer.ToArray());
                var br = new BinaryReader(ms);

                ushort[] deviceIDs = new ushort[(deviceCount + 1)];

                // Copy the buffer to the deviceIDs array
                for (int i = 0; i < deviceIDs.Length; i++)
                {
                    deviceIDs[i] = br.ReadUInt16();
                }

                // Skip the device count, which is at the first index
                for (int i = 1; i <= deviceCount; i++)
                {
                    ushort deviceID = deviceIDs[i];

                    ushort deviceType = client.ReadAny<ushort>(
                        IOADS_IGR_IODEVICESTATE_BASE + deviceID,
                        IOADS_IOF_READDEVTYPE);

                    string deviceName = client.ReadString(
                        IOADS_IGR_IODEVICESTATE_BASE + deviceID,
                        IOADS_IOF_READDEVNAME, 256);

                    deviceName = deviceName.TrimEnd('\0');


                    //Read the ams net id. It is stored as a 6 byte array.
                    Memory<byte> amsBuffer = new byte[6];

                    client.Read(
                        IOADS_IGR_IODEVICESTATE_BASE + deviceID,
                        IOADS_IOF_READDEVNETID, amsBuffer);

                    var amsNetId = new AmsNetId(amsBuffer.ToArray());

                    var ecMaster = new EtherCatMaster(amsNetId);
                    ecMaster.DeviceType = deviceType;
                    ecMaster.Name = deviceName;

                    etherCatMasters[i - 1] = ecMaster;
                }

                return etherCatMasters;
            }
        }
    }
}
