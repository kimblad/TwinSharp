using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The ZeroOffsets class is responsible for handling zero offsets (G54 etc) in the CNC system.
    /// </summary>
    public class ZeroOffsets
    {
        readonly AdsClient sdaClient;
        readonly uint channelNumber;

        internal ZeroOffsets(AdsClient sdaClient, int channelNumber)
        {
            this.sdaClient = sdaClient;
            this.channelNumber = (uint)channelNumber;
        }


        /// <summary>
        /// Gets an handle to a zero point based of the zero point index. G54 is index 1.
        /// </summary>
        /// <param name="zeroPointIndex"></param>
        /// <returns></returns>
        private uint GetZeroPointHandle(int zeroPointIndex)
        {
            //SDA clients global variables are stored dynamically, so we need to find the correct handle first.
            var ascii = new ASCIIEncoding();
            var bufferToWrite = ascii.GetBytes(string.Format("V.G.NP[{0}].V", zeroPointIndex));
            var memoryToWrite = new ReadOnlyMemory<byte>(bufferToWrite);



            var bufferToRead = new byte[4];
            var memoryToRead = new Memory<byte>(bufferToRead);

            sdaClient.ReadWrite(0x122300 + channelNumber, 0x46, memoryToRead, memoryToWrite);


            uint handleToZeroPoint = bufferToRead.ToUint();

            return handleToZeroPoint;
        }

        /// <summary>
        /// Gets the zero offsets for a specific zero point index. G54 is index 1.
        /// </summary>
        /// <param name="zeroPointIndex"></param>
        /// <returns>An array of all axes zero point.</returns>
        public int[] GetZeroOffsets(int zeroPointIndex)
        {
            uint handleToZeroPoint = GetZeroPointHandle(zeroPointIndex);

            var bufferToRead = new byte[1024];
            var memoryToRead = new Memory<byte>(bufferToRead);

            //Read and store a copy of existing zero points
            int byteCountRead = sdaClient.Read(0x122300 + channelNumber, handleToZeroPoint, memoryToRead);

            var ms = new MemoryStream(bufferToRead);
            var br = new BinaryReader(ms);

            var currentZeros = new int[byteCountRead / sizeof(int)];

            for (int i = 0; i < currentZeros.Length; i++)
            {
                currentZeros[i] = br.ReadInt32();
            }

            return currentZeros;
        }

        /// <summary>
        /// Sets the zero offsets for a specific zero point index. G54 is index 1.
        /// </summary>
        /// <param name="zeroPointIndex"></param>
        /// <param name="axisIndex"></param>
        /// <param name="offset"></param>
        public void SetZeroOffset(int zeroPointIndex, int axisIndex, int offset)
        {
            //First get a copy of existing zeros.
            var currentZeros = GetZeroOffsets(zeroPointIndex);

            //Now alter the zero point and change the user selected index.
            currentZeros[axisIndex] = offset;

            //Write it back to SDA.

            var updatedZeroPointsBuffer = new byte[currentZeros.Length * sizeof(int)];
            var ms = new MemoryStream(updatedZeroPointsBuffer);
            var bw = new BinaryWriter(ms);

            for (int i = 0; i < currentZeros.Length; i++)
            {
                bw.Write(currentZeros[i]);
            }

            var readonlymem = new ReadOnlyMemory<byte>(updatedZeroPointsBuffer);
            uint handleToZeroPoint = GetZeroPointHandle(zeroPointIndex);

            sdaClient.Write(0x122300 + channelNumber, handleToZeroPoint, readonlymem);
        }
    }

}