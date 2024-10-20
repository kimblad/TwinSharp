using TwinCAT.Ads;
using TwinCAT.PlcOpen;

namespace TwinSharp
{
    public class AxisParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal AxisParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4000 + id;
        }

        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 80);
        }

        public AxisType Type
        {
            get => (AxisType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        public uint CycleTime
        {
            get => client.ReadAny<uint>(indexGroup, 0x04);
        }

        public string PhysicalUnit
        {
            get => client.ReadString(indexGroup, 0x05, 10);
        }

        public double RefVelocityCamDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x06);
            set => client.WriteAny(indexGroup, 0x06, value);
        }

        public double RefVelocitySyncDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x07);
            set => client.WriteAny(indexGroup, 0x07, value);
        }

        public double VelocityHandSlow
        {
            get => client.ReadAny<double>(indexGroup, 0x08);
            set => client.WriteAny(indexGroup, 0x08, value);
        }

        public double VelocityHandFast
        {
            get => client.ReadAny<double>(indexGroup, 0x09);
            set => client.WriteAny(indexGroup, 0x09, value);
        }

        public double VelocityRapidTraverse
        {
            get => client.ReadAny<double>(indexGroup, 0x0A);
            set => client.WriteAny(indexGroup, 0x0A, value);
        }

        public bool PositionRangeMonitoringEnabled
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x0F);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x0F, digit);
            }
        }

        public double PositionRangeMonitoringWindow
        {
            get => client.ReadAny<double>(indexGroup, 0x10);
            set => client.WriteAny(indexGroup, 0x10, value);
        }

        public bool MotionMonitoringEnabled
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x11);
                return digit == 1;
            }
            set
            {
               ushort digit = (ushort)(value ? 1 : 0);
               client.WriteAny(indexGroup, 0x11, digit);
            }
        }

        public double MotionMonitoringSeconds
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
            set => client.WriteAny(indexGroup, 0x12, value);
        }

        public bool LoopEnabled
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x13);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x13, digit);
            }
        }


        public double LoopingDistance
        {
            get => client.ReadAny<double>(indexGroup, 0x14);
            set => client.WriteAny(indexGroup, 0x14, value);
        }

        public bool TargetPositionMonitoringEnabled
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x15);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x15, digit);
            }
        }

        public double TargetPositionMonitoringWindow
        {
            get => client.ReadAny<double>(indexGroup, 0x16);
            set => client.WriteAny(indexGroup, 0x16, value);
        }

        public double TargetPositionMonitoringSeconds
        {
            get => client.ReadAny<double>(indexGroup, 0x17);
            set => client.WriteAny(indexGroup, 0x17, value);
        }

        public double PulseWayPositiveDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x18);
            set => client.WriteAny(indexGroup, 0x18, value);
        }

        public double PulseWayNegativeDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x19);
            set => client.WriteAny(indexGroup, 0x19, value);
        }

        public ErrorReactionMode ErrorReactionMode
        {
            get => (ErrorReactionMode)client.ReadAny<uint>(indexGroup, 0x1A);
            set => client.WriteAny(indexGroup, 0x1A, (uint)value);
        }

        public double ErrorDelaySeconds
        {
            get => client.ReadAny<double>(indexGroup, 0x1B);
            set => client.WriteAny(indexGroup, 0x1B, value);
        }

        public uint ChannelID
        {
            get => client.ReadAny<uint>(indexGroup, 0x51);
        }

        public string ChannelName
        {
            get => client.ReadString(indexGroup, 0x52, 30);
        }

        public ChannelType ChannelType
        {
            get => (ChannelType)client.ReadAny<uint>(indexGroup, 0x53);
        }

        public uint GroupID
        {
            get => client.ReadAny<uint>(indexGroup, 0x54);
        }

        public string GroupName
        {
            get => client.ReadString(indexGroup, 0x55, 30);
        }

        public GroupType GroupType
        {
            get => (GroupType)client.ReadAny<uint>(indexGroup, 0x56);
        }

        public uint EncoderCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x57);
        }

        /// <summary>
        /// Read all encoder IDs, controller IDs and drive IDs
        /// </summary>
        internal void ReadAllSubElements(out uint[] encoderIDs, out uint[] controllerIDs, out uint[] driveIDs)
        {
            var buffer = new Memory<byte>(new byte[108]);
            client.Read(indexGroup, 0x5A, buffer);
            var bytesRead = buffer.ToArray();


            encoderIDs = new uint[EncoderCount];
            for (int i = 0; i < encoderIDs.Length; i++)
            {
                encoderIDs[i] = bytesRead[i];
            }


            controllerIDs = new uint[ControllerCount];
            for (int i = 0; i < controllerIDs.Length; i++)
            {
                //Controller IDs are stored offseted from position 36.
                controllerIDs[i] = bytesRead[i + 36];
            }

            driveIDs = new uint[DriveCount];
            for (int i = 0; i < driveIDs.Length; i++)
            {
                //Drive IDs are stored offseted from position 72.
                driveIDs[i] = bytesRead[i + 72];
            }
        }

        public uint[] EncoderIDs
        {
            get
            {
                ReadAllSubElements(out uint[] encoderIDs, out _, out _);
                return encoderIDs;
            }
        }

        public uint ControllerCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x58);
        }

        public uint DriveCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x59);
        }

        public double MaxPermittedAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0xF1);
            set => client.WriteAny(indexGroup, 0xF1, value);
        }

        public double MaxPermittedDeceleration
        {
            get => client.ReadAny<double>(indexGroup, 0xF2);
            set => client.WriteAny(indexGroup, 0xF2, value);
        }

        /// <summary>
        /// Default data set (e.g. mm/s^2)
        /// </summary>
        public double Acceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x101);
            set => client.WriteAny(indexGroup, 0x101, value);
        }

        /// <summary>
        /// Default data set (e.g. mm/s^2)
        /// </summary>
        public double Deceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x102);
            set => client.WriteAny(indexGroup, 0x102, value);
        }

        /// <summary>
        /// Default data set (e.g. mm/s^3)
        /// </summary>
        public double Jerk
        {
            get => client.ReadAny<double>(indexGroup, 0x103);
            set => client.WriteAny(indexGroup, 0x103, value);
        }

    }
}
