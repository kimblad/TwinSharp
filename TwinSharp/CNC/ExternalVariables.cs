using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC;

/// <summary>
/// Reads and writes CNC External Variables (V.E.*) via the SDA variable-by-name interface.
///
/// V.E. variables are shared data memory between the CNC NC channel, the HMI, and the PLC.
/// Unlike P-parameters, they persist across M30 and new program starts — values survive
/// the entire lifetime of the ADS connection and are only re-initialised at CNC boot.
///
/// Variables must be declared in a .lis ASCII list file configured at CNC start-up
/// (machine integrator responsibility). The HMI accesses them by their short name
/// (e.g. "rotation_angle"); the full NC/ADS name is "V.E.&lt;name&gt;" (channel scope)
/// or "V.E.G.&lt;name&gt;" (global scope — not handled here).
///
/// Variable access: SDA port (552), index group 0x122300 + channelNumber.
/// Uses 0x48 (read by name) and 0x4A (write by name) per TF5200 §4.3.5/§4.3.7.
/// </summary>
public class ExternalVariables
{
    readonly AdsClient sdaClient;
    readonly uint channelNumber;

    private const uint IdxOffs_ReadByName  = 0x48;
    private const uint IdxOffs_WriteByName = 0x4A;

    internal ExternalVariables(AdsClient sdaClient, int channelNumber)
    {
        this.sdaClient    = sdaClient;
        this.channelNumber = (uint)channelNumber;
    }

    private uint IndexGroup => 0x122300 + channelNumber;

    private static byte[] NameBytes(string varName) => Encoding.ASCII.GetBytes(varName + "\0");

    /// <summary>
    /// Reads the REAL64 (double) value of V.E.<paramref name="name"/>.
    /// </summary>
    public double GetDouble(string name)
    {
        var valueBuf = new byte[sizeof(double)];
        sdaClient.ReadWrite(IndexGroup, IdxOffs_ReadByName,
            new Memory<byte>(valueBuf),
            new ReadOnlyMemory<byte>(NameBytes($"V.E.{name}")));
        return BitConverter.ToDouble(valueBuf, 0);
    }

    /// <summary>
    /// Writes <paramref name="value"/> to V.E.<paramref name="name"/> (REAL64).
    /// The value persists until the CNC is rebooted — it survives M30 and new program starts.
    /// </summary>
    public void SetDouble(string name, double value)
    {
        var nameBytes  = NameBytes($"V.E.{name}");
        var valueBytes = BitConverter.GetBytes(value);
        var data       = new byte[nameBytes.Length + valueBytes.Length];
        nameBytes.CopyTo(data, 0);
        valueBytes.CopyTo(data, nameBytes.Length);
        sdaClient.Write(IndexGroup, IdxOffs_WriteByName, new ReadOnlyMemory<byte>(data));
    }

}
