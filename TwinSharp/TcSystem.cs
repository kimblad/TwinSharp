using System.Xml.Serialization;
using System.Xml;
using TwinCAT.Ads;
using System.Globalization;

namespace TwinSharp
{
    /// <summary>
    /// The TcSystem class represents a TwinCAT system and provides methods to interact with it.
    /// It allows switching the system to different modes (Config, Restart, Stop), listing EtherCAT masters,
    /// and listing local static routes. It also provides access to the system's real-time properties, license information,
    /// and file system through the Realtime, License, and FileSystem properties respectively.
    /// </summary>
    public class TcSystem
    {
        readonly AmsNetId target;


        /// <summary>
        /// Creates a representation of a TwinCAT system.
        /// </summary>
        /// <param name="target">Use AmsNetId.Local for a local system.</param>
        public TcSystem(AmsNetId target)
        {
            this.target = target;

            Realtime = new Realtime(target);
            License = new License(target);
            FileSystem = new FileSystem(target);
        }




        /// <summary>
        /// Gets the Realtime object which provides access to the TwinCAT system's real-time properties.
        /// </summary>
        public Realtime Realtime { get; private set; }

        /// <summary>
        /// Gets the License object which provides access to the TwinCAT system's license information.
        /// </summary>

        public License License { get; private set; }


        /// <summary>
        /// Gets the FileSystem object which provides access to the TwinCAT system's file system. Can be used to read and delete files on the remote system.
        /// </summary>
        public FileSystem FileSystem { get; private set; }


        /// <summary>
        /// A TwinCAT system in RUN mode (green TwinCAT system icon) can be switched to CONFIG mode (blue TwinCAT system icon) via the function block "TC_Config".
        /// If the system is already in CONFIG mode, it is first switched to STOP mode (red TwinCAT system icon) and then to CONFIG mode.
        /// </summary>
        public void SwitchToConfigMode()
        {
            using var client = new AdsClient();
            client.Connect(target, AmsPort.SystemService);

            var stateInfo = new StateInfo(AdsState.Reconfig, 0);
            client.WriteControl(stateInfo);
        }

        /// <summary>
        /// Can be used to restart the TwinCAT system. 
        /// Corresponds to the Restart command on the TwinCAT system menu (on the right of the Windows taskbar). Restarting the TwinCAT system involves the TwinCAT system first being stopped, and then immediately started again
        /// </summary>
        public void Restart()
        {
            using var client = new AdsClient();
            client.Connect(target, AmsPort.SystemService);

            var stateInfo = new StateInfo(AdsState.Reset, 0);
            client.WriteControl(stateInfo);
        }

        /// <summary>
        /// Can be used to stop the TwinCAT system. The function corresponds to the Stop command on the TwinCAT system menu (on the right of the Windows taskbar).
        /// </summary>
        public void Stop()
        {
            using var client = new AdsClient();
            client.Connect(target, AmsPort.SystemService);

            var stateInfo = new StateInfo(AdsState.Stop, 0);
            client.WriteControl(stateInfo);
        }

        /// <summary>
        /// Lists all the existing EtherCAT masters on the TwinCAT system.
        /// </summary>
        /// <returns></returns>
        public EtherCatMaster[] ListEtherCatMasters()
        {
            const uint IOADS_IGR_IODEVICESTATE_BASE = 0x5000;
            const uint IOADS_IOF_READDEVIDS = 0x1;
            const uint IOADS_IOF_READDEVNAME = 0x1;
            const uint IOADS_IOF_READDEVCOUNT = 0x2;
            const uint IOADS_IOF_READDEVNETID = 0x5;
            const uint IOADS_IOF_READDEVTYPE = 0x7;


            using var client = new AdsClient();
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

        /// <summary>
        /// List all the existing static routes on the local TwinCAT system.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static AmsRoute[] ListLocalStaticRoutes()
        {
            const string filePath = "C:\\TwinCAT\\3.1\\Target\\StaticRoutes.xml";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file StaticRoutes.xml was not found. Make sure that TwinCAT is installed locally.");
            }


            var settings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document };

            var serializer = new XmlSerializer(typeof(TcConfig));
            TcConfig? configs;
            
            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                configs = serializer.Deserialize(reader) as TcConfig;
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
