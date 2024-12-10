using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Platform data is data which cannot be assigned to a specific axis or a channel but has an effect on the entire NC control.
    /// It allows reading various properties of the CNC platform such as the tick counter, cycle time, version, 
    /// number of axes, number of channels, and their respective maximum allowable counts. 
    /// </summary>
    public class CncPlatform
    {
        readonly AdsClient client;
        readonly Dictionary<string, ObjectDescription> descriptions;
        internal CncPlatform(AdsClient comClient, Dictionary<string, ObjectDescription> descriptions)
        {
            client = comClient;
            this.descriptions = descriptions;
        }

        /// <summary>
        /// Tick counter of the CNC platform. Always increasing when CNC is running.
        /// </summary>
        public uint TickCounter
        {
            get
            {
                var obj = descriptions["cnc_tick_counter_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Cycle time of the CNC platform. Constant.
        /// </summary>
        public uint CycleTime
        {
            get
            {
                var obj = descriptions["cnc_cycle_time_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Version of the CNC platform.
        /// </summary>
        public string Version
        {
            get
            {
                var obj = descriptions["cnc_version_r"];
                return client.ReadString(obj.IndexGroup, obj.IndexOffset, 24);
            }
        }

        /// <summary>
        /// Number of axes configured in the CNC platform.
        /// </summary>
        public uint AxisCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_axis_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Number of channels configured in the CNC platform.
        /// </summary>
        public ushort ChannelCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_channel_r"];
                return client.ReadAny<ushort>(obj.IndexGroup, obj.IndexOffset);
            }

        }

        /// <summary>
        /// Maximum allowable number of axes that can be configured in the CNC platform.
        /// </summary>
        public uint MaxAxisCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_axis_max_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Maximum allowable number of spindles that can be configured in the CNC platform.
        /// </summary>
        public uint MaxSpindleCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_spindle_max_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Maximum allowable number of channels that can be configured in the CNC platform.
        /// </summary>
        public ushort MaxChannelCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_channel_max_r"];
                return client.ReadAny<ushort>(obj.IndexGroup, obj.IndexOffset);
            }
        }

    }
}
