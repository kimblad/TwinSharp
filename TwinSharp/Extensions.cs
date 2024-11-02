using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    public static class Extensions
    {
        public static bool ReadBool(this AdsClient client, string symbol)
        {
            var handle = client.CreateVariableHandle(symbol);
            return client.ReadAny<bool>(handle);
        }


        public static uint ReadUInt(this AdsClient client, string symbol)
        {
            var handle = client.CreateVariableHandle(symbol);
            return client.ReadAny<uint>(handle);
        }

        public static ulong ReadULong(this AdsClient client, string symbol)
        {
            var handle = client.CreateVariableHandle(symbol);
            return client.ReadAny<ulong>(handle);
        }

        public static string ReadString(this AdsClient client, string symbol, int len)
        {
            var handle = client.CreateVariableHandle(symbol);
            return client.ReadAnyString(handle, len, Encoding.UTF8);
        }

        public static string ReadString(this AdsClient client, uint indexGroup, uint indexOffset, int len)
        {
            return client.ReadAnyString(indexGroup, indexOffset, len, Encoding.UTF8);
        }

        public static DateTime ReadDateTime(this AdsClient client, string symbol)
        {
            var handle = client.CreateVariableHandle(symbol);
            var seconds = (uint)client.ReadAny(handle, typeof(uint));

            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddSeconds(seconds);
            return dt;
        }


    }
}
