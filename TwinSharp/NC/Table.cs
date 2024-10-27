using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The <c>Table</c> class encapsulates various functionalities, parameters, and states related to a table in the context of TwinCAT ADS.
    /// It provides a structured way to interact with tables, which are likely used for motion control or other automation tasks.
    /// </summary>
    public class Table
    {
        public readonly TableFunctions Functions;
        public readonly TableParameters Parameters;
        public readonly TableState State;

        internal Table(AdsClient client, uint id)
        {
            Functions = new TableFunctions(client, id);
            Parameters = new TableParameters(client, id);
            State = new TableState(client, id);
        }
    }
}
