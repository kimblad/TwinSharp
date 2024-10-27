using TwinCAT.Ads;

namespace TwinSharp
{
    public class PLC
    {
        public readonly PlcAppSystemInfo AppInfo;

        readonly AdsClient client;
        public PLC(AdsClient client)
        {
            this.client = client;
            AppInfo = new PlcAppSystemInfo(client);
        }

        public void Start()
        {
            var currentState = client.ReadState();

            if (currentState.AdsState == AdsState.Run)
                return; //Already running, if we try to set it anyway we get an exception.

            var stateInfo = new StateInfo(AdsState.Run, currentState.DeviceState);
            client.WriteControl(stateInfo);
        }

        public void Stop()
        {
            var currentState = client.ReadState();

            if (currentState.AdsState == AdsState.Stop)
                return; //Already stopped, if we try to set it anyway we get an exception.

            var stateInfo = new StateInfo(AdsState.Stop, currentState.DeviceState);
            client.WriteControl(stateInfo);
        }
    }
}
