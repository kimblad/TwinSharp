using System.Runtime.InteropServices;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Container that hold visualization data in any of the 11 existing versions.
    /// </summary>
    public struct SOLLKONT_VISU_PDU
    {
        ///<summary> number of structures SOLLKONT_VISU_DATA_V0 … SOLLKONT_VISU_DATA_V5 in the current message </summary>
        public int Count;

        ///<summary> Version identifier of visualisation data P-STUP-00039 </summary>
        public uint Version;

        ///<summary> Structure with visualisation data if P-STUP-00039 has the value 0. </summary>
        public SOLLKONT_VISU_DATA_V0[] v0;
    }

    /// <summary>
    /// Version 0 format of visualisation data.
    /// </summary>
    public struct SOLLKONT_VISU_DATA_V0
    {
        /// <summary> Visualization data </summary>
        public SOLLKONT_VISU_CH_DATA_STD Visu_data_std;

        ///<summary> Axis-specific visualisation data. </summary>
        public SOLLKONT_VISU_ACHS_DATA_STD[] Simu_achs_data_std;
    }


    /// <summary>
    /// Struct that contains standard visualisation data.
    /// </summary>
    public struct SOLLKONT_VISU_CH_DATA_STD
    {
        ///<summary> Block number in the NC program</summary>
        public int BlockNumber;

        ///<summary> 
        /// File offset from file start in bytes.
        /// >= 0 : valid data offset when program is active.
        /// == -1 : Offset not valid since no program is active
        ///</summary>
        public int FileOffset;

        ///<summary> ChannelNumber </summary>
        public ushort ChannelNumber;

        /// <summary>
        /// G Function.
        /// >= 0 : G function : G0, G1, G2, G3, G61 for polynomial blocks.
        /// == -1 : no G function active
        /// </summary>
        public short GFunction;

        /// <summary>
        /// Radius in [0.1 µm] for G2 / G3 blocks.
        /// </summary>
        public uint CircleRadius;

        /// <summary>
        /// Absolute position of circle centre point in the active machining plane (G17,G18,G19) in [0.1 µm] for G2 / G3 blocks (as of CNC Build V2.10.1032.03 and V2.10.1505.05)
        /// </summary>
        public double[] CircleCenterPoint;
    }

    /// <summary>
    /// Struct that contains axis-specific visualisation data.
    /// </summary>
    public struct SOLLKONT_VISU_ACHS_DATA_STD
    {
        /// <summary> Current Command position in [0.1 µm] </summary>
        public int CommandPosition;

        /// <summary> Logical axis number of the axis that the commanded position belongs to. </summary>
        public ushort LogicalAxisNumber;

        /// <summary> Bytes for alignment. </summary>
        public ushort AlignmentBytes;
    }


    /// <summary>
    /// Specifying velocity or position command values by the SPS effective in addition to
    /// the interpolator.No monitoring takes place of transferred values for compliance
    /// with the dynamic axis limits. To activate this interface, set the parameter P- AXIS-00732 to 1.
    /// </summary>
    public struct HLI_ADD_CMD_VALUE
    {
        /// <summary>
        /// Absolute value for additive position.
        /// Unit: 0,1 μm
        /// </summary>
        public int m_add_pos_value;

        /// <summary>
        /// Value for additive velocity.
        /// 1 μm/s
        /// </summary>
        public int m_add_speed_value;

        /// <summary> Reserved for future use. </summary>
        public int sgn32_free_1;
        
        /// <summary> Reserved for future use. </summary>
        public int sgn32_free_2;
        
        /// <summary> Reserved for future use. </summary>
        public int sgn32_free_3;
        
        /// <summary> Reserved for future use. </summary>
        public int sgn32_free_4;
        
        /// <summary> Reserved for future use. </summary>
        public int sgn32_free_5;

        /// <summary> Reserved for future use. </summary>
        public int sgn32_free_6;
    }


    /// <summary>
    /// A technology control unit contains elements for commanding, acknowledging and transferring any required parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct TECHNO_UNIT_CH
    {
        /// <summary>
        /// By setting please_rw, the CNC signals to the PLC that the technology control unit is to be executed.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool please_rw;

        /// <summary>
        /// Consumption data item.
        /// The CNC refreshes the data of the technology function only if this element is
        /// FALSE. After updating, the CNC sets this element to TRUE and so element
        /// done_w is set to FALSE.
        /// The PLC reads the data of the technology function if this element has the value
        /// TRUE.After the data is transferred, the PLC sets the value to FALSE.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool done_w;

        /// <summary>
        /// The type of technology function is transferred here. M code, H code, S or T. 
        /// </summary>
        public TechnologyFunction TechnologyType;

        /// <summary> Reserved for future use. </summary>
        public int fill_up_1;

        /// <summary>
        /// Depending on the content of the element TechnologyType this element contains the parameters of an
        /// M function/H function if the technology function type is HLI_INTF_M_FKT or HLI_INTF_H_FKT.
        /// S function (spindle) if the technology function type is HLI_INTF_SPINDEL.
        /// T function if the technology function type is HLI_INTF_TOOL.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 39)]
        public byte[] MSTH_PROCESS_CH;
    }

    /// <summary>
    /// Further describes the TECHNO_UNIT_CH struct when it is a M or H function.
    /// </summary>
    public struct HLI_M_H_PROZESS
    {
        /// <summary> Number of the M or H technology function. </summary>
        public uint Number;

        /// <summary> Expexted time for handling of the M or H technology function in [ms]. </summary>
        public int ExpectedTime;

        /// <summary> NC Block number of the M or H technology function. </summary>
        public uint BlockNumber;

        /// <summary> NC program row of the M or H technology function. </summary>
        public uint ProgramRow;

        /// <summary> Additional value, if programmed in NC program. </summary>
        public int additionalValue;

        /// <summary> Counter of written late sync, if active sync present. </summary>
        public ushort nr_late_sync;
        
        /// <summary> Reserved for future use. </summary>
        public ushort fill_up_1;

        /// <summary> Synchronisation mode </summary>
        public uint synchMode;
        
        /// <summary> Reserved for future use. </summary>
        public int fill_up_2;
    }


    /// <summary>
    /// Control unit to switch over the operation mode and poll the current state of operation mode management, including flow control of user data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_PROC_TRANS_TO_MODE_STATE
    {
        /// <summary> Operation mode from which the operation mode is to be changed. </summary>
        [MarshalAs(UnmanagedType.U4)]
        public OperationMode FromMode;

        /// <summary> State within the operation mode from which the state switchover is to occur. </summary>
        [MarshalAs(UnmanagedType.U4)]
        public OperationState FromState;

        /// <summary> Target operation mode when the operation mode is switched over. </summary>
        [MarshalAs(UnmanagedType.U4)]
        public OperationMode ToMode;

        /// <summary> Target state when operation mode is changed. </summary>
        [MarshalAs(UnmanagedType.U4)]
        public OperationState ToState;

        /// <summary> Not used (only for compatibility with the HÜMNOS standard). </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint ChannelNumber;

        /// <summary> Reserved for future use. </summary>
        public int fill_up_1;

        /// <summary>
        /// Parameters for operation mode change.
        /// It may be necessary to specify parameters when commanding an operation mode
        /// change to ensure the successful change to a specific state of an operation mode.
        /// These parameters are saved in this element.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 299)]
        public string Parameter;

        /// <summary> Reserved for future use. </summary>
        public int fill_up_2;
    }

    /// <summary>
    /// Struct for operation mode management that combines mode and state into one struct.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_IMCM_MODE_STATE
    {
        /// <summary> Operation mode. </summary>
        [MarshalAs(UnmanagedType.U4)]
        public OperationMode Mode;

        /// <summary> State within the operation mode. </summary>
        [MarshalAs(UnmanagedType.U4)]
        public OperationState State;
    }

    /// <summary>
    /// This structure contains user data of an error message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_SATZ
    {
        /// <summary> Head portion of error message. </summary>
        public HLI_ERROR_SATZ_KOPF Head;

        /// <summary> Body portion of error message. </summary>
        public HLI_ERROR_SATZ_RUMPF Body;
    }

    /// <summary>
    /// This structure contains user data of an error message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_SATZ_KOPF
    {
        /// <summary> Unique error number. </summary>
        public uint ErrorId;

        /// <summary> Reserved for future use. </summary>
        public int FillUp1;

        /// <summary> Module name of the module signalling an error. </summary>
        public HLI_MODUL_NAME ModulName;

        /// <summary> Line number in the module on which the error occurred. </summary>
        public int Line;

        /// <summary> Error number when a utility function is used. </summary>
        public uint UtilErrorId;

        /// <summary> Module name of the module with utility functions signalling an error. </summary>
        public HLI_MODUL_NAME UtilModulName;

        /// <summary> Line number of the line in which the error occurred in a module with utility function. </summary>
        public int UtilLine;

        /// <summary> 
        /// Error messages may be issued at several different points in the NC kernel. A
        /// unique multiple error number is issued to distinguish multiple usage.
        /// </summary>
        public ushort MultipleId;

        /// <summary> Type of commandable function in which an error occurred. </summary>
        public ushort BfType;

        /// <summary> Channel number of the channel in which the signalled error occurred. </summary>
        public ushort CncChannel;

        /// <summary> Communication ID of the BF signalling an error in the CNC. </summary>
        public ushort KommuId;

        /// <summary> No log error message by event logger. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SuppressTc2EventLogOutput;

        /// <summary> Reserved for future use. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FillUp2;

        /// <summary> Reserved for future use. </summary>
        public short FillUp3;

        /// <summary> Time specification on output of an error message. </summary>
        public HLI_FB_ZEITANGABE TimeStamp;

        /// <summary> Version name of the CNC specified in the error message. </summary>
        public HLI_INTF_VERSION_NAME VersionName;

        /// <summary> Undocumented, found through trial and error. </summary>
        public int FillUp4;

        /// <summary> Undocumented, found through trial and error. </summary>
        public int FillUp5;

        /// <summary>
        /// Recovery class of an error.
        /// Value range [0, 8]
        /// </summary>
        public ushort RectificationType;

        /// <summary>
        /// Reaction class of an error.
        /// Value range [0, 8]
        /// </summary>
        public ushort ReactionType;

        /// <summary>
        /// Body type of an error. Depending on the error type, the error set body contains
        /// further information on the error which occurred.
        /// 1: HLI_RUMPF_TYP_NC_PROG
        /// 2: HLI_RUMPF_TYP_MDS
        /// 3: HLI_RUMPF_TYP_KOMMU
        /// 4: HLI_RUMPF_TYP_RAMDISK
        /// 5: HLI_RUMPF_TYP_FILE
        /// 6: HLI_RUMPF_TYP_INTPR_FILE
        /// 7: HLI_RUMPF_TYP_LISTE_BINAER
        /// 8: HLI_RUMPF_TYP_GCM
        /// 9: HLI_RUMPF_TYP_LEER
        /// 10: HLI_RUMPF_TYP_HLI
        /// 11: HLI_RUMPF_TYP_NC_PROG_LR
        /// </summary>
        public uint BodyType; 
    }

    /// <summary>
    /// Structure that contains the name of one module.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_MODUL_NAME
    {
        /// <summary> Name of the module. </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.HLI_MODUL_NAME_LAENGE + 1)]
        public string Name;
    }

    /// <summary>
    /// Time specification on output of an error message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_FB_ZEITANGABE
    {
        /// <summary> Date of the instant of the error message. </summary>
        public uint DateCounter;

        /// <summary> Number of interrupt cycles since system start at the instant of an error message </summary>
        public uint CycleCounter;
    }

    /// <summary>
    /// Structure that contains the CNC version name.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_INTF_VERSION_NAME
    {
        /// <summary> CNC version name. </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.HLI_MODUL_NAME_LAENGE + 1)]
        public string Name;
    }
    
    /// <summary>
    /// Body portion of that an error message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_SATZ_RUMPF
    {
        /// <summary> Error mask. </summary>
        public HLI_ERROR_MASKE Mask;

        /// <summary> Individual error information 1. </summary>
        public HLI_WERT_B Value1;

        /// <summary> Individual error information 2. </summary>
        public HLI_WERT_B Value2;

        /// <summary> Individual error information 3. </summary>
        public HLI_WERT_B Value3;

        /// <summary> Individual error information 4. </summary>
        public HLI_WERT_B Value4;

        /// <summary> Individual error information 5. </summary>
        public HLI_WERT_B Value5;

        /// <summary> Identifier 1. </summary>
        public HLI_WERT Identifier1;

        /// <summary> Identifier 2. </summary>
        public HLI_WERT Identifier2;

        /// <summary> Identifier 3. </summary>
        public HLI_WERT Identifier3;

        /// <summary> Identifier 4. </summary>
        public HLI_WERT Identifier4;
    }

    /// <summary>
    /// Error mask.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_MASKE
    {
        /// <summary> Data of error mask. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.HLI_ERR_MASK_MAXIDX)]
        public byte[] ErrorMask;
    }

    /// <summary>
    /// Additional error information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT_B
    {
        /// <summary> Data type. </summary>
        public uint Type;

        /// <summary> Dimension of datum. </summary>
        public uint Dimension;

        /// <summary> Significance of datum. </summary>
        public uint Importance;

        /// <summary> Reserved for future use. </summary>
        public int FillUp1;

        /// <summary> Datum itself. </summary>
        public HLI_WERT_B_DATA Content;
    }


    /// <summary>
    /// Contains the actual data of an error message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT_B_DATA
    {

        /// <summary> Data. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.HLI_WERT_B_DATA_MAXIDX)]
        public byte[] Data;
    }


    /// <summary>
    /// Error value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT
    {
        /// <summary> Data type. </summary>
        public uint Type;

        /// <summary> Reserved for future use. </summary>
        public int FillUp1;

        /// <summary> Content. </summary>
        public HLI_WERT_DATA Content;
    }

    /// <summary>
    /// Error value data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT_DATA
    {
        /// <summary> Content data. </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.HLI_WERT_DATA_MAXIDX)]
        public byte[] Data;
    }

    /// <summary>
    /// Error information in relation to NC program.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_RUMPF_NC_PROG
    {
        /// <summary> Logical path number (see start-up list). </summary>
        public ushort LogicalPathNumber;

        /// <summary> Is file that gave the error encrypted. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FileEncrypted;

        /// <summary> Reserved for future use. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FillUp1;

        /// <summary> Reserved for future use. </summary>
        public int FillUp3;

        /// <summary> File name. </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.HLI_LAENGE_NAME + 1)]
        public string ProgramName;

        /// <summary> Reserved for future use. </summary>
        public int FillUp4;

        /// <summary> File offset in bytes. </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.HLI_LAENGE_NAME + 1)]
        public string FileName;

        /// <summary>  </summary>
        public uint FileOffset;

        /// <summary> Position in NC block in bytes. </summary>
        public ushort PositionOffsetNcBlock;

        /// <summary> Token offset in current NC line. </summary>
        public ushort TokenOffsetNcBlock;

        /// <summary> Block number of current NC line. </summary>
        public uint BlockNumber;
    }

    /// <summary>
    /// Control unit to manage data to activate a control element and assign it to an axis in manual mode.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_HB_ACTIVATION
    {
        /// <summary>
        /// Unique system-wide number of a logical axis.
        /// A control element is assigned to the specified logical axis with which the axis is to be moved in manual mode.
        /// </summary>
        public ushort LogicalAxisNumber;

        /// <summary>
        /// Number of the logical control element to be assigned to the logical axis.
        /// </summary>
        /// <remarks>
        /// When continuous and incremental jog mode is activated:
        /// one of the values which are defined as logical button pair numbers in the
        /// configuration list hand_mds.lis for the characteristics tasten_data[X].log_tasten_nr.
        /// When handwheel mode is activated:
        /// one of the values which are defined as logical handwheel numbers in the
        /// configuration list hand_mds.lis for the characteristics hr_data[0].log_hr_nr.
        /// If 0 is specified as the control element, the current operation mode of an axis is deselected
        /// </remarks>
        public ushort ControlElement;

        /// <summary>
        /// Manual operation mode to be assigned to the logical axis.
        /// </summary>
        /// <remarks>
        /// 0: no operation mode, current operation mode selected
        /// 1: Handwheel mode
        /// 2: Continuous jog mode
        /// 3: jog mode
        /// </remarks>
        public ushort OperationMode;

        /// <summary>
        /// Specifies the index of the parameter set to be used for the manual mode.
        /// Value range [0; 2]
        /// </summary>
        /// <remarks>
        /// The first value set in the parameter table (index = 0) is overwritten by the PLC
        /// interface when individual parameters are specified.The remaining parameter sets
        /// are not changed. This means, they correspond to the values specified in the axis-
        /// specific parameter lists.
        /// </remarks>
        public ushort ParameterIndex;

        /// <summary> Reserved for future use. </summary>
        public ushort FillUp1;

        /// <summary> Reserved for future use. </summary>
        public ushort FillUp2;
    }

    /// <summary>
    /// Control unit to activate/deactivate rapid traverse mode by a normal button press in manual mode.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_HB_RAPID_KEY
    {
        /// <summary> Logical button number for which the rapid traverse mode should be selected/deselected. </summary>
        public ushort LogicalKeyNumber;

        /// <summary>
        /// Rapid traverse mode of the button on/off.
        /// TRUE = Button for rapid traverse mode is active. The parameterised rapid traverse path motion is used for continuous jog mode.
        /// FALSE = Button not active in rapid traverse mode. The parameterised normal path velocity is used in continuous joy mode.]
        /// </summary>
        public ushort KeyPressed;

        /// <summary> Reserved for future use. </summary>
        public uint FillUp1;
    }

    /// <summary>
    /// Control unit to manage data to enforce a button press in manual mode,
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_HB_KEY
    {
        /// <summary>
        /// Logical button number from which the command comes.
        /// </summary>
        public ushort LogicalKeyNumber;

        /// <summary>
        /// Start/end of button press event and motion direction of buttons in manual mode.
        /// -1: Start of button press, negative motion direction
        /// 0: End of button press
        /// +1: Start of button press, positive motion direction
        /// </summary>
        public short Direction;

        /// <summary>
        /// Lifetime of the button signal.
        /// This is a time period defined by number of interpolator cycles.
        /// If this element has a value unequal to 0, the CNC independently generates the
        /// “End of button press” event after receiving a “Start of button press” event after the
        /// time period expires and which was defined by the number of specified interpolator cycles.
        /// </summary>
        public uint LifeTime;

        /// <summary>
        /// Retriggering "start of button press" event.
        /// If the element "Life time of a button signal" has a value unequal to 0, the “start of button press” event is retriggered if the “Lifetime of the button signal” has not yet expired.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool F_Refresh;

        /// <summary> Reserved for future use. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FillUp1;

        /// <summary> Reserved for future use. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FillUp2;

        /// <summary> Reserved for future use. </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool FillUp3;

        /// <summary> Reserved for future use. </summary>
        public int FillUp4;
    }

    /// <summary>
    /// Control unit to manage data for parameterising continuous jog mode in manual mode
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_HB_TIP_PARAMETER
    {
        /// <summary>
        /// Unique system-wide number of a logical axis.
        /// The specified logical axis is assigned the velocity at which it will be moved in manual mode in continuous jog mode.
        /// </summary>
        public ushort LogicalAxisNumber;

        /// <summary> Reserved for future use. </summary>
        public ushort FillUp1;

        /// <summary>
        /// Velocity to be assigned to the logical axis in continuous jog mode.
        /// Unit: 1 μm/s
        /// </summary>
        public uint Speed;
    }

    /// <summary>
    /// Control unit to manage data for parameterising incremental jog mode in manual mode.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_HB_JOG_PARAMETER
    {
        /// <summary>
        /// Unique system-wide number of a logical axis.
        /// The specified logical axis is assigned the velocity and incremental step width for
        /// each button press to move the axis in manual mode in incremental jog mode.
        /// </summary>
        public ushort LogicalAxisNumber;

        /// <summary> Reserved for future use. </summary>
        public ushort FillUp1;

        /// <summary>
        /// Path traversed by the logical axis in incremental jog mode each time the incremental jog button is pressed.
        /// Unit: 0.1 μm
        /// </summary>
        public uint Distance;

        /// <summary>
        /// Velocity to be assigned to the logical axis in incremental jog mode.
        /// Unit: 1 μm/s
        /// </summary>
        public uint Speed;

        /// <summary> Reserved for future use. </summary>
        public int FillUp2;
    }

    /// <summary>
    /// Control unit to manage data for parameterising handwheel mode in manual mode.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_HB_HR_PARAMETER
    {
        /// <summary>
        /// Unique system-wide number of a logical axis.
        /// The specified logical axis is assigned the handwheel resolution which is the basis for moving the axis in manual mode handwheel mode.
        /// </summary>
        public ushort LogicalAxisNumber;

        /// <summary> Reserved for future use. </summary>
        public ushort FillUp1;

        /// <summary>
        /// Resolution of axis motion path for one handwheel revolution.
        /// The internally used total resolution of the axis in 0.1 μm per applied handwheel
        /// increment results from the current handwheel resolution in 0.1 μm/increment
        /// divided by the physical handwheel resolution in increment/revolution of the handwheel specified
        /// Unit: 0.1 μm / handwheel revolution
        /// </summary>
        public int Resolution;
    }

    /// <summary>
    /// Combined interface identifier: index + class. Mirrors <c>HLI_MC_IFC_ID</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_MC_IFC_ID
    {
        /// <summary>Interface index (alias HLI_MC_IFC_INDEX : UINT).</summary>
        public ushort i_index;
        /// <summary>Interface class (alias HLI_MC_IFC_CLASS : UINT).</summary>
        public ushort i_class;
    }

    /// <summary>
    /// Job descriptor: job ID and interface identifier. Mirrors <c>HLI_MC_JOB_DESCR</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_MC_JOB_DESCR
    {
        /// <summary>Unique job identifier.</summary>
        public uint job_id;
        /// <summary>Interface ID that issued the job.</summary>
        public HLI_MC_IFC_ID ifc_id;
    }

    /// <summary>
    /// Job stack containing up to <c>HLI_MC_JOB_STACK_MAX</c> (15) job descriptors.
    /// Mirrors <c>HLI_MC_JOB_STACK</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_MC_JOB_STACK
    {
        /// <summary>Number of active jobs in the stack.</summary>
        public ushort count;
        /// <summary>Current iterator position.</summary>
        public ushort iterator;
        /// <summary>ID control field.</summary>
        public ushort id_control;
        /// <summary>Fill-up for alignment.</summary>
        public ushort fill_up_1;
        /// <summary>Job descriptor array. 15 elements [0..HLI_MC_JOB_STACK_MAXIDX].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_MC_JOB_STACK_MAX)]
        public HLI_MC_JOB_DESCR[] job;
    }

    /// <summary>
    /// Additive program info block containing the job stack.
    /// Mirrors <c>HLI_S_ADD_PROG_INFO</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_S_ADD_PROG_INFO
    {
        /// <summary>Job stack for this channel.</summary>
        public HLI_MC_JOB_STACK job_stack;
    }

    /// <summary>
    /// Header for a lock-free triple buffer. Mirrors <c>HLI_TRIPLE_BUF_HEAD</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_TRIPLE_BUF_HEAD
    {
        /// <summary>Preference lock word (L1).</summary>
        public uint pref_lock;

        /// <summary>New read consensus flag (L2).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool new_read_consense;

        /// <summary>TRUE once the buffer has been initialised (internal).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool initialized;

        /// <summary>Actual read consensus index (PRECON).</summary>
        public ushort actual_read_consense;

        /// <summary>Preferred read indices. 2 elements [0..1] (PREFER[]).</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ushort[] preferred_read_index;

        /// <summary>Size in bytes of each data element (internal).</summary>
        public uint data_element_size;

        /// <summary>Index of the most recently written buffer slot (LATEST).</summary>
        public ushort latest_written_index;

        /// <summary>Unused.</summary>
        public short sgn16_free_1;

        /// <summary>Unused.</summary>
        public int sgn32_free_1;
    }

    /// <summary>
    /// Time-to-distance data for one triple-buffer slot. Mirrors <c>HLI_STATE_TTD</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_STATE_TTD
    {
        /// <summary>Calculated time-to-distance values. 10 elements [0..HLI_BAHN_POS_TIME_LAH_MAXIDX]. Unit: µs.</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_BAHN_POS_TIME_LAH_MAX)]
        public int[] time_to_distance;

        /// <summary>Validity flags for time_to_distance. 10 elements [0..HLI_BAHN_POS_TIME_LAH_MAXIDX].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_BAHN_POS_TIME_LAH_MAX)]
        public byte[] time_to_distance_valid;

        /// <summary>Unused.</summary>
        public short sgn16_free_3;
        /// <summary>Unused.</summary>
        public short sgn16_free_2;
        /// <summary>Unused.</summary>
        public short sgn16_free_1;

        /// <summary>Enlacement of the edge at the pressure roll.</summary>
        public uint enlacement;

        /// <summary>Velocity of the edge in the power applicator. Unit: 1 µm/s.</summary>
        public int edge_velocity;

        /// <summary>Length of consumed edge since start of TTD function. Unit: 0.1 µm.</summary>
        public int edge_position;

        /// <summary>Unused.</summary>
        public int sgn32_free_9;
        /// <summary>Unused.</summary>
        public int sgn32_free_8;
        /// <summary>Unused.</summary>
        public int sgn32_free_7;
        /// <summary>Unused.</summary>
        public int sgn32_free_6;
        /// <summary>Unused.</summary>
        public int sgn32_free_5;
        /// <summary>Unused.</summary>
        public int sgn32_free_4;
        /// <summary>Unused.</summary>
        public int sgn32_free_3;
        /// <summary>Unused.</summary>
        public int sgn32_free_2;
        /// <summary>Unused.</summary>
        public int sgn32_free_1;
    }

    /// <summary>
    /// Axis name string. Mirrors <c>HLI_ACHS_NAME</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ACHS_NAME
    {
        /// <summary>Axis name characters. Up to HLI_ACHS_NAME_LAENGE (19) characters + null terminator.</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HliConstants.HLI_ACHS_NAME_LAENGE + 1)]
        public string zeichen;
    }

    /// <summary>
    /// Manual mode display data for one axis. Mirrors <c>HLI_HB_AXIS_DISPLAY_DATA</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_HB_AXIS_DISPLAY_DATA
    {
        /// <summary>Handwheel resolution. Unit: 0.1 µm / revolution.</summary>
        public int hr_aufloesung;

        /// <summary>Continuous jog velocity. Unit: 1 µm/s.</summary>
        public int tipp_geschw;

        /// <summary>Incremental jog distance. Unit: 0.1 µm.</summary>
        public int jog_weg;

        /// <summary>Incremental jog velocity. Unit: 1 µm/s.</summary>
        public int jog_geschw;

        /// <summary>Manual mode state.</summary>
        public ushort state;

        /// <summary>Active manual operation mode. See HLI_MANM_* constants.</summary>
        public ushort operation_mode;

        /// <summary>Logical number of the active control element (key or handwheel).</summary>
        public ushort control_element;

        /// <summary>Fill-up for alignment.</summary>
        public ushort fill_up_1;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_1;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_2;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_3;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_4;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_5;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_6;
    }

    /// <summary>
    /// Per-axis coordinate display data. Mirrors <c>HLI_BAHN_COORD_DISP_DATA</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_BAHN_COORD_DISP_DATA
    {
        /// <summary>Axis name.</summary>
        public HLI_ACHS_NAME axis_name_r;

        /// <summary>Logical axis number.</summary>
        public ushort log_achs_nr_r;

        /// <summary>Axis type. See HLI_AXIS_TYPE_* constants.</summary>
        public ushort axis_type_r;

        /// <summary>Axis state.</summary>
        public ushort state_r;

        /// <summary>TRUE when homing has been completed.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool homing_done_r;

        /// <summary>Conveyor synchronisation state. See HLI_CONV_SYNC_STATE_* constants.</summary>
        public byte conv_sync_state_r;

        /// <summary>Command position. Unit: 0.1 µm.</summary>
        public int cmd_position_r;

        /// <summary>Interpolated position. Unit: 0.1 µm.</summary>
        public int current_position_r;

        /// <summary>Actual position from the position controller. Unit: 0.1 µm.</summary>
        public int act_position_r;

        /// <summary>PCS active position compensated by dynamic coordinate system (output of dyn. CS). Unit: 0.1 µm.</summary>
        public int active_pos_pcs_dyn_cs_r;

        /// <summary>MCS active position compensated by dynamic coordinate system (output of dyn. CS). Unit: 0.1 µm.</summary>
        public int active_pos_mcs_dyn_cs_r;

        /// <summary>Offset from manual mode. Unit: 0.1 µm.</summary>
        public int man_offset_r;

        /// <summary>Distance to go in the current block. Unit: 0.1 µm.</summary>
        public int dist_to_go_r;

        /// <summary>Total offset (zero offsets + manual offset). Unit: 0.1 µm.</summary>
        public int total_offset_r;

        /// <summary>ACS position after transformation, or PCS position with offsets. Unit: 0.1 µm.</summary>
        public int acs_position_r;

        /// <summary>W0 position: TCP position in the base Cartesian machine coordinate system (active_position_mcs). Unit: 0.1 µm.</summary>
        public int w0_position_r;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_1;

        /// <summary>Manual mode display data for this axis.</summary>
        public HLI_HB_AXIS_DISPLAY_DATA hb_display_r;

        /// <summary>Velocity of the TCP in the base Cartesian machine coordinate system. Unit: 1 µm/s.</summary>
        public int w0_velocity_r;

        /// <summary>Acceleration of the TCP in the base Cartesian machine coordinate system. Unit: 1 µm/s².</summary>
        public int w0_acceleration_r;

        /// <summary>PCS position. Unit: 0.1 µm.</summary>
        public int pcs_position_r;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_2;

        /// <summary>Distance to go to end of geometry marked with #DIST TO GO [AX]. Unit: 0.1 µm.</summary>
        public double dist_to_geometry_end_r;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_5;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_6;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_7;
        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_8;

        /// <summary>Reserved for future use (real64_free_10..1). 10 × LREAL.</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public double[] real64_free;
    }

    /// <summary>
    /// Conveyor synchronisation status data. Mirrors <c>HLI_CONVEYOR_DATA</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_CONVEYOR_DATA
    {
        /// <summary>Logical axis number of the currently displayed conveyor axis.</summary>
        public ushort log_master_axis_nr;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public short sgn16_free_1;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public int sgn32_free_2;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public double real64_free_2;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public double real64_free_1;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public int sgn32_free_1;

        /// <summary>Current position difference between the parallel machine axis and the conveyor axis. Unit: 0.1 µm.</summary>
        public int synchro_lag;
    }

    /// <summary>
    /// Triple-buffered time-to-distance state. Mirrors <c>HLI_STATE_TTD_TRIPLE_BUF</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_STATE_TTD_TRIPLE_BUF
    {
        /// <summary>Triple buffer management header.</summary>
        public HLI_TRIPLE_BUF_HEAD head;

        /// <summary>Triple buffer data slots. 3 elements [0..2].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public HLI_STATE_TTD[] data;
    }

    /// <summary>
    /// Kinematic transformation display data for one transformation step.
    /// Mirrors <c>HLI_BAHN_TRAFO_DISP_DATA</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_BAHN_TRAFO_DISP_DATA
    {
        /// <summary>Kinematic identification number.</summary>
        public ushort kin_id;

        /// <summary>TRUE when this kinematic transformation is active.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool kin_trafo_active;

        /// <summary>Fill-up for 4-byte alignment.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool fill_up_1;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public int fill_up_2;
    }

    /// <summary>
    /// Position look-ahead data. Mirrors <c>HLI_LOOKAHEAD_DATA</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_LOOKAHEAD_DATA
    {
        /// <summary>Time to pass through the configured look-ahead distance. Unit: µs.</summary>
        public int time_equivalent;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public int fill_up_1;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_2;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_1;
    }

    /// <summary>
    /// Dynamic data for the active NC block. Mirrors <c>HLI_BLOCK_DYNAMIC</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_BLOCK_DYNAMIC
    {
        /// <summary>End velocity of a movement NC block. Unit: 1 µm/s.</summary>
        public int vel_end_geo;

        /// <summary>Fill-up for 8-byte alignment.</summary>
        public int fill_up_1;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_2;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_1;
    }

    /// <summary>
    /// Edge function status signals and bend angle. Mirrors <c>HLI_EDGE_FUNCTION</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_EDGE_FUNCTION
    {
        /// <summary>Signal 1: approaching movement to edge is active.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool signal_1;

        /// <summary>Signal 2: piercing time is active.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool signal_2;

        /// <summary>Signal 3: leaving movement is active.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool signal_3;

        /// <summary>Fill-up for 4-byte alignment.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool fill_up_1;

        /// <summary>Bend angle at the end of the motion NC block.</summary>
        public int angle_end;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_2;

        /// <summary>Reserved for future use.</summary>
        public int sgn32_free_1;
    }

    /// <summary>
    /// A CNC file name string. Mirrors <c>HLI_FILE_NAME</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_FILE_NAME
    {
        /// <summary>File name characters. Up to HLI_NAME_SIZE (259) characters + null terminator.</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HliConstants.HLI_LAENGE_NAME + 1)]
        public string zeichen;
    }

    /// <summary>
    /// Tool path display data: orientation, tangent, and normal vectors in the coordinate system.
    /// Mirrors <c>HLI_TOOL_PATH_DISP_DATA</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HLI_TOOL_PATH_DISP_DATA
    {
        /// <summary>Orientation vector. 3 elements [0..HLI_CS_AXES_MAXIDX].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_CS_AXES_NR)]
        public int[] ori_vec;

        /// <summary>Fill-up for 8-byte alignment after ori_vec.</summary>
        public int fill_up_1;

        /// <summary>Path tangent vector (tb). 3 elements [0..HLI_CS_AXES_MAXIDX].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_CS_AXES_NR)]
        public int[] tb_vec;

        /// <summary>Fill-up for 8-byte alignment after tb_vec.</summary>
        public int fill_up_2;

        /// <summary>Vector perpendicular to orientation and path tangent (tn). 3 elements [0..HLI_CS_AXES_MAXIDX].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_CS_AXES_NR)]
        public int[] tn_vec;

        /// <summary>Fill-up for 8-byte alignment after tn_vec.</summary>
        public int fill_up_3;

        /// <summary>Vector perpendicular to tb and tn (fn). 3 elements [0..HLI_CS_AXES_MAXIDX].</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_CS_AXES_NR)]
        public int[] fn_vec;

        /// <summary>Fill-up for 8-byte alignment after fn_vec.</summary>
        public int fill_up_4;
    }

    /// <summary>
    /// Real-time status of the CNC path interpolator, mirroring the <c>STATE_BAHN</c> PLC struct.
    /// Accessed via <c>HLI_Global_Variables.gpCh[n]^.bahn_state</c>.
    /// Read in a single ADS call via <see cref="RealTimeStatus.State"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StateBahn
    {
        /// <summary>Path covered in the current block. Unit: 0.1 µm.</summary>
        public int CoveredDistance;

        /// <summary>Programmed feed rate (F word). Unit: 1 µm/s.</summary>
        public int CommandFeed;

        /// <summary>Active command feed weighted by real-time influences. Unit: 1 µm/s.</summary>
        public int CommandFeedActive;

        /// <summary>Actual interpolator feed rate. Unit: 1 µm/s.</summary>
        public int ActiveFeed;

        /// <summary>Synchronisation state of the channel.</summary>
        public uint SynchState;

        /// <summary>TRUE when the end of the NC program has been reached.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool ProgramEnd;

        /// <summary>TRUE when the CNC is waiting for the operator to clear an error.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool WaitErrorRemoval;

        /// <summary>TRUE while the path interpolator is actively generating motion commands.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool InterpolationActive;

        /// <summary>TRUE when all participating axes have reached their target positions.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool AxesInPosition;

        /// <summary>TRUE when the CNC is waiting for a technology function (M/H/S/T) to be acknowledged.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool WaitTechnoAcknowledge;

        /// <summary>TRUE when the CNC is waiting for a continue signal (e.g. after M00/M01).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool WaitContinue;

        /// <summary>TRUE during a programmed dwell time (G04).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool DwellTimeActive;

        /// <summary>TRUE when the CNC is paused waiting for axes to reach their in-position window.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool WaitAxesInPosition;

        /// <summary>TRUE while a block search is running.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool BlockSearchActive;

        /// <summary>TRUE when a speed limit has been detected on the current path segment.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SpeedLimitDetected;

        /// <summary>TRUE when rapid mode (G00) is active.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool RapidMode;

        /// <summary>TRUE when the CNC is waiting for continue after a collision detection event.</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool WaitContinueAfterCollision;

        /// <summary>Tool path orientation and tangent vectors (tool_path_r).</summary>
        public HLI_TOOL_PATH_DISP_DATA ToolPath;
        /// <summary>Per-axis coordinate display data. 32 elements [0..HLI_CH_AX_MAXIDX] (coord_r).</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_CH_AX_MAX)]
        public HLI_BAHN_COORD_DISP_DATA[] Coord;
        /// <summary>Additive program info (job stack) for this channel (add_prog_info_r).</summary>
        public HLI_S_ADD_PROG_INFO AddProgInfo;
        /// <summary>Active stop conditions bitmask (stop_conditions_r). See <see cref="StopConditionFlags"/>.</summary>
        public StopConditionFlags StopConditions;

        /// <summary>Current NC block number (block_nr_r).</summary>
        public int BlockNumber;

        /// <summary>Active file name (file_name_r).</summary>
        public HLI_FILE_NAME FileName;

        /// <summary>File offset of the active NC block in bytes (active_fileoffset_r).</summary>
        public int ActiveFileOffset;
        /// <summary>Edge function signals and bend angle (edge_function_r).</summary>
        public HLI_EDGE_FUNCTION EdgeFunction;
        /// <summary>Dynamic data of the active NC block (block_dynamic_r).</summary>
        public HLI_BLOCK_DYNAMIC BlockDynamic;
        /// <summary>Position look-ahead data (lookahead_r).</summary>
        public HLI_LOOKAHEAD_DATA Lookahead;

        /// <summary>Block number of the calling NC block (calling_block_nr_r).</summary>
        public int CallingBlockNumber;

        /// <summary>Number of NC blocks decoded since program start (block_count_r).</summary>
        public uint BlockCount;

        /// <summary>TRUE when a block has been inserted by the PLC (block_inserted_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool BlockInserted;

        /// <summary>TRUE when G200 is active (g200_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool G200Active;

        /// <summary>TRUE when G201 is active (g201_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool G201Active;

        /// <summary>TRUE when a measurement function is active (measure_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool MeasureActive;

        /// <summary>TRUE when the escape function is enabled (escape_enabled_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EscapeEnabled;

        /// <summary>TRUE when a jump is active (jump_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool JumpActive;

        /// <summary>TRUE when an approach move is active (approach_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool ApproachActive;

        /// <summary>TRUE when waiting for external command speed (wait_ext_command_speed_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool WaitExtCommandSpeed;

        /// <summary>Distance from program start. Unit: 0.1 µm (dist_prog_start).</summary>
        public double DistanceFromProgramStart;

        /// <summary>Real-time loop count (rt_loop_count_r).</summary>
        public int RtLoopCount;

        /// <summary>File offset of the active program trace position (active_prg_trace_fileoffset_r).</summary>
        public int ActivePrgTraceFileOffset;
        /// <summary>Kinematic transformation display data. 3 elements [0..HLI_KIN_TRAFO_STEPS_MAXIDX] (trafo_r).</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_KIN_TRAFO_STEPS_MAX)]
        public HLI_BAHN_TRAFO_DISP_DATA[] Trafo;

        /// <summary>TRUE when delete-distance-to-go is active (delete_distance_to_go_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool DeleteDistanceToGoActive;

        /// <summary>TRUE when EDM (Electrical Discharge Machining) mode is active (edm_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EdmActive;

        /// <summary>Block search state. See HLI_BS_* constants (block_search_state_r).</summary>
        public short BlockSearchState;

        /// <summary>Path deviation during block search. Unit: 0.1 µm (block_search_path_deviation_r).</summary>
        public uint BlockSearchPathDeviation;

        /// <summary>Interpolator instance identifier (interp_id).</summary>
        public uint InterpId;

        /// <summary>Time until the next feed block in the lookahead. Unit: µs (time_to_next_feed_block_r).</summary>
        public uint TimeToNextFeedBlock;

        /// <summary>User-defined value for reversible stop (stop_reversible_usr_val_r).</summary>
        public uint StopReversibleUsrVal;

        /// <summary>Time elapsed since the start of the current block chain. Unit: µs (time_since_block_chain_start_r).</summary>
        public uint TimeSinceBlockChainStart;
        /// <summary>Triple-buffered time-to-distance state (time_to_distance_r).</summary>
        public HLI_STATE_TTD_TRIPLE_BUF TimeToDistance;

        /// <summary>Secant angle at the current path position (secant_angle). Unit: 0.001°.</summary>
        public int SecantAngle;

        /// <summary>TRUE when SecantAngle contains a valid value (secant_angle_valid).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool SecantAngleValid;

        /// <summary>TRUE when an area monitoring violation has been detected (area_mon_violation_detected_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool AreaMonViolationDetected;

        /// <summary>TRUE when the escape trigger is suspended (escape_trigger_is_suspended_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EscapeTriggerIsSuspended;

        /// <summary>TRUE when the interpolator is currently inside its real-time loop (inside_rt_loop_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool InsideRtLoop;

        /// <summary>File offset of the next NC block (active_fileoffset_next_ncbl_r). Unit: bytes.</summary>
        public int ActiveFileOffsetNextNcbl;

        /// <summary>Active D word (active_d_word_r).</summary>
        public int ActiveDWord;

        /// <summary>Path length of the escape movement (escape_path_length). Unit: 0.1 µm.</summary>
        public double EscapePathLength;
        /// <summary>ESA (Escape Strategy) velocity values. 3 elements [0..HLI_ESA_POINTS_MAXIDX]. Unit: 1 µm/s (esa_vb).</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_ESA_POINTS_MAX)]
        public double[] EsaVb;

        /// <summary>Validity flags for EsaVb. 3 elements [0..HLI_ESA_POINTS_MAXIDX] (esa_data_valid).</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_ESA_POINTS_MAX)]
        public byte[] EsaDataValid;

        /// <summary>TRUE when XyzComp values are valid (xyz_comp_valid_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool XyzCompValid;

        /// <summary>TRUE when the backward interpolation buffer end has been reached (backward_buffer_end_reached_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool BackwardBufferEndReached;

        /// <summary>TRUE when orbit mode is active (orbit_active_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool OrbitActive;

        /// <summary>Active escape strategy (escape_strategy_r).</summary>
        public ushort EscapeStrategy;

        /// <summary>TRUE when waiting for an escape strategy change (escape_strategy_wait_change_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool EscapeStrategyWaitChange;

        /// <summary>TRUE when the orbit radius is zero (orbit_radius_zero_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool OrbitRadiusZero;

        /// <summary>TRUE when waiting to extend the orbit NC block (orbit_wait_extend_ncbl_r).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool OrbitWaitExtendNcbl;

        /// <summary>TRUE when inside singularity avoidance movement (in_singularity_avoidance).</summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool InSingularityAvoidance;

        /// <summary>Singularity tracking status (st_status).</summary>
        public ushort StStatus;

        /// <summary>Singularity tracking turn number (st_turn).</summary>
        public ushort StTurn;

        /// <summary>XYZ compensation values. 3 elements [0..HLI_CS_AXES_MAXIDX]. Unit: 0.1 µm (xyz_comp_r).</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HliConstants.HLI_CS_AXES_NR)]
        public double[] XyzComp;

        /// <summary>Path inclination angle (inclination_r). Unit: 0.001°.</summary>
        public uint Inclination;

        /// <summary>Virtual tool length (virt_tool_length_r). Unit: 0.1 µm.</summary>
        public uint VirtToolLength;

        /// <summary>Conveyor synchronisation status (conveyor_data).</summary>
        public HLI_CONVEYOR_DATA ConveyorData;

        /// <summary>Reserved for future use (real64_free_7..1). 7 × LREAL.</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public double[] real64_free;
    }

    /// <summary>
    /// Payload of an NC-program message sent via <c>#MSG PLC</c> or <c>#MSGINFO PLC</c>.
    /// Mirrors <c>HLI_MSG_SENDUNG</c> from the HLI interface.
    /// Layout: typ (UDINT) — fill_up_1 (DINT, mod-8 alignment pad) — inhalt (STRING(119)).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_MSG_SENDUNG
    {
        /// <summary>
        /// Message type: 1 = #MSG (text), 2 = #MSGINFO.
        /// See <see cref="HliConstants.HLI_MESSAGE_TYPE_TEXT"/> etc.
        /// </summary>
        public uint Typ;

        /// <summary>Fill member for mod-8 alignment. Not used by the HMI.</summary>
        public int FillUp1;

        /// <summary>Message text. Up to HLI_MSG_CONTENT_STRL (119) characters + null terminator.</summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HliConstants.HLI_MSG_CONTENT_STRL + 1)]
        public string Inhalt;
    }
}


