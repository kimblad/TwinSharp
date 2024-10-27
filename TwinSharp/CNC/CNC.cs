using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class CNC : IDisposable
    {
        public CncPlatform Platform;
        public CncAxis[] Axes;
        public CncChannel[] Channels;

        public AdsClient geoClient; //("GEO CNC") operates in the interpolation cycle of the CNC. Among other tasks, it calculates the respective set values for the axes and controls the axes.
        public AdsClient sdaClient; //The "SDA CNC task" deals with decoding and processing of the NC programs and should therefore have a lower priority than the GEO task.
        public AdsClient comClient; //The "COM CNC task" deals with the communication integration of the CNC kernel.

        public CNC(AmsNetId target)
        {
            geoClient = new AdsClient();
            geoClient.Connect(target, 551);

            sdaClient = new AdsClient();
            sdaClient.Connect(target, 552);

            comClient = new AdsClient();
            comClient.Connect(target, 553);

            var comDescriptions = CreateComDictionary(comClient);

            Platform = new CncPlatform(comClient, comDescriptions);

            Channels = new CncChannel[Platform.ChannelCount];
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new CncChannel(geoClient, comClient, i + 1, comDescriptions);
            }

            Axes = new CncAxis[Platform.AxisCount];
            for (int i = 0; i < Axes.Length; i++)
            {
                Axes[i] = new CncAxis((uint)(i + 1), target, comClient);
            }
        }

        private static Dictionary<string, ObjectDescription> CreateComDictionary(AdsClient comClient)
        {
            uint indexGroup = 0x130100 + 0; //0 lists everything, otherwise lists per channel
            uint indexOffset = 0; //Total object count is found here.

            const uint objectDescriptionSize = 84;

            uint objectCount = comClient.ReadAny<uint>(indexGroup, indexOffset);

            //Reading several object descriptions with one access
            //If exactly a multiple of 84 bytes is provided as return memory, object descriptions are returned in
            //sequence starting from the transferred index.

            var buffer = new byte[objectCount * objectDescriptionSize];
            var memory = new Memory<byte>(buffer);

            int bytesRead = comClient.Read(indexGroup, indexOffset + 1, memory);

            var objectDescriptions = new Dictionary<string, ObjectDescription>();
            var oneDescription = new byte[objectDescriptionSize];

            for (int i = 0; i < objectCount; i++)
            {
                Array.Copy(buffer, i * objectDescriptionSize, oneDescription, 0, objectDescriptionSize);
                var objectDescription = new ObjectDescription(oneDescription);
                objectDescriptions.Add(objectDescription.Name, objectDescription);
            }

            return objectDescriptions;
        }



        public void Dispose()
        {
            comClient?.Dispose();
            geoClient?.Dispose();
            sdaClient?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
    public class ObjectDescription
    {
        public uint Id;
        public uint Size;
        public ushort WriteAccess;
        public uint IndexGroup;
        public uint IndexOffset;
        public string Name;
        public string Type;



        public ObjectDescription(byte[] bytes)
        {
            if (bytes.Length != 84)
                throw new Exception("Invalid length when creating Object Description");

            var ms = new MemoryStream(bytes);
            using var br = new BinaryReader(ms);
            var ascii = new ASCIIEncoding();

            Id = br.ReadUInt32();
            Size = br.ReadUInt32();
            WriteAccess = br.ReadUInt16();
            br.ReadUInt16(); //padding
            IndexGroup = br.ReadUInt32();
            IndexOffset = br.ReadUInt32();
            Name = ascii.GetString(br.ReadBytes(32), 0, 32).TrimEnd('\0');
            Type = ascii.GetString(br.ReadBytes(32), 0, 32).TrimEnd('\0');
        }

        public override string ToString() => Name;

    }
}