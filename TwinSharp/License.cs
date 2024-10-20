using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    public class License
    {
        public ST_CheckLicense[] GetValidLicenses()
        {
            var validLicences = new List<ST_CheckLicense>();


            using (AdsClient client = new AdsClient())
            {
                client.Connect(AmsPort.R0_LicenseServer);


                uint validLicenceCount = (uint)client.ReadAny(0x0101_0006, 0x1, typeof(uint));
                uint invalidLicenceCount = (uint)client.ReadAny(0x0101_000A, 0x1, typeof(uint));

                if (validLicenceCount < 1)
                    return validLicences.ToArray();


                byte[] response = new byte[48 * validLicenceCount];
                response = (byte[])client.ReadAny(0x0101_0006, 0x01, typeof(byte[]), new int[] { response.Length });

                for (int i = 0; i < validLicenceCount; i++)
                {
                    //Get the license name as a string
                    byte[] readBytes = new byte[81]; //81 is max length of license string name.
                    Memory<byte> memoryToRead = new Memory<byte>(readBytes);
                    ReadOnlyMemory<byte> memoryToWrite = new ReadOnlyMemory<byte>(response, 48 * i, 16);

                    int byteCountThatWasRead = client.ReadWrite(0x0101_000C, 0x0, memoryToRead, memoryToWrite);

                    ASCIIEncoding ascii = new ASCIIEncoding();
                    string descriptionText = ascii.GetString(readBytes, 0, byteCountThatWasRead - 1);


                    //Construct a license from the originally recieved bytes.
                    byte[] instanceBytes = new byte[48];

                    Array.Copy(response, i * 48, instanceBytes, 0, 48);
                    ST_CheckLicense st_CheckLicense = new ST_CheckLicense(instanceBytes, descriptionText);

                    validLicences.Add(st_CheckLicense);
                }
            }
            return validLicences.ToArray();
        }
    }
}
