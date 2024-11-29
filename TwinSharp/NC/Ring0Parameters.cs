using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the parameters for Ring 0, providing access to various settings and configurations
    /// related to the SAF and SVB tasks, global time compensation, and cyclic data consistency.
    /// </summary>
    public class Ring0Parameters
    {
        readonly AdsClient client;
        const uint indexGroup = 0x1000;
        internal Ring0Parameters(AdsClient client) 
        {
            this.client = client;
        }

        /// <summary>
        /// Cycle time SAF task
        /// Unit: 100 ns
        /// </summary>
        public uint CycleTimeSaf
        {
            get => client.ReadAny<uint>(indexGroup, 0x10);
        }

        /// <summary>
        /// Cycle time SVB task.
        /// Unit: 100 ns
        /// </summary>
        public uint CycleTimeSvb
        {
            get => client.ReadAny<uint>(indexGroup, 0x12);
        }

        /// <summary>
        /// Global Time Compensation Shift (for SAF Task).
        /// Unit: ns
        /// </summary>
        public int GlobalTimeCompensationShift
        {
            get => client.ReadAny<int>(indexGroup, 0x14);
        }

        /// <summary>
        /// Cyclic data consistence check and correction of the NC setpoint values
        /// Unit: 0/1
        /// </summary>
        public ushort CyclicDataConsistenceCheck
        {
            get => client.ReadAny<ushort>(indexGroup, 0x20);
            set => client.WriteAny(indexGroup, 0x20, value);
        }
    }
}
