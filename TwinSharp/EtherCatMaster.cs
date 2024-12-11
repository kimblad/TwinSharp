using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{

    /// <summary>
    /// The EtherCatMaster class provides methods to interact with an EtherCAT master device.
    /// It allows for reading the current state, device type, and name of the master, as well as
    /// retrieving information about connected slaves, such as their configuration, state, and topology.
    /// Additionally, it can read unexpected state changes and convert device status to a string representation.
    /// </summary>
    public class EtherCatMaster
    {
        readonly AdsClient client;
        string name;


        internal EtherCatMaster(AmsNetId netId)
        {
            AmsNetId = netId;
            name = "";

            client = new AdsClient();
            client.Connect(netId, AmsPort.USEDEFAULT);
        }


        /// <summary>
        /// The AmsNetId of the EtherCAT master.
        /// </summary>
        public AmsNetId AmsNetId
        {
            get;
            private set;
        }


        /// <summary>
        /// The device type of the EtherCAT master.
        /// </summary>
        public ushort DeviceType
        {
            get;
            internal set;
        }


        /// <summary>
        /// The name of the EtherCAT master.
        /// </summary>
        public string Name
        {
            //TODO: Unknown how to get the name of the EtherCAT master from this class.
            //Can only be found when searching for all masters in the system.
            get => name;
            internal set => name = value;
        }

        /// <summary>
        /// Can be used to read the current state of the EtherCAT master. Corresponds to the PLC FB: FB_EcGetMasterDevState 
        /// </summary>
        public ushort MasterDevState
        {
            get => client.ReadAny<ushort>(0x45, 0x0);
        }

        /// <summary>
        /// Converts the device status of the EtherCAT master to a string.
        /// For masterDevState == 0 'OK' is returned, otherwise, 'Not OK – Link error', e.g. for masterDevState == 1. If several errors are pending, they are separated by hyphens.
        /// </summary>
        /// <param name="masterDevState"></param>
        /// <returns></returns>
        public string MasterDevStateToString(ushort masterDevState)
        {
            if (masterDevState == 0)
            {
                return "OK";
            }

            var sb = new StringBuilder();

            if((masterDevState & 0x0001) != 0)
            {
                sb.Append("Link error");
                sb.Append(" - ");
            }

            if((masterDevState & 0x0002) != 0)
            {
                sb.Append("I/O locked after link error (I/O reset required)");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0004) != 0)
            {
                sb.Append("Link error (redundancy adapter)");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0008) != 0)
            {
                sb.Append("Missing one frame (redundancy mode)");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0010) != 0)
            {
                sb.Append("Out of send resources (I/O reset required)");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0020) != 0)
            {
                sb.Append("Watchdog triggered");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0040) != 0)
            {
                sb.Append("Ethernet driver(miniport) not found");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0080) != 0)
            {
                sb.Append("I/O reset active");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0100) != 0)
            {
                sb.Append("At least one device in 'INIT' state");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0200) != 0)
            {
                sb.Append("At least one device in 'PRE-OP' state");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0400) != 0)
            {
                sb.Append("At least one device in 'SAFE-OP' state");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x0800) != 0)
            {
                sb.Append("At least one device indicates an error state");
                sb.Append(" - ");
            }

            if ((masterDevState & 0x1000) != 0)
            {
                sb.Append("DC not in sync");
                sb.Append(" - ");
            }


            // Remove the trailing " - " if it exists
            if (sb.Length > 3 && sb.ToString().EndsWith(" - "))
            {
                sb.Remove(sb.Length - 3, 3);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Can be used to determine the number of slaves that are connected to the master. Corresponds to the PLC FB: FB_EcGetSlaveCount.
        /// </summary>
        public uint SlaveCount
        {
            get => client.ReadAny<uint>(0x6, 0x0);
        }


        /// <summary>
        /// Count of number of slaves configured in TwinCAT.
        /// </summary>
        public ushort SlaveCountConfigured
        {
            get => client.ReadAny<ushort>(0xF302, 0xF0200000);
        }


        /// <summary>
        /// Generates an array of all configured Slaves from the Master object directory.
        /// Corresponds to the function block FB_EcGetConfSlaves
        /// </summary>
        /// <returns></returns>
        public ST_EcSlaveConfigData[] GetConfiguredSlaves()
        {
            ushort numberOfConfiguredSlaves = SlaveCountConfigured;

            const uint baseIndex = 0x8000_0100;
            //const uint postIndex = 0x0100;
            const uint indexIncrement = 0x1_0000;

            uint index;
            var descriptions = new ST_EcSlaveConfigData[numberOfConfiguredSlaves];

            for (ushort i = 0; i < numberOfConfiguredSlaves; i++)
            {
                index = baseIndex + (i * indexIncrement);
                
                byte[] descriptionBytes = new byte[80];
                var readBuffer = new Memory<byte>(descriptionBytes);

                client.Read(0xF302, index, readBuffer);

                var description = new ST_EcSlaveConfigData(descriptionBytes);

                descriptions[i] = description;
            }

            return descriptions;
        }

        /// <summary>
        /// Can be used to read the unexpected EtherCAT state changes of all the slaves connected to the master. It returns the number of unexpected state changes of all slaves as an array of UDINTs. 
        /// EtherCAT state changes are unexpected if they were not requested by the EtherCAT master, e.g. if an EtherCAT slave spontaneously switches from OP state to SAFEOP state.
        /// Corresponds to the function block FB_EcGetAllSlaveAbnormalStateChanges.
        /// </summary>
        public uint[] GetAllSlaveAbnormalStateChanges()
        {
            uint slaveCount = SlaveCount;

            byte[] buffer = new byte[slaveCount * sizeof(uint)];
            var readBuffer = new Memory<byte>(buffer);

            client.Read(0x13, 0x0, readBuffer);


            uint[] stateChanges = new uint[slaveCount];
            var ms = new MemoryStream(buffer);
            var br = new BinaryReader(ms);

            for (int i = 0; i < slaveCount; i++)
            {
                stateChanges[i] = br.ReadUInt32();
            }

            return stateChanges;
        }

        /// <summary>
        /// Reads the EtherCAT status and the Link status of all the slaves connected to the master.
        /// Corresponds to the function block FB_EcGetAllSlaveStates.
        /// </summary>
        public ST_EcSlaveState[] GetAllSlaveStates()
        {
            const int slaveStateSize = 2; //ST_EcSlaveState is 2 bytes long

            uint slaveCount = SlaveCount;

            byte[] buffer = new byte[slaveCount * slaveStateSize];
            var readBuffer = new Memory<byte>(buffer);

            client.Read(0x9, 0x0, readBuffer);

            ST_EcSlaveState[] slaveStates = new ST_EcSlaveState[slaveCount];

            var ms = new MemoryStream(buffer);
            var br = new BinaryReader(ms);

            for (int i = 0; i < slaveCount; i++)
            {
                var oneSlaveState = br.ReadBytes(slaveStateSize);
                slaveStates[i] = new ST_EcSlaveState(oneSlaveState);
            }

            return slaveStates;
        }

        /// <summary>
        /// Can be used to determine topology information. Equivavalent to the function block FB_EcGetSlaveTopolgyInfo.
        /// </summary>
        /// <returns>An array of structures of type ST_TopologyDataEx, which contains the topology data.</returns>
        public ST_TopologyDataEx[] GetSlaveTopologyInfo()
        {
            const int oneTopologySize = 64;

            uint slaveCount = SlaveCount;

            byte[] buffer = new byte[oneTopologySize * slaveCount];
            var readBuffer = new Memory<byte>(buffer);

            client.Read(0x22, 0x0, readBuffer);

            ST_TopologyDataEx[] topologyInfos = new ST_TopologyDataEx[slaveCount];

            var ms = new MemoryStream(buffer);
            var br = new BinaryReader(ms);

            for (int i = 0; i < slaveCount; i++)
            {
                var oneTopologyDescription = br.ReadBytes(oneTopologySize);
                topologyInfos[i] = new ST_TopologyDataEx(oneTopologyDescription);
            }

            return topologyInfos;
        }

        /// <summary>
        /// Returns a string representation of the EtherCAT master. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + " " + AmsNetId.ToString();
        }

    }
}
