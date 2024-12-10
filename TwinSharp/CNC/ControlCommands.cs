using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The ControlCommands class provides methods to interact with and control various aspects of the CNC.
    /// It allows for the activation and deactivation of skip modes, reading and setting the current and commanded channel modes,
    /// and enabling or disabling optional stops during NC program execution.
    /// </summary>
    public class ControlCommands
    {
        readonly AdsClient comClient;
        readonly Dictionary<string, ObjectDescription> descriptions;

        internal ControlCommands(AdsClient comClient, Dictionary<string, ObjectDescription> descriptions)
        {
            this.comClient = comClient;
            this.descriptions = descriptions;
        }

        /// <summary>
        /// Activates/deactivates skip mode at interpreter level for the NC program. The status of skip mode is only evaluated at the start of the NC program. Switchover during execution of an NC program has no effect.
        /// Skip levels active simultaneously are enabled by bitwise ORing.
        /// Example:
        /// Enable all skip levels by setting 0x3FF.
        /// </summary>
        public SkipModes SkipMode
        {
            get
            {
                var obj = descriptions["mc_command_block_ignore_r"];
                return (SkipModes)comClient.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
            set
            {
                var obj = descriptions["mc_command_block_ignore_w"];
                comClient.WriteAny(obj.IndexGroup, obj.IndexOffset, (uint)value);
            }
        }

        /// <summary>
        /// Current special channel mode such as syntax check or machining time calculation
        /// </summary>
        public ChannelMode ChannelModeActive
        {
            get
            {
                var obj = descriptions["mc_active_execution_mode_r"];
                return (ChannelMode)comClient.ReadAny<int>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Selection of a special channel mode such as syntax check or machining time calculation
        /// </summary>
        public ChannelMode ChannelModeCommanded
        {
            get
            {
                var obj = descriptions["mc_command_execution_mode_r"];
                return (ChannelMode)comClient.ReadAny<int>(obj.IndexGroup, obj.IndexOffset);
            }
            set
            {
                var obj = descriptions["mc_command_execution_mode_w"];
                comClient.WriteAny(obj.IndexGroup, obj.IndexOffset, (int)value);
            }
        }

        /// <summary>
        /// Activating/deactivating optional stop.
        /// If the function M01(optional stop) is programmed in the current block of the NC program, set this element to the value TRUE to stop at block end (ramped-down deceleration complying with the permissible accelerations).
        /// The following block can be enabled by activating the element “continue machining” if the NC kernel indicates that all axes are located within the control window by resetting the status flag wait_axes_in_position_r.
        /// </summary>
        public bool OptionalStop
        {
            get
            {
                var obj = descriptions["mc_command_M01_stop_enable_r"];
                return comClient.ReadAny<bool>(obj.IndexGroup, obj.IndexOffset);
            }
            set
            {
                var obj = descriptions["mc_command_M01_stop_enable_w"];
                comClient.WriteAny(obj.IndexGroup, obj.IndexOffset, value);
            }
        }
    }
}