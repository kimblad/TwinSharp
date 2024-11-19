namespace TwinSharp.NC
{
    public enum EndPositionType
    {
        NOT_DEFINED = 0,
        AbsolutePosition = 1,
        RelativePosition = 2,
        ContinousPositionPositive = 3,
        ContinousPositionNegative = 4,
        ModuloPosition = 5,
    }

    public enum ActualPositionType
    {
        NOT_DEFINED = 0,
        AbsolutePosition = 1,
        RelativePosition = 2,
        ModuloPosition = 5,
    }

    public enum DriveOutputStartType
    {
        NOT_DEFINED = 0,
        Percent = 1,
        Velocity = 2,
    }

    public enum AxisType
    {
        NOT_DEFINED = 0,
        ContinousAxisServo = 1,
        DiscreteAxisHighLow = 2,
        ContinousAxisStepper = 3,
        EncoderAxis = 5,
        ContinousAxisPositionPressureControl = 6,
        TimeBaseGenerator = 7,
    }

    public enum ErrorReactionMode
    {
        Instantaneous = 0, //default
        Delayed = 1, //e.g. for Master/Slave-coupling
    }

    public enum ChannelType
    {
        Standard = 1,
        Interpreter = 2,
        FIFO = 3,
        KinematicTransformation = 4
    }

    public enum GroupType
    {
        NOT_DEFINED = 0,
        PTPGroup = 1,
        Group1D = 2,
        Group2D = 3,
        Group3D = 4,
        HighLowSpeed = 5,
        LowCostStepperMotor = 6,
        TableGroup = 7,
        EncoderGroup = 9,
        FIFOGroup = 11,
        KinematicTransformationGroup = 12,
    }

    public enum DriveType
    {
        NOT_DEFINED = 0,
        M2400_DAC1 = 1,
        M2400_DAC2 = 2,
        M2400_DAC3 = 3,
        M2400_DAC4 = 4,
        KL4xxx = 5,
        KL4xxx_NonLinear = 6,
        Discete_TwoSpeed = 7,
        Stepper = 8,
        Sercos = 9,
        KL5051 = 10,
        AX2000_B200 = 11,
        ProfilDrive = 12,
        Universal = 13,
        NcBackplane = 14,
        CANopen_Lenze = 15,
        CANopen_DS402_MDP742 = 16,
        AX2000_B900 = 17,
        KL2531_Stepper = 20,
        KL2532_DC = 21,
        TCOM = 22,
        MDP_733 = 23,
        MDP_703 = 24
    }

    public enum EncoderType
    {
        NOT_DEFINED,
        Simulation,
        ABS_M3000,
        INC_M31X0,
        INC_KL5101,
        ABS_KL5001_SSI,
        INC_KL5051,
        ABS_KL30XX,
        INC_Sercos_P,
        INC_Sercos_PV,
        INC_Binary,
        ABS_M2510,
        ABS_FOX50,
        HYDRAULIC_FORCE,
        AX2000_B200,
        PROFIDRIVE,
        UNIVERSAL,
        NCBACKPLANE,
        CANOPEN_LENZE,
        CANOPEN_DS402_MDP513_MDP742,
        AX2000_B900,
        KL5151,
        IP5209,
        KL2531_Stepper,
        KL2532_DC,
        TIMEBASEGENERATOR,
        INC_TCOM,
        CANOPEN_MDP513_64BIT,
        SPECIFIC
    }

    public enum ControllerType
    {
        NOT_DEFINED = 0,
        P_ControllerPosition = 1,
        PP_Controller = 2,
        PID_Controller = 3,
        P_ControllerVelocity = 5,
        PI_ControllerVelocity = 6,
        High_LowSpeedController = 7,
        StepperMotorController = 8,
        SercosController = 9,
        TComController = 14,
    }



    public enum SignalEdge
    {
        RisingEdge = 0,
        FallingEdge = 1
    }

    public enum ProbeMode
    {
        Single = 1,
        Continous = 2
    }

    public enum TableInterpolationType
    {
        Linear = 0, //Linear interpolation (NC_INTERPOLATIONTYPE_LINEAR) (Standard)
        FourPoint = 1, //4-point interpolation (NC_INTERPOLATIONTYPE_4POINT) (for equidistant table types only)
        CubicSpline = 2, //Cubic spline interpolation of all reference points ("global spline") (NC_INTERPOLATIONTYPE_SPLINE
        SlidingCubicSpline = 3, //Cubic spline interpolation of all reference points ("global spline") (NC_INTERPOLATIONTYPE_SPLINE
    }

    public enum TableSubType
    {
        EquidistantMasterNonCyclicContinuation = 1, //(n*m) Table with equidistant master positions and no cyclic continuation of the master profile (equidistant linear)
        EquidistantMasterCyclicContinuation = 2, //(n*m) Table with equidistant master positions and cyclic continuation of the master profile (equidistant cyclic)
        NonEquidistantMasterNonCyclicContinuation = 3, //(n*m) Table with non-equidistant, but strictly monotonously increasing master positions and a non-cyclic continuation of the master profile (monotonously linear)
        NonEquidistantMasterCyclicContinuation = 4, //(n*m) Table with non-equidistant, but strictly monotonously increasing master positions and a cyclic continuation of the master profile (monotonously cyclic)
    }

    public enum TableMainType
    {
        CamPlate = 1, //(n*m) Cam plate tables (Camming)
        CharacteristicCurve = 10, //(n*m) Characteristic curves tables (Characteristics) (e.g. hydraulic valve characteristic curves). Only non-cyclic table sub-types (1, 3) are supported!
        MotionFunction = 16, //(n*m) "Motion Function" tables (MF). Only non-equidistant table sub-types (3, 4) are supported!
    }

    public enum TableActivationMode
    {
        Instantaneous = 0, //default
        MasterCamPosition = 1,
        MasterAxisPosition = 2,
        NextCycle = 3,
        NextCycleOnce = 4,
        AsSoonAsPossible = 5,
        Off = 6,
        DeleteQueuedData = 7
    }

    public enum MasterScalingType
    {
        UserDefined = 0, //default
        WithAutoOffset = 1,
        Off = 2,
    }

    public enum SlaveScalingType
    {
        UserDefined = 0, //default
        WithAutoOffset = 1,
        Off = 2,
    }

    public enum GroupAxisStartType
    {
        NOT_DEFINED = 0,
        AbsoluteStart = 1,
        RelativeStart = 2,
        ContinousStartPositive = 3,
        ContinousStartNegative = 4,
        ModuloStartOLD = 5,
        ModuloStartShortestDistance = 261,
        JogPositiveSlow = 272, //Undocumented
        JogNegativeSlow = 273, //Undocumented
        ModuloStartPositiveDirection = 517,
        JogPositiveFast = 528, //Undocumented
        JogNegativeFast = 529, //Undocumented
        ModuloStartNegativeDirection = 773,
        StopAndLock = 4096,
        Halt = 8192,
    }

    [Flags]
    public enum StateDWordFlags : uint
    {
        None = 0,
        ReadyForOperation = 1,
        Homed = 2,
        NotMoving = 4,
        InPositionArea = 8,
        InTargetPosition = 16,
        Protected = 32,
        ErrorPropagationDelayed = 64,
        HasBeenStopped = 128,
        HasJob = 256,
        PositiveDirection = 512,
        NegativeDirection = 1024,
        HomingBusy = 2048,
        ConstantVelocity = 4096,
        Compensating = 8192,
        ExtSetPointGenEnabled = 16384,
        NotImplementedYet = 32768,
        ExternalLatchValid = 65536,
        NewTargetPosition = 131072,
        NotImplementedYet2 = 262144,
        ContinuousMotion = 524288,
        ControlLoopClosed = 1048576,
        CamTableQueued = 2097152,
        CamDataQueued = 4194304,
        CamScalingPending = 8388608,
        CmdBuffered = 16777216,
        PTPmode = 33554432,
        SoftLimitMinExceeded = 67108864,
        SoftLimitMaxExceeded = 134217728,
        DriveDeviceError = 268435456,
        MotionCommandsLocked = 536870912,
        IoDataInvalid = 1073741824,
        Error = 2147483648
    }

    [Flags]
    public enum ControlDWordFlags : uint
    {
        None = 0,
        PositionAreaMonitoring = 1,
        TargetPositionMonitoring = 2,
        LoopMode = 4,
        MotionMonitoring = 8,
        PEHTimeMonitoring = 16,
        BacklashCompensation = 32,
        DelayedErrorReaction = 64,
        ModuloPositioning = 128,
        SimulationAxis = 256,
        Unused1 = 512,
        Unused2 = 1024,
        Unused3 = 2048,
        StopMonitoring = 4096,
        Unused4 = 8192,
        Unused5 = 16384,
        Unused6 = 32768,
        PositionLagMonitoring = 65536,
        VeloLagMonitoring = 131072,
        SoftLimitMinMonitoring = 262144,
        SoftLimitMaxMonitoring = 524288,
        PositionCorrection = 1048576,
        AllowSlaveCommands = 2097152,
        AllowExtSetAxisCommands = 4194304,
        NcApplicationRequested = 8388608,
    }

    /// <summary>
    /// Coupling state of the axis
    /// </summary>
    public enum CoupleState
    {
        /// <summary>
        /// Single axis that is neither a master nor a slave (SINGLE)
        /// </summary>
        Single = 0,
        /// <summary>
        /// Master axis with any number of slaves (MASTER)
        /// </summary>
        Master = 1,
        /// <summary>
        /// Slave axis that is the master of another another slave (MASTERSLAVE)
        /// </summary>
        MasterSlave = 2,
        /// <summary>
        /// Just a slave axis (SLAVE)
        /// </summary>
        Slave = 3,
    }
}
