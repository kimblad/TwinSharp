using TwinCAT.Ads;

namespace TwinSharp
{
    public class PLC
    {
        public readonly PlcAppSystemInfo AppInfo;
        public PLC(AdsClient client)
        {
            AppInfo = new PlcAppSystemInfo(client);
        }
    }
}
