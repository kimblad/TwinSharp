using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The TableFunctions class provides methods to generate and delete various types of tables
    /// with specified dimensions and interpolation types. It interacts with a TwinCAT AdsClient
    /// to perform these operations. The class supports generating general tables, valve diagram
    /// tables, and motion function tables, each with specific table types and dimensions.
    /// </summary>
    public class TableFunctions
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal TableFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0xA200 + id;
        }

        /// <summary>
        /// Generates table with dimension (n*m):
        /// Table types: 1,2,3,4 
        /// Dimension: at least 2x1
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="lineCount"></param>
        /// <param name="columnCount"></param>
        public void GenerateTable(TableInterpolationType tableType, uint lineCount, uint columnCount)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)tableType);
            bw.Write(lineCount);
            bw.Write(columnCount);

            client.WriteAny(indexGroup, 0x10000, ms.ToArray());
        }

        /// <summary>
        /// Generates valve diagram table with dimension (n*m):
        /// Table types: 1,3 
        /// Dimension: at least 2x1
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="lineCount"></param>
        /// <param name="columnCount"></param>
        public void GenerateValveDiagramTable(TableInterpolationType tableType, uint lineCount, uint columnCount)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)tableType);
            bw.Write(lineCount);
            bw.Write(columnCount);

            client.WriteAny(indexGroup, 0x10001, ms.ToArray());
        }

        /// <summary>
        /// Generates "Motion Function" table with dimension (n*m):
        /// Table types: 3, 4
        /// Dimension: at least 2x1
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="lineCount"></param>
        /// <param name="columnCount"></param>
        public void GenerateMotionFunctionTable(TableInterpolationType tableType, uint lineCount, uint columnCount)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)tableType);
            bw.Write(lineCount);
            bw.Write(columnCount);

            client.WriteAny(indexGroup, 0x10010, ms.ToArray());
        }

        /// <summary>
        /// Deletes table with dimension (n*m)
        /// Table types: 1,2,3,4 
        /// </summary>
        public void DeleteTable()
        {
            client.WriteAny(indexGroup, 0x20000, true);
        }
    }
}