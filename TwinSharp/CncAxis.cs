using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    public class CncAxis
    {
        readonly uint index;
        readonly AmsNetId target;
        internal CncAxis(uint index, AmsNetId target)
        {
            this.index = index;
            this.target = target;
        }

        public string GetParameters()
        {
            const uint group = 0x550_0001;

            using var client = new AdsClient();
            client.Connect(target, AmsPort.R0_TComServer);


            int bytesAvailable = client.ReadAny<int>(group, 0x550_0808 + index);

            //Get size of the parameter list
            //Axis 1 = 0x550_0409
            //Axis 2 = 0x550_040a
            //Axis 9 = 0x550_040b
            //Axis 4 = 0x550_040c
            //Get the parameter list
            var buffer = new byte[bytesAvailable];
            var memory = new Memory<byte>(buffer);
            int byteCountRead = client.Read(group, 0x550_0408 + index, memory);

            var ms = new MemoryStream(buffer);
            var br = new BinaryReader(ms);
            var ascii = new ASCIIEncoding();

            var unknown = br.ReadBytes(42); //First 42 bytes are unknown
            var description = br.ReadBytes(bytesAvailable - 42);

            return ascii.GetString(description, 0, bytesAvailable - 42);
        }

        public override string ToString()
        {
            return $"Axis {index}";
        }
    }
}