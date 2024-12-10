using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The GroupParameters class provides access to various parameters of a group in the TwinCAT NC system.
    /// It allows reading and writing of group-specific settings such as ID, name, type, cycle times, and FIFO configurations.
    /// This class interacts with the TwinCAT ADS client to perform read and write operations on the group parameters.
    /// </summary>
    public class GroupParameters
    {
        private readonly AdsClient client;
        private readonly uint indexGroup;

        internal GroupParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x3000 + id;
        }

        /// <summary>
        /// Group ID
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Group name
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

        /// <summary>
        /// Group type
        /// </summary>
        public GroupType Type
        {
            get => (GroupType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// SAF cycle time group
        /// </summary>
        public uint SafCycleTime
        {
            get => client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// SVB cycle time group
        /// </summary>
        public uint SvbCycleTime
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

        /// <summary>
        /// Single block operation mode.
        /// </summary>
        public bool SingleBlockOperationMode
        {
            get
            {
                ushort digit = client.ReadAny<ushort>(indexGroup, 0x06);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x06, digit);
            }
        }

        /// <summary>
        /// Size of the SVB table (max. number of SVB entries
        /// </summary>
        public uint MaxSvbEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0B);
        }

        /// <summary>
        /// Size of the SAF table (max. number of SAF entries
        /// </summary>
        public uint MaxSafEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0C);
        }

        /// <summary>
        /// Internal SAF cycle time divisor (divides the internal SAF cycle time by this factor)
        /// </summary>
        public uint SafCycleTimeDivisor
        {
            get => client.ReadAny<uint>(indexGroup, 0x10);
        }

        /// <summary>
        /// Channel ID that this group belongs to.
        /// </summary>
        public uint ParentChannelID
        {
            get => client.ReadAny<uint>(indexGroup, 0x21);
        }

        /// <summary>
        /// Channel name that this group belongs to.
        /// </summary>
        public string ParentChannelName
        {
            get => client.ReadString(indexGroup, 0x22, 30);
        }

        /// <summary>
        /// Channel type that this group belongs to.
        /// </summary>
        public ChannelType ParentChannelType
        {
            get => (ChannelType)client.ReadAny<uint>(indexGroup, 0x23);
        }

        /// <summary>
        /// Number in the channel
        /// </summary>
        public uint NumberInTheChannel
        {
            get => client.ReadAny<uint>(indexGroup, 0x24);
        }

        /// <summary>
        /// FIFO dimension (m = number of axes)
        /// </summary>
        public uint FifoDimension
        {
            get => client.ReadAny<uint>(indexGroup, 0x701);
        }

        /// <summary>
        /// FIFO size(length) (n = number of FIFO entries)
        /// </summary>
        public uint FifoLength
        {
            get => client.ReadAny<uint>(indexGroup, 0x702);
        }

        /// <summary>
        /// Interpolation type for FIFO setpoint generator
        /// </summary>
        public FifoInterpolationType FifoInterpolationType
        {
            get => (FifoInterpolationType)client.ReadAny<uint>(indexGroup, 0x703);
        }

        /// <summary>
        /// Override type for FIFO setpoint generator
        /// </summary>
        public FifoOverrideType FifoOverrideType
        {
            get => (FifoOverrideType)client.ReadAny<uint>(indexGroup, 0x704);
            set => client.WriteAny(indexGroup, 0x704, (uint)value);
        }

        /// <summary>
        /// P-T2 time for override change(T1= T2 = T0)
        /// </summary>
        public double FifoTimeForOverrideChange
        {
            get => client.ReadAny<double>(indexGroup, 0x705);
            set => client.WriteAny(indexGroup, 0x705, value);
        }

        /// <summary>
        /// Time delta for two sequenced FIFO entries (FIFO entry timebase)
        /// </summary>
        public double FifoTimeDelta
        {
            get => client.ReadAny<double>(indexGroup, 0x706);
            set => client.WriteAny(indexGroup, 0x706, value);
        }
    }
}