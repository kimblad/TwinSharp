using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The EncoderState class provides access to various encoder states and properties 
    /// through an AdsClient instance. It allows reading and writing of encoder-related 
    /// data such as error codes, actual positions, velocities, accelerations, and other 
    /// relevant metrics.
    /// </summary>

    public class EncoderState
    {
        readonly AdsClient client;
        readonly uint indexGroup;
        internal EncoderState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x5100 + id;
        }

        /// <summary>
        /// Error state encoder
        /// </summary>
        public uint ErrorCode
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Actual position (charge with actual position compensation value). Symbol: "ActPos".
        /// </summary>
        public double ActualPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x02);
        }

        /// <summary>
        /// Modulo actual position. Symbol: "ActPosModulo".
        /// </summary>
        public double ActualPositionModulo
        {
            get => client.ReadAny<double>(indexGroup, 0x03);
        }

        /// <summary>
        /// Modulo actual rotation.
        /// </summary>
        public int ActualModuloRotation
        {
            get => client.ReadAny<int>(indexGroup, 0x04);
        }

        /// <summary>
        /// Optional: Actual velocity. Symbol: "ActVel".
        /// </summary>
        public double ActualVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x05);
        }

        /// <summary>
        /// Optional: Actual acceleration. Symbol: "ActAcc".
        /// </summary>
        public double ActualAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x06);
        }

        /// <summary>
        /// Encoder actual increments.
        /// </summary>
        public int ActualIncrements
        {
            get => client.ReadAny<int>(indexGroup, 0x07);
        }

        /// <summary>
        /// Software actual increment counter.
        /// </summary>
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
        /// Actual position correction value (measuring system error correction).
        /// </summary>
        public double ActualPositionCorrectionValue
        {
            get => client.ReadAny<double>(indexGroup, 0x0A);
        }

        /// <summary>
        /// Actual position without actual position compensation value.
        /// </summary>
        public double ActualPositionWithoutCompensation
        {
            get => client.ReadAny<double>(indexGroup, 0x0B);
        }

        /// <summary>
        /// Actual position compensation value due to the dead time compensation.
        /// </summary>
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


        /// <summary>
        /// Read the actual position buffer.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="position"></param>
        public void ReadActualPositionBuffer(out uint timestamp, out double position)
        {
            //TODO: check on real CoE drive (not soft encoder).
            var buffer = new byte [16];

            client.Read(indexGroup, 0x16, buffer.AsMemory());

            var br = new BinaryReader(new MemoryStream(buffer));

            timestamp = br.ReadUInt32();
            br.ReadUInt32(); //skip 4 bytes
            position = br.ReadDouble();
        }
    }
}
