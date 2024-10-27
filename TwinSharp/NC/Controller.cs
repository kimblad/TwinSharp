using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class Controller
    {
        public readonly ControllerParameters Parameters;
        public readonly ControllerState State;

        internal Controller(AdsClient client, uint id)
        {
            Parameters = new ControllerParameters(client, id);
            State = new ControllerState(client, id);
        }
    }
}
