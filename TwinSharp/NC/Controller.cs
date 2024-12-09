using TwinCAT.Ads;

namespace TwinSharp.NC
{

    public class Controller
    {
        internal Controller(AdsClient client, uint id)
        {
            Parameters = new ControllerParameters(client, id);
            State = new ControllerState(client, id);
        }

        public ControllerParameters Parameters { get; private set; }

        public ControllerState State { get; private set; }

    }
}
