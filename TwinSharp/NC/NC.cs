using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The NC class provides access to the NC (Numerical Control) system using TwinCAT ADS protocol.
    /// It initializes and manages the Ring0Manager, Axes, Channels, Groups and Tables components.
    /// </summary>
    public class NC
    {
        public readonly Ring0Manager Ring0Manager;
        public readonly Axis[] Axes;
        public readonly Channel[] Channels;
        public readonly Group[] Groups;
        public readonly Table[] Tables;


        public NC(AmsNetId target) 
        {
            Init(target, out Ring0Manager, out Axes, out Channels, out Groups, out Tables);
        }

        public NC()
        {
            var target = AmsNetId.Local;
            Init(target, out Ring0Manager, out Axes, out Channels, out Groups, out Tables);
        }


        private void Init(AmsNetId target, out Ring0Manager ring0Manager, out Axis[] axes, out Channel[] channels, out Group[] groups, out Table[] tables)
        {
            ring0Manager = new Ring0Manager(target);

            var client = new AdsClient();
            client.Connect(target, AmsPort.R0_NCSAF);


            axes = InitAxes(client, ring0Manager);
            channels = InitChannels(client, ring0Manager);
            groups = InitGroups(client, ring0Manager);
            tables = InitTables(client, ring0Manager);
        }



        private Axis[] InitAxes(AdsClient client, Ring0Manager ring0Manager)
        {
            var axes = new Axis[ring0Manager.State.AxisCount];
            uint[] ids = ring0Manager.State.AxisIds;

            for (int i = 0; i < axes.Length; i++)
            {
                uint id = ids[i];
                axes[i] = new Axis(client, id);
            }

            return axes;
        }

        private Channel[] InitChannels(AdsClient client, Ring0Manager ring0Manager)
        {
            var channels = new Channel[ring0Manager.State.ChannelCount];
            uint[] ids = ring0Manager.State.ChannelIds;

            for (int i = 0; i < channels.Length; i++)
            {
                uint id = ids[i];
                channels[i] = new Channel(client, id);
            }
            return channels;
        }

        private Group[] InitGroups(AdsClient client, Ring0Manager ring0Manager)
        {
            var groups = new Group[ring0Manager.State.GroupCount];
            uint[] ids = ring0Manager.State.GroupIds;
            for (int i = 0; i < groups.Length; i++)
            {
                uint id = ids[i];
                groups[i] = new Group(client, id);
            }
            return groups;
        }

        private Table[] InitTables(AdsClient client, Ring0Manager ring0Manager)
        {
            var tables = new Table[ring0Manager.State.TableCount];
            uint[] ids = ring0Manager.State.TableIds;

            for (int i = 0; i < tables.Length; i++)
            {
                uint id = ids[i];
                tables[i] = new Table(client, id);
            }

            return tables;
        }
    }
}