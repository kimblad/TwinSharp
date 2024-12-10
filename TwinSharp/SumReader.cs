using System.Diagnostics;
using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// Using the ADS Sum Command it is possible to read or write several variables in one command.
    /// </summary>
    public class SumReader
    {
        readonly AdsClient client;
        readonly List<SumReadVariable> variablesToRead;


        byte[] bufferToWrite;
        byte[] bufferToRead;
        ReadOnlyMemory<byte> memoryToWriteToAds;
        Memory<byte> memoryToReadIntoFromAds;

        byte[] valuesWithoutErrorCodes;

        /// <summary>
        /// Creates a new sum reader. Used for reading multiple variables in one ADS command.
        /// </summary>
        /// <param name="client"></param>
        public SumReader(AdsClient client)
        {
            this.client = client;

            variablesToRead = [];
            bufferToWrite = [];
            bufferToRead = [];

            AdsErrorCodes = [];
            valuesWithoutErrorCodes = [];
        }

        /// <summary>
        /// The ADS return codes for the variables that were read.
        /// </summary>
        public AdsErrorCode[] AdsErrorCodes { get; private set; }


        /// <summary>
        /// Add a variable based on symbol name to this sum reader.
        /// </summary>
        /// <param name="symbolName"></param>
        /// <param name="type"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddVariable(string symbolName, Type type)
        {
            throw new NotImplementedException();
            //TODO: Implement this method. I think we can find the index group and offset of this symbol and then use the other
            // AddVariable method to add the variable.

            //RegenerateBuffers();
        }

        /// <summary>
        /// Add a variable based on index group and index offset to this sum reader.
        /// </summary>
        /// <param name="indexGroup"></param>
        /// <param name="indexOffset"></param>
        /// <param name="type"></param>
        public void AddVariable(uint indexGroup, uint indexOffset, Type type)
        {
            var variable = new SumReadVariable(indexGroup, indexOffset, type);
            variablesToRead.Add(variable);
            RegenerateBuffers();
        }

        private void RegenerateBuffers()
        {
            int readLength = variablesToRead.Count * 4; //4 bytes for each variables Ads Error code.

            int writeLength = variablesToRead.Count * 12;
            bufferToWrite = new byte[writeLength];

            //Write data for handles into the stream.
            var ms = new MemoryStream(bufferToWrite);
            using var br = new BinaryWriter(ms);

            foreach (var variable in variablesToRead)
            {
                br.Write(variable.Index);
                br.Write(variable.Offset);
                br.Write(variable.Length);

                readLength += variable.Length;
            }

            //Rezise memorys and buffers.
            memoryToWriteToAds = new ReadOnlyMemory<byte>(bufferToWrite);

            bufferToRead = new byte[readLength];
            memoryToReadIntoFromAds = new Memory<byte>(bufferToRead);

            AdsErrorCodes = new AdsErrorCode[variablesToRead.Count];

            //Dimension the returned values array to not include the ads error codes.
            valuesWithoutErrorCodes = new byte[SizeWithoutErrorCodes];
        }

        /// <summary>
        /// Reads the added variables. The values are stored in the readValues array in the order they were added.
        /// If the operation is not succesful, false is returned and the field "AdsErrorCodes" will contain the error codes for the respective variable.
        /// </summary>
        /// <param name="returnedValues"></param>
        /// <returns>If the operation was succesful without ADS errors. If not succesful, user should examine the <see cref="AdsErrorCodes"/> </returns>
        public bool ReadVariables(out byte[] returnedValues)
        {
            const uint indexSumCommand = 0xF080;
            uint subIndex = (uint)variablesToRead.Count; //Documentation specifies that subIndex should be the number of variables to read.

            int bytesRead = client.ReadWrite(indexSumCommand, subIndex,
                memoryToReadIntoFromAds, memoryToWriteToAds);


            //First the ADS error codes are supplied.
            //Then the data for the actual variables are supplied.
            //Read the ADS error codes.
            var ms = new MemoryStream(bufferToRead);
            using var br = new BinaryReader(ms);

            bool readSuccess = true;

            for (int i = 0; i < variablesToRead.Count; i++)
            {
                var errorCode = (AdsErrorCode)br.ReadUInt32();
                AdsErrorCodes[i] = errorCode;

                //If any ADS has an error code, the read is considered a failure.
                if (errorCode != AdsErrorCode.NoError)
                    readSuccess = false;
            }

            //Create a new array without the ADS error codes.
            Array.Copy(bufferToRead, SizeOfAdsErrorCodes, valuesWithoutErrorCodes, 0, SizeWithoutErrorCodes);

            returnedValues = valuesWithoutErrorCodes;

            return readSuccess;
        }


        private int SizeOfAdsErrorCodes
        {
            get => variablesToRead.Count * 4;
        }

        private int SizeWithoutErrorCodes
        {
            get => bufferToRead.Length - SizeOfAdsErrorCodes;
        }

        private class SumReadVariable
        {
            readonly internal uint Index;
            readonly internal uint Offset;
            readonly internal Type Type;
            readonly internal int Length;

            internal SumReadVariable(uint index, uint offset, Type type)
            {
                Index = index;
                Offset = offset;
                Type = type;

                

                Length = GetLength(type);
            }   

            private int GetLength(Type type)
            {
                if (type == typeof(string))
                {
                    throw new ArgumentException("When reading a string you must use the constructor that supplies a length.");
                }

                if (type == typeof(bool))
                    return sizeof(bool);
                else if (type == typeof(byte))
                    return sizeof(byte);
                else if (type == typeof(ushort))
                    return sizeof(ushort);
                else if (type == typeof(uint))
                    return sizeof(uint);
                else if (type == typeof(sbyte))
                    return sizeof(sbyte);
                else if (type == typeof(short))
                    return sizeof(short);
                else if (type == typeof(int))
                    return sizeof(int);
                else if (type == typeof(long))
                    return sizeof(long);
                else if (type == typeof(ulong))
                    return sizeof(ulong);
                else if (type == typeof(float))
                    return sizeof(float);
                else if (type == typeof(double))
                    return sizeof(double);
                else
                    throw new ArgumentException("Unsupported type. Use the constructuor that supplies a length.");
            }

            internal SumReadVariable(uint index, uint offset, Type type, int length)
            {
                Index = index;
                Offset = offset;
                Type = type;
                Length = length;
            }

        }
    }
}
