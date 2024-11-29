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

        /// <summary>
        /// Name of the possible target system logged on to the current TwinCAT router
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Address of tue TwinCAT target system
        /// The address depends on the transport protocol being used.In addition to TCP/IP addresses, addresses of Profibus devices are possible, which in turn must support the ADS protocol in order to be addressed as "target system" or "remote system".
        /// </summary>
        public string Adress { get; }

        /// <summary>
        /// AmsNetId of the target system
        /// </summary>
        public AmsNetId AmsNetId { get; }

        /// <summary>
        /// Protocol used for communication with the target system.
        /// </summary>
        public string Protocol { get; }
        public int Flags { get; }


        public StateInfo GetStateInfo()
        {
            using var client = new AdsClient();
            client.Connect(AmsNetId, AmsPort.SystemService);
            return client.ReadState();
        }


        public override string ToString()
        {
            return Name + " " + AmsNetId.ToString();
        }
    }
}
