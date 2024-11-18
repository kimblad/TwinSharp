using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    public static class Extensions
    {
        public static string ReadString(this AdsClient client, uint indexGroup, uint indexOffset, int len)
        {
            return client.ReadAnyString(indexGroup, indexOffset, len, Encoding.UTF8);
        }

        public static DateTime ReadDateTime(this AdsClient client, uint handle)
        {
            var seconds = (uint)client.ReadAny(handle, typeof(uint));

            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddSeconds(seconds);
            return dt;
        }

        public static uint ToUint(this byte[] buffer)
        {
            return ((uint)buffer[3] << 24) | ((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | (uint)buffer[0];
        }


    }
}
