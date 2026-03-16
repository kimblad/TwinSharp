using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// Contains extension methods for various classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Reads a string from the specified index group and offset using the UTF8 encoding.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="indexGroup"></param>
        /// <param name="indexOffset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string ReadString(this AdsClient client, uint indexGroup, uint indexOffset, int len)
        {
            return client.ReadAnyString(indexGroup, indexOffset, len, Encoding.UTF8);
        }

        /// <summary>
        /// Reads a uint from the specified index group and offset and converts it to a DateTime object.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static DateTime ReadDateTime(this AdsClient client, uint handle)
        {
            var seconds = (uint)client.ReadAny(handle, typeof(uint));

            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddSeconds(seconds);
            return dt;
        }

        /// <summary>
        /// Reads a ULINT or UDINT symbol as ulong. On the TwinCAT usermode runtime some
        /// counter fields are exposed as UDINT (4 bytes) instead of ULINT (8 bytes).
        /// Tries to read 8 bytes first; if the runtime returns only 4 bytes it retries
        /// as uint and widens the result.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static ulong ReadUlintOrUdint(this AdsClient client, uint handle)
        {
            try
            {
                return client.ReadAny<ulong>(handle);
            }
            catch (AdsErrorException)
            {
                return client.ReadAny<uint>(handle);
            }
        }

        /// <summary>
        /// Converts a byte array to a uint.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static uint ToUint(this byte[] buffer)
        {
            return ((uint)buffer[3] << 24) | ((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | (uint)buffer[0];
        }


    }
}
