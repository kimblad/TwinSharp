using TwinCAT.Ads;
using TwinCAT.Router.Native;

namespace TwinSharp.NC
{
    public class ChannelParameters
    {
        private readonly AdsClient client;
        private readonly uint indexGroup;

        internal ChannelParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x2000 + id;
        }

        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 80);
        }

        public ChannelType Type
        {
            get => (ChannelType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        public InterpreterType InterpreterType
        {
            get => (InterpreterType)client.ReadAny<uint>(indexGroup, 0x04);
        }

        public uint ProgramLoadBufferSize
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

        public uint ProgramNumberJobList
        {
            get => client.ReadAny<uint>(indexGroup, 0x06);
        }

        public InterpolationLoadLogMode InterpolationLoadLogMode
        {
            get => (InterpolationLoadLogMode)client.ReadAny<uint>(indexGroup, 0x07);
            set => client.WriteAny(indexGroup, 0x07, (uint)value);
        }

        public InterpolationTraceMode InterpolationTraceMode
        {
            get => (InterpolationTraceMode)client.ReadAny<uint>(indexGroup, 0x08);
            set => client.WriteAny(indexGroup, 0x08, (uint)value);
        }

        /// <summary>
        /// Records all feeder entries in a log file named "TcNci.log"
        /// </summary>
        public uint RecordAllFeederEntries
        {
            get => client.ReadAny<uint>(indexGroup, 0x0A);
            set => client.WriteAny(indexGroup, 0x0A, value);
        }

        /// <summary>
        ///  Channel specific level for NC logger messages 0: errors only 1: all NC messages
        /// </summary>
        public uint NcLoggerLevel
        {
            get => client.ReadAny<uint>(indexGroup, 0x0B);
            set => client.WriteAny(indexGroup, 0x0B, value);
        }


        public double G70Factor
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
            set => client.WriteAny(indexGroup, 0x12, value);
        }

        public double G71Factor
        {
            get => client.ReadAny<double>(indexGroup, 0x13);
            set => client.WriteAny(indexGroup, 0x13, value);
        }

        public ushort ActivationOfDefaultGcode
        {
            get => client.ReadAny<ushort>(indexGroup, 0x15);
            set => client.WriteAny(indexGroup, 0x15, value);
        }

        /// <summary>
        /// Group ID (only explicit for 3D and FIFO channel)
        /// </summary>
        public uint GroupId
        {
            get => client.ReadAny<uint>(indexGroup, 0x21);
        }
    }
}