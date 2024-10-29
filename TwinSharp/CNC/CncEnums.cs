namespace TwinSharp.CNC
{
    public enum SkipModes : uint
    {
        Off = 0x0,
        SkipLevel1 = 0x1,
        SkipLevel2 = 0x2,
        SkipLevel3 = 0x4,
        SkipLevel4 = 0x8,
        SkipLevel5 = 0x10,
        SkipLevel6 = 0x20,
        SkipLevel7 = 0x40,
        SkipLevel8 = 0x80,
        SkipLevel9 = 0x100,
        SkipLevel10 = 0x200,
    }

    public enum ChannelModes
    {
        ISG_STANDARD = 0x0000, // Normal mode
        SOLLKON_BlockSearch = 0x0001, // Block search
        SOLLKON_NominalContourVisualisation = 0x0002, // Nominal contour visualisation simulation with output of visualisation data
        SOLLKON_SuppressOutput = 0x0802, // Nominal contour visualisation simulation without output of visualisation data
        ON_LINE = 0x0004, // Online visualisation simulation
        SYNCHK = 0x0008, // Syntax check simulation
        PROD_TIME = 0x0010, // Simulation machining time calculation (in TwinCAT without function)
        ONLINE_PROD_TIME = 0x0020, // Simulation online machining time calculation
        MACHINE_LOCK = 0x0040, // Dry run without axis motion
        ADD_MDI_BLOCK = 0x0080, // Extended manual block mode: the end of a manual block is not evaluated as a program end. It permits the commanding of further manual blocks.
        KIN_TRAFO_OFF = 0x0100, // Overwrites automatic enable for kinematic transformations by a characteristic parameter defined in the channel parameters (sda_mds*.lis).
        BEARB_MODE_SCENE = 0x1000, // When SCENE mode is enabled, the output of #SCENE commands is activated on the interface (see also [FCT-C17// Scene contour visualisation]). An additional client is linked to this output via DataFactory / CORBA.
        SUPPRESS_TECHNO_OUTPUT = 0x2000, // Without output of technology functions (M/H/T). Set implicitly in connection with syntax check.
    }

    public enum OperationMode
    {
        STANDBY_MODE = 1,
        AUTOMATIC_MODE = 2,
        MDI_MODE = 3,
        MANUAL_MODE = 4,
        REFERENCE_MODE = 5
    }

    public enum OperationState
    {
        NoSignificance = 0,
        PROCESS_DESELECTED = 1,
        PROCESS_SELECTED = 2,
        PROCESS_READY = 3,
        PROCESS_ACTIVE = 4,
        PROCESS_HOLD = 5,
        PROCESS_ERROR = 6
    }

    public enum AxisState : uint
    {
        HLI_AXIS_READY = 1, //The axis is ready and moves according to the specified command values after a command.
        HLI_AXIS_ACTIVE = 3, //The axis is currently moved by the CNC due to an NC command or manual mode.
        HLI_AXIS_HOLD = 5, //The CNC cannot move the axis because an external signal is set, such as feedhold or tracking mode, or the required drive enables are missing.
        HLI_AXIS_ERROR = 7 //After an error (in the drive or CNC, e.g. a software limit switch violation) the axis is in error state. Commanding a new motion is only possible after a CNC reset.
    }
}
