using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace TwinSharp
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

        public uint DeviceType
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1000, 0x0));
        }

        public byte ErrorRegister
        {
            get => client.ReadAny<byte>(indexGroup, CombineIndexAndSubIndex(0x1001, 0x00));
        }

        public string ManufacturerDeviceName
        {
            get => client.ReadAnyString(indexGroup, CombineIndexAndSubIndex(0x1008, 0x00), 80, Encoding.ASCII);
        }

        public string ManufacturerHardwareVersion
        {
            get => client.ReadAnyString(indexGroup, CombineIndexAndSubIndex(0x1009, 0x00), 80, Encoding.ASCII);
        }
        public string ManufacturerSoftwareVersion
        {
            get => client.ReadAnyString(indexGroup, CombineIndexAndSubIndex(0x100A, 0x00), 80, Encoding.ASCII);
        }

        public uint VendorID
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x1));
        }

        public uint ProductCode
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x2));
        }

        public uint RevisionNumber
        {
            get => client.ReadAny<uint>(indexGroup, CombineIndexAndSubIndex(0x1018, 0x3));
        }

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
