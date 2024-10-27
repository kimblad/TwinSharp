using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Axis status as taken from HLI. https://infosys.beckhoff.com/content/1033/tf5200_hli_interface/174749195.html?id=8719845080216701702
    /// </summary>
    public class AxisStatus
    {
        private readonly AdsClient comClient;
        private const uint indexGroup = 0x120200;
        readonly private uint indexOffset;

        public readonly AxisStatusInChannel? StatusInChannel;

        internal AxisStatus(uint index, AdsClient comClient)
        {
            this.comClient = comClient;
            indexOffset = 0x10000 * index;

            if(ChannelNumber > 0)
                StatusInChannel = new AxisStatusInChannel(comClient, index);
        }


        public ushort Type
        {
            get => comClient.ReadAny<ushort>(indexGroup, indexOffset + 0x01);
        }

        /// <summary>
        /// Command position of current cycle in the axis coordinate system.
        /// </summary>
        public int CommandedPositionACS
        {
            get => comClient.ReadAny<int>(indexGroup, indexOffset + 0x02);
        }

        /// <summary>
        /// Actual position of the current cycle in the axis coordinate system
        /// </summary>
        public int ActualPositionACS
        {
            get => comClient.ReadAny<int>(indexGroup, indexOffset + 0x03);
        }

        /// <summary>
        /// Target position in the current NC block, ACS. This represents the target position of the program coordinate system referred to the axes. It is valid only as long as no transformation is active. Currently, the target position is not transformed back onto the axes.
        /// </summary>
        public int EndPositionACS
        {
            get => comClient.ReadAny<int>(indexGroup, indexOffset + 0x04);
        }

        public double ActiveFeedrate
        {
            get => comClient.ReadAny<double>(indexGroup, indexOffset + 0x05);
        }

        public double CurrentFeedrate
        {
            get => comClient.ReadAny<double>(indexGroup, indexOffset + 0x06);
        }

        public sbyte Direction
        {
            get => comClient.ReadAny<sbyte>(indexGroup, indexOffset + 0x07);
        }

        public uint Mode
        {
            get => comClient.ReadAny<uint>(indexGroup, indexOffset + 0x08);
        }

        public string AxisName
        {
            get => comClient.ReadAny<string>(indexGroup, indexOffset + 0x09);
        }

        public bool InPosition
        {
            get => comClient.ReadAny<bool>(indexGroup, indexOffset + 0x0A);
        }

        public bool HomingDone
        {
            get => comClient.ReadAny<bool>(indexGroup, indexOffset + 0x0B);
        }

        /// <summary>
        /// Even if an axis is not moved in the PCS, a corresponding Cartesian or kinematic transformation may nevertheless execute a motion of the physical axis.
        /// Example: 90° rotation about Z; Y is moved if X is programmed.
        /// </summary>
        public AxisState State
        {
            get => (AxisState)comClient.ReadAny<ushort>(indexGroup, indexOffset + 0x0C);
        }


        /// <summary>
        /// Return the number of the channel to which the axis is assigned, or 0 if unassigned.
        /// </summary>
        public ushort ChannelNumber
        {
            get => comClient.ReadAny<ushort>(indexGroup, indexOffset + 0x0F);
        }

    }
}