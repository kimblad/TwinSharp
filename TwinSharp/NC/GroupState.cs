using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The GroupState class provides properties to interact with and retrieve various states and information
    /// from a TwinCAT NC group via an AdsClient. It includes properties for error codes, axis counts, group states,
    /// and emergency stop status, among others. Each property reads or writes data from the TwinCAT system using
    /// specific index groups and offsets.
    /// </summary>
    public class GroupState
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal GroupState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x3100 + id;
        }

        /// <summary>
        /// Error code group
        /// </summary>
        public int ErrorCode
        {
            get => client.ReadAny<int>(indexGroup, 0x01);
        }

        /// <summary>
        /// Number of master axes
        /// </summary>
        public uint MasterAxisCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x02);
        }

        /// <summary>
        /// Number of slave axes
        /// </summary>
        public uint SlaveAxisCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// SVB group state (state)
        /// </summary>
        public uint SvbGroupState
        {
            get => client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// SAF group state (main state)
        /// </summary>
        public uint SafGroupState
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

        /// <summary>
        /// Moving state (state)
        /// </summary>
        public uint MovingState
        {
            get => client.ReadAny<uint>(indexGroup, 0x06);
        }

        /// <summary>
        /// SAF sub-group state (sub state)
        /// </summary>
        public uint SafSubGroupState
        {
            get => client.ReadAny<uint>(indexGroup, 0x07);
        }

        /// <summary>
        /// Referencing state (state)
        /// </summary>
        public uint ReferencingState
        {
            get => client.ReadAny<uint>(indexGroup, 0x08);
        }

        /// <summary>
        /// Coupling state (state)
        /// </summary>
        public uint CouplingState
        {
            get => client.ReadAny<uint>(indexGroup, 0x09);
        }

        /// <summary>
        /// Coupling table index
        /// </summary>
        public uint CouplingTableIndex
        {
            get => client.ReadAny<uint>(indexGroup, 0x0A);
        }

        /// <summary>
        /// Current number of SVB entries/tasks
        /// </summary>
        public uint CurrentSvbEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0B);
        }

        /// <summary>
        /// Current number of SAF entries/tasks
        /// </summary>
        public uint CurrentSafEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0C);
        }

        /// <summary>
        /// Current block number (only active for interpolation group)
        /// </summary>
        public uint CurrentBlockNumber
        {
            get => client.ReadAny<uint>(indexGroup, 0x0D);
        }

        /// <summary>
        /// Current number of free SVB entries/tasks
        /// </summary>
        public uint CurrentFreeSvbEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0E);
        }

        /// <summary>
        /// Current number of free SAF entries/tasks
        /// </summary>
        public uint CurrentFreeSafEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0F);
        }

        /// <summary>
        /// Emergency Stop (E-Stop) active?
        /// </summary>
        public bool EmergencyStopActive
        {
            get
            {
                ushort digit = client.ReadAny<ushort>(indexGroup, 0x11);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x11, digit);
            }
        }
    }
}