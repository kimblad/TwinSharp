using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class OperationModeManager : IDisposable
    {
        readonly AdsClient geoClient;
        readonly AdsClient comClient;
        readonly AdsClient plcClient;

        readonly uint geoIndexGroup;
        readonly uint comIndexGroup;
        public readonly OperationModeAdresses Adresses;

        readonly Dictionary<Identifier, uint> variableHandles;
        internal OperationModeManager(AdsClient plcClient, AdsClient geoClient, AdsClient comClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.geoClient = geoClient;
            this.comClient = comClient;


            geoIndexGroup = 0x123300 + (uint)channelNumber;
            comIndexGroup = 0x120100 + (uint)channelNumber;

            variableHandles = CreateVariableHandles(channelNumber);

            Adresses = new OperationModeAdresses(geoIndexGroup);

            InterfaceExists = true;
        }

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

        public void SetModeAndState(HLI_PROC_TRANS_TO_MODE_STATE unit)
        {
            plcClient.WriteAny(variableHandles[Identifier.CommandModeAndState], unit);
            plcClient.WriteAny(variableHandles[Identifier.CommandSempahor], true);
        }

        public HLI_PROC_TRANS_TO_MODE_STATE RequestedModeAndState
        {
            get => plcClient.ReadAny<HLI_PROC_TRANS_TO_MODE_STATE>(variableHandles[Identifier.RequestedModeAndState]);
        }

        public HLI_IMCM_MODE_STATE OperationModeAndStateActual
        {
            get => plcClient.ReadAny<HLI_IMCM_MODE_STATE>(variableHandles[Identifier.GetModeAndState]);
        }

        public OperationState OperationStateActual
        {
            get => (OperationState)plcClient.ReadAny<uint>(variableHandles[Identifier.OperationStateActual]);
        }
        public OperationMode OperationModeActual
        {
            get => (OperationMode)plcClient.ReadAny<uint>(variableHandles[Identifier.OperationModeActual]);
        }

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
}