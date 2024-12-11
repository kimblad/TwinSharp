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
}


