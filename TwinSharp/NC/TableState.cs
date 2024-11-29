using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the state of a table in a TwinCAT NC (Numerical Control) system.
    /// This class provides access to the 'User Counter'.
    /// It uses an AdsClient to communicate with the TwinCAT system and read the necessary data.
    /// </summary>
    public class TableState
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal TableState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0xA100 + id;
        }

        /// <summary>
        /// 'User Counter' (number of table user)
        /// </summary>
        public int UserCounter
        {
            get
            {
                return client.ReadAny<int>(indexGroup, 0x0A);
            }
        }

    }
}
