using TwinCAT.Ads;

namespace TwinSharp
{
    public class ControllerState
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal ControllerState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x6100 + id;
        }

        /// <summary>
        /// Error state controller
        /// </summary>
        public int ErrorState
        {
            get => client.ReadAny<int>(indexGroup, 0x01);
        }

        /// <summary>
        /// Controller output in absolute units.
        /// Base Unit / s
        /// Symbolic access possible "CtrlOutput".
        /// </summary>
        public double OutputAbsoluteUnits
        {
            get => client.ReadAny<double>(indexGroup, 0x02);
        }

        /// <summary>
        /// Controller output in percent
        /// </summary>
        public double OutputPercent
        {
            get => client.ReadAny<double>(indexGroup, 0x03);
        }

        /// <summary>
        /// Controller output in volts
        /// </summary>
        public double OutputVolts
        {
            get => client.ReadAny<double>(indexGroup, 0x04);
        }


    }
}
