using System.Runtime.InteropServices;

namespace TwinSharp.CNC
{
    public static class Extensions
    {
        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                var ptr = handle.AddrOfPinnedObject();
                if (ptr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Failed to pin the byte array when creating struct.");
                }
                var result = Marshal.PtrToStructure<T>(ptr);
                return result;
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
