﻿using TwinCAT.Ads;

namespace TwinSharp
{
    /// <summary>
    /// Specifying velocity or position command values by the SPS effective in addition to the interpolator. No monitoring takes place of transferred values for compliance with the dynamic axis limits. To activate this interface, set the parameter P-AXIS-00732 to 1.
    /// </summary>
    public class ExternalAxisCommanding
    {
        readonly AdsClient plcClient;
        readonly Dictionary<Identifier, uint> variableHandles;

        public ExternalAxisCommanding(uint index, AdsClient plcClient)
        {
            this.plcClient = plcClient;

            variableHandles = CreateVariableHandles(index, plcClient);
        }

        private Dictionary<Identifier, uint> CreateVariableHandles(uint index, AdsClient plcClient)
        {
            string prefix = string.Format("HLI_Global_Variables.gpAx[{0}]^.lr_mc_control.add_cmd_values", index - 1);

            var handles = new Dictionary<Identifier, uint>();
            uint handle;

            handle = plcClient.CreateVariableHandle(prefix + ".command_w");
            handles.Add(Identifier.TransferData, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".enable_w");
            handles.Add(Identifier.InterfaceExists, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".command_w.m_add_pos_value");
            handles.Add(Identifier.AddedPosition, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".command_w.m_add_speed_value");
            handles.Add(Identifier.AddedVelocity, handle);

            return handles;
        }

        public bool InterfaceExists
        {
            set => plcClient.WriteAny(variableHandles[Identifier.InterfaceExists], value);
        }

        /// <summary>
        /// Unit 0.1 µm
        /// </summary>
        public int AdditivePosition
        {
            set => plcClient.WriteAny(variableHandles[Identifier.AddedPosition], value);
        }

        /// <summary>
        /// Unit 1 µm/s
        /// </summary>
        public int AdditiveVelocity
        {
            set => plcClient.WriteAny(variableHandles[Identifier.AddedVelocity], value);
        }

        enum Identifier
        {
            TransferData,
            InterfaceExists,
            AddedPosition,
            AddedVelocity
        }
    }
}
