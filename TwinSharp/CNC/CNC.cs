using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Represents the TwinCAT CNC system. This class provides access to the CNC's platform, axes, and channels,
    /// </summary>
    public class CNC : IDisposable
    {
        /// <summary>
        /// Creates a new representation of a existing TwinCAT CNC system at the specified target AmsNetId.
        /// </summary>
        /// <param name="target"></param>
        public CNC(AmsNetId target)
        {
            ClientGeo = new AdsClient();
            ClientGeo.Connect(target, 551);

            ClientSda = new AdsClient();
            ClientSda.Connect(target, 552);

            ClientCom = new AdsClient();
            ClientCom.Connect(target, 553);

            ClientPlc = new AdsClient();
            ClientPlc.Connect(target, 851);

#if DEBUG
            ClientGeo.Timeout = 60_0000;
            ClientSda.Timeout = 60_0000;
            ClientCom.Timeout = 60_0000;
            ClientPlc.Timeout = 60_0000;
#endif

            var comDescriptions = CreateComDictionary(ClientCom);

            Platform = new CncPlatform(ClientCom, comDescriptions);

            Channels = new CncChannel[Platform.ChannelCount];
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new CncChannel(ClientPlc, ClientGeo, ClientSda, ClientCom, i + 1, comDescriptions);
            }

            Axes = new CncAxis[Platform.AxisCount];
            for (int i = 0; i < Axes.Length; i++)
            {
                Axes[i] = new CncAxis((uint)(i + 1), ClientPlc, ClientCom);
            }
        }

        /// <summary>
        /// Platform data is data which cannot be assigned to a specific axis or a channel but has an effect on the entire NC control.
        /// </summary>
        public CncPlatform Platform { get; private set; }

        /// <summary>
        /// Array of all existing CNC axes.
        /// </summary>
        public CncAxis[] Axes { get; private set; }


        /// <summary>
        /// Array of all existing CNC channels.
        /// </summary>
        public CncChannel[] Channels { get; private set; }

        /// <summary>
        /// ADS client connected to the GEO CNC. Operates in the interpolation cycle of the CNC. Among other tasks, it calculates the respective set values for the axes and controls the axes.
        /// </summary>
        public AdsClient ClientGeo { get; private set; }

        /// <summary>
        /// ADS client connected to the "SDA CNC task" deals with decoding and processing of the NC programs and should therefore have a lower priority than the GEO task.
        /// </summary>
        public AdsClient ClientSda { get; private set; }

        /// <summary>
        /// ADS client connected to the "COM CNC task" deals with the communication integration of the CNC kernel
        /// </summary>
        public AdsClient ClientCom { get; private set; }

        /// <summary>
        /// ADS Client connected to the PLC. Used by HLI interface.
        /// </summary>
        public AdsClient ClientPlc { get; private set; }

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


        /// <summary>
        /// Dispose the CNC object. Releases ADS handles internally.
        /// </summary>
        public void Dispose()
        {
            ClientCom?.Dispose();
            ClientGeo?.Dispose();
            ClientSda?.Dispose();
            ClientPlc?.Dispose();

            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Represents the description of a TASK COM object.
    /// </summary>
    public class ObjectDescription
    {
        /// <summary> Internal unique ID of an object. </summary>
        public uint Id;
        
        /// <summary> Size of object in bytes. </summary>
        public uint Size;
        
        /// <summary> TRUE if object is describable. </summary>
        public ushort WriteAccess;

        /// <summary> Index group for direct object access to the content. </summary>
        public uint IndexGroup;

        /// <summary> Index offset for direct object access to the content. </summary>
        public uint IndexOffset;
        
        /// <summary> Name of the object.  </summary>
        public string Name;

        /// <summary> 
        /// Object data type BOOL, BYTE, SINT, WORD, INT,
        /// DWORD, DINT, LWORD, LINT, REAL, LREAL, STRING
        /// </summary>
        public string Type;



        internal ObjectDescription(byte[] bytes)
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

        /// <summary>
        /// Returns the name of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

    }
}