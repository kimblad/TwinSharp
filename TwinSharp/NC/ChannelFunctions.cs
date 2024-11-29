using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class ChannelFunctions
    {
        private readonly AdsClient client;
        private readonly uint groupIndex;

        internal ChannelFunctions(AdsClient client, uint id)
        {
            this.client = client;
            groupIndex = 0x2200 + id;
        }

        public void LoadProgramByNumber(uint programNumber)
        {
            client.WriteAny(groupIndex, 0x01, programNumber);
        }

        public void StartInterpreter()
        {
            client.Write(groupIndex, 0x02);
        }

        public void LoadProgramByName(string programName)
        {
            client.WriteAny(groupIndex, 0x04, programName, [programName.Length]);
        }

        public void SetInterpreterOperationMode(InterpreterOperationMode mode)
        {
            client.WriteAny(groupIndex, 0x05, (ushort)mode);
        }

        public void SetPathForSubRoutines(string path)
        {
            client.WriteAny(groupIndex, 0x06, path, [path.Length]);
        }

        public void ResetChannel()
        {
            client.Write(groupIndex, 0x10);
        }

        public void StopChannel()
        {
            client.Write(groupIndex, 0x11);
        }

        public void RetryChannel()
        {
            client.Write(groupIndex, 0x12);
        }

        public void SkipChannel()
        {
            client.Write(groupIndex, 0x13);
        }
    }
}