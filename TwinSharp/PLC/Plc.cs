using TwinCAT.Ads;

namespace TwinSharp.PLC
{
    /// <summary>
    /// The PLC class provides methods to control a TwinCAT PLC runtime system using an AdsClient.
    /// It allows starting, stopping, and resetting the PLC, as well as accessing various system information
    /// and status variables through the PlcAppSystemInfo instance.
    /// </summary>
    public class PLC
    {

        readonly AdsClient client;

        /// <summary>
        /// Initializes a new instance of the PLC class with the specified AdsClient.
        /// The AdsClient should typically be connected to the target system at port AmsPort.PlcRuntime_851 (851).
        /// </summary>
        public PLC(AdsClient client)
        {
            this.client = client;
            AppInfo = new PlcAppSystemInfo(client);
        }


        /// <summary>
        /// Gets the PlcAppSystemInfo instance which provides access to various system information and status variables of a TwinCAT PLC application.
        /// </summary>
        public PlcAppSystemInfo AppInfo { get; private set; }


        /// <summary>
        /// Can be used to start a PLC runtime system on a TwinCAT system. The function block can, for instance, be used to start the PLC on a remote PC.
        /// Equivavalent to the function block PLC_Start.
        /// </summary>
        public void Start()
        {
            var currentState = client.ReadState();

            if (currentState.AdsState == AdsState.Run)
                return; //Already running, if we try to set it anyway we get an exception.

            var stateInfo = new StateInfo(AdsState.Run, currentState.DeviceState);
            client.WriteControl(stateInfo);
        }

        /// <summary>
        /// Can be used to stop a PLC runtime system on a TwinCAT system. The function block can, for instance, be used to stop the PLC on a remote or a local PC.
        /// Equivavalent to the function block PLC_Stop.
        /// </summary>
        public void Stop()
        {
            var currentState = client.ReadState();

            if (currentState.AdsState == AdsState.Stop)
                return; //Already stopped, if we try to set it anyway we get an exception.

            var stateInfo = new StateInfo(AdsState.Stop, currentState.DeviceState);
            client.WriteControl(stateInfo);
        }

        /// <summary>
        /// Can be used to reset a PLC runtime system. When the PLC is reset, the PLC variables are filled with their initial values, and the execution of the PLC program is stopped.
        /// Equivavalent to the function block PLC_Reset.
        /// </summary>
        public void Reset()
        {
            var currentState = client.ReadState();

            if (currentState.AdsState == AdsState.Reset)
                return; //Already reset, if we try to set it anyway we get an exception.

            var stateInfo = new StateInfo(AdsState.Reset, currentState.DeviceState);
            client.WriteControl(stateInfo);
        }
    }
}
