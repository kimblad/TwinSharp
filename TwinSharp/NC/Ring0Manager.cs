using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class Ring0Manager
    {
        public readonly Ring0Parameters Parameters;
        public readonly Ring0State State;

        public Ring0Manager(AmsNetId target)
        {
            AdsClient client = new AdsClient();

            //Ring 0 manager works at port 500
            client.Connect(target, AmsPort.R0_NC);

            Parameters = new Ring0Parameters(client);
            State = new Ring0State(client);
        }

    }
}
