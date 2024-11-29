using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the state of an NC (Numerical Control) channel, providing access to various channel properties such as error codes, group count, interpreter state, operation mode, and program information using an AdsClient.
    /// </summary>
    public class ChannelState
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal ChannelState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x2100 + id;
        }

        /// <summary>
        /// Error code Channel 
        /// </summary>
        public int ErrorCode
        {
            get => client.ReadAny<int>(indexGroup, 0x01);
        }

        /// <summary>
        /// Number of groups in the Channel
        /// </summary>
        public uint GroupCount
        {
            get => client.ReadAny<uint>(indexGroup, 0x02);
        }

        /// <summary>
        /// Interpreter status
        /// </summary>
        public InterpreterState InterpreterState
        {
            get => (InterpreterState)client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// Interpreter/channel operation mode
        /// </summary>
        public InterpreterOperationMode InterpreterOperationMode
        {
            get => (InterpreterOperationMode)client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// Current loaded program number
        /// </summary>
        public uint CurrentLoadedProgramNumber
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

        /// <summary>
        /// Program name of currently loaded program
        /// (100 characters, null-terminated)
        /// </summary>
        public string CurrentLoadedProgramName
        {
            get => client.ReadString(indexGroup, 0x07, 100);
        }

        /// <summary>
        /// Interpreter simulation mode 0: off (default) 1: on
        /// </summary>
        public uint InterpreterSimulationMode
        {
            get => client.ReadAny<uint>(indexGroup, 0x08);
        }

        /// <summary>
        /// If the interpreter is in the aborted state, the current text index can be read out here
        /// </summary>
        public uint TextIndex
        {
            get => client.ReadAny<uint>(indexGroup, 0x10);
        }
    }
}