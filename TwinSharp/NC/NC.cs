using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The NC class provides access to the NC (Numerical Control) system using TwinCAT ADS protocol.
    /// It initializes and manages the Ring0Manager, Axes, Channels, Groups and Tables components.
    /// </summary>
    public class NC
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NC"/> class.
        /// This constructor sets up the Ring0Manager and establishes a connection to the TwinCAT ADS client.
        /// It also initializes the Axes, Channels, Groups, and Tables components using the provided target AmsNetId.
        /// </summary>
        /// <param name="target">The AmsNetId of the target device to connect to.</param>

        public NC(AmsNetId target)
        {
            Ring0Manager = new Ring0Manager(target);

            var client = new AdsClient();
            client.Connect(target, AmsPort.R0_NCSAF);


            Axes = InitAxes(client, Ring0Manager);
            Channels = InitChannels(client, Ring0Manager);
            Groups = InitGroups(client, Ring0Manager);
            Tables = InitTables(client, Ring0Manager);
        }

        /// <summary>
        /// Gets the Ring0Manager instance which manages the low-level operations and state of the NC system.
        /// </summary>
        public Ring0Manager Ring0Manager { get; private set; }

        /// <summary>
        /// Gets the array of Axis objects representing the axes in the NC system.
        /// </summary>
        public Axis[] Axes { get; private set; }

        /// <summary>
        /// Gets the array of Channel objects representing the channels in the NC system.
        /// </summary>
        public Channel[] Channels { get; private set; }

        /// <summary>
        /// Gets the array of Group objects representing the groups in the NC system.
        /// </summary>
        public Group[] Groups { get; private set; }

        /// <summary>
        /// Gets the array of Table objects representing the tables in the NC system.
        /// </summary>
        public Table[] Tables { get; private set; }



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