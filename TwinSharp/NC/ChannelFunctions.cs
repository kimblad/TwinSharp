using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The ChannelFunctions class provides methods to interact with and control NC (Numerical Control) channels
    /// using an AdsClient. It allows loading NC programs by number or name, starting the interpreter, setting
    /// the interpreter operation mode, setting paths for subroutines, and controlling the channel with reset,
    /// stop, retry, and skip functionalities.
    /// </summary>
    public class ChannelFunctions
    {
        private readonly AdsClient client;
        private readonly uint groupIndex;

        internal ChannelFunctions(AdsClient client, uint id)
        {
            this.client = client;
            groupIndex = 0x2200 + id;
        }

        /// <summary>
        /// Load NC program with program number
        /// </summary>
        /// <param name="programNumber"></param>
        public void LoadProgramByNumber(uint programNumber)
        {
            client.WriteAny(groupIndex, 0x01, programNumber);
        }

        /// <summary>
        /// Start interpreter
        /// </summary>
        public void StartInterpreter()
        {
            client.Write(groupIndex, 0x02);
        }

        /// <summary>
        /// Load NC program by name. The standard NC path does not have to be given although it may. Other paths are also permitted.
        /// </summary>
        /// <param name="programName"></param>
        public void LoadProgramByName(string programName)
        {
            client.WriteAny(groupIndex, 0x04, programName, [programName.Length]);
        }

        /// <summary>
        /// Set the interpreter/channel operation mode
        /// </summary>
        /// <param name="mode"></param>
        public void SetInterpreterOperationMode(InterpreterOperationMode mode)
        {
            client.WriteAny(groupIndex, 0x05, (ushort)mode);
        }

        /// <summary>
        /// Set the path for subroutines
        /// </summary>
        /// <param name="path"></param>
        public void SetPathForSubRoutines(string path)
        {
            client.WriteAny(groupIndex, 0x06, path, [path.Length]);
        }

        /// <summary>
        /// Reset channel
        /// </summary>
        public void ResetChannel()
        {
            client.Write(groupIndex, 0x10);
        }

        /// <summary>
        /// Stop channel
        /// </summary>
        public void StopChannel()
        {
            client.Write(groupIndex, 0x11);
        }

        /// <summary>
        /// "Retry" Channel(restart Channel)
        /// </summary>
        public void RetryChannel()
        {
            client.Write(groupIndex, 0x12);
        }

        /// <summary>
        /// "Skip" Channel (skip task/block)
        /// </summary>
        public void SkipChannel()
        {
            client.Write(groupIndex, 0x13);
        }
    }
}