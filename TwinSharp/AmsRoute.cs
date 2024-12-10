using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// Represents a route to a TwinCAT target system, including its name, address, AmsNetId, protocol, and flags.
    /// Provides functionality to retrieve the state information of the route.
    /// </summary>
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

        /// <summary>
        /// Bitmask of flags that describe the route.
        /// </summary>
        public int Flags { get; }


        /// <summary>
        /// Gets the state information for this ams route.
        /// </summary>
        /// <returns></returns>
        public StateInfo GetStateInfo()
        {
            using var client = new AdsClient();
            client.Connect(AmsNetId, AmsPort.SystemService);
            return client.ReadState();
        }


        /// <summary>
        /// Returns a string representation of the route.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + " " + AmsNetId.ToString();
        }
    }
}
