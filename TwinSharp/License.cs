using System.Text;
using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// Represents a license manager for a TwinCAT system.
    /// </summary>
    public class License
    {
        readonly AmsNetId target;

        /// <summary>
        /// Initializes a new instance of the <see cref="License"/> class.
        /// </summary>
        /// <param name="target">The target AmsNetId.</param>
        internal License(AmsNetId target)
        {
            this.target = target;
        }

        /// <summary>
        /// Gets the valid licenses for the target.
        /// </summary>
        /// <returns>An array of valid licenses.</returns>
        public ST_CheckLicense[] GetValidLicenses()
        {
            var validLicences = new List<ST_CheckLicense>();

            using (AdsClient client = new())
            {
                client.Connect(target, AmsPort.R0_LicenseServer);

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
                    var memoryToRead = new Memory<byte>(readBytes);
                    var memoryToWrite = new ReadOnlyMemory<byte>(response, 48 * i, 16);

                    int byteCountThatWasRead = client.ReadWrite(0x0101_000C, 0x0, memoryToRead, memoryToWrite);

                    var ascii = new ASCIIEncoding();
                    string descriptionText = ascii.GetString(readBytes, 0, byteCountThatWasRead - 1);

                    //Construct a license from the originally received bytes.
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
