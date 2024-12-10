using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// If an axis belongs to a channel, this class describes the status of that axis in that channel.
    /// </summary>
    public class AxisStatusInChannel
    {
        private readonly AdsClient comClient;
        const uint IndexGroup = 0x120101;
        private readonly uint indexOffset;


        internal AxisStatusInChannel(AdsClient comClient, uint axisIndex)
        {
            this.comClient = comClient;
            indexOffset = 0x10000 * axisIndex;

            Adresses = new AxisStatusInChannelAdresses(axisIndex);
        }

        /// <summary>
        /// Contains the index group and index offsets of Axis Channel Status objects. Can be used if you want to add ADS Device notifications, or SumRead commands.
        /// </summary>
        public AxisStatusInChannelAdresses Adresses { get; private set; }


        /// <summary>
        /// Logical number of the axis.
        /// </summary>
        public ushort LogicalNumber
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x01);
        }

        /// <summary>
        /// Name of the axis.
        /// </summary>
        public string Name
        {
            get => comClient.ReadString(IndexGroup, indexOffset + 0x02, 16);
        }


        /// <summary>
        /// Type of axis.
        /// 1 = Translator.
        /// 2 = Rotator.
        /// 4 = Spindle.
        /// </summary>
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

        /// <summary>
        /// Axis state in the PCS.
        /// </summary>
        public AxisState AxisStatePCS
        {
            get => (AxisState)comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x09);
        }

        /// <summary>
        /// Manual state.
        /// </summary>
        public ushort ManualState
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x0A);
        }

        /// <summary>
        /// Operation mode.
        /// </summary>
        public ushort OperationMode
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x0B);
        }

        /// <summary>
        /// Control element.
        /// </summary>
        public ushort ControlElement
        {
            get => comClient.ReadAny<ushort>(IndexGroup, indexOffset + 0x0C);
        }

        /// <summary>
        /// Continuous speed.
        /// </summary>
        public uint ContinuousSpeed
        {
            get => comClient.ReadAny<uint>(IndexGroup, indexOffset + 0x0D);
        }

        /// <summary>
        /// Incremental speed.
        /// </summary>
        public uint IncrementalSpeed
        {
            get => comClient.ReadAny<uint>(IndexGroup, indexOffset + 0x0E);
        }

        /// <summary>
        /// Incremental distance.
        /// </summary>
        public uint IncrementalDistance
        {
            get => comClient.ReadAny<uint>(IndexGroup, indexOffset + 0x0F);
        }

        /// <summary>
        /// Handwheel resolution.
        /// </summary>
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

        /// <summary>
        /// Index group for all Axis Channel Status objects.
        /// </summary>
        public uint IndexGroup => 0x120101;

        /// <summary>
        /// Sub index group of the actual position in the PCS.
        /// </summary>
        public uint ActualPositionPCS => baseOffset + 0x07;

    }
}
