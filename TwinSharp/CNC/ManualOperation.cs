using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class ManualOperation
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;




        public ManualOperation(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
            TipParameters = new TipParameters(plcClient, channelNumber);
            JogParameters = new JogParameters(plcClient, channelNumber);
            HrParameters = new HrParameters(plcClient, channelNumber);

            Keys = new Key[Constants.HLI_KEY_MAXIDX];
            for (int i = 0; i < Keys.Length; i++)
            {
                Keys[i] = new Key(plcClient, channelNumber, i); 
            }

            RapidKey = new RapidKey(plcClient, channelNumber);

            HandWheelIncs = new HandWheelInc[Constants.HLI_HW_MAXIDX];
            for (int i = 0; i < HandWheelIncs.Length; i++)
            {
                HandWheelIncs[i] = new HandWheelInc(plcClient, channelNumber, i);
            }
        }

        public TipParameters TipParameters { get; private set; }

        public JogParameters JogParameters { get; private set; }

        public HrParameters HrParameters { get; private set; }

        public Key[] Keys { get; private set; }

        public RapidKey RapidKey { get; private set; }

        public HandWheelInc[] HandWheelIncs { get; private set; }

        public ushort GetManualModeState(int axisIndex)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_state.coord_r[{axisIndex}].hb_display_r.state";
            uint handle = plcClient.CreateVariableHandle(symbol);
            return plcClient.ReadAny<ushort>(handle);
        }

        public ushort GetOperationModeState(int axisIndex)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_state.coord_r[{axisIndex}].hb_display_r.operation_mode";
            uint handle = plcClient.CreateVariableHandle(symbol);
            return plcClient.ReadAny<ushort>(handle);
        }

        /// <summary>
        /// Logical number of the control element currently linked to the axis in question. 
        /// </summary>
        /// <param name="axisIndex"></param>
        /// <returns></returns>
        public ushort GetControlElementNumber(int axisIndex)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_state.coord_r[{axisIndex}].hb_display_r.control_element";
            uint handle = plcClient.CreateVariableHandle(symbol);
            return plcClient.ReadAny<ushort>(handle);
        }

        /// <summary>
        /// Path velocity of the axis in question when moved in continuous jog mode.
        /// </summary>
        /// <param name="axisIndex"></param>
        /// <returns></returns>
        public int GetPathVelocityContinous(int axisIndex)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_state.coord_r[{axisIndex}].hb_display_r.tipp_geschw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            return plcClient.ReadAny<int>(handle);
        }



        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.activation.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(HLI_HB_ACTIVATION controlElement)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.activation.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, controlElement);
        }

        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.activation.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }

    public class HandWheelInc
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;
        readonly int handWheelIndex;

        public HandWheelInc(AdsClient plcClient, int channelNumber, int handWheelIndex)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
            this.handWheelIndex = handWheelIndex;
        }

        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.handwheel_incs[{handWheelIndex}].enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(short data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.handwheel_incs[{handWheelIndex}].command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }
    }

    public class HrParameters
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;

        public HrParameters(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.hr_parameter.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(HLI_HB_HR_PARAMETER data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.hr_parameter.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.hr_paramater.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }

    }

    public class JogParameters
    {
        private AdsClient plcClient;
        private int channelNumber;

        internal JogParameters(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.jog_parameter.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(HLI_HB_JOG_PARAMETER data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.jog_parameter.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.jog_paramater.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }

    public class TipParameters
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;

        public TipParameters(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.tip_parameter.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(HLI_HB_TIP_PARAMETER data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.tip_parameter.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.tip_parameter.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }

    public class Key
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;
        readonly int keyIndex;

        internal Key(AdsClient plcClient, int channelNumber, int keyIndex)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
            this.keyIndex = keyIndex;
        }

        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.key[{keyIndex}].enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(HLI_HB_KEY data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.key[{keyIndex}].command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.key[{keyIndex}].command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }


    public class RapidKey
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;

        internal RapidKey(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.rapid_key.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        public void WriteCommandElement(HLI_HB_RAPID_KEY data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.rapid_key.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.rapid_key.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }
}