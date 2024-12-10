using System.Runtime.InteropServices;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Contains extension methods for various classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a byte array to a structure of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
