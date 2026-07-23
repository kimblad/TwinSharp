using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Provides read-only access to the real-time status of the CNC path interpolator
    /// via the HLI <c>bahn_state</c> variable (<c>STATE_BAHN</c> struct).
    /// <para>
    /// All data is read in a single ADS call by accessing the entire <see cref="StateBahn"/>
    /// struct via <see cref="State"/>.
    /// </para>
    /// </summary>
    public class RealTimeStatus : IDisposable
    {
        readonly AdsClient plcClient;
        readonly uint bahnStateHandle;

        internal RealTimeStatus(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            bahnStateHandle = plcClient.CreateVariableHandle(
                $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_state");
        }

        /// <summary>
        /// Reads the complete real-time status from the CNC path interpolator in one ADS call.
        /// </summary>
        public StateBahn State => plcClient.ReadAny<StateBahn>(bahnStateHandle);

        /// <summary>Disposes the RealTimeStatus and releases the ADS variable handle.</summary>
        public void Dispose()
        {
            plcClient.DeleteVariableHandle(bahnStateHandle);
            GC.SuppressFinalize(this);
        }
    }
}
