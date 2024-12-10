using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents a controller in the TwinCAT NC system. This class provides access to the controller's parameters
    /// and state, allowing for the retrieval and manipulation of various control parameters and state information.
    /// It interacts with an AdsClient to communicate with the underlying system.
    /// </summary>
    public class Controller
    {
        internal Controller(AdsClient client, uint id)
        {
            Parameters = new ControllerParameters(client, id);
            State = new ControllerState(client, id);
        }

        /// <summary>
        /// Gets the parameters of the controller, which include various control parameters such as ID, name, type, and control weights.
        /// </summary>
        public ControllerParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the state of the controller, which includes various state parameters such as error state, output in absolute units, output in percent, and output in volts.
        /// </summary>
        public ControllerState State { get; private set; }

    }
}
