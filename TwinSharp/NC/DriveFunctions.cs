using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The DriveFunctions class provides methods to interact with and manipulate drive tables.
    /// It allows for the removal and deletion of characteristic drive tables.
    /// </summary>
    public class DriveFunctions
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal DriveFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x7200 + id;
        }

        /// <summary>
        /// Remove and delete the characteristic drive table
        /// </summary>
        /// <param name="tableId">Table-ID</param>
        public void RemoveAndDeleteCharacteristicDriveTable(ulong tableId)
        {
            client.WriteAny(indexGroup, 0x102, tableId);
        }

    }
}
