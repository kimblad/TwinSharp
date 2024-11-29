using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Manages the low level Ring 0 operations for the NC system.
    /// </summary>
    public class Ring0Manager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ring0Manager"/> class.
        /// Connects to the specified target using the ADS client.
        /// </summary>
        /// <param name="target">The target AmsNetId to connect to.</param>
        public Ring0Manager(AmsNetId target)
        {
            AdsClient client = new AdsClient();

            // Ring 0 manager works at port 500
            client.Connect(target, AmsPort.R0_NC);

            Parameters = new Ring0Parameters(client);
            State = new Ring0State(client);
        }

        /// <summary>
        /// Gets the parameters for the Ring 0 operations.
        /// </summary>
        public Ring0Parameters Parameters { get; private set; }

        /// <summary>
        /// Gets the state information for the Ring 0 operations.
        /// </summary>
        public Ring0State State { get; private set; }
    }
}
