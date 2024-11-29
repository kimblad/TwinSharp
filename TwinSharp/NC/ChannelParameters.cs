using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents the parameters of a channel in the TwinCAT NC system.
    /// This class provides properties to access various channel parameters such as ID, Name, Type, InterpreterType, 
    /// ProgramLoadBufferSize, ProgramNumberJobList, InterpolationLoadLogMode, InterpolationTraceMode, 
    /// RecordAllFeederEntries, NcLoggerLevel, G70Factor, G71Factor, ActivationOfDefaultGcode, and GroupId.
    /// These properties interact with the TwinCAT ADS client to read and write the respective values.
    /// </summary>
    public class ChannelParameters
    {
        private readonly AdsClient client;
        private readonly uint indexGroup;

        internal ChannelParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x2000 + id;
        }

        /// <summary>
        /// Channel ID
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Channel name
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 80);
        }

        /// <summary>
        /// Channel type
        /// </summary>
        public ChannelType Type
        {
            get => (ChannelType)client.ReadAny<uint>(indexGroup, 0x03);
        }

        /// <summary>
        /// Interpreter type
        /// </summary>
        public InterpreterType InterpreterType
        {
            get => (InterpreterType)client.ReadAny<uint>(indexGroup, 0x04);
        }

        /// <summary>
        /// Program load buffer size in bytes
        /// </summary>
        public uint ProgramLoadBufferSize
        {
            get => client.ReadAny<uint>(indexGroup, 0x05);
        }

        /// <summary>
        /// Program no. according to job list
        /// </summary>
        public uint ProgramNumberJobList
        {
            get => client.ReadAny<uint>(indexGroup, 0x06);
        }

        /// <summary>
        /// Load log mode
        /// </summary>
        public InterpolationLoadLogMode InterpolationLoadLogMode
        {
            get => (InterpolationLoadLogMode)client.ReadAny<uint>(indexGroup, 0x07);
            set => client.WriteAny(indexGroup, 0x07, (uint)value);
        }

        /// <summary>
        /// Trace mode
        /// </summary>
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

        /// <summary>
        /// Factor for G70.
        /// </summary>
        public double G70Factor
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
            set => client.WriteAny(indexGroup, 0x12, value);
        }

        /// <summary>
        /// Factor for G71.
        /// </summary>
        public double G71Factor
        {
            get => client.ReadAny<double>(indexGroup, 0x13);
            set => client.WriteAny(indexGroup, 0x13, value);
        }

        /// <summary>
        /// Activation of default G-code. 0/1 default: FALSE
        /// </summary>
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