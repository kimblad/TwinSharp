using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class EncoderState
    {
        readonly AdsClient client;
        readonly uint indexGroup;
        internal EncoderState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x5100 + id;
        }

        public uint ErrorCode
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public double ActualPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x02);
        }

        public double ActualPositionModulo
        {
            get => client.ReadAny<double>(indexGroup, 0x03);
        }

        public int ActualModuloRotation
        {
            get => client.ReadAny<int>(indexGroup, 0x04);
        }

        public double ActualVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x05);
        }

        public double ActualAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x06);
        }

        public int ActualIncrements
        {
            get => client.ReadAny<int>(indexGroup, 0x07);
        }

        public int SoftwareActualIncrements
        {
            get => client.ReadAny<int>(indexGroup, 0x08);
        }


        /// <summary>
        /// "Calibrate flag"
        /// </summary>
        public bool ReferenceFlag
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x09);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x09, digit);
            }
        }


        /// <summary>
        /// Measuring system error correction
        /// </summary>
        public double ActualPositionCorrectionValue
        {
            get => client.ReadAny<double>(indexGroup, 0x0A);
        }

        public double ActualPositionWithoutCompensation
        {
            get => client.ReadAny<double>(indexGroup, 0x0B);
        }

        public double ActualPositionDueToDeadTimeCompensation
        {
            get => client.ReadAny<double>(indexGroup, 0x0C);
        }


        /// <summary>
        /// Sum of time shift for encoder dead time (parameterized and variable dead time).
        /// Note: A dead time is specified in the system as a positive value.
        /// </summary>
        public double TimeShiftSumDueToDeadTimeCompensation
        {
            get => client.ReadAny<double>(indexGroup, 0x0D);
        }

        /// <summary>
        /// Charge with actual position compensation value.
        /// </summary>
        public double ActualPositionUnfiltered
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
        }

        /// <summary>
        /// Filtered actual position (offset with actual position correction value, without dead time compensation)
        /// </summary>
        public double ActualPositionFiltered
        {
            get => client.ReadAny<double>(indexGroup, 0x13);
        }


        /// <summary>
        /// Optional: actual drive velocity(transferred directly from SoE, CoE or MDP 742 drive)
        /// Base Unit / s
        /// New from TC3.1 B4020.30
        /// </summary>
        public double ActualDriveVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x14);
        }

        /// <summary>
        /// Optional: Unfiltered actual velocity
        /// Base Unit / s
        /// </summary>
        public double ActualVelocityUnfiltered
        {
            get => client.ReadAny<double>(indexGroup, 0x15);
        }


        public void ReadActualPositionBuffer(out double position, out double time)
        {
            //TODO: check on real CoE drive (not soft encoder).
            var buffer = new Memory<byte>(new byte[16]);

            client.Read(indexGroup, 0x10, buffer);

            position = BitConverter.ToDouble(buffer.Span.Slice(0, 8));
            time = BitConverter.ToDouble(buffer.Span.Slice(8, 8));
        }
    }
}
