namespace TwinSharp.CNC
{
    /// <summary>
    /// Global HLI constants from <c>HLI_Global_Constants</c> (VAR_GLOBAL CONSTANT).
    /// These mirror the ISG TwinCAT CNC HLI library constants verbatim.
    /// Use these values when sizing structs or interpreting HLI status fields.
    /// </summary>
    public static class HliConstants
    {
        // -----------------------------------------------------------------------
        // Library / interface version strings
        // -----------------------------------------------------------------------
        /// <summary>Version of the HLI library itself.</summary>
        public const string HLI_LIB_VERSION = "V310.HLI.20.08.00";
        /// <summary>Version of the HLI interface.</summary>
        public const string HLI_INTF_ISG_VERSION = "V310.HLI.20.08";
        /// <summary>Name of the Named Shared Memory file.</summary>
        public const string HLI_INTF_SHM_NAME = "PLC_INTF.DAT";

        // -----------------------------------------------------------------------
        // System-level max counts and highest-index constants
        // -----------------------------------------------------------------------
        /// <summary>Max number of CNC systems in the installation.</summary>
        public const int HLI_SYS_CNC_MAX = 2;
        /// <summary>Highest index of CNC system array (TwinCAT specific).</summary>
        public const int HLI_SYS_CNC_MAXIDX = 1;

        /// <summary>Max number of CNC channels in the system.</summary>
        public const int HLI_SYS_CH_MAX = 32;
        /// <summary>Highest index of channel array.</summary>
        public const int HLI_SYS_CH_MAXIDX = 31;

        /// <summary>Max number of axes in the system.</summary>
        public const int HLI_SYS_AX_MAX = 128;
        /// <summary>Highest index of axis array.</summary>
        public const int HLI_SYS_AX_MAXIDX = 127;

        /// <summary>Max number of axes to be traced.</summary>
        public const int HLI_SYS_TRACE_AX_MAX = 16;
        /// <summary>Highest index of trace axis array.</summary>
        public const int HLI_SYS_TRACE_AX_MAXIDX = 15;

        /// <summary>Max number of axes per channel.</summary>
        public const int HLI_CH_AX_MAX = 32;
        /// <summary>Highest index of per-channel axis array.</summary>
        public const int HLI_CH_AX_MAXIDX = 31;

        /// <summary>Number of main axes in the coordinate system.</summary>
        public const int HLI_CS_AXES_NR = 3;
        /// <summary>Highest index of main coordinate system axis array. Array size = HLI_CS_AXES_NR.</summary>
        public const int HLI_CS_AXES_MAXIDX = 2;

        /// <summary>Max number of kinematic transformation steps.</summary>
        public const int HLI_KIN_TRAFO_STEPS_MAX = 3;
        /// <summary>Highest index of kinematic transformation steps array.</summary>
        public const int HLI_KIN_TRAFO_STEPS_MAXIDX = 2;

        /// <summary>Max PLCopen request interfaces (0=PLC, 1=HMI, 2=CNC).</summary>
        public const int HLI_PLCO_REQUEST_IFC_MAX = 3;
        /// <summary>Highest index of PLCopen group interfaces.</summary>
        public const int HLI_PLCO_REQUEST_IFC_MAXIDX = 2;

        /// <summary>Max PLCopen axis request interfaces (0=PLC, 1=HMI).</summary>
        public const int HLI_PLCO_AXIS_REQUEST_IFC_MAX = 2;
        /// <summary>Highest index of PLCopen axis interfaces.</summary>
        public const int HLI_PLCO_AXIS_REQUEST_IFC_MAXIDX = 1;

        /// <summary>Max number of job stack levels.</summary>
        public const int HLI_MC_JOB_STACK_MAX = 15;
        /// <summary>Highest index of job stack array.</summary>
        public const int HLI_MC_JOB_STACK_MAXIDX = 14;

        /// <summary>Number of position/time LAH (look-ahead) instances.</summary>
        public const int HLI_BAHN_POS_TIME_LAH_MAX = 10;
        /// <summary>Highest index of position/time LAH array.</summary>
        public const int HLI_BAHN_POS_TIME_LAH_MAXIDX = 9;

        /// <summary>Max number of ESA data points displayed at HLI.</summary>
        public const int HLI_ESA_POINTS_MAX = 3;
        /// <summary>Highest index of ESA data array.</summary>
        public const int HLI_ESA_POINTS_MAXIDX = 2;

        /// <summary>Number of ESA axes.</summary>
        public const int HLI_ESA_AXES_MAX = 3;
        /// <summary>Highest index of ESA axes array.</summary>
        public const int HLI_ESA_AXES_MAXIDX = 2;

        // -----------------------------------------------------------------------
        // Technology unit counts
        // -----------------------------------------------------------------------
        /// <summary>Max standard-synchronised techno units per channel.</summary>
        public const int HLI_TU_CH_STD_SYNC_MAX = 20;
        /// <summary>Max late-synchronised techno units per channel.</summary>
        public const int HLI_TU_CH_LATE_SYNC_MAX = 10;
        /// <summary>Max real-time-synchronised techno units per channel.</summary>
        public const int HLI_TU_CH_RT_SYNC_MAX = 10;
        /// <summary>Max standard-synchronised techno units per axis.</summary>
        public const int HLI_TU_AX_STD_SYNC_MAX = 20;
        /// <summary>Max late-synchronised techno units per axis.</summary>
        public const int HLI_TU_AX_LATE_SYNC_MAX = 10;
        /// <summary>Highest index of standard-sync channel techno units.</summary>
        public const int HLI_TU_CH_STD_SYNC_MAXIDX = 19;
        /// <summary>Highest index of late-sync channel techno units.</summary>
        public const int HLI_TU_CH_LATE_SYNC_MAXIDX = 9;
        /// <summary>Highest index of standard-sync axis techno units.</summary>
        public const int HLI_TU_AX_STD_SYNC_MAXIDX = 19;
        /// <summary>Highest index of late-sync axis techno units.</summary>
        public const int HLI_TU_AX_LATE_SYNC_MAXIDX = 9;
        /// <summary>Highest index of real-time-sync channel techno units.</summary>
        public const int HLI_TU_CH_RT_SYNC_MAXIDX = 9;

        // -----------------------------------------------------------------------
        // Manual operation
        // -----------------------------------------------------------------------
        /// <summary>Highest index of handwheel array.</summary>
        public const int HLI_HW_MAXIDX = 11;
        /// <summary>Highest index of key-pair array.</summary>
        public const int HLI_KEY_MAXIDX = 31;
        /// <summary>Highest index of axis coupling array.</summary>
        public const int HLI_AX_COUPLING_MAXIDX = 3;

        // -----------------------------------------------------------------------
        // GCM / CTM
        // -----------------------------------------------------------------------
        /// <summary>Max number of GCM system entries.</summary>
        public const int HLI_SYS_GCM_MAX = 4;
        /// <summary>Highest index of GCM array.</summary>
        public const int HLI_SYS_GCM_MAXIDX = 3;
        /// <summary>Max CTM units.</summary>
        public const int HLI_SYS_CTM_MAX = 32;
        /// <summary>Highest index of CTM array.</summary>
        public const int HLI_SYS_CTM_MAXIDX = 31;
        /// <summary>Highest index of CTM values per unit.</summary>
        public const int HLI_CTM_VALUES_PER_UNIT_MAXIDX = 47;
        /// <summary>Highest index of CTM string.</summary>
        public const int HLI_CTM_STRING_MAXIDX = 23;

        // -----------------------------------------------------------------------
        // MDS / tool
        // -----------------------------------------------------------------------
        /// <summary>Max MDS ID count.</summary>
        public const int HLI_ID_MDS_MAX = 16;
        /// <summary>Highest index of MDS ID array.</summary>
        public const int HLI_ID_MDS_MAXIDX = 15;
        /// <summary>Max number of axis offsets for a tool.</summary>
        public const int CNC_TOOL_AXIS_OFFSET_MAX = 32;
        /// <summary>Max number of head offsets for a tool.</summary>
        public const int CNC_TOOL_HEAD_OFFSET_MAX = 70;
        /// <summary>Max number of parameters for a tool.</summary>
        public const int CNC_TOOL_PARAM_MAX = 60;
        /// <summary>Max number of tools per unit.</summary>
        public const int CNC_TOOL_PER_UNIT_MAX = 50;
        /// <summary>Highest index of tool axis offsets.</summary>
        public const int CNC_TOOL_AXIS_OFFSET_MAXIDX = 31;
        /// <summary>Highest index of tool head offsets.</summary>
        public const int CNC_TOOL_HEAD_OFFSET_MAXIDX = 69;
        /// <summary>Highest index of tool parameters.</summary>
        public const int CNC_TOOL_PARAM_MAXIDX = 59;
        /// <summary>Highest index of tools per unit.</summary>
        public const int CNC_TOOL_PER_UNIT_MAXIDX = 49;

        // -----------------------------------------------------------------------
        // String / name lengths (allocated including null terminator)
        // -----------------------------------------------------------------------
        /// <summary>Allocated length of version name string (usable = 22).</summary>
        public const int HLI_INTF_VERSION_NAME_LAENGE = 23;
        /// <summary>Allocated length of version name string (usable = 22).</summary>
        public const int HLI_VERSION_NAME_LAENGE = 23;
        /// <summary>Allocated length of axis name string (usable = 18).</summary>
        public const int HLI_ACHS_NAME_LAENGE = 19;
        /// <summary>Allocated length of channel name string (usable = 18).</summary>
        public const int HLI_CHANNEL_NAME_LAENGE = 19;
        /// <summary>Max length of a file name (usable = 259).</summary>
        public const int HLI_LAENGE_NAME = 259;
        /// <summary>Max length of a file name alias.</summary>
        public const int HLI_NAME_SIZE = HLI_LAENGE_NAME;
        /// <summary>Allocated length of variable string (usable = 20).</summary>
        public const int HLI_VAR_STRING_LAENGE = 23;
        /// <summary>Max length of a C module name.</summary>
        public const int HLI_MODUL_NAME_LAENGE = 15;
        /// <summary>Allocated length of struct name string (usable = 32).</summary>
        public const int HLI_STRUKT_NAME_LAENGE = 35;
        /// <summary>Max length of error message string.</summary>
        public const int HLI_ERR_MSG_STRING_LENGTH = 23;
        /// <summary>Max length of drive parameter name.</summary>
        public const int HLI_DRIVE_PARAM_NAME_SIZE = 31;
        /// <summary>Number of characters forming the major HLI version.</summary>
        public const int HLI_MAJOR_VER_LENGTH = 12;
        /// <summary>Max length of a parameter identifier.</summary>
        public const int HLI_PARAM_ID_LEN = 75;
        /// <summary>Max length of a parameter value.</summary>
        public const int HLI_PARAM_VAL_LEN = 67;
        /// <summary>Max length of a list identifier.</summary>
        public const int HLI_LIST_ID_LEN = 11;
        /// <summary>Max length of a message content string.</summary>
        public const int HLI_MSG_CONTENT_STRL = 119;
        /// <summary>Max length of ADS NetId string.</summary>
        public const int HLI_ADS_NETID_STRL = 23;
        /// <summary>Max length of IMCM process transition parameter string.</summary>
        public const int HLI_IMCM_PROC_TRANS_PARAM_STRL = 315;
        /// <summary>Highest index of IMCM process transition parameter string.</summary>
        public const int HLI_IMCM_PROC_TRANS_PARAM_MAXIDX = 315;
        /// <summary>Max length of IMCM mode-state parameter string.</summary>
        public const int HLI_IMCM_MODE_STATE_PARAM_STRL = 299;
        /// <summary>Highest index of IMCM mode-state parameter string.</summary>
        public const int HLI_IMCM_MODE_STATE_PARAM_MAXIDX = 299;
        /// <summary>Max length of error message string (alternate).</summary>
        public const int HLI_ERR_MSG_STR_LEN = 23;
        /// <summary>Max length of HMI-PLC string value.</summary>
        public const int HLI_HMI_PLC_STR_STRL = 299;

        // -----------------------------------------------------------------------
        // Error / data array sizes
        // -----------------------------------------------------------------------
        /// <summary>Highest index of HLI error mask array (TwinCAT value).</summary>
        public const int HLI_ERR_MASK_MAXIDX = 671;
        /// <summary>Highest index of HLI value-B data array.</summary>
        public const int HLI_WERT_B_DATA_MAXIDX = 23;
        /// <summary>Highest index of HLI value data array.</summary>
        public const int HLI_WERT_DATA_MAXIDX = 7;
        /// <summary>Highest index of axis techno-unit data array.</summary>
        public const int HLI_TECH_UNIT_AX_MAXIDX = 39;
        /// <summary>Highest index of channel techno-unit data array.</summary>
        public const int HLI_TECH_UNIT_CH_MAXIDX = 39;
        /// <summary>Highest index of SERCOS data array per axis.</summary>
        public const int HLI_SERCOS_DATA_MAXIDX = 2047;
        /// <summary>Size of the additive program info block.</summary>
        public const int HLI_ADD_PROG_INFO_SIZE = 23;
        /// <summary>Max number of additional info entries.</summary>
        public const int HLI_ADD_INFO_MAX = 2;
        /// <summary>Highest index of additional info array.</summary>
        public const int HLI_ADD_INFO_MAXIDX = 1;

        // -----------------------------------------------------------------------
        // Contour look-ahead
        // -----------------------------------------------------------------------
        /// <summary>Max contour look-ahead blocks.</summary>
        public const int HLI_CONTOUR_LOOKAHEAD_BLOCKS_MAX = 10;
        /// <summary>Highest index of contour look-ahead block array.</summary>
        public const int HLI_CONTOUR_LOOKAHEAD_BLOCKS_MAXIDX = 9;

        // -----------------------------------------------------------------------
        // Position look-ahead
        // -----------------------------------------------------------------------
        /// <summary>Number of entries in position look-ahead array.</summary>
        public const int HLI_MAX_POSITION_LOOKAHEAD = 6;
        /// <summary>Highest index of position look-ahead array (PLC constant).</summary>
        public const int HLI_POS_LOOKAHEAD_MAXIDX = 5;

        // -----------------------------------------------------------------------
        // HMI-PLC interface
        // -----------------------------------------------------------------------
        /// <summary>Number of global HMI-PLC keys.</summary>
        public const int HLI_HMI_PLC_KEY_GLOB_MAX = 50;
        /// <summary>Number of global HMI-PLC parameters.</summary>
        public const int HLI_HMI_PLC_PAR_GLOB_MAX = 20;
        /// <summary>Number of global HMI-PLC strings.</summary>
        public const int HLI_HMI_PLC_STR_GLOB_MAX = 10;
        /// <summary>Number of per-channel HMI-PLC keys.</summary>
        public const int HLI_HMI_PLC_KEY_CHAN_MAX = 50;
        /// <summary>Number of per-channel HMI-PLC parameters.</summary>
        public const int HLI_HMI_PLC_PAR_CHAN_MAX = 20;
        /// <summary>Number of per-channel HMI-PLC strings.</summary>
        public const int HLI_HMI_PLC_STR_CHAN_MAX = 20;
        /// <summary>Number of per-axis HMI-PLC keys.</summary>
        public const int HLI_HMI_PLC_KEY_AXIS_MAX = 5;
        /// <summary>Number of per-axis HMI-PLC parameters.</summary>
        public const int HLI_HMI_PLC_PAR_AXIS_MAX = 5;
        /// <summary>Number of per-axis HMI-PLC strings.</summary>
        public const int HLI_HMI_PLC_STR_AXIS_MAX = 1;
        /// <summary>Highest index of global HMI-PLC keys.</summary>
        public const int HLI_HPK_GLOB_MAXIDX = 49;
        /// <summary>Highest index of global HMI-PLC parameters.</summary>
        public const int HLI_HPP_GLOB_MAXIDX = 19;
        /// <summary>Highest index of global HMI-PLC strings.</summary>
        public const int HLI_HPS_GLOB_MAXIDX = 9;
        /// <summary>Highest index of per-channel HMI-PLC keys.</summary>
        public const int HLI_HPK_CHAN_MAXIDX = 49;
        /// <summary>Highest index of per-channel HMI-PLC parameters.</summary>
        public const int HLI_HPP_CHAN_MAXIDX = 19;
        /// <summary>Highest index of per-channel HMI-PLC strings.</summary>
        public const int HLI_HPS_CHAN_MAXIDX = 19;
        /// <summary>Highest index of per-axis HMI-PLC keys.</summary>
        public const int HLI_HPK_AXIS_MAXIDX = 4;
        /// <summary>Highest index of per-axis HMI-PLC parameters.</summary>
        public const int HLI_HPP_AXIS_MAXIDX = 4;
        /// <summary>Highest index of per-axis HMI-PLC strings.</summary>
        public const int HLI_HPS_AXIS_MAXIDX = 0;
        /// <summary>String length of HMI-PLC key name.</summary>
        public const int HLI_HMI_PLC_KEY_NAME_STRL = 11;
        /// <summary>String length of HMI-PLC parameter name.</summary>
        public const int HLI_HMI_PLC_PAR_NAME_STRL = 47;
        /// <summary>String length of HMI-PLC string name.</summary>
        public const int HLI_HMI_PLC_STR_NAME_STRL = HLI_HMI_PLC_PAR_NAME_STRL;

        // -----------------------------------------------------------------------
        // External variables
        // -----------------------------------------------------------------------
        /// <summary>Highest index of E-variable array.</summary>
        public const int HLI_E_VAR_MAXIDX = 199;
        /// <summary>Highest index of external 32-bit variable array (TwinCAT test/debug).</summary>
        public const int HLI_EXT_VAR32_MAXIDX = 239;
        /// <summary>Highest index of external string variable array.</summary>
        public const int HLI_EXT_VAR_STR_MAXIDX = 23;

        // -----------------------------------------------------------------------
        // PLCopen platform interface
        // -----------------------------------------------------------------------
        /// <summary>Max request data size for PLCopen platform interface.</summary>
        public const int HLI_PLCO_PF_REQUEST_SIZE_MAX = 512;
        /// <summary>Highest index of PLCopen platform request data array.</summary>
        public const int HLI_PLCO_PF_REQUEST_SIZE_MAXIDX = 511;
        /// <summary>Max response data size for PLCopen platform interface.</summary>
        public const int HLI_PLCO_PF_RESPONSE_SIZE_MAX = 176;
        /// <summary>Highest index of PLCopen platform response data array.</summary>
        public const int HLI_PLCO_PF_RESPONSE_SIZE_MAXIDX = 175;
        /// <summary>Number of PLCopen platform request slots.</summary>
        public const int HLI_PLCO_PF_REQUEST_NUM = 2;
        /// <summary>Highest index of PLCopen platform request slots.</summary>
        public const int HLI_PLCO_PF_REQUEST_MAXIDX = 1;
        /// <summary>Number of PLCopen platform response slots.</summary>
        public const int HLI_PLCO_PF_RESPONSE_NUM = 2;
        /// <summary>Highest index of PLCopen platform response slots.</summary>
        public const int HLI_PLCO_PF_RESPONSE_MAXIDX = 1;

        // -----------------------------------------------------------------------
        // PLCopen axis interface
        // -----------------------------------------------------------------------
        /// <summary>Max request data size for PLCopen axis interface.</summary>
        public const int HLI_PLCO_AXIS_REQUEST_SIZE_MAX = 512;
        /// <summary>Highest index of PLCopen axis request data array.</summary>
        public const int HLI_PLCO_AXIS_REQUEST_SIZE_MAXIDX = 511;
        /// <summary>Max response data size for PLCopen axis interface.</summary>
        public const int HLI_PLCO_AXIS_RESPONSE_SIZE_MAX = 144;
        /// <summary>Highest index of PLCopen axis response data array.</summary>
        public const int HLI_PLCO_AXIS_RESPONSE_SIZE_MAXIDX = 143;
        /// <summary>Number of normal PLCopen axis request slots.</summary>
        public const int HLI_PLCO_AXIS_REQUEST_NUM = 10;
        /// <summary>Highest index of normal PLCopen axis request slots.</summary>
        public const int HLI_PLCO_AXIS_REQUEST_MAXIDX = 9;
        /// <summary>Number of high-priority PLCopen axis request slots.</summary>
        public const int HLI_PLCO_AXIS_HIGH_PRIOR_NUM = 4;
        /// <summary>Highest index of high-priority PLCopen axis request slots.</summary>
        public const int HLI_PLCO_AXIS_HIGH_PRIOR_MAXIDX = 3;
        /// <summary>Number of PLCopen axis response slots.</summary>
        public const int HLI_PLCO_AXIS_RESPONSE_NUM = 10;
        /// <summary>Highest index of PLCopen axis response slots.</summary>
        public const int HLI_PLCO_AXIS_RESPONSE_MAXIDX = 9;
        /// <summary>Max display jobs per axis.</summary>
        public const int HLI_PLCO_AXIS_DISPLAY_JOB_MAX = 20;
        /// <summary>Highest index of display jobs per axis.</summary>
        public const int HLI_PLCO_AXIS_DISPLAY_JOB_MAXIDX = 19;

        // -----------------------------------------------------------------------
        // PLCopen group interface
        // -----------------------------------------------------------------------
        /// <summary>Max axes per PLCopen group.</summary>
        public const int HLI_PLCO_GROUP_AXIS_MAX = 32;
        /// <summary>Highest index of axes per PLCopen group.</summary>
        public const int HLI_PLCO_GROUP_AXIS_MAXIDX = 31;
        /// <summary>Max request data size for PLCopen group interface.</summary>
        public const int HLI_PLCO_GROUP_REQUEST_SIZE_MAX = 1024;
        /// <summary>Highest index of PLCopen group request data array.</summary>
        public const int HLI_PLCO_GROUP_REQUEST_SIZE_MAXIDX = 1023;
        /// <summary>Max response data size for PLCopen group interface.</summary>
        public const int HLI_PLCO_GROUP_RESPONSE_SIZE_MAX = 144;
        /// <summary>Highest index of PLCopen group response data array.</summary>
        public const int HLI_PLCO_GROUP_RESPONSE_SIZE_MAXIDX = 143;
        /// <summary>Number of normal PLCopen group request slots.</summary>
        public const int HLI_PLCO_GROUP_REQUEST_NUM = 10;
        /// <summary>Highest index of normal PLCopen group request slots.</summary>
        public const int HLI_PLCO_GROUP_REQUEST_MAXIDX = 9;
        /// <summary>Number of high-priority PLCopen group request slots.</summary>
        public const int HLI_PLCO_GROUP_HIGH_PRIOR_NUM = 4;
        /// <summary>Highest index of high-priority PLCopen group request slots.</summary>
        public const int HLI_PLCO_GROUP_HIGH_PRIOR_MAXIDX = 3;
        /// <summary>Number of PLCopen group response slots.</summary>
        public const int HLI_PLCO_GROUP_RESPONSE_NUM = 10;
        /// <summary>Highest index of PLCopen group response slots.</summary>
        public const int HLI_PLCO_GROUP_RESPONSE_MAXIDX = 9;
        /// <summary>Max display jobs per group.</summary>
        public const int HLI_PLCO_DISPLAY_JOB_MAX = 20;
        /// <summary>Highest index of display jobs per group.</summary>
        public const int HLI_PLCO_DISPLAY_JOB_MAXIDX = 19;

        // -----------------------------------------------------------------------
        // TDP (Tool Data Provider)
        // -----------------------------------------------------------------------
        /// <summary>Highest index of TDP axis offsets.</summary>
        public const int HLI_TDP_AX_OFFS_MAXIDX = 31;
        /// <summary>Highest index of TDP tool-head offsets.</summary>
        public const int HLI_TDP_TOOLHEAD_OFFS_MAXIDX = 69;
        /// <summary>Highest index of TDP additional info.</summary>
        public const int HLI_TDP_ADD_INFO_MAXIDX = 1;
        /// <summary>Highest index of TDP tool parameters.</summary>
        public const int HLI_TDP_TOOL_PARAM_MAXIDX = 151;
        /// <summary>Highest index of TDP kinematic name.</summary>
        public const int HLI_TDP_KIN_NAME_MAXIDX = 15;

        // -----------------------------------------------------------------------
        // Triple buffer
        // -----------------------------------------------------------------------
        /// <summary>Triple buffer read preference index.</summary>
        public const int HLI_TRIPLE_BUF_RD_PREF = 0;
        /// <summary>Triple buffer write preference index.</summary>
        public const int HLI_TRIPLE_BUF_WR_PREF = 1;

        // -----------------------------------------------------------------------
        // Miscellaneous sizing
        // -----------------------------------------------------------------------
        /// <summary>Highest index of MC channel fill-up bytes.</summary>
        public const int HLI_MCCH_FILLUP_MAXIDX = 559;
        /// <summary>Jump abort marker (normal abort).</summary>
        public const int HLI_JUMP_ABORT = -1;
        /// <summary>Jump abort marker (emergency abort).</summary>
        public const int HLI_JUMP_ABORT_EMERGENCY = -2;
        /// <summary>Value indicating an invalid turn number.</summary>
        public const int HLI_TURN_NOT_VALID = 9999;
        /// <summary>Value indicating an invalid pose status.</summary>
        public const int HLI_POSE_STATUS_NOT_VALID = 9999;

        // -----------------------------------------------------------------------
        // Technology function type constants
        // -----------------------------------------------------------------------
        /// <summary>M function technology type.</summary>
        public const uint HLI_INTF_M_FKT = 1;
        /// <summary>H function technology type.</summary>
        public const uint HLI_INTF_H_FKT = 2;
        /// <summary>S (spindle) technology type.</summary>
        public const uint HLI_INTF_SPINDEL = 3;
        /// <summary>T (tool) technology type.</summary>
        public const uint HLI_INTF_TOOL = 4;
        /// <summary>Axis block technology type.</summary>
        public const uint HLI_INTF_AXBLOCK = 5;
        /// <summary>V/E variable technology type 1.</summary>
        public const uint HLI_INTF_V_E_VARIABLE = 6;
        /// <summary>V/E variable technology type 2.</summary>
        public const uint HLI_INTF_V_E_VARIABLE_1 = 7;
        /// <summary>V/E variable technology type 3.</summary>
        public const uint HLI_INTF_V_E_VARIABLE_2 = 8;
        /// <summary>Unknown technology type.</summary>
        public const uint HLI_INTF_UNKNOWN = 9;

        // -----------------------------------------------------------------------
        // Stop condition flags (stop_conditions_r bitmask)
        // -----------------------------------------------------------------------
        /// <summary>Feed hold active.</summary>
        public const uint HLI_SC_FEEDHOLD = 0x1;
        /// <summary>No axis feed enable (VFG).</summary>
        public const uint HLI_SC_VFG = 0x2;
        /// <summary>Single-block mode.</summary>
        public const uint HLI_SC_SINGLE_BLOCK = 0x4;
        /// <summary>M00 or M01 stop.</summary>
        public const uint HLI_SC_M00_OR_M01 = 0x10;
        /// <summary>PLC acknowledge pending.</summary>
        public const uint HLI_SC_PLC_ACKNOWLEDGE = 0x20;
        /// <summary>Feed override is zero.</summary>
        public const uint HLI_SC_OVERRIDE_ZERO = 0x40;
        /// <summary>Rapid override is zero.</summary>
        public const uint HLI_SC_OVERRIDE_RAPID_ZERO = 0x80;
        /// <summary>Dwell time active.</summary>
        public const uint HLI_SC_DELAY_TIME = 0x200;
        /// <summary>Channel synchronisation active.</summary>
        public const uint HLI_SC_CHANNEL_SYNC = 0x800;
        /// <summary>Interpolator input buffer empty.</summary>
        public const uint HLI_SC_IPO_INPUT_EMPTY = 0x1000;
        /// <summary>Read-in disabled.</summary>
        public const uint HLI_SC_IPO_INPUT_DISABLED = 0x2000;
        /// <summary>Waiting for axes to reach position.</summary>
        public const uint HLI_SC_WAIT_FOR_AXES = 0x8000;
        /// <summary>Channel error.</summary>
        public const uint HLI_SC_CHANNEL_ERROR = 0x10000;
        /// <summary>Waiting for technology function acknowledge.</summary>
        public const uint HLI_SC_WAIT_TECHNO_ACK = 0x20000;
        /// <summary>Waiting for continue after collision.</summary>
        public const uint HLI_SC_W_C_AFTER_COLLISION = 0x40000;
        /// <summary>Slope supply problem.</summary>
        public const uint HLI_SC_SLOPE_SUPPLY_PROBLEM = 0x80000;
        /// <summary>Back interpolation active.</summary>
        public const uint HLI_SC_BACK_INTERPOLATION = 0x100000;
        /// <summary>Reversible stop active.</summary>
        public const uint HLI_SC_STOP_REVERSIBLE = 0x200000;
        /// <summary>Breakpoint stop, waiting for continue motion.</summary>
        public const uint HLI_SC_BREAKPOINT_STOP = 0x400000;
        /// <summary>Debug breakpoint stop via PDU.</summary>
        public const uint HLI_SC_DEBUG_BREAKPOINT_STOP = 0x800000;
        /// <summary>Dynamic position limit stop.</summary>
        public const uint HLI_SC_DYN_POS_LIMIT_STOP = 0x1000000;
        /// <summary>Channel stopped by M0.</summary>
        public const uint HLI_SC_M0_STOP = 0x2000000;
        /// <summary>Channel stopped by M1.</summary>
        public const uint HLI_SC_M1_STOP = 0x4000000;
        /// <summary>Stop because of #INSERT [...].</summary>
        public const uint HLI_SC_INSERT_STOP_AT_DIST = 0x8000000;
        /// <summary>Decoder waiting and macro FIFO empty.</summary>
        public const uint HLI_SC_DEC_SYN_CHAN_EMPTY = 0x10000000;

        // -----------------------------------------------------------------------
        // Block search state constants
        // -----------------------------------------------------------------------
        /// <summary>Block search inactive.</summary>
        public const int HLI_BS_INACTIVE = 0;
        /// <summary>Waiting for PLC to enable block search.</summary>
        public const int HLI_BS_WAIT_FOR_PLC_ON = 1;
        /// <summary>Block search active.</summary>
        public const int HLI_BS_ACTIVE = 2;
        /// <summary>Waiting for PLC to disable block search.</summary>
        public const int HLI_BS_WAIT_FOR_PLC_OFF = 3;
        /// <summary>Waiting for return to contour.</summary>
        public const int HLI_BS_WAIT_RETURN_TO_CONTOUR = 4;
        /// <summary>Returning to contour.</summary>
        public const int HLI_BS_RETURNING_TO_CONTOUR = 5;
        /// <summary>Waiting for continue-on-contour signal.</summary>
        public const int HLI_BS_WAIT_FOR_CONTINUE_CONTOUR = 6;

        // -----------------------------------------------------------------------
        // Operation mode / state constants
        // -----------------------------------------------------------------------
        /// <summary>Process deselected.</summary>
        public const uint HLI_IMCM_PROCESS_DESELECTED = 1;
        /// <summary>Process selected.</summary>
        public const uint HLI_IMCM_PROCESS_SELECTED = 2;
        /// <summary>Process ready.</summary>
        public const uint HLI_IMCM_PROCESS_READY = 3;
        /// <summary>Process active.</summary>
        public const uint HLI_IMCM_PROCESS_ACTIVE = 4;
        /// <summary>Process hold.</summary>
        public const uint HLI_IMCM_PROCESS_HOLD = 5;
        /// <summary>Process error.</summary>
        public const uint HLI_IMCM_PROCESS_ERROR = 6;
        /// <summary>Process selecting.</summary>
        public const uint HLI_IMCM_PROCESS_SELECTING = 7;
        /// <summary>Process deselecting.</summary>
        public const uint HLI_IMCM_PROCESS_DESELECTING = 8;
        /// <summary>Process preparing.</summary>
        public const uint HLI_IMCM_PROCESS_PREPARING = 9;
        /// <summary>Process clearing.</summary>
        public const uint HLI_IMCM_PROCESS_CLEARING = 10;
        /// <summary>Process starting.</summary>
        public const uint HLI_IMCM_PROCESS_STARTING = 11;
        /// <summary>Process aborting.</summary>
        public const uint HLI_IMCM_PROCESS_ABORTING = 12;
        /// <summary>Process stopping.</summary>
        public const uint HLI_IMCM_PROCESS_STOPPING = 13;
        /// <summary>Process resuming.</summary>
        public const uint HLI_IMCM_PROCESS_RESUMING = 14;
        /// <summary>Process resetting.</summary>
        public const uint HLI_IMCM_PROCESS_RESETTING = 15;
        /// <summary>Process finishing.</summary>
        public const uint HLI_IMCM_PROCESS_FINISHING = 16;

        // -----------------------------------------------------------------------
        // Message type constants
        // -----------------------------------------------------------------------
        /// <summary>#MSG text message.</summary>
        public const uint HLI_MESSAGE_TYPE_TEXT = 1;
        /// <summary>#MSG INFO message.</summary>
        public const uint HLI_MESSAGE_TYPE_INFO = 2;
        /// <summary>#MSG SYN_ACK message.</summary>
        public const uint HLI_MESSAGE_TYPE_TEXT_SYN_ACKN = 3;
        /// <summary>#MSG INFO SYN_ACK message.</summary>
        public const uint HLI_MESSAGE_TYPE_INFO_SYN_ACKN = 4;
        /// <summary>Additive block information message.</summary>
        public const uint HLI_MESSAGE_TYPE_ADD_BLOCK_INFO = 5;

        // -----------------------------------------------------------------------
        // Axis mode flags
        // -----------------------------------------------------------------------
        /// <summary>Axis is moving.</summary>
        public const uint HLI_INTF_AXMODE_MOVEMENT = 0x1;
        /// <summary>Axis is clampable.</summary>
        public const uint HLI_INTF_AXMODE_CLAMPABLE = 0x2;

        // -----------------------------------------------------------------------
        // Manual mode constants
        // -----------------------------------------------------------------------
        /// <summary>No manual mode selected.</summary>
        public const uint HLI_HB_MODE_NONE = 0;
        /// <summary>Handwheel manual mode.</summary>
        public const uint HLI_HB_MODE_HR = 1;
        /// <summary>Continuous jog (Tip) manual mode.</summary>
        public const uint HLI_HB_MODE_TIP = 2;
        /// <summary>Incremental jog manual mode.</summary>
        public const uint HLI_HB_MODE_JOG = 3;

        // -----------------------------------------------------------------------
        // Axis type constants
        // -----------------------------------------------------------------------
        /// <summary>Linear translator axis.</summary>
        public const ushort HLI_AXIS_TYPE_TRANSLATOR = 1;
        /// <summary>Rotary axis.</summary>
        public const ushort HLI_AXIS_TYPE_ROTATOR = 2;
        /// <summary>Spindle axis.</summary>
        public const ushort HLI_AXIS_TYPE_SPINDLE = 4;

        // -----------------------------------------------------------------------
        // Conveyor state constants
        // -----------------------------------------------------------------------
        /// <summary>Conveyor speed is constant.</summary>
        public const uint HLI_CONVEYOR_SPEED_CONST = 0;
        /// <summary>Conveyor is accelerating.</summary>
        public const uint HLI_CONVEYOR_ACCELERATION = 1;
        /// <summary>Conveyor is decelerating.</summary>
        public const uint HLI_CONVEYOR_DECELERATION = 2;
        /// <summary>Conveyor speed is zero.</summary>
        public const uint HLI_CONVEYOR_SPEED_ZERO = 3;
        /// <summary>Machining workpiece with conveyor speed zero.</summary>
        public const uint HLI_CONVEYOR_MACH_WITH_SPEED_ZERO = 4;

        // -----------------------------------------------------------------------
        // Conveyor sync state
        // -----------------------------------------------------------------------
        /// <summary>Axis idle, no conveyor action active.</summary>
        public const byte HLI_CONV_SYNC_STATE_IDLE = 0;
        /// <summary>Conveyor axis waiting for start.</summary>
        public const byte HLI_CONV_SYNC_STATE_WAIT = 1;
        /// <summary>Conveyor axis synchronous to conveyor.</summary>
        public const byte HLI_CONV_SYNC_STATE_LINKED = 2;
        /// <summary>Conveyor axis synchronising.</summary>
        public const byte HLI_CONV_SYNC_STATE_SYNC_IN = 3;
        /// <summary>Conveyor axis desynchronising.</summary>
        public const byte HLI_CONV_SYNC_STATE_SYNC_OUT = 4;
        /// <summary>Conveyor stopped.</summary>
        public const byte HLI_CONV_SYNC_STATE_STOP = 5;
        /// <summary>Axis interpolation time synchronous.</summary>
        public const byte HLI_CONV_SYNC_STATE_SYNC_IN_TIME = 6;
        /// <summary>Conveyor axis in error.</summary>
        public const byte HLI_CONV_SYNC_STATE_ERROR = 7;
        /// <summary>Force sync error.</summary>
        public const byte HLI_CONV_SYNC_STATE_FORCE_SYNC_ERROR = 8;
        /// <summary>Internal sync state cannot be mapped.</summary>
        public const byte HLI_CONV_SYNC_STATE_UNKNOWN = 9;

        // -----------------------------------------------------------------------
        // Scaling
        // -----------------------------------------------------------------------
        /// <summary>HLI scaling resolution.</summary>
        public const float HLI_SCALING_RESOLUTION = 1000000f;
        /// <summary>Inverse of HLI scaling resolution.</summary>
        public const float HLI_SCALING_RESOLUTION_INV = 1E-06f;
        /// <summary>Convert HLI value to numbers after decimal point.</summary>
        public const float HLI_CT_ACCESS_UNCONV_N_ADP = 1E-09f;
        /// <summary>Convert numbers after decimal point to HLI write value.</summary>
        public const float HLI_CT_ACCESS_CONV_N_ADP = 1000000000f;
    }
}
