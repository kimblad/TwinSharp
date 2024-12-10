using System.Security.Cryptography;
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


        internal AxisStatus(uint index, AdsClient comClient)
        {
            this.comClient = comClient;
            indexOffset = 0x10000 * index;

            if(ChannelNumber > 0)
                StatusInChannel = new AxisStatusInChannel(comClient, index);
        }

        /// <summary>
        /// If the axis belongs to a channel, this class can be used to get the status of the axis in that channel.
        /// </summary>
        public AxisStatusInChannel? StatusInChannel
        {
            get;
            private set;
        }


        /// <summary>
        /// Type of axis.
        /// 1 = Translator.
        /// 2 = Rotator.
        /// 4 = Spindle.
        /// </summary>
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

        /// <summary>
        /// Active feedrate of the axis in mm/s.
        /// </summary>
        public double ActiveFeedrate
        {
            get => comClient.ReadAny<double>(indexGroup, indexOffset + 0x05);
        }

        /// <summary>
        /// Current feedrate of the axis in mm/s.
        /// </summary>
        public double CurrentFeedrate
        {
            get => comClient.ReadAny<double>(indexGroup, indexOffset + 0x06);
        }

        /// <summary>
        /// Positive sign if the last output setpoint generated led to a positive direction of motion.
        /// Negative sign if the last output setpoint generated led to a negative direction of motion.
        /// 0 if stationary.
        /// </summary>
        public sbyte Direction
        {
            get => comClient.ReadAny<sbyte>(indexGroup, indexOffset + 0x07);
        }

        /// <summary>
        /// The axis mode configured in the axis parameter list (P-AXIS-00015) is indicated.
        /// </summary>
        public uint Mode
        {
            get => comClient.ReadAny<uint>(indexGroup, indexOffset + 0x08);
        }

        /// <summary>
        /// Name of the logical axis used for the current reference in the current automatic
        /// program / manual block(e.g.X, Y, Z). This may be changed by default when the
        /// channel(SDA-MDS list) is programmed or dynamically in the NC program by
        /// means of a swap command.
        /// </summary>
        public string AxisName
        {
            get => comClient.ReadAny<string>(indexGroup, indexOffset + 0x09);
        }

        /// <summary>
        /// The axis is located in position, i.e. the control window is reached and the axis is not interpolated
        /// </summary>
        public bool InPosition
        {
            get => comClient.ReadAny<bool>(indexGroup, indexOffset + 0x0A);
        }

        /// <summary>
        /// The axis completed homing successfully and is now referenced.
        /// </summary>
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