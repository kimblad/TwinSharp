using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The GroupFunctions class provides methods to control and manage an axis group in a TwinCAT NC (Numerical Control) system.
    /// It allows for resetting, stopping, clearing, and performing an emergency stop on the group.
    /// Additionally, it supports starting and managing FIFO (First In, First Out) operations for the group.
    /// </summary>
    public class GroupFunctions
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal GroupFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x3200 + id;
        }

        /// <summary>
        /// Reset group
        /// </summary>
        public void Reset()
        {
            client.Write(indexGroup, 0x01);
        }

        /// <summary>
        /// Stop group
        /// </summary>
        public void Stop()
        {
            client.Write(indexGroup, 0x02);
        }

        /// <summary>
        /// Clear group (buffer/task)
        /// </summary>
        public void Clear()
        {
            client.Write(indexGroup, 0x03);
        }

        /// <summary>
        /// Emergency stop(E-stop) (emergency stop with controlled ramp)
        /// </summary>
        /// <param name="deceleration">Deceleration (must be greater than or equal to the original deceleration)</param>
        /// <param name="jerk">Jerk (must greater than or equal to the original jerk)</param>
        public void EmergencyStop(double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(deceleration);
            bw.Write(jerk);
            client.WriteAny(indexGroup, 0x04, ms.ToArray());
        }

        /// <summary>
        /// Start FIFO group(FIFO table must have been filled in advance)
        /// </summary>
        public void FifoStart()
        {
            client.Write(indexGroup, 0x701);
        }

        /// <summary>
        /// Write x FIFO entries (lines): (x*m)-values (one or more lines) n: FIFO length (number of lines) m: FIFO dimension (number of columns) range of values x: [1 ... n]
        /// </summary>
        /// <param name="entries"></param>
        public void FifoWrite(double[] entries)
        {
            client.WriteAny(indexGroup, 0x710, entries);
        }

        /// <summary>
        /// Overwrite the last x FIFO entries (lines): (x*m)-values (one or more lines) n: FIFO length (number of lines) m: FIFO dimension (number of columns) range of values x: [1 ... n]
        /// </summary>
        /// <param name="entries"></param>
        public void FifoOverwrite(double[] entries)
        {
            client.WriteAny(indexGroup, 0x711, entries);
        }
    }
}