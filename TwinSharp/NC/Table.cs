using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The <c>Table</c> class encapsulates various functionalities, parameters, and states related to a table in the context of TwinCAT ADS.
    /// It provides a structured way to interact with tables, which are likely used for motion control or other automation tasks.
    /// </summary>
    public class Table
    {

        internal Table(AdsClient client, uint id)
        {
            Functions = new TableFunctions(client, id);
            Parameters = new TableParameters(client, id);
            State = new TableState(client, id);
        }

        /// <summary>
        /// Gets the functions related to table operations such as generating and deleting tables.
        /// </summary>
        public TableFunctions Functions { get; private set; }

        /// <summary>
        /// Gets the parameters of the table including ID, name, types, dimensions, and other properties.
        /// </summary>
        public TableParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the state of the table including the user counter.
        /// </summary>
        public TableState State { get; private set; }
    }
}
