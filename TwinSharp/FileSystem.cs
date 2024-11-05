using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// The FileSystem class provides methods for interacting with the file system on a target device using the TwinCAT ADS protocol.
    /// It allows opening, closing, reading, writing, seeking, deleting files, as well as creating and deleting directories.
    /// </summary>
    public class FileSystem : IDisposable
    {
        readonly AdsClient client;
        internal FileSystem(AmsNetId target)
        {
            client = new AdsClient();
            client.Connect(target, AmsPort.SystemService);
        }

        /// <summary>
        /// Creates a new file or opens an existing file for editing.
        /// Equivavelent to the TwinCAT function block FB_FileOpen 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mode"></param>
        public ushort FileOpen(string path, FileOpenModeFlags mode)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(path);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[4];
            var readMemory = new Memory<byte>(readBytes);

            uint subIndex = 0x10_000 + (uint)mode;
            int bytesReturned = client.ReadWrite(0x78, subIndex, readMemory, writeMemory);

            ushort handle = BitConverter.ToUInt16(readBytes, 0);
            return handle;
        }

        public void FileClose(ushort handle)
        {
            var writeBytes = new byte[0];
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            //Data is 0 and index offset is the handle.

            client.ReadWrite(0x79, handle, readMemory, writeMemory);
        }

        /// <summary>
        /// Reads strings from a file. The string is read up to and including the line feed character, or up to the end of the file or the maximum permitted length of sLine. The null termination is appended automatically. The file must have been opened in text mode.
        /// Equivavelent to the function block FB_FileGets.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public string FileGetString(ushort handle, out bool endOfFile)
        {
            var writeBytes = new byte[0];
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[255];
            var readMemory = new Memory<byte>(readBytes);

            //Data is 0 and index offset is the handle.
            int byteCountRead = client.ReadWrite(0x7E, handle, readMemory, writeMemory);

            if(byteCountRead == 0)
            {
                endOfFile = true;
                return string.Empty;
            }

            var ascii = new ASCIIEncoding();
            string line = ascii.GetString(readBytes, 0, byteCountRead - 1);

            endOfFile = readBytes[byteCountRead - 1] != 0;

            return line;
        }


        /// <summary>
        /// Writes strings into a file. The string is written to the file up to the null termination, but without the null character. The file must have been opened in text mode.
        /// Equivalent to the function block FB_FilePuts
        /// </summary>
        /// <param name="line"></param>
        public void FilePutString(ushort fileHandle, string str)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(str);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            //Index offset is the handle.
            int byteCountRead = client.ReadWrite(0x7F, fileHandle, readMemory, writeMemory);
        }

        /// <summary>
        /// The contents of an already opened file can be read. Before a read access, the file must have been opened in the corresponding mode. In addition to the FOPEN_MODEREAD, the appropriate format (FOPEN_MODEBINARY or FOPEN_MODETEXT) is also important to achieve the desired result.
        /// Equivavalent to the function block FB_FileRead.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="byteCountToRead">Number of bytes to be read.</param>
        /// <returns></returns>
        public byte[] FileRead(ushort handle, int byteCountToRead)
        {
            var writeBytes = new byte[0];
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[byteCountToRead];
            var readMemory = new Memory<byte>(readBytes);

            //Data is 0 and index offset is the handle.
            int byteCountRead = client.ReadWrite(0x7A, handle, readMemory, writeMemory);

            //If we filled the entire array, simply return it.
            if (byteCountRead == byteCountToRead)
                return readBytes;

            //Otherwise make a new array of correct length so user know the length returned.
            byte[] truncated = new byte[byteCountRead];
            Array.Copy(readBytes, truncated, byteCountRead);

            return truncated;
        }


        /// <summary>
        /// Writes data into a file. For write access the file must have been opened in the corresponding mode, and it must be closed again for further processing by external programs.
        /// Equivalent to the function block FB_FileWrite.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="data"></param>
        /// <returns>The number of bytes that were sucessfully written.</returns>
        public uint FileWrite(ushort handle, byte[] data)
        {
            var writeMemory = new ReadOnlyMemory<byte>(data);

            var readBytes = new byte[4];
            var readMemory = new Memory<byte>(readBytes);

            //Index offset is the handle.
            int byteCountRead = client.ReadWrite(0x7B, handle, readMemory, writeMemory);

            uint byteCountWritten = BitConverter.ToUInt32(readBytes);

            return byteCountWritten;
        }

        /// <summary>
        /// Sets the file pointer of an open file to a definable position.
        /// Equivavalent to the function block FB_FileSeek.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="position"></param>
        /// <param name="origin"></param>
        public void FileSeek(ushort handle, int position, SeekOrigin origin)
        {
            var writeBytes = new byte[8];
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var ms = new MemoryStream(writeBytes);
            var bw = new BinaryWriter(ms);

            bw.Write(position);
            bw.Write((int)origin);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            //Index offset is the handle.
            client.ReadWrite(0x7C, handle, readMemory, writeMemory);
        }

        /// <summary>
        /// Determines the current position of the file pointer. The position indicates the relative offset from the start of the file. 
        /// Equivavalent to the function block FB_FileTell.
        /// Note that for files opened in "Append at end of file" mode, the current position is determined by the last I/O operation, not by the position of the next write access.
        /// After a read operation, for example, the file pointer is at the position where the next read access will take place, not at the position where the next write access will take place. In append mode, the file pointer is always moved to the end before the write operation.
        /// If no previous I/O operation was performed and the file was opened in append mode, the file pointer is at the start of the file.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns>The current position of the file pointer.</returns>
        public int FileTell(uint handle)
        {
            var writeBytes = new byte[0];
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[4];
            var readMemory = new Memory<byte>(readBytes);

            //Index offset is the handle.
            client.ReadWrite(0x7D, handle, readMemory, writeMemory);

            int filePointerPosition = BitConverter.ToInt32(readBytes);
            return filePointerPosition;
        }

        /// <summary>
        /// Deletes a file from the data storage device.
        /// Equivavalent to the function block FB_FileDelete.
        /// </summary>
        /// <param name="pathName">File name, including the full path.</param>
        public void FileDelete(string pathName)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(pathName);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            client.ReadWrite(0x83, 0x10000, readMemory, writeMemory);
        }

        /// <summary>
        /// Can be used to rename a file.
        /// Equivavalent to the function block FB_FileRename.
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public void FileRename(string oldPath, string newPath)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(oldPath + '\0' + newPath);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            client.ReadWrite(0x84, 0x10000, readMemory, writeMemory);
        }

        /// <summary>
        /// Create a new folder on the target. Note: does not create folders recursively.
        /// </summary>
        /// <param name="path"></param>
        public void CreateDirectory(string path)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(path);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            int bytesReturned = client.ReadWrite(0x8A, 0x1, readMemory, writeMemory);
        }


        /// <summary>
        /// Can be used to delete a directory from the data storage device.
        /// A directory containing files cannot be deleted.
        /// Equivavalent to the function block FB_RemoveDir.
        /// </summary>
        /// <param name="path"></param>
        public void DeleteDirectory(string path)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(path);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[0];
            var readMemory = new Memory<byte>(readBytes);

            client.ReadWrite(0x8B, 0x1, readMemory, writeMemory);
        }

        public ST_FindFileEntry GetFileProperties(string path)
        {
            var ascii = new ASCIIEncoding();
            var writeBytes = ascii.GetBytes(path);
            var writeMemory = new ReadOnlyMemory<byte>(writeBytes);

            var readBytes = new byte[324];
            var readMemory = new Memory<byte>(readBytes);

            int bytesReturned = client.ReadWrite(0x85, 0x1, readMemory, writeMemory);


            var ms = new MemoryStream(readBytes);
            var br = new BinaryReader(ms);
            ST_FindFileEntry entry = new ST_FindFileEntry();


            ushort someSortOfUndocumentedHandle = br.ReadUInt16(); //Not found in documentation.
            br.ReadUInt16();

            int attributeFlags = br.ReadInt32();

            entry.FileAttributes.ReadOnly = BitIsSet(attributeFlags, 0);
            entry.FileAttributes.Hidden = BitIsSet(attributeFlags, 1);
            entry.FileAttributes.System = BitIsSet(attributeFlags, 2);
            entry.FileAttributes.Directory = BitIsSet(attributeFlags, 4); //Not a typo, found with ADS Monitor.
            entry.FileAttributes.Archive = BitIsSet(attributeFlags, 5);
            entry.FileAttributes.Device = BitIsSet(attributeFlags, 6);
            entry.FileAttributes.Normal = BitIsSet(attributeFlags, 7);
            entry.FileAttributes.Temporary = BitIsSet(attributeFlags, 8);
            entry.FileAttributes.SparseFile = BitIsSet(attributeFlags, 9);
            entry.FileAttributes.ReparsePoint = BitIsSet(attributeFlags, 10);
            entry.FileAttributes.Compressed = BitIsSet(attributeFlags, 11);
            entry.FileAttributes.Offline = BitIsSet(attributeFlags, 12);
            entry.FileAttributes.NotContentIndexed = BitIsSet(attributeFlags, 13);
            entry.FileAttributes.Encrypted = BitIsSet(attributeFlags, 14);

            ulong creationTimeTicks = br.ReadUInt64();
            entry.CreationTime = CreateWin32EpochFileTime(creationTimeTicks);

            ulong accessedTimeTicks = br.ReadUInt64();
            entry.LastAccessTime = CreateWin32EpochFileTime(accessedTimeTicks);

            ulong lastWriteTimeTicks = br.ReadUInt64();
            entry.LastWriteTime = CreateWin32EpochFileTime(lastWriteTimeTicks);

            //ulong fileSize = br.ReadUInt64();

            //uint lowPart = br.ReadUInt32();
            //uint highPart = br.ReadUInt32();
            //ulong fileSize = br.ReadUInt64();

            ulong fileSizeLegacyFormat = br.ReadUInt64();
            entry.FileSize = BinaryPrimitives.ReverseEndianness(fileSizeLegacyFormat);




            int position = bytesReturned - Constants.MAX_STRING_LENGTH -1 - 13 -1;


            string untrimmed = ascii.GetString(readBytes, position, Constants.MAX_STRING_LENGTH -1).TrimEnd('\0');

            string trimmed = untrimmed.TrimEnd('\0'); //Remove trailing null chars.
            if (trimmed.Length > 5)
                trimmed = trimmed.Substring(0, untrimmed.Length - 5); //There's 5 unkown chars added (null d null r e) remove them.

            entry.FileName = trimmed;

            position += Constants.MAX_STRING_LENGTH - 1;
            entry.AlternateFileName = ascii.GetString(readBytes, position, 13).TrimEnd('\0');
            return entry;
        }

        private bool BitIsSet(int number, int bitPosition)
        {
            var mask = 0;
            mask |= (1 << bitPosition);
            return (number & mask) == mask;
        }


        private DateTime CreateWin32EpochFileTime(ulong ticks)
        {
            var dt = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddTicks((long)ticks);

            return dt;
        }


        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}