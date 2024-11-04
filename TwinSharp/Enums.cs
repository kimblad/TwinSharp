namespace TwinSharp
{

    public enum E_LicenseHResult : long
    {
        //success
        E_LHR_LicenseOK = 0,
        E_LHR_LicenseOK_Pending = 0x203,
        E_LHR_LicenseOK_Demo = 0x254,
        E_LHR_LicenseOK_OEM = 0x255,
        //error
        E_LHR_LicenseNoFound = (0x98110700 + 0x24),
        E_LHR_LicenseExpired = (0x98110700 + 0x25),
        E_LHR_LicenseExceeded = (0x98110700 + 0x26),
        E_LHR_LicenseInvalid = (0x98110700 + 0x27),
        E_LHR_LicenseSystemIdInvalid = (0x98110700 + 0x28),
        E_LHR_LicenseNoTimeLimit = (0x98110700 + 0x29),
        E_LHR_LicenseTimeInFuture = (0x98110700 + 0x2A),
        E_LHR_LicenseTimePeriodToLong = (0x98110700 + 0x2B),
        E_LHR_DeviceException = (0x98110700 + 0x2C),
        E_LHR_LicenseDuplicated = (0x98110700 + 0x2D),
        E_LHR_SignatureInvalid = (0x98110700 + 0x2E),
        E_LHR_CertificateInvalid = (0x98110700 + 0x2F),
        E_LHR_LicenseOemNotFound = (0x98110700 + 0x30),
        E_LHR_LicenseRestricted = (0x98110700 + 0x31),
        E_LHR_LicenseDemoDenied = (0x98110700 + 0x32),
        E_LHR_LicensePlatformLevelInv = (0x98110700 + 0x33)
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
        ModuloStartPositiveDirection = 517,
        ModuloStartNegativeDirection = 773,
        StopAndLock = 4096,
        Halt = 8192,
    }

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

    public enum FileOpenModeFlags
    {
        FOPEN_MODEREAD = 0x1,       // "r": Opens for reading. If the file does not exist or cannot be found, the call fails. 
        FOPEN_MODEWRITE = 0x2,      // "w": Opens an empty file for writing. If the given file exists, its contents are destroyed. 
        FOPEN_MODEAPPEND = 0x4,     // "a": Opens for writing at the end of the file (appending) without removing the EOF marker before writing new data to the file; creates the file first if it doesnot exist. 
        FOPEN_MODEPLUS = 0x8,       // "+": Opens for reading and writing 
        FOPEN_MODEBINARY = 0x10,    // "b": Open in binary (untranslated) mode. 
        FOPEN_MODETEXT = 0x20,      // "t": Open in text (translated) mode.     }
    }
}
