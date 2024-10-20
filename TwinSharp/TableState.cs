using TwinCAT.Ads;

namespace TwinSharp
{
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
