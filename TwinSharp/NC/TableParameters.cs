using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The TableParameters class provides methods to interact with table parameters in a TwinCAT ADS system.
    /// It allows reading and writing various table properties such as ID, Name, SubType, MainType, LineCount, ColumnCount, TotalCount, StepWidth, MasterPeriod, and SlaveDifferencePerMasterPeriod.
    /// It also provides methods to get and set the activation mode for online changes, read and write single values in the table, and convert slave positions to master positions.
    /// </summary>
    public class TableParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal TableParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0xA000 + id;
        }

        /// <summary>
        /// Table ID
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Table name
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

        /// <summary>
        /// Table sub type
        /// </summary>
        public TableSubType SubTupe
        {
            get => (TableSubType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// Table main type
        /// </summary>
        public TableMainType MainType
        {
            get => (TableMainType)client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// Number of lines (n)
        /// </summary>
        public uint LineCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x10);
        }

        /// <summary>
        /// Number of columns (m)
        /// </summary>
        public uint ColumnCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x11);
        }

        /// <summary>
        /// Total number of elements (n*m)
        /// </summary>
        public uint TotalCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x12);
        }

        /// <summary>
        /// Step width (position delta) (equidistant table )
        /// Equidistant Tables
        /// Base unit (e.g. mm)
        /// </summary>
        public double StepWidth
        {
            get => client.ReadAny<double>(indexGroup, 0x13);
        }

        /// <summary>
        /// Master period (cyclic table).
        /// Base unit (e.g. degree)
        /// </summary>
        public double MasterPeriod
        {
            get => client.ReadAny<double>(indexGroup, 0x14);
        }

        /// <summary>
        /// Slave difference per master period (cyclic table).
        /// Base unit (e.g. degree)
        /// </summary>
        public double SlaveDifferencePerMasterPeriod
        {
            get => client.ReadAny<double>(indexGroup, 0x15);
        }

        /// <summary>
        /// Get activation mode for online change from table data (only MF).
        /// </summary>
        /// <param name="activationMode">Activation mode: 0: 'instantaneous' (default) 1: 'master cam pos.' 2: 'master' axis pos.' 3: 'next cycle' 4: 'next cycle once' 5: 'as soon as possible' 6: 'off' 7: 'delete queued data'</param>
        /// <param name="activationPosition">Activation position (e.g. mm)</param>
        /// <param name="masterScalingType">Master scaling type 0: user defined (default) 1: scaling with auto offset 2: off</param>
        /// <param name="slaveScalingType">Slave scaling type 0: user defined (default) 1: scaling with auto offset 2: off</param>
        public void GetActivationMode(out TableActivationMode activationMode, out double activationPosition, out CamScalingMode masterScalingType, out CamScalingMode slaveScalingType)
        {
            var buffer = new Memory<byte>(new byte[20]);

            client.Read(indexGroup, 0x1A, buffer);

            var ms = new MemoryStream(buffer.ToArray());
            var br = new BinaryReader(ms);

            activationMode = (TableActivationMode)br.ReadUInt32();
            activationPosition = br.ReadDouble();
            masterScalingType = (CamScalingMode)br.ReadUInt32();
            slaveScalingType = (CamScalingMode)br.ReadUInt32();
        }

        /// <summary>
        /// Set Activation mode for online change from table data (only MF).
        /// </summary>
        /// <param name="activationMode">Activation mode: 0: 'instantaneous' (default) 1: 'master cam pos.' 2: 'master' axis pos.' 3: 'next cycle' 4: 'next cycle once' 5: 'as soon as possible' 6: 'off' 7: 'delete queued data'</param>
        /// <param name="activationPosition">Activation position (e.g. mm)</param>
        /// <param name="masterScalingType">Master scaling type 0: user defined (default) 1: scaling with auto offset 2: off</param>
        /// <param name="slaveScalingType">Slave scaling type 0: user defined (default) 1: scaling with auto offset 2: off</param>
        public void SetActivationMode(TableActivationMode activationMode, double activationPosition, CamScalingMode masterScalingType, CamScalingMode slaveScalingType)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)activationMode);
            bw.Write(activationPosition);
            bw.Write((uint)masterScalingType);
            bw.Write((uint)slaveScalingType);

            client.Write(indexGroup, 0x1A, ms.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public double GetSingleValue(uint line, uint column)
        {
            var buffer = new Memory<byte>(new byte[16]);

            var ms = new MemoryStream(buffer.ToArray());
            var bw = new BinaryWriter(ms);

            bw.Write(line);
            bw.Write(column);
            bw.Write((double)0.0);

            client.Read(indexGroup, 0x20, ms.ToArray());

            //TODO: Check if this is working.
            var br = new BinaryReader(ms);
            br.ReadUInt32(); //Skip line
            br.ReadUInt32(); //Skip column
            var value = br.ReadDouble();

            return value;
        }

        /// <summary>
        /// Write single value [n,m].
        /// </summary>
        /// <param name="line">n-th line</param>
        /// <param name="column">m-th column</param>
        /// <param name="value">Single value. Base unit (e.g. mm)</param>
        public void SetSingleValue(uint line, uint column, double value)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(line);
            bw.Write(column);
            bw.Write(value);

            client.Write(indexGroup, 0x20, ms.ToArray());
        }

        /// <summary>
        /// Read slave position to the given master position (relates only to the "row values" of the table)
        /// </summary>
        public double SlavePositionToMaster
        {
            get => client.ReadAny<double>(indexGroup, 0x21);
            set => client.WriteAny(indexGroup, 0x21, value);
        }
    }
}