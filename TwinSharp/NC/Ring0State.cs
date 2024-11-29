using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the state of the Ring 0 system, providing access to various counts and IDs
    /// for channels, groups, axes, encoders, controllers, drives, and tables.
    /// </summary>
    public class Ring0State
    {
        private readonly AdsClient client;
        const uint indexGroup = 0x1100;

        internal Ring0State(AdsClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Quantity of Channel
        /// 0, 1...255
        /// </summary>
        public uint ChannelCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Quantity of group
        /// 0, 1...255
        /// </summary>
        public uint GroupCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x02);
        }

        /// <summary>
        /// Quantity of Axis
        /// 0, 1...255
        /// </summary>
        public uint AxisCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// Quantity of Encoder
        /// 0, 1...255
        /// </summary>
        public uint EncoderCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// Quantity of controller
        /// 0, 1...255
        /// </summary>
        public uint ControllerCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

        /// <summary>
        /// Quantity of Drives
        /// 0, 1...255
        /// </summary>
        public uint DriveCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x06);
        }

        /// <summary>
        /// Quantity of table (n x m)
        /// 0, 1...255
        /// </summary>
        public uint TableCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x0A);
        }

        /// <summary>
        /// Supplies the Channel IDs for all Channels in the system
        /// </summary>
        public uint[] ChannelIds
        {
            get
            {
                int count = (int)ChannelCount;
                if (count == 0)
                    return [];

                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x31, [count]);
                return ids;
            }
        }

        /// <summary>
        /// Supplies the group IDs for all groups in the system
        /// </summary>
        public uint[] GroupIds
        {
            get
            {
                int count = (int)GroupCount;
                if (count == 0)
                    return [];

                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x32, [count]);
                return ids;
            }
        }

        /// <summary>
        /// Supplies the axis IDs for all axes in the system
        /// </summary>
        public uint[] AxisIds
        {
            get
            {
                int count = (int)AxisCount;
                if (count == 0)
                    return [];

                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x33, [count]);
                return ids;
            }
        }

        /// <summary>
        /// Supplies the encoder IDs for all encoders in the system
        /// </summary>
        public uint[] EncoderIds
        {
            get
            {
                int count = (int)EncoderCount;
                if (count == 0)
                    return [];

                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x34, [count]);
                return ids;
            }
        }

        /// <summary>
        /// Supplies the controller IDs for all controllers in the system
        /// </summary>
        public uint[] ControllerIds
        {
            get
            {
                int count = (int)ControllerCount;
                if (count == 0)
                    return [];

                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x35, [count]);
                return ids;
            }
        }

        /// <summary>
        /// Supplies the drive IDs for all drives in the system
        /// </summary>
        public uint[] DriveIds
        {
            get
            {
                int count = (int)DriveCount;
                if (count == 0)
                    return [];
                if (count == 0)
                    return [];
                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x36, [count]);
                return ids;
            }
        }

        /// <summary>
        /// Supplies the table IDs for all tables in the system
        /// </summary>
        public uint[] TableIds
        {
            get
            {
                int count = (int)TableCount;
                if (count == 0)
                    return [];

                uint[] ids = client.ReadAny<uint[]>(indexGroup, 0x3A, [count]);
                return ids;
            }
        }
    }
}
