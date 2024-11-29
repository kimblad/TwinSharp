using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class ChannelState
    {
        private readonly AdsClient client;
        private readonly uint indexGroup;

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

        public InterpreterState InterpreterState
        {
            get => (InterpreterState)client.ReadAny<uint>(indexGroup, 0x03);
        }

        public InterpreterOperationMode InterpreterOperationMode
        {
            get => (InterpreterOperationMode)client.ReadAny<uint>(indexGroup, 0x04);
        }

        public uint CurrentLoadedProgramNumber
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

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