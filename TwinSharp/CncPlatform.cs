using TwinCAT.Ads;

namespace TwinSharp
{
    public class CncPlatform
    {
        readonly AdsClient client;
        readonly Dictionary<string, ObjectDescription> descriptions;
        internal CncPlatform(AdsClient comClient, Dictionary<string, ObjectDescription> descriptions)
        {
            this.client = comClient;
            this.descriptions = descriptions;
        }

        public uint TickCounter
        {
            get
            {
                var obj = descriptions["cnc_tick_counter_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        public uint CycleTime
        {
            get
            {
                var obj = descriptions["cnc_cycle_time_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        public string Version
        {
            get
            {
                var obj = descriptions["cnc_version_r"];
                return client.ReadString(obj.IndexGroup, obj.IndexOffset, 24);
            }
        }

        public uint AxisCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_axis_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }


        public ushort ChannelCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_channel_r"];
                return client.ReadAny<ushort>(obj.IndexGroup, obj.IndexOffset);
            }

        }

        public uint MaxAxisCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_axis_max_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        public uint MaxSpindleCount
        {
            get
            {
                var obj = descriptions["cnc_number_of_spindle_max_r"];
                return client.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }

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
