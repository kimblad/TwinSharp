using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.EtherCAT
{
    /// <summary>
    /// Describes an EtherCAT device, using all standard objects as defined from the EtherCAT standard.
    /// This class can be derived and extended to create a specific EtherCAT device.
    /// </summary>
    public class EtherCatDevice
    {
        readonly AdsClient client;

        private const uint indexGroup = 0xF302;

        /// <summary>
        /// Creates a representation of an EtherCAT device. 
        /// The client should typically be connected to the AmsNetId of the EtherCAT master and the port number is the slaves adress (example 1025).
        /// </summary>
        /// <param name="client"></param>
        public EtherCatDevice (AdsClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// The device type of the EtherCAT device.
        /// </summary>
        public uint DeviceType
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1000, 0x0));
        }

        /// <summary>
        /// Returns the error register of the EtherCAT device.
        /// </summary>
        public byte ErrorRegister
        {
            get => client.ReadAny<byte>(indexGroup, CombineIndexAndSubIndex(0x1001, 0x00));
        }

        /// <summary>
        /// This parameter specifies the manufacturers device name of the device.
        /// </summary>
        public string ManufacturerDeviceName
        {
            get => client.ReadAnyString(indexGroup, CombineIndexAndSubIndex(0x1008, 0x00), 80, Encoding.ASCII);
        }

        /// <summary>
        /// This parameter specifies the hardware version of the device.
        /// </summary>
        public string ManufacturerHardwareVersion
        {
            get => client.ReadAnyString(indexGroup, CombineIndexAndSubIndex(0x1009, 0x00), 80, Encoding.ASCII);
        }

        /// <summary>
        /// The Software Version object has the following parameter:
        /// </summary>
        public string ManufacturerSoftwareVersion
        {
            get => client.ReadAnyString(indexGroup, CombineIndexAndSubIndex(0x100A, 0x00), 80, Encoding.ASCII);
        }

        /// <summary>
        /// This parameter specifies the vendor ID of the device.
        /// </summary>
        public uint VendorID
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x1));
        }

        /// <summary>
        /// This parameter specifies the product code of the device.
        /// </summary>
        public uint ProductCode
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x2));
        }

        /// <summary>
        /// The revision number of the EtherCAT device.
        /// </summary>
        public uint RevisionNumber
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x3));
        }

        /// <summary>
        /// The serial number of the EtherCAT device.
        /// </summary>
        public uint SerialNumber
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x4));
        }


        /// <summary>
        /// When using the ADS and reading CoE objects the index and subindex should be combined.
        /// </summary>
        private uint CombineIndexAndSubIndex(uint index, uint subindex)
        {
            return (index << 16) + subindex;
        }
    }
}
