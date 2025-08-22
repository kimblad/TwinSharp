using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinSharp.NC;

namespace TwinSharpControls
{
    public partial class NcAxis : UserControl
    {
        Axis? axis;

        public NcAxis()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the axis to be displayed in the control.
        /// </summary>
        public Axis? Axis
        {
            get => axis;
            set
            {
                axis = value;
                ncOnline1.Axis = axis;
                ncFunctions1.Axis = axis;
            }
        }
    }
}
