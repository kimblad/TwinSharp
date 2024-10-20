using TwinCAT.Ads;

namespace TwinSharp
{
    public class EncoderParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal EncoderParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x5000 + id;
        }

        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

        public EncoderType Type
        {
            get => (EncoderType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        public double ScalingFactor
        {
            get => client.ReadAny<double>(indexGroup, 0x00000006);
            set => client.WriteAny(indexGroup, 0x00000006, value);
        }

        public double PositionOffset
        {
            get => client.ReadAny<double>(indexGroup, 0x00000007);
            set => client.WriteAny(indexGroup, 0x00000007, value);
        }

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

        public double ModuloFactor
        {
            get => client.ReadAny<double>(indexGroup, 0x00000009);
            set => client.WriteAny(indexGroup, 0x00000009, value);
        }

        public uint EncoderMode
        {
            get => client.ReadAny<uint>(indexGroup, 0x0000000A);
            set => client.WriteAny(indexGroup, 0x0000000A, value);
        }

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

        public double SoftEndPositionMin
        {
            get => client.ReadAny<double>(indexGroup, 0x0000000D);
            set => client.WriteAny(indexGroup, 0x0000000D, value);
        }

        public double SoftEndPositionMax
        {
            get => client.ReadAny<double>(indexGroup, 0x0000000E);
            set => client.WriteAny(indexGroup, 0x0000000E, value);
        }

        public uint EncoderEvaluationDirection
        {
            get => client.ReadAny<uint>(indexGroup, 0x0000000F);
            set => client.WriteAny(indexGroup, 0x0000000F, value);
        }

        public double FilterSecondsPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x00000010);
            set => client.WriteAny(indexGroup, 0x00000010, value);
        }

        public double FilterSecondsVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x00000011);
            set => client.WriteAny(indexGroup, 0x00000011, value);
        }

        public double FilterSecondsAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x00000012);
            set => client.WriteAny(indexGroup, 0x00000012, value);
        }
    }
}
