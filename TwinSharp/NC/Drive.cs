using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class Drive
    {
        public readonly DriveParameters Parameters;
        public readonly DriveState State;

        internal Drive(AdsClient client, uint id)
        {
            Parameters = new DriveParameters(client, id);
            State = new DriveState(client, id);
        }
    }
}
