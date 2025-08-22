using TwinCAT.Ads;
using TwinSharp;
using TwinSharp.NC;

namespace TwinSharpControls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateTargetsInComboBox();
        }

        private void PopulateTargetsInComboBox()
        {
            comboBoxTargets.Items.Clear();

            //Add a local target first.
            var local = new ComboBoxTargetItem("Local", AmsNetId.Local);
            comboBoxTargets.Items.Add(local);

            //Add all defined static routes.
            var routes = TcSystem.ListLocalStaticRoutes();

            foreach (var route in routes)
            {
                var item = new ComboBoxTargetItem(route.Name, route.AmsNetId);
                comboBoxTargets.Items.Add(item);
            }

            //Select something
            if (comboBoxTargets.Items.Count > 0)
            {
                comboBoxTargets.SelectedIndex = 0;
            }

        }

        private void comboBoxTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Find axes at new target.
            PopulateAxesInComboBox();
        }

        private void comboBoxAxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ncAxis1.Axis = comboBoxAxes.SelectedItem as Axis;
        }

        private void PopulateAxesInComboBox()
        {
            comboBoxAxes.Items.Clear();

            //Check what target we are using.
            if (comboBoxTargets.SelectedItem is ComboBoxTargetItem targetItem)
            {
                Axis[] axes;
                try
                {
                    var nc = new NC(targetItem.AmsNetId);
                    axes = nc.Axes;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not find axes at target: " + exc.Message);
                    return;
                }


                foreach (var axis in axes)
                {
                    comboBoxAxes.Items.Add(axis);
                }

                //Select something
                if (comboBoxAxes.Items.Count > 0)
                {
                    comboBoxAxes.SelectedIndex = 0;
                }
            }

        }
    }


    /// <summary>
    /// Container for objects stored in target combo box.
    /// </summary>
    public class ComboBoxTargetItem
    {
        public ComboBoxTargetItem(string name, AmsNetId netId)
        {
            Name = name;
            AmsNetId = netId;
        }

        public AmsNetId AmsNetId { get; set; }
        public string Name { get; set; }



        public override string ToString()
        {
            return Name + " " + AmsNetId.ToString();
        }
    }
}
