using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC;

/// <summary>
/// Reads and writes tool table entries (V.G.WZ[j].*) via the SDA variable-by-name interface.
/// Writes to V.G.WZ[j].R persist across mode changes and program resets,
/// unlike WZ_AKT.R which is a runtime overlay that resets on tool deselect.
/// Requires CNC build ≥ 3.1.3079.08 and internal tool table (not external tool management).
///
/// Variable access: SDA port (552), index group 0x122300 + channelNumber.
/// Uses 0x48 (read by name) and 0x4A (write by name) per TF5200 ADS Access on CNC §4.3.5/§4.3.7.
/// V.G.WZ[j].R does not support the handle lookup (0x46) — access by name is required.
/// </summary>
public class ToolManagement
{
    readonly AdsClient sdaClient;
    readonly uint channelNumber;

    // Unit: CNC tool table stores radii/lengths in 0.1 µm integers (same as zero offsets).
    private const double UmPerMm = 10_000.0;

    // SDA variable interface index offsets — TF5200 ADS Access on CNC, section 4.3.x.
    private const uint IdxOffs_ReadByName  = 0x48;
    private const uint IdxOffs_WriteByName = 0x4A;

    internal ToolManagement(AdsClient sdaClient, int channelNumber)
    {
        this.sdaClient     = sdaClient;
        this.channelNumber = (uint)channelNumber;
    }

    private uint IndexGroup => 0x122300 + channelNumber;

    // Variable name as ASCII bytes with null terminator, as required by the 0x48/0x4A protocol.
    private static byte[] NameBytes(string varName) => Encoding.ASCII.GetBytes(varName + "\0");

    /// <summary>
    /// Returns the stored radius (mm) for tool <paramref name="toolIndex"/> (1-based D-number).
    /// Reads V.G.WZ[toolIndex].R directly from the internal tool table.
    /// </summary>
    public double GetToolRadiusMm(int toolIndex)
    {
        // 0x48: ReadWrite — write buffer = name+'\0', read buffer = value (SGN32, 0.1 µm)
        var valueBuf = new byte[sizeof(int)];
        sdaClient.ReadWrite(IndexGroup, IdxOffs_ReadByName,
            new Memory<byte>(valueBuf),
            new ReadOnlyMemory<byte>(NameBytes($"V.G.WZ[{toolIndex}].R")));
        return BitConverter.ToInt32(valueBuf, 0) / UmPerMm;
    }

    /// <summary>
    /// Writes the radius (mm) for tool <paramref name="toolIndex"/> (1-based D-number)
    /// into the internal tool table. Persists across mode changes and program resets.
    /// </summary>
    public void SetToolRadiusMm(int toolIndex, double radiusMm)
    {
        // 0x4A: Write — write buffer = name+'\0' + value (SGN32, 0.1 µm), no return value
        var nameBytes  = NameBytes($"V.G.WZ[{toolIndex}].R");
        var valueBytes = BitConverter.GetBytes((int)Math.Round(radiusMm * UmPerMm));
        var data = new byte[nameBytes.Length + valueBytes.Length];
        nameBytes.CopyTo(data, 0);
        valueBytes.CopyTo(data, nameBytes.Length);
        sdaClient.Write(IndexGroup, IdxOffs_WriteByName, new ReadOnlyMemory<byte>(data));
    }

    /// <summary>
    /// Returns the D-number of the tool currently active in the channel (V.A.WNR),
    /// or 0 if no tool is selected.
    /// </summary>
    public int GetActiveToolIndex()
    {
        var valueBuf = new byte[sizeof(int)];
        sdaClient.ReadWrite(IndexGroup, IdxOffs_ReadByName,
            new Memory<byte>(valueBuf),
            new ReadOnlyMemory<byte>(NameBytes("V.A.WNR")));
        return BitConverter.ToInt32(valueBuf, 0);
    }
}
