using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// If the axis belongs to a channel, this class can be used to get the status of the axis in that channel.
    /// </summary>
    public class AxisStatusInChannel
    {
        private readonly AdsClient comClient;
        public const uint IndexGroup = 0x120101;
        private readonly uint indexOffset;

        public readonly AxisStatusInChannelAdresses Adresses;

        internal AxisStatusInChannel(AdsClient comClient, uint axisIndex)
        {
            this.comClient = comClient;
            indexOffset = 0x10000 * axisIndex;

            Adresses = new AxisStatusInChannelAdresses(axisIndex);
        }

        public ushort LogicalNumber
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x01);
        }

        public string Name
        {
            get => comClient.ReadString(IndexGroup, indexOffset + 0x02, 16);
        }

        public ushort Type
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x03);
        }

        /// <summary>
        /// Distance to go in the current NC block, difference between target position and command position.
        /// </summary>
        public int DistanceToGoPCS
        {
            get => comClient.ReadAny<int>(IndexGroup, indexOffset + 0x04);
        }

        /// <summary>
        /// Target position of the current NC block.
        /// </summary>
        public int EndPositionPCS
        {
            get => comClient.ReadAny<int>(IndexGroup, indexOffset + 0x05);
        }

        /// <summary>
        /// Position preset in the current cycle as setpoint.
        /// </summary>
        public int CommandedPositionPCS
        {
            get => comClient.ReadAny<int>(IndexGroup, indexOffset + 0x06);
        }

        /// <summary>
        /// Actual ACS position converted in the PCS.
        /// </summary>
        public int ActualPositionPCS
        {
            get => comClient.ReadAny<int>(IndexGroup, Adresses.ActualPositionPCS);
        }

        /// <summary>
        /// The axis completed homing successfully and is now referenced.
        /// </summary>
        public bool HomingDone
        {
            get => comClient.ReadAny<bool>(IndexGroup, indexOffset + 0x08);
        }

        public AxisState AxisStatePCS
        {
            get => (AxisState)comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x09);
        }

        public ushort ManualState
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x0A);
        }

        public ushort OperationMode
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x0B);
        }

        public ushort ControlElement
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x0C);
        }

        public uint ContinuousSpeed
        {
            get => comClient.ReadAny<uint>(IndexGroup, indexOffset + 0x0D);
        }

        public uint IncrementalSpeed
        {
            get => comClient.ReadAny<uint>(IndexGroup, indexOffset + 0x0E);
        }

        public uint IncrementalDistance
        {
            get => comClient.ReadAny<uint>(IndexGroup, indexOffset + 0x0F);
        }

        public double HandwheelResolution
        {
            get => comClient.ReadAny<double>(IndexGroup, indexOffset + 0x10);
        }

    }


    ///Contains the index group and index offsets of Axis Channel Status objects. Can be used if you want to add ADS Device notifications, or SumRead commands.
    public class AxisStatusInChannelAdresses
    {
        uint baseOffset;
        internal AxisStatusInChannelAdresses(uint axisIndex) 
        {
            baseOffset = 0x10000 * axisIndex;

        }

        public uint IndexGroup => 0x120101;
        
        public uint ActualPositionPCS => baseOffset + 0x07;

    }
}
