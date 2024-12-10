using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The OperationModeManager class provides an interface to manage and control the operation modes and states of the CNC via an AdsClient.
    /// It allows setting and retrieving the current operation mode and state, resetting the operation mode, and managing the interface existence.
    /// </summary>
    public class OperationModeManager : IDisposable
    {
        readonly AdsClient plcClient;

        readonly uint geoIndexGroup;

        readonly Dictionary<Identifier, uint> variableHandles;
        internal OperationModeManager(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;


            geoIndexGroup = 0x123300 + (uint)channelNumber;

            variableHandles = CreateVariableHandles(channelNumber);

            Adresses = new OperationModeAdresses(geoIndexGroup);

            InterfaceExists = true;
        }

        /// <summary>
        /// Adresses used by the operation mode manager. Contains the main index group and sub index groups of common properties. 
        /// These can be used to add device notificaitons by the user.
        /// </summary>
        public OperationModeAdresses Adresses  { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <param name="parameter">It may be necessary to specify parameters when commanding an operation mode change to ensure the successful change to a specific state of an operation mode.</param>
        public void SetModeAndState(OperationMode mode, OperationState state, string parameter)
        {
            var modeAndState = new HLI_PROC_TRANS_TO_MODE_STATE();

            modeAndState.ToMode = mode;
            modeAndState.ToState = state;
            modeAndState.Parameter = parameter;

            plcClient.WriteAny(variableHandles[Identifier.CommandModeAndState], modeAndState);
            plcClient.WriteAny(variableHandles[Identifier.CommandSempahor], true);
        }

        /// <summary>
        /// Sets a new mode and state.
        /// </summary>
        /// <param name="unit"></param>
        public void SetModeAndState(HLI_PROC_TRANS_TO_MODE_STATE unit)
        {
            plcClient.WriteAny(variableHandles[Identifier.CommandModeAndState], unit);
            plcClient.WriteAny(variableHandles[Identifier.CommandSempahor], true);
        }

        /// <summary>
        /// Gets the requested operation mode and state.
        /// </summary>
        public HLI_PROC_TRANS_TO_MODE_STATE RequestedModeAndState
        {
            get => plcClient.ReadAny<HLI_PROC_TRANS_TO_MODE_STATE>(variableHandles[Identifier.RequestedModeAndState]);
        }

        /// <summary>
        /// Gets the actual operation mode and state.
        /// </summary>
        public HLI_IMCM_MODE_STATE OperationModeAndStateActual
        {
            get => plcClient.ReadAny<HLI_IMCM_MODE_STATE>(variableHandles[Identifier.GetModeAndState]);
        }

        /// <summary>
        /// Gets the actual operation state.
        /// </summary>
        public OperationState OperationStateActual
        {
            get => (OperationState)plcClient.ReadAny<uint>(variableHandles[Identifier.OperationStateActual]);
        }

        /// <summary>
        /// Gets the actual operation mode.
        /// </summary>
        public OperationMode OperationModeActual
        {
            get => (OperationMode)plcClient.ReadAny<uint>(variableHandles[Identifier.OperationModeActual]);
        }

        /// <summary>
        /// Signal to the CNC that the interface exists and we want to use it.
        /// </summary>
        public bool InterfaceExists
        {
            set => plcClient.WriteAny(variableHandles[Identifier.InterfaceExists], value);
        }


        private Dictionary<Identifier, uint> CreateVariableHandles(int channelNumber)
        {
            var handles = new Dictionary<Identifier, uint>();
            uint handle;

            string prefix = string.Format("HLI_Global_Variables.gpCh[{0}]^.channel_mc_control.mode_and_state", channelNumber - 1);

            handle = plcClient.CreateVariableHandle(prefix + ".state_r");
            handles.Add(Identifier.GetModeAndState, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".request_r");
            handles.Add(Identifier.RequestedModeAndState, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".state_r.mode");
            handles.Add(Identifier.OperationModeActual, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".state_r.state");
            handles.Add(Identifier.OperationStateActual, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".enable_w");
            handles.Add(Identifier.InterfaceExists, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".command_w");
            handles.Add(Identifier.CommandModeAndState, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".command_semaphor_rw");
            handles.Add(Identifier.CommandSempahor, handle);

            return handles;
        }

        /// <summary>
        /// Reset the operation mode to standby mode.
        /// </summary>
        public void Reset()
        {
            var currentMode = OperationModeActual;

            var reset = new HLI_PROC_TRANS_TO_MODE_STATE();
            reset.ToMode = OperationMode.STANDBY_MODE;
            reset.ToState = OperationState.PROCESS_SELECTED;

            SetModeAndState(reset);


            //Restore old auto/man mode.
            SetModeAndState(currentMode, OperationState.PROCESS_SELECTED, string.Empty);
        }


        /// <summary>
        /// Dispose the operation mode manager. Deletes all ADS device notifications.
        /// </summary>
        public void Dispose()
        {
            foreach (var handle in variableHandles)
            {
                plcClient.DeleteDeviceNotification(handle.Value);
            }

            GC.SuppressFinalize(this);
        }

        enum Identifier
        {
            OperationModeActual,
            OperationStateActual,
            InterfaceExists,
            CommandModeAndState,
            GetModeAndState,
            CommandSempahor,
            RequestedModeAndState
        }

    }

    /// <summary>
    /// This class contains the adresses of objects used by the operation mode manager.
    /// </summary>
    public class OperationModeAdresses
    {
        uint geoIndexGroup;
        internal OperationModeAdresses(uint geoIndexGroup)
        {
            this.geoIndexGroup = geoIndexGroup;
        }

        /// <summary> Main index group for all adresses. </summary>
        public uint GeoIndexGroup => geoIndexGroup;

        /// <summary> Sub index group of the actual operation state. </summary>
        public uint OperationStateActual => 0x1F;

        /// <summary> Sub index group of the actual operation mode. </summary>
        public uint OperationModeActual => 0x20;
    }
}