using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The DriveParameters class provides an interface to interact with drive parameters
    /// in a TwinCAT system using the AdsClient. It allows reading and writing various
    /// properties of a drive such as its ID, name, type, and motor polarity inversion status.
    /// </summary>
    public class DriveParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal DriveParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x7000 + id;
        }

        /// <summary>
        /// The ID of the drive.
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }


        /// <summary>
        /// The name of the drive.
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }


        /// <summary>
        /// The type of the drive.
        /// </summary>
        public NcDriveType Type
        {
            get => (NcDriveType)client.ReadAny<uint>(indexGroup, 0x03);
        }


        /// <summary>
        /// Writing is not allowed if the controller enable has been issued.
        /// </summary>
        public bool MotorPolarityInverted
        {
            get
            {
                var digit = client.ReadAny<ushort>(indexGroup, 0x06);
                return digit == 1;
            }
            set
            {
                ushort digit = (ushort)(value ? 1 : 0);
                client.WriteAny(indexGroup, 0x06, digit);
            }
        }


    }
}
