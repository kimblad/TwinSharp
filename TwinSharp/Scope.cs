using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// Class to interact with a TwinCAT 2 scope. Note: not compatible with TwinCAT 3.
    /// </summary>
    public class Scope
    {
        readonly AdsClient client;

        const uint indexGroupStates = 0x1000;
        const uint indexGroupFunctions = 0x2000;
        const uint indexGroupView = 0x3000;

        /// <summary>
        /// Creates a new instance of the Scope class. The AmsNetId should typically point to a TwinCAT 2 runtime.
        /// </summary>
        /// <param name="amsNetId"></param>
        public Scope(AmsNetId amsNetId)
        {
            client = new AdsClient();
            client.Connect(amsNetId, 14000);
        }

        /// <summary>
        /// Gets or sets the online mode of the scope. If true, the scope is online and can be triggered. If false, the scope is offline.
        /// </summary>
        public bool OnlineMode
        {
            get
            {
                return client.ReadAny<bool>(indexGroupStates, 0x01);
            }
            set
            {
                if (value)
                    client.WriteAny(indexGroupStates, 0x01, true);
                else
                    client.WriteAny(indexGroupStates, 0x02, true);
            }
        }


        /// <summary>
        /// Load *.scp File (Scope Configuration Project)
        /// </summary>
        /// <param name="filename">E.g. D:\\TwinCAT\\scope\\achse2.scp</param>
        public void LoadConfigurationFile(string filename)
        {
            client.WriteAny(indexGroupFunctions, 0x01, filename);
        }

        /// <summary>
        /// Issuing this command triggers the Scope. It must, however, be online.
        /// </summary>
        public void ManualTrigger()
        {
            //Note: The Scope View properties can currently only be used if only one view is active in the application.
            //In other words, the ID is always 1.

            const uint id = 1;
            client.WriteAny(indexGroupView, 0x0100 + id, true);
        }
    }
}
