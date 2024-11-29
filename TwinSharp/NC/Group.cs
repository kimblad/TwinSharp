using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class Group
    {
        public readonly GroupParameters Parameters;
        public readonly GroupState State;
        public readonly GroupFunctions Functions;


        public Group(AdsClient client, uint id)
        {
            Parameters = new GroupParameters(client, id);
            State = new GroupState(client, id);
            Functions = new GroupFunctions(client, id);
        }
    }
}