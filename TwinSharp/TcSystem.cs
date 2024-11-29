using System.Xml.Serialization;
using System.Xml;
using TwinCAT.Ads;
using System.Globalization;

namespace TwinSharp
{
    public class TcSystem
    {
        public readonly Realtime Realtime;
        public readonly License License;
        public readonly FileSystem FileSystem;

        readonly AmsNetId target;

        /// <summary>
        /// Create a representation of a TwinCAT system on the local machine.
        /// </summary>
        public TcSystem()
        {
            target = AmsNetId.Local;
            Init(target, out Realtime, out License, out FileSystem);
        }

        /// <summary>
        /// Create a representation of a TwinCAT system on a remote target machine.
        /// </summary>
        /// <param name="target"></param>
        public TcSystem(AmsNetId target)
        {
            this.target = target;
            Init(target, out Realtime, out License, out FileSystem);
        }


        //Called by mulitple constructors to set readonly objects.
        private void Init(AmsNetId target, out Realtime realtime, out License license, out FileSystem fileSystem)
        {
            realtime = new Realtime(target);
            license = new License(target);
            fileSystem = new FileSystem(target);
        }

        /// <summary>
        /// A TwinCAT system in RUN mode (green TwinCAT system icon) can be switched to CONFIG mode (blue TwinCAT system icon) via the function block "TC_Config".
        /// If the system is already in CONFIG mode, it is first switched to STOP mode (red TwinCAT system icon) and then to CONFIG mode.
        /// </summary>
        public void SwitchToConfigMode()
        {
            using (var client = new AdsClient())
            {
                client.Connect(target, AmsPort.SystemService);

                var stateInfo = new StateInfo(AdsState.Reconfig, 0);
                client.WriteControl(stateInfo);
            }
        }

        /// <summary>
        /// Can be used to restart the TwinCAT system. 
        /// Corresponds to the Restart command on the TwinCAT system menu (on the right of the Windows taskbar). Restarting the TwinCAT system involves the TwinCAT system first being stopped, and then immediately started again
        /// </summary>
        public void Restart()
        {
            using (var client = new AdsClient())
            {
                client.Connect(target, AmsPort.SystemService);

                var stateInfo = new StateInfo(AdsState.Reset, 0);
                client.WriteControl(stateInfo);
            }
        }

        /// <summary>
        /// Can be used to stop the TwinCAT system. The function corresponds to the Stop command on the TwinCAT system menu (on the right of the Windows taskbar).
        /// </summary>
        public void Stop()
        {
            using (var client = new AdsClient())
            {
                client.Connect(target, AmsPort.SystemService);

                var stateInfo = new StateInfo(AdsState.Stop, 0);
                client.WriteControl(stateInfo);
            }
        }

        public EtherCatMaster[] ListEtherCatMasters()
        {
            const uint IOADS_IGR_IODEVICESTATE_BASE = 0x5000;
            const uint IOADS_IOF_READDEVIDS = 0x1;
            const uint IOADS_IOF_READDEVNAME = 0x1;
            const uint IOADS_IOF_READDEVCOUNT = 0x2;
            const uint IOADS_IOF_READDEVNETID = 0x5;
            const uint IOADS_IOF_READDEVTYPE = 0x7;


            using (var client = new AdsClient())
            {
                client.Connect(target, AmsPort.R0_IO);


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

                    //EtherCAT masters AmsNetId is typically in the form: [192.168.5.89].2.1
                    //Combine the last two digits with the targets ams net id to create a AmsNetId that can be reached from remote 
                    byte[] combined = target.ToBytes();
                    combined[4] = amsNetId.ToBytes()[4];
                    combined[5] = amsNetId.ToBytes()[5];

                    var combinedAms = new AmsNetId(combined);

                    var ecMaster = new EtherCatMaster(combinedAms)
                    {
                        DeviceType = deviceType,
                        Name = deviceName
                    };

                    etherCatMasters[i - 1] = ecMaster;
                }

                return etherCatMasters;
            }
        }


        public static AmsRoute[] ListLocalStaticRoutes()
        {
            const string filePath = "C:\\TwinCAT\\3.1\\Target\\StaticRoutes.xml";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file StaticRoutes.xml was not found. Make sure the path is correct.");
            }


            var settings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document };

            XmlSerializer ser = new XmlSerializer(typeof(TcConfig));
            TcConfig? configs;
            
            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                configs = ser.Deserialize(reader) as TcConfig;
            }


            if (configs == null)
                return [];

            if (configs.Items == null || configs.Items.Length == 0)
                return [];


            //Convert from the parsed xml format where everything is strings.
            var xmlRoutes = configs.Items[0].Route;
            var routes = new AmsRoute[xmlRoutes.Length];


            for (int i = 0; i < xmlRoutes.Length; i++)
            {
                var amsNetId = new AmsNetId(xmlRoutes[i].NetId); //Xml parsed netId as string. Create a real AmsNetId object.


                if (!int.TryParse(xmlRoutes[i].Flags, CultureInfo.InvariantCulture, out int flags))
                    flags = 0;


                var route = new AmsRoute(xmlRoutes[i].Name, xmlRoutes[i].Address, amsNetId, xmlRoutes[i].Type, flags);

                routes[i] = route;
            }

            return routes;
        }
    }
}
