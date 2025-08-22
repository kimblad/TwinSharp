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
