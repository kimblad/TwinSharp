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
}


