using System.Text;

namespace TwinSharp
{
    /// <summary>
    /// Struct that describes the amount of Windows (shared) cores and isolated cores for TwinCAT.
    /// </summary>
    public struct RTimeCpuSettings
    {
        /// <summary> Number of Windows (shared) cores. </summary>
        public uint WinCPUs;
        
        /// <summary> Number of non windows cores. </summary>
        public uint NonWinCPUs;
        
        /// <summary> Affinity mask. </summary>
        public ulong AffinityMask;
        
        /// <summary> Number of real time cores. </summary>
        public uint RtCpus;
        
        /// <summary> CPU type. </summary>
        public uint CpuType;
        
        /// <summary> CPU family. </summary>
        public uint CpuFamily;

        /// <summary> CPU frequency. </summary>
        public uint CpuFreq;
    };


    /// <summary>
    /// Struct that describes the realtime latency of the CPU.
    /// </summary>
    public struct RTimeCpuLatency
    {
        /// <summary> The current latency time of a TwinCAT system in µs.</summary>
        public uint Current;

        /// <summary> The maximum latency time of a TwinCAT system in µs (maximum latency time since the TwinCAT system was last started).</summary>
        public uint Maximum;

        /// <summary> Limit. </summary>
        public uint Limit;
    };

    /// <summary>
    /// Structure with license information.
    /// </summary>
    public struct ST_CheckLicense
    {
        /// <summary>
        /// License ID
        /// </summary>
        public readonly Guid LicenseId;

        /// <summary>
        /// Expiration time of the license
        /// </summary>
        public readonly DateTime ExpirationTime;

        /// <summary>
        /// Expiration time of the license as a string
        /// </summary>
        public readonly string ExpirationTimeString;

        /// <summary>
        /// License status
        /// </summary>
        public readonly E_LicenseHResult eResult;

        /// <summary>
        /// Number of instances for this license (0=unlimited)
        /// </summary>
        public readonly uint nCount;

        /// <summary>
        /// Name of the license
        /// </summary>
        public string LicenseName;

        /// <summary>
        /// Constructor for ST_CheckLicense from a byte array of length 48.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="descriptionText"></param>
        /// <exception cref="Exception"></exception>
        public ST_CheckLicense(byte[] bytes, string descriptionText)
        {
            if (bytes.Length != 48)
                throw new Exception("Can not create ST_CheckLicense. Wrong length of input buffer:" + bytes.Length.ToString());

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);


            byte[] guidArray = new byte[16];
            br.ReadBytes(16);
            LicenseId = new Guid(guidArray);

            DateTime origin = new DateTime(1601, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            long ticks = br.ReadInt64();
            ExpirationTime = origin.Add(new TimeSpan(ticks));

            ExpirationTimeString = ExpirationTime.ToLongDateString() + " " + ExpirationTime.ToLongTimeString();

            eResult = (E_LicenseHResult)br.ReadUInt32();

            nCount = br.ReadUInt32();

            LicenseName = descriptionText;
        }

        /// <summary>
        /// Returns a string representation of the license.
        /// </summary>
        /// <returns></returns>
        public override readonly string ToString()
        {
            return LicenseName + " " + ExpirationTimeString + " " + eResult;
        }
    }

    /// <summary>
    /// The structure ST_EcSlaveState contains the EtherCAT state and the link state of an EtherCAT slave device.
    /// </summary>
    public struct ST_EcSlaveState
    {
        /// <summary>
        /// EtherCAT state of a slave
        /// </summary>
        public byte DeviceState;

        /// <summary>
        /// Link state of a slave
        /// </summary>
        public byte LinkState;

        /// <summary>
        /// Constructor for ST_EcSlaveState from a byte array of length 2.
        /// </summary>
        /// <param name="bytes"></param>
        /// <exception cref="Exception"></exception>
        public ST_EcSlaveState(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new Exception("Can not create ST_EcSlaveState. Wrong length of input buffer:" + bytes.Length.ToString());

            DeviceState = bytes[0];
            LinkState = bytes[1];
        }
    }

    /// <summary>
    /// The structure ST_EcSlaveIdentity contains the EtherCAT identity data for an EtherCAT slave device.
    /// </summary>
    public struct ST_EcSlaveIdentity
    {
        /// <summary>
        /// Vendor-ID of the slave device
        /// </summary>
        public uint VendorId;

        /// <summary>
        /// Product code of the slave device
        /// </summary>
        public uint ProductCode;

        /// <summary>
        /// Indicates the revision number of the slave device.
        /// </summary>
        public uint RevisionNo;

        /// <summary>
        /// Indicates the serial number of the slave device.
        /// </summary>
        public uint SerialNo;

        /// <summary>
        /// Constructor for ST_EcSlaveIdentity from a byte array of length 16.
        /// </summary>
        /// <param name="bytes"></param>
        /// <exception cref="Exception"></exception>
        public ST_EcSlaveIdentity(byte[] bytes)
        {
            if (bytes.Length != 16)
                throw new Exception("Can't construct a ST_EcSlaveIdentity from " + bytes.Length.ToString() + " bytes.");

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);

            VendorId = br.ReadUInt32();
            ProductCode = br.ReadUInt32();
            RevisionNo = br.ReadUInt32();
            SerialNo = br.ReadUInt32();
        }
    }

    /// <summary>
    /// The structure ST_EcSlaveConfigData contains the EtherCAT configuration data for an EtherCAT slave device.
    /// </summary>
    public struct ST_EcSlaveConfigData
    {
        /// <summary>
        /// Used internally.
        /// </summary>
        public ushort Entries;

        /// <summary>
        /// Adress of an EtherCAT slave.
        /// </summary>
        public ushort Addr;

        /// <summary>
        /// EtherCAT type of a slave.
        /// </summary>
        public string Type; //15 long

        /// <summary>
        /// Name of an EtherCAT slave.
        /// </summary>
        public string Name; //31 long

        /// <summary>
        /// EtherCAT device type of a slave
        /// </summary>
        public uint DevType;

        /// <summary>
        /// EtherCAT identity data of a slave
        /// </summary>
        public ST_EcSlaveIdentity SlaveIdentity;

        /// <summary>
        /// Mailbox OutSize of an EtherCAT slave
        /// </summary>
        public ushort MailboxOutSize;

        /// <summary>
        /// Mailbox InSize of an EtherCAT slave
        /// </summary>
        public ushort MailboxInSize;

        /// <summary>
        /// Link status of an EtherCAT slave
        /// </summary>
        public byte LinkStatus;


        /// <summary>
        /// Constructor for ST_EcSlaveConfigData from a byte array of length 80.
        /// </summary>
        /// <param name="bytes"></param>
        /// <exception cref="Exception"></exception>
        public ST_EcSlaveConfigData(byte[] bytes)
        {
            if (bytes.Length != 80)
                throw new Exception("Can not create EcSlaveConfigData. Wrong length of input buffer:" + bytes.Length.ToString());

            var ms = new MemoryStream(bytes);
            var br = new BinaryReader(ms);
            var ascii = new ASCIIEncoding();


            Entries = br.ReadUInt16();
            Addr = br.ReadUInt16();

            Type = ascii.GetString(br.ReadBytes(16), 0, 16).TrimEnd('\0');
            Name = ascii.GetString(br.ReadBytes(32), 0, 32).TrimEnd('\0');

            DevType = br.ReadUInt32();

            byte[] identityBytes = br.ReadBytes(16);
            SlaveIdentity = new ST_EcSlaveIdentity(identityBytes);

            MailboxOutSize = br.ReadUInt16();
            MailboxInSize = br.ReadUInt16();
            LinkStatus = br.ReadByte();
        }

        /// <summary>
        /// Returns a string representation of the slave configuration data.
        /// </summary>
        /// <returns></returns>
        public override readonly string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// The structure ST_TopologyDataEx contains information on EtherCAT topology and hot-connect groups.
    /// </summary>
    public struct ST_TopologyDataEx
    {
        /// <summary>
        /// Dedicated physical EtherCAT address of the EtherCAT slave device
        /// </summary>
        public ushort nOwnPhysicalAddr;

        /// <summary>
        /// Dedicated auto-increment EtherCAT address of the EtherCAT slave device
        /// </summary>
        public ushort nOwnAutoIncAddr;

        /// <summary>
        /// Physical and auto-increment address information of the EtherCAT slave devices at port A…D
        /// </summary>
        public ST_PortAddr stPhysicalAddr;

        /// <summary>
        /// Auto-increment address information of the EtherCAT slave devices at port A…D
        /// </summary>
        public ST_PortAddr stAutoIncAddr;

        /// <summary>
        /// Reserved for future use
        /// </summary>
        public uint[] aReserved1;

        /// <summary>
        /// Status bits of the EtherCAT slave device
        /// </summary>
        public uint nStatusBits;

        /// <summary>
        /// Configured number of Hot Connect group devices
        /// </summary>
        public ushort nHCSlaveCountCfg;

        /// <summary>
        /// Found number of Hot Connect group devices
        /// </summary>
        public ushort nHCSlaveCountAct; //Found number of Hot Connect group devices

        /// <summary>
        /// Reserved for future use
        /// </summary>
        public uint[] aReserved2;

        /// <summary>
        /// Constructor for ST_TopologyDataEx from a byte array of length 64.
        /// </summary>
        /// <param name="bytes"></param>
        /// <exception cref="Exception"></exception>
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
    public struct ST_PortAddr
    {
        /// <summary>
        /// Address of the previous EtherCAT slave at port A of the current EtherCAT slave
        /// </summary>
        public ushort PortA;

        /// <summary>
        /// Address of the optional subsequent EtherCAT slave at port B of the current EtherCAT slave
        /// </summary>
        public ushort PortB;

        /// <summary>
        /// Address of the optional subsequent EtherCAT slave at port C of the current EtherCAT slave
        /// </summary>
        public ushort PortC; 

        /// <summary>
        /// Address of the optional subsequent EtherCAT slave at port D of the current EtherCAT slave
        /// </summary>
        public ushort PortD;

        /// <summary>
        /// Constructor for ST_PortAddr
        /// </summary>
        /// <param name="bytes"></param>
        /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// The structure ST_FindFileEntry contains information about a file or directory found by the FindFirstFile and FindNextFile functions.
    /// </summary>
    public struct ST_FindFileEntry
    {
        /// <summary>
        /// Zero-terminated string with the name of the file or directory (type: T_MaxString).
        /// </summary>
        public string FileName;

        /// <summary>
        /// Zero-terminated string with the alternative name of the file or directory in conventional 8.3 format(filename.ext).
        /// </summary>
        public string AlternateFileName;

        /// <summary>
        /// File attributes.
        /// </summary>
        public ST_FileAttributes FileAttributes;

        /// <summary>
        /// File size in bytes.
        /// </summary>
        public ulong FileSize;

        /// <summary>
        /// Creation time of the file.
        /// </summary>
        public DateTime CreationTime;

        /// <summary>
        /// For a file the structure indicates when it was last accessed (read or write). For a directory the structure indicates when it was created.
        /// </summary>
        public DateTime LastAccessTime;

        /// <summary>
        /// Last write time of the file.
        /// </summary>
        public DateTime LastWriteTime;
    }

    /// <summary>
    /// Describes the different attributes that a file can have. Such as readonly, hidden, system etc.
    /// </summary>
    public struct ST_FileAttributes
    {
        /// <summary> FILE_ATTRIBUTE_READONLY </summary>
        public bool ReadOnly;
        
        /// <summary> FILE_ATTRIBUTE_HIDDEN </summary>
        public bool Hidden;             
        
        /// <summary> FILE_ATTRIBUTE_SYSTEM </summary>
        public bool System;             
        
        /// <summary> FILE_ATTRIBUTE_DIRECTORY </summary>
        public bool Directory;          
        
        /// <summary> FILE_ATTRIBUTE_ARCHIVE </summary>
        public bool Archive;

        /// <summary> FILE_ATTRIBUTE_DEVICE. Under CE: FILE_ATTRIBUTE_INROM or FILE_ATTRIBUTE_ENCRYPTED </summary>
        public bool Device;             
        
        /// <summary> FILE_ATTRIBUTE_NORMAL </summary>
        public bool Normal;             
        
        /// <summary> FILE_ATTRIBUTE_TEMPORARY </summary>
        public bool Temporary;          
        
        /// <summary> FILE_ATTRIBUTE_SPARSE_FILE </summary>
        public bool SparseFile;         
        
        /// <summary> FILE_ATTRIBUTE_REPARSE_POINT </summary>
        public bool ReparsePoint;       
        
        /// <summary> FILE_ATTRIBUTE_COMPRESSED </summary>
        public bool Compressed;

        /// <summary> FILE_ATTRIBUTE_OFFLINE. Under CE: FILE_ATTRIBUTE_ROMSTATICREF</summary>
        public bool Offline;            

        /// <summary> FILE_ATTRIBUTE_NOT_CONTENT_INDEXED. Under CE: FILE_ATTRIBUTE_ROMMODULE </summary>
        public bool NotContentIndexed;

        /// <summary> FILE_ATTRIBUTE_ENCRYPTED </summary>
        public bool Encrypted;          
    }
}
