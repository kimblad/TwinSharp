using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Additionally needs to be activated in the startup parameters by using the keyword FCT_DYN_POS_LIMIT of the parameter P-STUP-00070
    /// </summary>
    public class DynamicPositionLimitation
    {
        readonly uint axisIndex;
        readonly AdsClient plcClient;
        readonly Dictionary<string, uint> variableHandles;


        public DynamicPositionLimitation(uint axisIndex, AdsClient plcClient)
        {
            this.axisIndex = axisIndex;
            this.plcClient = plcClient;
            variableHandles = new Dictionary<string, uint>();
        }


        public void LowerPositionLimitInterfaceExists(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpAx[{axisIndex}]^.ipo_mc_control.dyn_pos_limit_low.enable_w";
            uint handle = GetOrCreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }


        /// <summary>
        ///  Sets a position value and describes the lower limit of the position range which the axis should not exceed.
        /// </summary>
        /// <param name="position">Unit: 0.1 µm or 10-4 °</param>
        public void SetLowerPositionLimit(int position)
        {
            string symbol = $"HLI_Global_Variables.gpAx[{axisIndex}]^.ipo_mc_control.dyn_pos_limit_low.command_w";
            uint handle = GetOrCreateVariableHandle(symbol);
            plcClient.WriteAny(handle, position);
        }

        public HLI_DYNPL_STATE LowerPositionLimitMonitoringState
        {
            get
            {
                string symbol = $"HLI_Global_Variables.gpAx[{axisIndex}]^.ipo_mc_control.dyn_pos_limit_low.state_r";
                uint handle = GetOrCreateVariableHandle(symbol);
                return (HLI_DYNPL_STATE)plcClient.ReadAny<int>(handle);
            }
        }

        private uint GetOrCreateVariableHandle(string symbol)
        {
            if (variableHandles.TryGetValue(symbol, out uint handle))
                return handle;

            //Symbol and handle do not exist, create them
            handle = plcClient.CreateVariableHandle(symbol);
            variableHandles.Add(symbol, handle);
            return handle;
        }


    }

    public enum HLI_DYNPL_STATE
    {
        HLI_DYNPL_STATE_INACTIVE = 0, //The position limit is not active.
        HLI_DYNPL_STATE_ACTIVATION = 1, //This is the transition state after commanding the control unit until monitoring of axis position to the limit is activated.
        HLI_DYNPL_STATE_ACTIVE = 2, //The position limit is active and the axis position limit is monitored.
        HLI_DYNPL_STATE_ACTIVE_BRAKING = 3, //A braking operation was initiated down to standstill to prevent the axis from exceeding the position limit.
        HLI_DYNPL_STATE_ACTIVE_BRAKE = 4 //Deceleration process to maintain the position limit completed, axis is at standstill.
    }
}