using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace TwinSharp
{
    public class DriveState
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal DriveState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x7100 + id;
        }

        public int ErrorState
        {
            get => client.ReadAny<int>(indexGroup, 0x01);
        }

        /// <summary>
        /// Total output in absolute units.
        /// Base unit / s 
        /// Symbolic access possible: "DriveOutput"
        /// </summary>
        public double TotalOutputAbsoluteUnits
        {
            get => client.ReadAny<double>(indexGroup, 0x02);
        }

        /// <summary>
        /// Total output in percent.
        /// </summary>
        public double TotalOutputPercent
        {
            get => client.ReadAny<double>(indexGroup, 0x03);
        }

        /// <summary>
        /// Total output in volts.
        /// </summary>
        public double TotalOutputVolts
        {
            get => client.ReadAny<double>(indexGroup, 0x04);
        }
    }
}
