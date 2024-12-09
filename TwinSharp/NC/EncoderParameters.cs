using TwinCAT.Ads;

namespace TwinSharp.NC
{

    /// <summary>
    /// The EncoderParameters class provides an interface to interact with encoder parameters via an AdsClient.
    /// It allows reading and writing various encoder settings such as ID, name, type, scaling factor, position offset,
    /// count direction, modulo factor, mode, soft end monitoring, soft end positions, evaluation direction, and filter times.
    /// The class uses an AdsClient to communicate with the encoder and retrieve or update these parameters.
    /// </summary>
    public class EncoderParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal EncoderParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x5000 + id;
        }

        /// <summary>
        /// Encoder ID [1 ... 255]
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Encoder name
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

        /// <summary>
        /// Encoder type
        /// </summary>
        public EncoderType Type
        {
            get => (EncoderType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// Resulting scaling factor (numerator / denominator) Note: from TC3 the scaling factor consists of two components – numerator and denominator (default: 1.0). Writing is not allowed if the controller enable has been issued.
        /// </summary>
        public double ScalingFactor
        {
            get => client.ReadAny<double>(indexGroup, 0x00000006);
            set => client.WriteAny(indexGroup, 0x00000006, value);
        }

        /// <summary>
        /// Position offset. Writing is not allowed if the controller enable has been issued.
        /// </summary>
        public double PositionOffset
        {
            get => client.ReadAny<double>(indexGroup, 0x00000007);
            set => client.WriteAny(indexGroup, 0x00000007, value);
        }

        /// <summary>
        /// Encoder count direction. Writing is not allowed if the controller enable has been issued.
        /// </summary>
        public bool EncoderCountDirectionInverted
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x00000008);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x00000008, digit);
            }
        }

        /// <summary>
        /// Modulo factor. 
        /// </summary>
        public double ModuloFactor
        {
            get => client.ReadAny<double>(indexGroup, 0x00000009);
            set => client.WriteAny(indexGroup, 0x00000009, value);
        }

        /// <summary>
        /// Encoder mode.
        /// </summary>
        public EncoderMode EncoderMode
        {
            get => (EncoderMode)client.ReadAny<uint>(indexGroup, 0x0000000A);
            set => client.WriteAny(indexGroup, 0x0000000A, (uint)value);
        }

        /// <summary>
        /// Soft end min. monitoring?
        /// </summary>
        public bool SoftEndMinMonitoring
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x0000000B);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x0000000B, digit);
            }
        }

        /// <summary>
        /// Soft end max. monitoring?
        /// </summary>
        public bool SoftEndMaxMonitoring
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x0000000C);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x0000000C, digit);
            }
        }

        /// <summary>
        /// Soft end position min.
        /// </summary>
        public double SoftEndPositionMin
        {
            get => client.ReadAny<double>(indexGroup, 0x0000000D);
            set => client.WriteAny(indexGroup, 0x0000000D, value);
        }

        /// <summary>
        /// Soft end position max.
        /// </summary>
        public double SoftEndPositionMax
        {
            get => client.ReadAny<double>(indexGroup, 0x0000000E);
            set => client.WriteAny(indexGroup, 0x0000000E, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public EncoderEvaluationDirection EncoderEvaluationDirection
        {
            get => (EncoderEvaluationDirection)client.ReadAny<uint>(indexGroup, 0x0000000F);
            set => client.WriteAny(indexGroup, 0x0000000F, (uint)value);
        }

        /// <summary>
        /// Filter time for actual position value in seconds (P-T1). [0.0 ... 60.0].
        /// </summary>
        public double FilterSecondsPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x00000010);
            set => client.WriteAny(indexGroup, 0x00000010, value);
        }

        /// <summary>
        /// Filter time for actual velocity value in seconds (P-T1). [0.0 ... 60.0].
        /// </summary>
        public double FilterSecondsVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x00000011);
            set => client.WriteAny(indexGroup, 0x00000011, value);
        }

        /// <summary>
        /// Filter time for actual acceleration value in seconds (P-T1). [0.0 ... 60.0].
        /// </summary>
        public double FilterSecondsAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x00000012);
            set => client.WriteAny(indexGroup, 0x00000012, value);
        }
    }
}
