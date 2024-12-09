using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class DriveParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal DriveParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x7000 + id;
        }

        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

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
