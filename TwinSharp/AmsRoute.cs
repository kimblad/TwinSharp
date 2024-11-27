using TwinCAT.Ads;

namespace TwinSharp
{
    public class AmsRoute
    {
        internal AmsRoute(string name, string adress, AmsNetId amsNetId, string protocol, int flags)
        {
            Name = name;
            Adress = adress;
            AmsNetId = amsNetId;
            Protocol = protocol;
            Flags = flags;
        }

        public string Name { get; }
        public string Adress { get; }
        public AmsNetId AmsNetId { get; }

        public string Protocol { get; }
        public int Flags { get; }


        public override string ToString()
        {
            return Name + " " + AmsNetId.ToString();
        }
    }
}
