using TwinCAT.Ads;

namespace TwinSharp
{
    public class DriveFunctions
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal DriveFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x7200 + id;
        }

        public void RemoveAndDeleteCharacteristicDriveTable(ulong tableId)
        {
            client.WriteAny(indexGroup, 0x102, tableId);
        }

    }
}
