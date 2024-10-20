using System.Text;

namespace TwinSharp
{

    public struct RTimeCpuSettings
    {
        public uint nWinCPUs;
        public uint nNonWinCPUs;
        public ulong affinityMask;
        public uint nRtCpus;
        public uint nCpuType;
        public uint nCpuFamily;
        public uint nCpuFreq;
    };

    public struct RTimeCpuLatency
    {
        public uint current;
        public uint maximum;
        public uint limit;
    };

    public class ST_CheckLicense
    {
        public readonly Guid stLicenseId;
        public readonly DateTime tExpirationTime;
        public readonly string sExpirationTime;
        public readonly E_LicenseHResult eResult;
        public readonly uint nCount;
        public string sLicenseName;

        public ST_CheckLicense(byte[] bytes, string descriptionText)
        {
            if (bytes.Length != 48)
                throw new Exception("Can not create ST_CheckLicense. Wrong length of input buffer:" + bytes.Length.ToString());

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);


            byte[] guidArray = new byte[16];
            br.ReadBytes(16);
            stLicenseId = new Guid(guidArray);

            DateTime origin = new DateTime(1601, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            long ticks = br.ReadInt64();
            tExpirationTime = origin.Add(new TimeSpan(ticks));

            sExpirationTime = tExpirationTime.ToLongDateString() + " " + tExpirationTime.ToLongTimeString();

            eResult = (E_LicenseHResult)br.ReadUInt32();

            nCount = br.ReadUInt32();

            sLicenseName = descriptionText;
        }


        public override string ToString()
        {
            return sLicenseName + " " + sExpirationTime + " " + eResult;
        }
    }

    public class ST_EcSlaveState
    {
        public byte DeviceState;
        public byte LinkState;

        public ST_EcSlaveState(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new Exception("Can not create ST_EcSlaveState. Wrong length of input buffer:" + bytes.Length.ToString());

            DeviceState = bytes[0];
            LinkState = bytes[1];
        }
    }

    public class ST_EcSlaveIdentity
    {
        public uint vendorId;
        public uint productCode;
        public uint revisionNo;
        public uint serialNo;


        public ST_EcSlaveIdentity(byte[] bytes)
        {
            if (bytes.Length != 16)
                throw new Exception("Can't construct a ST_EcSlaveIdentity from " + bytes.Length.ToString() + " bytes.");

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            vendorId = br.ReadUInt32();
            productCode = br.ReadUInt32();
            revisionNo = br.ReadUInt32();
            serialNo = br.ReadUInt32();
        }
    }

    public class ST_EcSlaveConfigData
    {
        public ushort nEntries;
        public ushort nAddr;
        public string sType; //15 long
        public string sName; //31 long
        public uint nDevType;
        public ST_EcSlaveIdentity stSlaveIdentity;
        public ushort nMailboxOutSize;
        public ushort nMailboxInSize;
        public byte nLinkStatus;

        public ST_EcSlaveConfigData(byte[] bytes)
        {
            if (bytes.Length != 80)
                throw new Exception("Can not create EcSlaveConfigData. Wrong length of input buffer:" + bytes.Length.ToString());

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);
            var ascii = new ASCIIEncoding();


            nEntries = br.ReadUInt16();
            nAddr = br.ReadUInt16();

            sType = ascii.GetString(br.ReadBytes(16), 0, 16).TrimEnd('\0');
            sName = ascii.GetString(br.ReadBytes(32), 0, 32).TrimEnd('\0');

            nDevType = br.ReadUInt32();

            byte[] identityBytes = br.ReadBytes(16);
            stSlaveIdentity = new ST_EcSlaveIdentity(identityBytes);

            nMailboxOutSize = br.ReadUInt16();
            nMailboxInSize = br.ReadUInt16();
            nLinkStatus = br.ReadByte();
        }
    }

    public class ST_TopologyDataEx
    {
        public ushort nOwnPhysicalAddr; //Dedicated physical EtherCAT address of the EtherCAT slave device
        public ushort nOwnAutoIncAddr; //Dedicated auto-increment EtherCAT address of the EtherCAT slave device
        public ST_PortAddr stPhysicalAddr; //Physical address information of the EtherCAT slave devices at port A…D
        public ST_PortAddr stAutoIncAddr; //Auto-increment address information of the EtherCAT slave devices at port A…D
        public uint[] aReserved1;
        public uint nStatusBits;
        public ushort nHCSlaveCountCfg; //Configured number of Hot Connect group devices
        public ushort nHCSlaveCountAct; //Found number of Hot Connect group devices
        public uint[] aReserved2;


        public ST_TopologyDataEx(byte[] bytes)
        {
            if (bytes.Length != 64)
                throw new Exception("Can not create ST_TopologyDataEx. Wrong length of input buffer:" + bytes.Length.ToString());

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            nOwnPhysicalAddr = br.ReadUInt16();
            nOwnAutoIncAddr = br.ReadUInt16();


            byte[] portBytes = new byte[8];
            portBytes = br.ReadBytes(8);
            stPhysicalAddr = new ST_PortAddr(portBytes);

            portBytes = br.ReadBytes(8);
            stAutoIncAddr = new ST_PortAddr(portBytes);

            aReserved1 = new uint[4];
            for (int i = 0; i < aReserved1.Length; i++)
            {
                aReserved1[i] = br.ReadUInt32();
            }

            nStatusBits = br.ReadUInt32();

            nHCSlaveCountCfg = br.ReadUInt16();
            nHCSlaveCountAct = br.ReadUInt16();

            aReserved2 = new uint[5];
            for (int i = 0; i < aReserved2.Length; i++)
            {
                aReserved2[i] = br.ReadUInt32();
            }
        }
    }

     /// <summary>
    /// The structure ST_PortAddr contains EtherCAT topology information for EtherCAT slave device. EtherCAT slave devices typically have 2 to 4 ports.
    /// </summary>
    public class ST_PortAddr
    {
        public ushort PortA; //Address of the previous EtherCAT slave at port A of the current EtherCAT slave
        public ushort PortB; //Address of the optional subsequent EtherCAT slave at port B of the current EtherCAT slave
        public ushort PortC; //Address of the optional subsequent EtherCAT slave at port C of the current EtherCAT slave
        public ushort PortD; //Address of the optional subsequent EtherCAT slave at port D of the current EtherCAT slave

        public ST_PortAddr(byte[] bytes)
        {
            if (bytes.Length != 8)
                throw new Exception("Can not create ST_PortAddr. Wrong length of input buffer:" + bytes.Length.ToString());

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            PortA = br.ReadUInt16();
            PortB = br.ReadUInt16();
            PortC = br.ReadUInt16();
            PortD = br.ReadUInt16();
        }

    }
}
