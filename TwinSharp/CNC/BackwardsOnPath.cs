using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The function for forward/backward motion on the path permits backward motion along the originally programmed path by means of a real-time signal when the NC program is active. Backward motion is terminated by resetting the real-time signal. Forward motion is then resumed.
    /// </summary>
    public class BackwardsOnPath
    {
        readonly AdsClient plcClient;
        readonly Dictionary<Identifier, uint> variableHandles;
        internal BackwardsOnPath(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            variableHandles = CreateVariableHandles(plcClient, channelNumber);
        }

        private Dictionary<Identifier, uint> CreateVariableHandles(AdsClient plcClient, int channelNumber)
        {
            var handles = new Dictionary<Identifier, uint>();
            uint handle;

            string prefix = string.Format("HLI_Global_Variables.gpCh[{0}]^.bahn_mc_control", channelNumber - 1);
            
            handle = plcClient.CreateVariableHandle(prefix + ".backward_motion.command_w");
            handles.Add(Identifier.SetSelectBackwardMotion, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".backward_motion.enable_w");
            handles.Add(Identifier.SetEnableBackwardMotion, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".backward_motion.state_r");
            handles.Add(Identifier.GetSelectBackwardMotion, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".simulate_motion.command_w");
            handles.Add(Identifier.SetSelectSimulatedMotion, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".simulate_motion.state_r");
            handles.Add(Identifier.GetSelectSimulatedMotion, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".backward_storage_off.command_w");
            handles.Add(Identifier.SetResetBackwardMotionMemory, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".backward_storage_off.state_r");
            handles.Add(Identifier.GetResetBackwardMotionMemory, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed.command_w");
            handles.Add(Identifier.SetSpecifyExternalPathVelocity, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed.state_r");
            handles.Add(Identifier.GetSpecifyExternalPathVelocity, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed_valid.command_w");
            handles.Add(Identifier.SetActivateExternalPathVelocity, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed_valid.state_r");
            handles.Add(Identifier.GetActivateExternalPathVelocity, handle);

            return handles;
        }


        /// <summary>
        /// Select/deselect backward motion on the path In basic setting, M/H functions are executed without synchronisation(MOS) in this mode.
        /// </summary>
        public bool SelectBackwardMotion
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetSelectBackwardMotion]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetSelectBackwardMotion], value);
        }


        /// <summary>
        /// Signal to CNC that we want to use this interface.
        /// </summary>
        public bool EnableInterface
        {
            set => plcClient.WriteAny(variableHandles[Identifier.SetEnableBackwardMotion], value);
        }

        /// <summary>
        /// Select/deselect simulated forward motion on the path In basic setting, M/H functions are executed without synchronisation (MOS) in this mode. Sections in the NC program can be skipped during program runtime in combination with the NC command #OPTIONAL EXECUTION.
        /// </summary>
        public bool SelectSimulatedForwardMotion
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetSelectSimulatedMotion]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetSelectSimulatedMotion], value);
        }

        /// <summary>
        /// Deselects backward motion memory No further NC block is saved in the memory. The memory is deleted. The backward motion memory can only be cleared if no NC program is active.
        /// </summary>
        public bool ResetBackwardMotionMemory
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetResetBackwardMotionMemory]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetResetBackwardMotionMemory], value);
        }

        /// <summary>
        /// External path velocity specified. The path velocity setting is activated by the control unit ext_command_speed_valid. If the velocity specified in negative, the tool moves backwards along the path
        /// Unit: 1 μm/s
        /// </summary>
        public uint ExternalPathVelocity
        {
            //TODO: Why is this an uint when documentation says it can be negative. Check with Beckhoff.
            get => plcClient.ReadAny<uint>(variableHandles[Identifier.GetSpecifyExternalPathVelocity]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetSpecifyExternalPathVelocity], value);
        }

        /// <summary>
        /// Activate the velocity commanded in the ext_command_speed control unit. To reach the commanded velocity, all axes involved in the motion are accelerated or decelerated. If this value is TRUE, the sign is considered in the current path feed (active_feed_r control unit).
        /// </summary>
        public bool ActivateExternalPathVelocity
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetActivateExternalPathVelocity]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetActivateExternalPathVelocity], value);
        }


        enum Identifier
        {
            SetSelectBackwardMotion,
            GetSelectBackwardMotion,
            SetSelectSimulatedMotion,
            GetSelectSimulatedMotion,
            SetResetBackwardMotionMemory,
            GetResetBackwardMotionMemory,
            SetSpecifyExternalPathVelocity,
            GetSpecifyExternalPathVelocity,
            SetActivateExternalPathVelocity,
            GetActivateExternalPathVelocity,
            SetEnableBackwardMotion,
        }
    }
}