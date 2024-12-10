namespace TwinSharp.CNC
{

    /// <summary>
    /// Enumeration that describes the skip mode at interpreter level. Skip levels active simultaneously are enabled by bitwise ORing.
    /// </summary>
    public enum SkipModes : uint
    {
        /// <summary> Skip mode NC block OFF. </summary>
        Off = 0x0,
        
        /// <summary> Skip level 1 </summary>
        SkipLevel1 = 0x1,

        /// <summary> Skip level 2 </summary>
        SkipLevel2 = 0x2,

        /// <summary> Skip level 3 </summary>
        SkipLevel3 = 0x4,

        /// <summary> Skip level 4 </summary>
        SkipLevel4 = 0x8,

        /// <summary> Skip level 5 </summary>
        SkipLevel5 = 0x10,

        /// <summary> Skip level 6 </summary>
        SkipLevel6 = 0x20,

        /// <summary> Skip level 7 </summary>
        SkipLevel7 = 0x40,

        /// <summary> Skip level 8 </summary>
        SkipLevel8 = 0x80,

        /// <summary> Skip level 9 </summary>
        SkipLevel9 = 0x100,

        /// <summary> Skip level 10 </summary>
        SkipLevel10 = 0x200,
    }

    /// <summary>
    /// Enum for selection of a special channel mode such as syntax check or machining time calculation
    /// </summary>
    public enum ChannelMode
    {
        ///<summary> Normal mode </summary>
        ISG_STANDARD = 0x0000,

        ///<summary> Block search </summary>
        SOLLKON_BlockSearch = 0x0001,
        
        ///<summary> Nominal contour visualisation simulation with output of visualisation data </summary>
        SOLLKON_NominalContourVisualisation = 0x0002,
        
        ///<summary> Nominal contour visualisation simulation without output of visualisation data </summary>
        SOLLKON_SuppressOutput = 0x0802,
        
        ///<summary> Online visualisation simulation </summary>
        ON_LINE = 0x0004,
        
        ///<summary> Syntax check simulation </summary>
        SYNCHK = 0x0008,
        
        ///<summary> Simulation machining time calculation (in TwinCAT without function) </summary>
        PROD_TIME = 0x0010,
        
        ///<summary> Simulation online machining time calculation </summary>
        ONLINE_PROD_TIME = 0x0020,
        
        ///<summary> Dry run without axis motion </summary>
        MACHINE_LOCK = 0x0040,
        
        ///<summary> Extended manual block mode: the end of a manual block is not evaluated as a program end. It permits the commanding of further manual blocks. </summary>
        ADD_MDI_BLOCK = 0x0080,
        
        ///<summary> Overwrites automatic enable for kinematic transformations by a characteristic parameter defined in the channel parameters (sda_mds*.lis). </summary>
        KIN_TRAFO_OFF = 0x0100,
        
        ///<summary> When SCENE mode is enabled, the output of #SCENE commands is activated on the interface (see also [FCT-C17// Scene contour visualisation]). An additional client is linked to this output via DataFactory / CORBA. </summary>
        BEARB_MODE_SCENE = 0x1000,
        
        ///<summary> Without output of technology functions (M/H/T). Set implicitly in connection with syntax check. </summary>
        SUPPRESS_TECHNO_OUTPUT = 0x2000,
    }


    /// <summary>
    /// The CNC distinguishes between five operating modes. It is possible to switch over between these operating modes via the operator-control and/or PLC interface, whereby only one operating mode may be active at any one time.
    /// This enumeration lists the operating modes that are defined.
    /// </summary>
    public enum OperationMode
    {
        /// <summary> No operating mode is selected. Default after starting the control. </summary>
        STANDBY_MODE = 1,

        /// <summary> The control can run a complete NC program automatically. In this case, program execution can be interrupted and resumed. </summary>
        AUTOMATIC_MODE = 2,

        /// <summary> Movements are commanded by the operator-control computer via a single NC block. The NC block is transferred as a string to the control and executed via a START command. Interruption and resumption of movement are possible in this case. </summary>
        MDI_MODE = 3,

        /// <summary> Movements are commanded by peripherals connected directly to the control (keys, handwheels). </summary>
        MANUAL_MODE = 4,

        /// <summary> The axes can be referenced. An NC program of name rpf.nc is started in this case. </summary>
        REFERENCE_MODE = 5
    }


    /// <summary>
    /// Enumeration of the possible states of an operation mode. Depending on the actually selected operation mode, these states may contain a further meaning.
    /// </summary>
    public enum OperationState
    {
        /// <summary>  </summary>
        NoSignificance = 0,
        
        /// <summary> Operation mode is deselected. </summary>
        PROCESS_DESELECTED = 1,
        
        /// <summary> Operation mode is selected. </summary>
        PROCESS_SELECTED = 2,
        
        /// <summary> NC/Program is selected, or MDI block is selected, or manual mode is programmed or homing is programmed. </summary>
        PROCESS_READY = 3,
        
        /// <summary> NC program is running, or the MDI NC block(s) are running, or manual mode is running or homing is running. </summary>
        PROCESS_ACTIVE = 4,
        
        /// <summary> NC program is interrupted, or the MDI NC block(s) are stopped, or manual mode is stopped, or homing is stopped. </summary>
        PROCESS_HOLD = 5,

        /// <summary> An error occurred while the NC program is executed, or error state for other modes. </summary>
        PROCESS_ERROR = 6       
    }


    /// <summary>
    /// Enumeration that lists the possible states of an axis.
    /// </summary>
    public enum AxisState : uint
    {
        /// <summary>
        /// The axis is ready and moves according to the specified command values after a command.
        /// </summary>
        HLI_AXIS_READY = 1,

        /// <summary>
        /// The axis is currently moved by the CNC due to an NC command or manual mode.
        /// </summary>
        HLI_AXIS_ACTIVE = 3,

        /// <summary>
        /// The CNC cannot move the axis because an external signal is set, such as feedhold or tracking mode, or the required drive enables are missing.
        /// </summary>
        HLI_AXIS_HOLD = 5,

        /// <summary>
        /// After an error (in the drive or CNC, e.g. a software limit switch violation) the axis is in error state. Commanding a new motion is only possible after a CNC reset.
        /// </summary>
        HLI_AXIS_ERROR = 7 
    }

    /// <summary>
    /// Enum for the selection of a technology function (M/H/S/T)
    /// </summary>
    public enum TechnologyFunction : ushort
    {
        ///<summary> M-Function </summary>
        HLI_INTF_M_FKT = 1,

        ///<summary> H-Function </summary>
        HLI_INTF_H_FKT = 2,

        ///<summary> S-Function (spindle) </summary>
        HLI_INTF_SPINDEL = 3,

        ///<summary> T-Function (tool) </summary>
        HLI_INTF_TOOL = 4,
    }
}
