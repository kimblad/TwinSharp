using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the parameters of an axis in a TwinCAT NC system.
    /// This class provides properties to get and set various parameters of an axis,
    /// such as ID, name, type, cycle time, physical unit, velocities, monitoring settings,
    /// error reaction mode, and more. It uses an AdsClient to read and write these parameters
    /// from a TwinCAT system. The class also includes methods to read all sub-elements like
    /// encoder IDs, controller IDs, and drive IDs.
    /// </summary>
    public class AxisParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal AxisParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4000 + id;
        }

        /// <summary>
        /// Axis ID
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Axis name
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 80).TrimEnd('\0');
        }

        /// <summary>
        /// Axis type
        /// </summary>
        public AxisType Type
        {
            get => (AxisType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// Cycle time axis (SEC)
        /// </summary>
        public uint CycleTime
        {
            get => client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// Physical unit
        /// </summary>
        public string PhysicalUnit
        {
            get => client.ReadString(indexGroup, 0x05, 10);
        }

        /// <summary>
        /// Ref. velocity in cam direction
        /// </summary>
        public double RefVelocityCamDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x06);
            set => client.WriteAny(indexGroup, 0x06, value);
        }

        /// <summary>
        /// Ref. velocity in sync direction
        /// </summary>
        public double RefVelocitySyncDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x07);
            set => client.WriteAny(indexGroup, 0x07, value);
        }

        /// <summary>
        /// Velocity hand slow
        /// </summary>
        public double VelocityHandSlow
        {
            get => client.ReadAny<double>(indexGroup, 0x08);
            set => client.WriteAny(indexGroup, 0x08, value);
        }

        /// <summary>
        /// Velocity hand fast
        /// </summary>
        public double VelocityHandFast
        {
            get => client.ReadAny<double>(indexGroup, 0x09);
            set => client.WriteAny(indexGroup, 0x09, value);
        }

        /// <summary>
        /// Velocity rapid traverse
        /// </summary>
        public double VelocityRapidTraverse
        {
            get => client.ReadAny<double>(indexGroup, 0x0A);
            set => client.WriteAny(indexGroup, 0x0A, value);
        }

        /// <summary>
        /// Position range monitoring?
        /// </summary>
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

        /// <summary>
        /// Position range monitoring window
        /// </summary>
        public double PositionRangeMonitoringWindow
        {
            get => client.ReadAny<double>(indexGroup, 0x10);
            set => client.WriteAny(indexGroup, 0x10, value);
        }

        /// <summary>
        /// Motion monitoring enabled
        /// </summary>
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

        /// <summary>
        /// Motion monitoring time.
        /// </summary>
        public double MotionMonitoringSeconds
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
            set => client.WriteAny(indexGroup, 0x12, value);
        }

        /// <summary>
        /// Loop enabled
        /// </summary>
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

        /// <summary>
        /// Looping distance (±) e.g. mm
        /// </summary>
        public double LoopingDistance
        {
            get => client.ReadAny<double>(indexGroup, 0x14);
            set => client.WriteAny(indexGroup, 0x14, value);
        }

        /// <summary>
        /// Target position monitoring enabled
        /// </summary>
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

        /// <summary>
        /// Target position monitoring window e.g. mm
        /// </summary>
        public double TargetPositionMonitoringWindow
        {
            get => client.ReadAny<double>(indexGroup, 0x16);
            set => client.WriteAny(indexGroup, 0x16, value);
        }

        /// <summary>
        /// Target position monitoring time in seconds.
        /// </summary>
        public double TargetPositionMonitoringSeconds
        {
            get => client.ReadAny<double>(indexGroup, 0x17);
            set => client.WriteAny(indexGroup, 0x17, value);
        }

        /// <summary>
        /// Pulse way in pos. direction e.g. mm
        /// </summary>
        public double PulseWayPositiveDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x18);
            set => client.WriteAny(indexGroup, 0x18, value);
        }

        /// <summary>
        /// Pulse way in neg. direction e.g. mm
        /// </summary>
        public double PulseWayNegativeDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x19);
            set => client.WriteAny(indexGroup, 0x19, value);
        }

        /// <summary>
        /// Error reaction mode: 0: instantaneous (default) 1: delayed (e.g. for Master/Slave-coupling)
        /// </summary>
        public ErrorReactionMode ErrorReactionMode
        {
            get => (ErrorReactionMode)client.ReadAny<uint>(indexGroup, 0x1A);
            set => client.WriteAny(indexGroup, 0x1A, (uint)value);
        }

        /// <summary>
        /// Error delay time (if delayed error reaction is selected)
        /// </summary>
        public double ErrorDelaySeconds
        {
            get => client.ReadAny<double>(indexGroup, 0x1B);
            set => client.WriteAny(indexGroup, 0x1B, value);
        }

        /// <summary>
        /// Channel ID
        /// </summary>
        public uint ChannelID
        {
            get => client.ReadAny<uint>(indexGroup, 0x51);
        }

        /// <summary>
        /// Channel name
        /// </summary>
        public string ChannelName
        {
            get => client.ReadString(indexGroup, 0x52, 30);
        }

        /// <summary>
        /// Channel type
        /// </summary>
        public ChannelType ChannelType
        {
            get => (ChannelType)client.ReadAny<uint>(indexGroup, 0x53);
        }

        /// <summary>
        /// Group ID
        /// </summary>
        public uint GroupID
        {
            get => client.ReadAny<uint>(indexGroup, 0x54);
        }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName
        {
            get => client.ReadString(indexGroup, 0x55, 30);
        }

        /// <summary>
        /// Group type
        /// </summary>
        public GroupType GroupType
        {
            get => (GroupType)client.ReadAny<uint>(indexGroup, 0x56);
        }

        /// <summary>
        /// Number of encoders that belong to this axis.
        /// </summary>
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

        /// <summary>
        /// Axis encoder IDs
        /// </summary>
        public uint[] EncoderIDs
        {
            get
            {
                ReadAllSubElements(out uint[] encoderIDs, out _, out _);
                return encoderIDs;
            }
        }

        /// <summary>
        /// Number of controllers that belong to this axis.
        /// </summary>
        public uint ControllerCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x58);
        }

        /// <summary>
        /// Number of drives that belong to this axis.
        /// </summary>
        public uint DriveCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x59);
        }

        /// <summary>
        /// Maximum permitted acceleration
        /// </summary>
        public double MaxPermittedAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0xF1);
            set => client.WriteAny(indexGroup, 0xF1, value);
        }

        /// <summary>
        /// Maximum permitted deceleration
        /// </summary>
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

        /// <summary>
        /// Reference velocity at reference output (velocity pre-control)
        /// </summary>
        public double RefVelocityAtRefOutput
        {
            get => client.ReadAny<double>(indexGroup, 0x00030101);
            set => client.WriteAny(indexGroup, 0x00030101, value);
        }

    }
}
