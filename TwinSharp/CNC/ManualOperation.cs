using System.ComponentModel.Design;
using System.ComponentModel;
using System.Numerics;
using System.Xml.Linq;
using TwinCAT.Ads;
using TwinSharp.NC;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Class that contains settings and functions for the manual operation of a CNC machine.
    /// Such as keys, rapid keys and hand wheels.
    /// </summary>
    public class ManualOperation
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;


        internal ManualOperation(AdsClient plcClient, int channelNumber)
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

        /// <summary>
        /// Control unit to manage data for parameterising continuous jog mode in manual
        /// mode, including flow control of user data.
        /// </summary>
        public TipParameters TipParameters { get; private set; }

        /// <summary>
        /// Control unit to manage data for parameterising incremental jog mode in manual
        /// manual, including flow control of user data
        /// </summary>
        public JogParameters JogParameters { get; private set; }

        /// <summary>
        /// Control unit to manage data for parameterising handwheel mode in manual mode,
        /// including flow control of user data
        /// </summary>
        public HrParameters HrParameters { get; private set; }

        /// <summary>
        /// Control unit to manage data to enforce a button press in manual mode, including
        /// flow control of user data.
        /// </summary>
        public Key[] Keys { get; private set; }

        /// <summary>
        /// During continuous jog mode it is possible to switch between the normal velocity and rapid traverse velocity with this control unit.
        /// </summary>
        public RapidKey RapidKey { get; private set; }

        /// <summary>
        /// Array of control units to manage the counts of handwheel increments for all
        /// handwheels, including flow control of user data.
        /// </summary>
        public HandWheelInc[] HandWheelIncs { get; private set; }


        /// <summary>
        /// Gets the manual mode state of an axis for the supplied axis index.
        /// </summary>
        /// <param name="axisIndex"></param>
        /// <returns></returns>
        public ushort GetManualModeState(int axisIndex)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_state.coord_r[{axisIndex}].hb_display_r.state";
            uint handle = plcClient.CreateVariableHandle(symbol);
            return plcClient.ReadAny<ushort>(handle);
        }

        /// <summary>
        /// Gets the operation mode state of an axis for the supplied axis index.
        /// </summary>
        /// <param name="axisIndex"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Present marker to the CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.activation.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        /// <summary>
        /// Write the command data for one of the manual control elements to the CNC.
        /// </summary>
        /// <param name="controlElement"></param>
        public void WriteCommandElement(HLI_HB_ACTIVATION controlElement)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.activation.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, controlElement);
        }

        /// <summary>
        /// CNC accepts the commanded data if this element has the value TRUE and sets
        /// this element to the value FALSE after complete acceptance of the data.
        /// You should set this element to the value TRUE if all data to be commanded has been written.
        /// </summary>
        /// <param name="signal"></param>
        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.activation.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }

    /// <summary>
    /// Control unit to manage the count of handwheel increments for a handwheel index, including flow control of user data.
    /// </summary>
    public class HandWheelInc
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;
        readonly int handWheelIndex;

        internal HandWheelInc(AdsClient plcClient, int channelNumber, int handWheelIndex)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
            this.handWheelIndex = handWheelIndex;
        }

        /// <summary>
        /// Signal to CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.handwheel_incs[{handWheelIndex}].enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        /// <summary>
        /// Write the count of handwheel increments to the CNC.
        /// </summary>
        /// <param name="data"></param>
        public void WriteCommandElement(short data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.handwheel_incs[{handWheelIndex}].command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }
    }


    /// <summary>
    /// Control unit to manage data for parameterising handwheel mode in manual mode,
    /// including flow control of user data
    /// </summary>
    public class HrParameters
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;

        internal HrParameters(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        /// <summary>
        /// Signal to CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.hr_parameter.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        /// <summary>
        /// Write the parameters for the handwheel mode to the CNC.
        /// </summary>
        /// <param name="data"></param>
        public void WriteCommandElement(HLI_HB_HR_PARAMETER data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.hr_parameter.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        /// <summary>
        /// CNC accepts the commanded data if this element has the value TRUE and sets
        /// this element to the value FALSE after complete acceptance of the data.
        /// You should set this element to the value TRUE if all data to be commanded has been written.
        /// </summary>
        /// <param name="signal"></param>
        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.hr_paramater.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }

    /// <summary>
    /// Control unit to manage data for parameterising incremental jog mode in manual
    /// manual, including flow control of user data
    /// </summary>
    public class JogParameters
    {
        private AdsClient plcClient;
        private int channelNumber;

        internal JogParameters(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        /// <summary>
        /// Signal to CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.jog_parameter.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        /// <summary>
        /// Write the parameters for the incremental jog mode to the CNC.
        /// </summary>
        /// <param name="data"></param>
        public void WriteCommandElement(HLI_HB_JOG_PARAMETER data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.jog_parameter.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        /// <summary>
        /// CNC accepts the commanded data if this element has the value TRUE and sets
        /// this element to the value FALSE after complete acceptance of the data.
        /// You should set this element to the value TRUE if all data to be commanded has been written.
        /// </summary>
        /// <param name="signal"></param>
        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.jog_paramater.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }

    /// <summary>
    /// Control unit to manage data for parameterising continuous jog mode in manual
    /// mode, including flow control of user data.
    /// </summary>
    public class TipParameters
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;

        internal TipParameters(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        /// <summary>
        /// Signal to CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.tip_parameter.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }


        /// <summary>
        /// Write the parameters for continuous jog mode to the CNC.
        /// </summary>
        /// <param name="data"></param>
        public void WriteCommandElement(HLI_HB_TIP_PARAMETER data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.tip_parameter.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        /// <summary>
        /// CNC accepts the commanded data if this element has the value TRUE and sets
        /// this element to the value FALSE after complete acceptance of the data.
        /// You should set this element to the value TRUE if all data to be commanded has been written.
        /// </summary>
        /// <param name="signal"></param>
        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.tip_parameter.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }


    /// <summary>
    /// Control unit to manage data to enforce a button press in manual mode, including
    /// flow control of user data.
    /// </summary>
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

        /// <summary>
        /// Signal to CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.key[{keyIndex}].enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        /// <summary>
        /// Write the command data for the key to the CNC.
        /// </summary>
        /// <param name="data"></param>
        public void WriteCommandElement(HLI_HB_KEY data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.key[{keyIndex}].command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        /// <summary>
        /// CNC accepts the commanded data if this element has the value TRUE and sets
        /// this element to the value FALSE after complete acceptance of the data.
        /// You should set this element to the value TRUE if all data to be commanded has been written.
        /// </summary>
        /// <param name="signal"></param>
        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.key[{keyIndex}].command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }


    /// <summary>
    /// During continuous jog mode it is possible to switch between the normal velocity and rapid traverse velocity.
    /// Here the rapid traverse is a button-specific feature and only becomes effective when the corresponding
    /// button is pushed and linked to an axis.
    /// </summary>
    public class RapidKey
    {
        readonly AdsClient plcClient;
        readonly int channelNumber;

        internal RapidKey(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;
        }

        /// <summary>
        /// Signal to CNC that the interface exists and we want to use it.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableControlElement(bool enabled)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.rapid_key.enable_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, enabled);
        }

        /// <summary>
        /// Write the command data for the rapid key to the CNC.
        /// </summary>
        /// <param name="data"></param>
        public void WriteCommandElement(HLI_HB_RAPID_KEY data)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.rapid_key.command_w";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, data);
        }

        /// <summary>
        /// CNC accepts the commanded data if this element has the value TRUE and sets
        /// this element to the value FALSE after complete acceptance of the data.
        /// You should set this element to the value TRUE if all data to be commanded has been written.
        /// </summary>
        /// <param name="signal"></param>
        public void SignalCommandSemaphor(bool signal)
        {
            string symbol = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.hb_mc_control.rapid_key.command_semaphor_rw";
            uint handle = plcClient.CreateVariableHandle(symbol);
            plcClient.WriteAny(handle, signal);
        }
    }
}