using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class NC
    {
        public readonly Ring0Manager Ring0Manager;
        public readonly Axis[] Axes;
        public readonly Table[] Tables;

        public NC(AmsNetId target) 
        {
            Init(target, out Ring0Manager, out Axes, out Tables);
        }

        public NC()
        {
            var target = AmsNetId.Local;
            Init(target, out Ring0Manager, out Axes, out Tables);
        }


        private void Init(AmsNetId target, out Ring0Manager ring0Manager, out Axis[] axes, out Table[] tables)
        {
            ring0Manager = new Ring0Manager(target);

            var client = new AdsClient();
            client.Connect(target, AmsPort.R0_NCSAF);

            axes = InitAxes(client, ring0Manager);
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