using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.InteropServices;
using TwinCAT.TypeSystem;

namespace TwinSharp.CNC
{
    public struct SOLLKONT_VISU_PDU
    {
        public int Count; //number of structures SOLLKONT_VISU_DATA_V0 … SOLLKONT_VISU_DATA_V5 in the current message
        public uint Version; //Version identifier of visualisation data P-STUP-00039

        public SOLLKONT_VISU_DATA_V0[] v0; //Structure with visualisation data if P-STUP-00039 has the value 0.
    }   
    public struct SOLLKONT_VISU_DATA_V0
    {
        public SOLLKONT_VISU_CH_DATA_STD Visu_data_std;
        public SOLLKONT_VISU_ACHS_DATA_STD[] Simu_achs_data_std; //Axis-specific visualisation data.
    }
    public struct SOLLKONT_VISU_CH_DATA_STD
    {
        public int BlockNumber; // block number in the NC program
        public int FileOffset; // file offset from file start in bytes
        // >= 0 : valid data offset when program is active
        // == -1 : Offset not valid since no program is active

        public ushort ChannelNumberr; // channel number
        public short GFunction;
        // >= 0 : G function : G0, G1, G2, G3, G61 for polynomial blocks
        // == -1 : no G function active

        public uint CircleRadius; // radius in [0.1 µm] for G2 / G3 blocks
        public double[] CircleCenterPoint; // Absolute position of circle centre point in the active machining plane (G17,G18,G19) in [0.1 µm] for G2 / G3 blocks (as of CNC Build V2.10.1032.03 and V2.10.1505.05)
    }

    public struct SOLLKONT_VISU_ACHS_DATA_STD
    {
        public int CommandPosition; // Current Command position in [0.1 µm]
        public ushort LogicalAxisNumber;

        public ushort AlignmentBytes;
    }

    public struct HLI_ADD_CMD_VALUE
    {
        public int m_add_pos_value;
        public int m_add_speed_value;
        public int sgn32_free_1;
        public int sgn32_free_2;
        public int sgn32_free_3;
        public int sgn32_free_4;
        public int sgn32_free_5;
        public int sgn32_free_6;
    }

    public struct MC_CONTROL_BOOL_UNIT
    {
        public bool reqeust_r;
        public bool enable_w;
        public bool command_w;
        public bool state_r;
        //public int fill_up_1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct TECHNO_UNIT_CH
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool please_rw;
        [MarshalAs(UnmanagedType.I1)]
        public bool done_w;
        public TechnologyFunction TechnologyType; //M code, H code, S or T
        public int fill_up_1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 39)]
        public byte[] MSTH_PROCESS_CH;
    }

    /// <summary>
    /// Further describes the TECHNO_UNIT_CH struct when it is a M or H function.
    /// </summary>
    public struct HLI_M_H_PROZESS
    {
        public uint Number; //Number of the M or H technology function.
        public int ExpectedTime; //Expexted time for handling of the M or H technology function in [ms].
        public uint BlockNumber; //NC Block number of the M or H technology function.
        public uint ProgramRow; //NC program row of the M or H technology function.
        public int additionalValue; //Additional value, if programmed in NC program.
        public ushort nr_late_sync; //Counter of written late sync, if active sync present.
        public ushort fill_up_1; //unused
        public uint synchMode; //Synchronisation mode
        public int fill_up_2; //unused
    }


    /// <summary>
    /// Control unit to switch over the operation mode and poll the current state of operation mode management, including flow control of user data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_PROC_TRANS_TO_MODE_STATE
    {
        [MarshalAs(UnmanagedType.U4)]
        public OperationMode FromMode;
        [MarshalAs(UnmanagedType.U4)]
        public OperationState FromState;
        [MarshalAs(UnmanagedType.U4)]
        public OperationMode ToMode;
        [MarshalAs(UnmanagedType.U4)]
        public OperationState ToState;
        [MarshalAs(UnmanagedType.U4)]
        public uint ChannelNumber; //Not used (only for compatibility with the HÜMNOS standard).
        public int fill_up_1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 299)]
        public string Parameter;
        public int fill_up_2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_IMCM_MODE_STATE
    {
        [MarshalAs(UnmanagedType.U4)]
        public OperationMode Mode;
        [MarshalAs(UnmanagedType.U4)]
        public OperationState State;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_SATZ
    {
        public HLI_ERROR_SATZ_KOPF Head;
        public HLI_ERROR_SATZ_RUMPF Tail;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_SATZ_KOPF
    {
        public uint ErrorId;
        public int FillUp1; 
        public HLI_MODUL_NAME ModulName; 
        public int Line;
        public uint UtilErrorId; 
        public HLI_MODUL_NAME UtilModulName; 
        public int UtilLine;
        public ushort MultipleId;
        public ushort BfType;
        public ushort CncChannel;
        public ushort KommuId;
        [MarshalAs(UnmanagedType.I1)]
        public bool SuppressTc2EventLogOutput; //no log error message by event logger
        [MarshalAs(UnmanagedType.I1)]
        public bool FillUp2;
        public short FillUp3; 
        public HLI_FB_ZEITANGABE TimeStamp; 
        public HLI_INTF_VERSION_NAME VersionName;
        public int FillUp4; //Undocumented, found through trial and error.
        public int FillUp5; //Undocumented, found through trial and error.
        public ushort RectificationType;
        public ushort ReactionType;
        public uint BodyType; 
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_MODUL_NAME
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.HLI_MODUL_NAME_LAENGE + 1)]
        public string Name;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_FB_ZEITANGABE
    {
        public uint DateCounter;
        public uint CycleCounter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_INTF_VERSION_NAME
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.HLI_MODUL_NAME_LAENGE + 1)]
        public string Name;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_SATZ_RUMPF
    {
        public HLI_ERROR_MASKE Mask;
        public HLI_WERT_B Value1;
        public HLI_WERT_B Value2;
        public HLI_WERT_B Value3;
        public HLI_WERT_B Value4;
        public HLI_WERT_B Value5;
        public HLI_WERT Identifier1;
        public HLI_WERT Identifier2;
        public HLI_WERT Identifier3;
        public HLI_WERT Identifier4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_ERROR_MASKE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.HLI_ERR_MASK_MAXIDX)]
        public byte[] ErrorMask;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT_B
    {
        public uint Type;
        public uint Dimension;
        public uint Importance;
        public int FillUp1;
        public HLI_WERT_B_DATA Content;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT_B_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.HLI_WERT_B_DATA_MAXIDX)]
        public byte[] Data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT
    {
        public uint Type;
        public int FillUp1;
        public HLI_WERT_DATA Content;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HLI_WERT_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.HLI_WERT_DATA_MAXIDX)]
        public byte[] Data;
    }
}


