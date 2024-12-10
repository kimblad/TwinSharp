using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The Drive class represents a drive in a TwinCAT system. It provides access to the drive's parameters
    /// and state through the DriveParameters and DriveState classes, respectively. The class is initialized
    /// with an AdsClient and a drive ID, which are used to interact with the drive's properties and state.
    /// </summary>
    public class Drive
    {
        internal Drive(AdsClient client, uint id)
        {
            Parameters = new DriveParameters(client, id);
            State = new DriveState(client, id);
            Functions = new DriveFunctions(client, id);
        }

        /// <summary>
        /// Gets the parameters of the drive.
        /// </summary>
        public DriveParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the state of the drive.
        /// </summary>
        public DriveState State { get; private set; }

        /// <summary>
        /// Gets the functions of the drive.
        /// </summary>
        public DriveFunctions Functions { get; private set; }
    }
}
