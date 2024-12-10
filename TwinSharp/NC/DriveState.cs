using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the state of a drive in a TwinCAT system.
    /// Provides properties to access various drive parameters such as error state, total output in absolute units, percent, and volts.
    /// </summary>
    public class DriveState
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal DriveState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x7100 + id;
        }

        /// <summary>
        /// Error state of the drive.
        /// </summary>
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
