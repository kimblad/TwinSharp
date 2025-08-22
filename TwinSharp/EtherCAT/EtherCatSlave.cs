using TwinCAT.Ads;
using TwinCAT.Router.Native;

namespace TwinSharp.EtherCAT
{

    /// <summary>
    /// Represents an EtherCAT slave device and provides methods to interact with it via TwinCAT ADS.
    /// </summary>
    public class EtherCatSlave
    {
        readonly AdsClient client;
        readonly uint slaveIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherCatSlave"/> class, representing an EtherCAT slave device.
        /// </summary>
        /// <param name="client">An ADS client that is connected to the EtherCAT master.</param>
        /// <param name="adress">The index of the EtherCAT slave device within the network. Typically starts at 1001.</param>
        public EtherCatSlave(AdsClient client, uint adress)
        {
            this.client = client;
            this.slaveIndex = adress;
        }


        /// <summary>
        /// Allows the CRC error counters of the individual ports (A, D, B and C) of a slave to be read. Equivavalent to the function block FB_EcGetSlaveCrcErrorEx.
        /// </summary>
        /// <returns></returns>
        public ST_EcCrcErrorEx CrcError
        {
            get => client.ReadAny<ST_EcCrcErrorEx>(0x12, slaveIndex);

            /*
            //This read operation returns a variable length of bytes, depending on the number of ports that the slave has.
            byte[] buffer = new byte[4 * 4];
            var readBuffer = new Memory<byte>(buffer);

            int byteCountRead = client.Read(0x12, slaveIndex, readBuffer);

            var crcErrors = new ST_EcCrcErrorEx();

            if(byteCountRead > 0)
                crcErrors.PortACount = BitConverter.ToUInt32(buffer, 0);

            if(byteCountRead > 4)
                crcErrors.PortBCount = BitConverter.ToUInt32(buffer, 4);

            if(byteCountRead > 8)
                crcErrors.PortCCount = BitConverter.ToUInt32(buffer, 8);

            if(byteCountRead > 12)
                crcErrors.PortDCount = BitConverter.ToUInt32(buffer, 12);

            return crcErrors;
            */
        }

        /// <summary>
        /// Can be used to read the CANopen identity of an individual EtherCAT slave device. Equivavalent to the function block FB_EcGetSlaveIdentity.
        /// </summary>
        /// <returns></returns>
        public ST_EcSlaveIdentity Identity
        {
            get => client.ReadAny<ST_EcSlaveIdentity>(0x11, slaveIndex);
        }

        /// <summary>
        /// Allows the EtherCAT status and the Link status of an individual EtherCAT slave to be read. Equivavalent to the function block FB_EcGetSlaveState.
        /// </summary>
        public ST_EcSlaveState State
        {
            get => client.ReadAny<ST_EcSlaveState>(0x09, slaveIndex);
        }

        /// <summary>
        /// With this function a slave can be requested to a specified EtherCAT state. Equivavalent to the function block FB_EcReqSlaveState.
        /// </summary>
        /// <param name="state"></param>
        public void RequestState(EcDeviceState state)
        {
            client.WriteAny(0x9, slaveIndex, (ushort)state);
        }

        /// <summary>
        /// Write any object to the CoE object dictionary.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="subIndex"></param>
        /// <param name="value"></param>
        public void CoeWriteAny(uint index, uint subIndex, object value)
        {
            client.WriteAny(0xF302, CombineIndexAndSubIndex(index, subIndex), value);
        }

        /// <summary>
        /// Read any object from the CoE object dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="subIndex"></param>
        /// <returns></returns>
        public T CoeReadAny<T>(uint index, uint subIndex)
        {
            return client.ReadAny<T>(0xF302, CombineIndexAndSubIndex(index, subIndex));
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
