using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents a group in the TwinCAT NC system.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        /// <param name="client">The ADS client.</param>
        /// <param name="id">The group ID.</param>
        public Group(AdsClient client, uint id)
        {
            Parameters = new GroupParameters(client, id);
            State = new GroupState(client, id);
            Functions = new GroupFunctions(client, id);
        }

        /// <summary>
        /// Gets the parameters of the group.
        /// </summary>
        public GroupParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the state of the group.
        /// </summary>
        public GroupState State { get; private set; }

        /// <summary>
        /// Gets the functions of the group.
        /// </summary>
        public GroupFunctions Functions { get; private set; }
    }
}
