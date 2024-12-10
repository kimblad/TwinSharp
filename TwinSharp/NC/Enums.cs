namespace TwinSharp.NC
{
    /// <summary>
    /// Enumeration of the different types of end position types (new end position) that exist in TwinCAT NC.
    /// </summary>
    public enum EndPositionType
    {
        /// <summary> End position not defined. </summary>
        NOT_DEFINED = 0,

        /// <summary> Absolute position </summary>
        AbsolutePosition = 1,

        /// <summary> Relative position </summary>
        RelativePosition = 2,

        /// <summary> Continuous position positive </summary>
        ContinousPositionPositive = 3,

        /// <summary> Continuous position negative </summary>
        ContinousPositionNegative = 4,

        /// <summary> Modulo position </summary>
        ModuloPosition = 5,
    }


    /// <summary>
    /// Enumeration of the different types of actual position that exist in TwinCAT NC.
    /// </summary>
    public enum ActualPositionType
    {
        /// <summary> Actual position type not defined. </summary>
        NOT_DEFINED = 0,
        
        /// <summary> Absolute position </summary>
        AbsolutePosition = 1,

        /// <summary> Relative position </summary>
        RelativePosition = 2,

        /// <summary> Modulo position </summary>
        ModuloPosition = 5,
    }

    /// <summary>
    /// Enumeration of the different types of drive output start that exist in TwinCAT NC.
    /// </summary>
    public enum DriveOutputStartType
    {
        /// <summary> Drive start type not defined. </summary>
        NOT_DEFINED = 0,
        
        /// <summary> Output in percent. </summary>
        Percent = 1,

        /// <summary> Output in velocity. </summary>
        Velocity = 2,
    }

    /// <summary>
    /// TwinCAT supports different axis types, which are defined in the enum AxisType.
    /// </summary>
    public enum AxisType
    {
        /// <summary>
        /// Not defined
        /// </summary>
        NOT_DEFINED = 0,

        /// <summary>
        /// Continuous axis (also SERCOS)
        /// </summary>
        ContinousAxisServo = 1,

        /// <summary>
        /// Discrete axis (high/low speed)
        /// </summary>
        DiscreteAxisHighLow = 2,

        /// <summary>
        /// Stepper motor axis (without PWM terminal KL2502/30 and without pulse train KL2521)
        /// </summary>
        ContinousAxisStepper = 3,

        /// <summary>
        /// Encoder axis
        /// </summary>
        EncoderAxis = 5,

        /// <summary>
        /// Continuous axis with operation mode switching for position/pressure control
        /// </summary>
        Hydraulic = 6,

        /// <summary>
        /// Time Base Generator
        /// </summary>
        TimeBaseGenerator = 7,

        /// <summary>
        /// Specific.
        /// </summary>
        Specific = 100
    }

    /// <summary>
    /// Enumeration of the possible error reaction modes that exist in TwinCAT NC.
    /// </summary>
    public enum ErrorReactionMode
    {
        /// <summary> Immediate, default. </summary>
        Instantaneous = 0,

        /// <summary> Delayed. E.g. for master/slave coupling.  </summary>
        Delayed = 1,
    }

    /// <summary>
    /// Enumeration of the different types of channels that exists in TwinCAT NC.
    /// </summary>
    public enum ChannelType
    {
        /// <summary> Standard channel. </summary>
        Standard = 1,
        
        /// <summary> Interpreter channel. </summary>
        Interpreter = 2,
        
        /// <summary> FIFO channel. </summary>
        FIFO = 3,

        /// <summary> Kinematic transformation channel. </summary>
        KinematicTransformation = 4
    }


    /// <summary>
    /// Enumeration of the different types of interpreters that exist in TwinCAT NC.
    /// </summary>
    public enum InterpreterType
    {
        /// <summary> Interpreter type not defined. </summary>
        NotDefined = 0,

        /// <summary> NC Interpreter DIN 66025 (GST). </summary>
        NCInterpreterDIN66025GST = 1,

        /// <summary> NC Interpreter DIN 66025 (Classic Dialect). </summary>
        NCInterpreterDIN66025ClassicDialect = 2
    }

    /// <summary>
    /// Enumeration of the different types of interpolation load log modes that exist in TwinCAT NC.
    /// </summary>
    public enum InterpolationLoadLogMode
    {
        /// <summary> Loader log off. </summary>
        LoaderLogOff = 0,

        /// <summary> Source only. </summary>
        SourceOnly = 1,

        /// <summary> Source and Compiled. </summary>
        SourceAndCompiled = 2
    }

    /// <summary>
    /// Enumeration of the different types of interpolation trace modes that exist in TwinCAT NC.
    /// </summary>
    public enum InterpolationTraceMode
    {
        /// <summary> Trace off </summary>
        TraceOff = 0,

        /// <summary> Trace line numbers </summary>
        TraceLineNumbers = 1,

        /// <summary> Trace Source </summary>
        TraceSource = 2,
    }


    /// <summary>
    /// Enumeration of all possible states of the interpreter in TwinCAT NC. The interpreter state reflects the current state of the Interpreter State Machine. The complete list is given below.
    /// </summary>
    public enum InterpreterState
    {
        /// <summary> Initilization of interpreter failed. </summary>
        ITP_STATE_INITFAILED = 0,

        /// <summary>
        /// The interpreter is in idle state if no NC program is loaded yet or if a group reset is executed. The interpreter also goes into idle state when a current program is stopped. In the case a group reset must be executed in order to prevent error 0x42C5. It is therefore recommended to execute a group reset after stopping via the PLC.
        /// </summary>
        ITP_STATE_IDLE = 1,

        /// <summary>
        /// After successful loading of an NC program, the interpreter is in ready state. After a program has been successfully processed and exited, the interpreter goes into ready state. In the meantime, however, other states are accepted. 
        /// </summary>
        ITP_STATE_READY = 2,

        /// <summary> Interpreter started. </summary>
        ITP_STATE_STARTED = 3,

        /// <summary> Interpreter scanning. </summary>
        ITP_STATE_SCANNING = 4,

        /// <summary> Interpreter running. </summary>
        ITP_STATE_RUNNING = 5,

        /// <summary> Interpreter stay running. </summary>
        ITP_STATE_STAY_RUNNING = 6,

        /// <summary> Interpereter writing table. </summary>
        ITP_STATE_WRITETABLE = 7,

        /// <summary> Interpreter block searching. </summary>
        ITP_STATE_SEARCHLINE = 8,

        /// <summary> Interpreter reached end. </summary>
        ITP_STATE_END = 9,

        /// <summary>
        /// This state is only accepted in Single Block Mode. As soon as the entry has been sent from the interpreter to the NC core, the interpreter goes into this mode.
        /// </summary>
        ITP_STATE_SINGLESTOP = 10,

        /// <summary> Interpreter aborting. </summary>
        ITP_STATE_ABORTING = 11,

        /// <summary>
        /// If a runtime error occurs during the processing of an NC program, the interpreter goes into aborted state. The actual error code is given in Channel State.
        /// </summary>
        ITP_STATE_ABORTED = 12,

        /// <summary> Interpreter has a fault. </summary>
        ITP_STATE_FAULT = 13,

        /// <summary> Interpreter is reset. </summary>
        ITP_STATE_RESET = 14,

        /// <summary> Interpreter is stopped. </summary>
        ITP_STATE_STOP = 15,

        /// <summary> Interpreter is waiting for acknowledgement of M-functtion. </summary>
        ITP_STATE_WAITFUNC = 16,

        /// <summary> Interpeter is flushing buffers. </summary>
        ITP_STATE_FLUSHBUFFERS = 17,
    }


    /// <summary>
    /// Enumeration of the different modes that the TwinCAT NC interpreter can operate in. 
    /// </summary>
    public enum InterpreterOperationMode
    {
        /// <summary> Default (deactivates the other modes) </summary>
        Default = 0x0,

        /// <summary> Single block mode in the NC core (Block execution task/SAF </summary>
        SingleBlockNC = 0x1,
        
        /// <summary> Single block mode in the interpreter </summary>
        SingleBlockInterpreter = 0x4000,
    }

    /// <summary>
    /// Enumeration of all possible group types in TwinCAT NC.
    /// </summary>
    public enum GroupType
    {
        /// <summary> Group not defined. </summary>
        NOT_DEFINED = 0,

        /// <summary> PTP-Group + x Slave </summary>
        PTPGroup = 1,

        /// <summary> 1D-Group + x Slave </summary>
        Group1D = 2,

        /// <summary> 2D-Group + x Slave </summary>
        Group2D = 3,

        /// <summary> 3D-Group + x Slave </summary>
        Group3D = 4,

        /// <summary> High/low speed + x Slave </summary>
        HighLowSpeed = 5,

        /// <summary> Low cost stepper motor (dig. IO) + x Slave </summary>
        LowCostStepperMotor = 6,

        /// <summary> Table Group + x Slave </summary>
        TableGroup = 7,

        /// <summary> Encoder Group + x Slave </summary>
        EncoderGroup = 9,

        /// <summary> FIFO Group + x Slave </summary>
        FIFOGroup = 11,

        /// <summary> Kinematic Transformation Group + x Slave </summary>
        KinematicTransformationGroup = 12,
    }


    /// <summary>
    /// Enumeration of the different ways that the TwinCAT NC FIFO can interpolate between supplied points.
    /// </summary>
    public enum FifoInterpolationType
    { 
        /// <summary> Linear interpolation between points. </summary>
        INTERPOLATIONTYPE_LINEAR = 0,
        
        /// <summary> 4 point interpolation. </summary>
        INTERPOLATIONTYPE_4POINT = 1,

        /// <summary> Cubic spline interpolation. </summary>
        INTERPOLATIONTYPE_CUBICSPLINE = 4
    }

    /// <summary>
    /// Determines the behaviour how quickly the FIFO should adapt to a new override.
    /// </summary>
    public enum FifoOverrideType
    {
        /// <summary> Instantenous override. Changes the override instantaneously to the desired value. This means that it is up to the user to modify the override slowly enough that excessive following errors do not result from the change in override. </summary>
        OVERRIDETYPE_INSTANTANEOUS = 1,

        /// <summary> Gradually adapt to the new override value, using the parameter P-T2 time. </summary>
        OVERRIDETYPE_PT2 = 2
    }

    /// <summary>
    /// TwinCAT supports different drives, these are defined in the enum DriveType.
    /// </summary>
    public enum NcDriveType
    {
        /// <summary> Not defined. </summary>
        NOT_DEFINED = 0,

        /// <summary> Analog Servo Drive: M2400 DAC 1. </summary>
        M2400_DAC1 = 1,

        /// <summary> Analog Servo Drive: M2400 DAC 2. </summary>
        M2400_DAC2 = 2,

        /// <summary> Analog Servo Drive: M2400 DAC 3. </summary>
        M2400_DAC3 = 3,

        /// <summary> Analog Servo Drive: M2400 DAC 4. </summary>
        M2400_DAC4 = 4,

        /// <summary> MDP 252/253: KL4xxx, PWM KL2502_30K (Frq-Cnt pulse mode), KL4132 (16-bit), Pulse-Train KL2521, IP2512- </summary>
        KL4xxx = 5,

        /// <summary> MDP 252/253: analog type for non-linear characteristics. </summary>
        KL4xxx_NonLinear = 6,

        /// <summary> High/low speed drive (digital) </summary>
        Discete_TwoSpeed = 7,

        /// <summary> Stepper motot drive (digital) </summary>
        Stepper = 8,

        /// <summary> SERCOS-Drive (digital) </summary>
        Sercos = 9,

        /// <summary>
        /// MDP 510: BISSI Drive KL5051 with 32 bits (see KL4xxx)
        /// </summary>
        KL5051 = 10,

        /// <summary>
        /// AX2000-B200 Lightbus, incremental with 32 bits (AX2000)
        /// </summary>
        AX2000_B200 = 11,

        /// <summary>
        /// Incremental with 32 Bit
        /// </summary>
        ProfilDrive = 12,

        /// <summary>
        /// Variable bit mask (max. 32 bits, signed value)
        /// </summary>
        Universal = 13,

        /// <summary>
        /// Variable bit mask (max. 32 bits, signed value)
        /// </summary>
        NcBackplane = 14,

        /// <summary>
        /// CANopen Lenze (max. 32 bits, signed value)
        /// </summary>
        CANopen_Lenze = 15,

        /// <summary>
        /// MDP 742 (DS402): CANopen and EtherCAT (AX2000-B510, AX2000-B1x0, EL7201, AX8000)
        /// </summary>
        CANopen_DS402_MDP742 = 16,

        /// <summary>
        /// AX2000-B900 Ethernet (max. 32 bits, signed value)
        /// </summary>
        AX2000_B900 = 17,

        /// <summary>
        /// Stepper motor terminal KL2531/KL2541
        /// </summary>
        KL2531_Stepper = 20,

        /// <summary>
        /// 2-channel DC motor stage KL2532/KL2542, 2-channel PWM DC motor stage KL2535/KL2545
        /// </summary>
        KL2532_DC = 21,

        /// <summary>
        /// TCOM Drive -> Interface to Soft Drive
        /// </summary>
        TCOM = 22,

        /// <summary>
        /// MDP 733: Modular Device Profile MDP 733 for DC (e.g. EL7332/EL7342)
        /// </summary>
        MDP_733 = 23,

        /// <summary>
        /// MDP 703: Modular Device Profile MDP 703 for stepper (e.g. EL7031/EL7041)
        /// </summary>
        MDP_703 = 24
    }


    /// <summary>
    /// Enumeration of all possible encoder types in TwinCAT NC.
    /// </summary>
    public enum EncoderType
    {
        /// <summary>
        /// Not defined
        /// </summary>
        NOT_DEFINED,

        /// <summary>
        /// Simulation encoder
        /// </summary>
        Simulation,

        /// <summary>
        /// Absolute, with 24 or 25 bits, and 12 and 13 bits single turn encoders (M3000)
        /// </summary>
        ABS_M3000,

        /// <summary>
        /// Incremental, with 24 bits (M31x0, M3200, M3100, M2000)
        /// </summary>
        INC_M31X0,

        /// <summary>
        /// MDP 511: Incremental with 16 bits and latch (MDP511: EL7041, EL5101, EL5151, EL2521, EL5021(SinCos); KL5101, IP5109, KL5111)
        /// </summary>
        INC_KL5101,

        /// <summary>
        /// MDP 500/501: Absolute SSI with 24 bits (KL5001, IP5009) (MDP 501: EL5001)
        /// </summary>
        ABS_KL5001_SSI,

        /// <summary>
        /// MDP 510: Absolute/Incremental BISSI with 16 bits (KL5051, PWM KL2502_30K(Frq-Cnt pulse mode), Pulse-Train KL2521, IP2512)
        /// </summary>
        INC_KL5051,

        /// <summary>
        /// Absolute analog input with 16 bits (KL30xx)
        /// </summary>
        ABS_KL30XX,

        /// <summary>
        /// SERCOS "Encoder" position
        /// </summary>
        INC_Sercos_P,

        /// <summary>
        /// SERCOS "Encoder" position and velocity
        /// </summary>
        INC_Sercos_PV,

        /// <summary>
        /// Binary incremental encoders (0/1)
        /// </summary>
        INC_Binary,

        /// <summary>
        /// Absolute analog input with 12 bits (M2510)
        /// </summary>
        ABS_M2510,

        /// <summary>
        /// T and R Fox 50 module (24 bits absolute (SSI))
        /// </summary>
        ABS_FOX50,

        /// <summary>
        /// MMW type: force acquisition from Pa, Pb, Aa, Ab
        /// </summary>
        HYDRAULIC_FORCE,

        /// <summary>
        /// Incremental AX2000-B200 Lightbus with 16/20 bits (AX2000)
        /// </summary>
        AX2000_B200,

        /// <summary>
        /// Incremental with 32 Bit
        /// </summary>
        PROFIDRIVE,

        /// <summary>
        /// Incremental with variable bit mask (max. 32 bits)
        /// </summary>
        UNIVERSAL,

        /// <summary>
        /// Incremental NC backplane
        /// </summary>
        NCBACKPLANE,

        /// <summary>
        /// Incremental CANopen Lenze
        /// </summary>
        CANOPEN_LENZE,

        /// <summary>
        /// MDP 513 / MDP 742 (DS402): CANopen and EtherCAT (AX2000-B510, AX2000-B1x0, EL7201, EL5032/32 Bit)
        /// </summary>
        CANOPEN_DS402_MDP513_MDP742,

        /// <summary>
        /// Incremental AX2000-B900 Ethernet
        /// </summary>
        AX2000_B900,

        /// <summary>
        /// Incremental with 16-bit counter and int. + ext 32-bit latch (KL5151_0000) (only switchable), the 2-channel KL5151_0050 has no latch.
        /// </summary>
        KL5151,

        /// <summary>
        /// Incremental with 16-bit counter and int. 32-bit latch (IP5209)
        /// </summary>
        IP5209,

        /// <summary>
        /// Incremental with 16-bit counter and int. + ext. 15-bit latch (only switchable) (stepper motor terminal KL2531/KL2541)
        /// </summary>
        KL2531_Stepper,

        /// <summary>
        /// Incremental with 16-bit counter and ext. 16-bit latch (only switchable) (2-channel DC motor output stage KL2532/KL2542), 2-channel PWM DC motor output stage KL2535/KL2545
        /// </summary>
        KL2532_DC,

        /// <summary>
        /// Time Base Generator
        /// </summary>
        TIMEBASEGENERATOR,

        /// <summary>
        /// TCOM Encoder -> Interface to Soft Drive Encoder
        /// </summary>
        INC_TCOM,

        /// <summary>
        /// MDP 513 (DS402, EnDat2.2, 64 Bit): EL5032/64 Bit
        /// </summary>
        CANOPEN_MDP513_64BIT,

        /// <summary>
        /// Specific
        /// </summary>
        SPECIFIC
    }

    /// <summary>
    /// Enumeration of the different types of controllers that exist in TwinCAT NC.
    /// </summary>
    public enum ControllerType
    {
        /// <summary> Controller not defined. </summary>
        NOT_DEFINED = 0,

        /// <summary> P-controller (standard) (position). </summary>
        P_ControllerPosition = 1,

        /// <summary> PP Controller (with ka) (position). </summary>
        PP_Controller = 2,

        /// <summary> PID Controller (with ka) (position). </summary>
        PID_Controller = 3,

        /// <summary> P Controller (velocity). </summary>
        P_ControllerVelocity = 5,

        /// <summary> PI Controller (velocity). </summary>
        PI_ControllerVelocity = 6,

        /// <summary> High/low speed controller (position). </summary>
        High_LowSpeedController = 7,

        /// <summary> Stepper motor controller (position). </summary>
        StepperMotorController = 8,

        /// <summary> Sercos Controller (Position in the drive) </summary>
        SercosController = 9,

        /// <summary> TCom Controller (Soft Drive) (Position in the drive) </summary>
        TComController = 14,
    }


    /// <summary>
    /// Enumeration of the different types of encoder modes that exist in TwinCAT NC.
    /// </summary>
    public enum EncoderMode
    {
        /// <summary> Encoder mode not defined </summary>
        NotDefined = 0,

        /// <summary> Determination of position. </summary>
        Position = 1,

        /// <summary> Determination of position and velocity. </summary>
        PositionVelocity = 2,

        /// <summary> Determination of position, velocity and acceleration. </summary>
        PositionVelocityAcceleration = 3,
    }


    /// <summary>
    /// Specifies the mode of evaluation for encoder signals.
    /// </summary>
    public enum EncoderEvaluationDirection
    {
        /// <summary>
        /// Evaluation in positive and negative counting direction (default configuration, i.e. compatible with the previous state)
        /// </summary>
        PositiveAndNegative = 0,

        /// <summary>
        /// Evaluation only in positive counting direction
        /// </summary>
        Positive = 1,

        /// <summary>
        /// Evaluation only in negative counting direction
        /// </summary>
        Negative = 2,

        /// <summary>
        /// Evaluation neither in positive nor in negative counting direction (evaluation blocked)
        /// </summary>
        EvaluationBlocked = 3
    }


    /// <summary>
    /// Enumeration of the two types of signal edges that exist. Rising edge and falling edge.
    /// </summary>
    public enum SignalEdge
    {
        /// <summary> Rising edge. Signal goes from low to high. </summary>
        RisingEdge = 0,
        
        /// <summary> Falling edge. Signal goes from high to low. </summary>
        FallingEdge = 1
    }

    /// <summary>
    /// Enumeration of the different types of probe modes that exist in TwinCAT NC.
    /// </summary>
    public enum ProbeMode
    {
        /// <summary> Single probe mode. </summary>
        Single = 1,

        /// <summary> Continous probe mode. </summary>
        Continous = 2
    }

    /// <summary>
    /// Interpolation mode for position tables (cam plates). Position tables consist of a list of master and slave positions between which interpolation can take place in different ways.
    /// The interpolation type is not used for extended cam plates(motion functions).
    /// </summary>
    public enum TableInterpolationType
    {
        /// <summary> Linear interpolation (NC_INTERPOLATIONTYPE_LINEAR) (Standard) </summary>
        Linear = 0,

        /// <summary> 4-point interpolation (NC_INTERPOLATIONTYPE_4POINT) (for equidistant table types only) </summary>
        FourPoint = 1,

        /// <summary> Cubic spline interpolation of all reference points ("global spline") (NC_INTERPOLATIONTYPE_SPLINE) </summary>
        CubicSpline = 2,

        /// <summary> Moving cubic spline interpolation with n sampling points ('local spline') </summary>
        SlidingCubicSpline = 3,
    }


    /// <summary>
    /// Enumeration of the different types of table subtypes that exist in TwinCAT NC.
    /// </summary>
    public enum TableSubType
    {
        /// <summary> (n*m) Table with equidistant master positions and no cyclic continuation of the master profile (equidistant linear) </summary>
        EquidistantMasterNonCyclicContinuation = 1,

        /// <summary> (n*m) Table with equidistant master positions and cyclic continuation of the master profile (equidistant cyclic) </summary>
        EquidistantMasterCyclicContinuation = 2,

        /// <summary> (n*m) Table with non-equidistant, but strictly monotonously increasing master positions and a non-cyclic continuation of the master profile (monotonously linear) </summary>
        NonEquidistantMasterNonCyclicContinuation = 3,

        /// <summary> (n*m) Table with non-equidistant, but strictly monotonously increasing master positions and a cyclic continuation of the master profile (monotonously cyclic) </summary>
        NonEquidistantMasterCyclicContinuation = 4,
    }


    /// <summary>
    /// Enumeration of the different types of main tables that exist in TwinCAT NC.
    /// </summary>
    public enum TableMainType
    {
        /// <summary> (n*m) Cam plate tables (Camming) </summary>
        CamPlate = 1,

        /// <summary> (n*m) Characteristic curves tables (Characteristics) (e.g. hydraulic valve characteristic curves). Only non-cyclic table sub-types (1, 3) are supported! </summary>
        CharacteristicCurve = 10,

        /// <summary> (n*m) "Motion Function" tables (MF). Only non-equidistant table sub-types (3, 4) are supported! </summary>
        MotionFunction = 16,
    }


    /// <summary>
    /// Enumeration of the different types of table activation modes that exist in TwinCAT NC.
    /// </summary>
    public enum TableActivationMode
    {
        /// <summary> The change takes effect immediately. (default) </summary>
        Instantaneous = 0,

        /// <summary> The change takes effect at a certain cam plate position (master position within the cam plate). The command must be issued ahead of this position </summary>
        MasterCamPosition = 1,

        /// <summary> The change takes effect at a certain absolute position of the master axis. The command must be issued ahead of this position. </summary>
        MasterAxisPosition = 2,

        /// <summary> For a cyclic cam plate, the change takes effect at the transition to the next period. </summary>
        NextCycle = 3,

        /// <summary> Modify the data at the beginning of the next cam table cycle, activation is valid for one cycle only </summary>
        NextCycleOnce = 4,

        /// <summary> Modified cam plate data take effect as soon as system dynamics allow. </summary>
        AsSoonAsPossible = 5,

        /// <summary> Changes in cam plate data are ignored. </summary>
        Off = 6,

        /// <summary> Queued cam plate data are deleted. Data are queued if the change was requested at a certain master position or at the end of the cycle, for example. </summary>
        DeleteQueuedData = 7
    }

    /// <summary>
    /// Enumeration of type and scope of the scaling of a cam plate coupling.
    /// </summary>
    public enum CamScalingMode
    {
        /// <summary> User defines scaling parameters -scaling and -offset </summary>
        UserDefined = 0, //default

        /// <summary> Offset is calculated automatically for best result </summary>
        WithAutoOffset = 1,

        /// <summary> No modificaiton accepted. </summary>
        Off = 2,
    }


    /// <summary>
    /// Used by NC axis functions such as StandardAxisStart to define the start type of the axis.
    /// </summary>
    public enum GroupAxisStartType
    {
        /// <summary> Undefined. </summary>
        NOT_DEFINED = 0,
        
        /// <summary> Absolute start. </summary>
        AbsoluteStart = 1,
        
        /// <summary> Relative start. </summary>
        RelativeStart = 2,

        /// <summary> Continuous start positive. </summary>
        ContinousStartPositive = 3,

        /// <summary> Continuous start negative. </summary>
        ContinousStartNegative = 4,

        /// <summary> Modulo start (OLD). </summary>
        ModuloStartOLD = 5,

        /// <summary> Modulo start on the shortest distance. </summary>
        ModuloStartShortestDistance = 261,
        
        /// <summary> Jog positive slow. Undocumented. </summary>
        JogPositiveSlow = 272,
        
        /// <summary> Jog negative slow. Undocumented. </summary>
        JogNegativeSlow = 273,

        /// <summary> Modulo start in positive direction (with modulo tolerance window). </summary>
        ModuloStartPositiveDirection = 517,

        /// <summary> Jog positive fast. Undocumented. </summary>
        JogPositiveFast = 528, 
        
        /// <summary> Jog negative fast. Undocumented. </summary>
        JogNegativeFast = 529,

        /// <summary> Modulo start in negative direction (with modulo tolerance window). </summary>
        ModuloStartNegativeDirection = 773,

        /// <summary> Stop and lock (axis locked for motion commands). </summary>
        StopAndLock = 4096,

        /// <summary> Halt (without motion lock). </summary>
        Halt = 8192,
    }

    /// <summary>
    /// The StateDWord is a 32 bit data word in the axis interface NC->PLC.
    /// </summary>
    [Flags]
    public enum StateDWordFlags : uint
    {
        /// <summary> None. </summary>
        None = 0,

        /// <summary> Axis is ready for operation </summary>
        ReadyForOperation = 1,

        /// <summary> Axis has been referenced/ homed ("Axis calibrated") </summary>
        Homed = 2,

        /// <summary> Axis is logically stationary ("Axis not moving") </summary>
        NotMoving = 4,

        /// <summary> Axis is in position window (physical feedback) </summary>
        InPositionArea = 8,

        /// <summary> Axis is at target position (PEH) (physical feedback) </summary>
        InTargetPosition = 16,

        /// <summary> Axis is in a protected operating mode (e.g. as a slave axis) </summary>
        Protected = 32,

        /// <summary> Axis signals an error pre warning (from TC 2.11) </summary>
        ErrorPropagationDelayed = 64,

        /// <summary> Axis has been stopped or is presently executing a stop </summary>
        HasBeenStopped = 128,

        /// <summary> Axis has instructions, is carrying instructions out </summary>
        HasJob = 256,

        /// <summary> Axis moving to logically larger values </summary>
        PositiveDirection = 512,

        /// <summary> Axis moving to logically smaller values </summary>
        NegativeDirection = 1024,

        /// <summary> Axis referenced ("Axis being calibrated") </summary>
        HomingBusy = 2048,

        /// <summary> Axis has reached its constant velocity or rotary speed </summary>
        ConstantVelocity = 4096,

        /// <summary> Section compensation passive[0]/active[1] (s. "MC_MoveSuperImposed") </summary>
        Compensating = 8192,

        /// <summary> External setpoint generator enabled </summary>
        ExtSetPointGenEnabled = 16384,

        /// <summary> Operating mode not yet executed (Busy). Not implemented yet! </summary>
        NotImplementedYet = 32768,

        /// <summary> External latch value or sensing switch has become valid </summary>
        ExternalLatchValid = 65536,

        /// <summary> Axis has a new target position or a new velocity </summary>
        NewTargetPosition = 131072,

        /// <summary> Axis is not at target position or cannot reach the target position (e.g. stop).Not implemented yet! </summary>
        NotImplementedYet2 = 262144,

        /// <summary> Axis has target position (±) endless </summary>
        ContinuousMotion = 524288,

        /// <summary> Axis is ready for operation and axis control loop is closed (e.g. position control) </summary>
        ControlLoopClosed = 1048576,

        /// <summary> CAM table is queued for  "Online Change" and waiting for activation </summary>
        CamTableQueued = 2097152,

        /// <summary> CAM data (only MF) are queued for  "Online Change" and waiting for activation </summary>
        CamDataQueued = 4194304,

        /// <summary> CAM scaling are queued for  "Online Change" and waiting for activation </summary>
        CamScalingPending = 8388608,

        /// <summary> Following command is queued in then command buffer (s. Buffer Mode) </summary>
        CmdBuffered = 16777216,

        /// <summary> Axis in PTP mode (no slave, no NCI axis, no FIFO axis) (from TC 2.10 Build 1326) </summary>
        PTPmode = 33554432,

        /// <summary> Position software limit switch minimum is exceeded (from TC 2.10 Build 1327) </summary>
        SoftLimitMinExceeded = 67108864,

        /// <summary> Position software limit switch maximum is exceeded (from TC 2.10 Build 1327) </summary>
        SoftLimitMaxExceeded = 134217728,

        /// <summary> Hardware drive device error (no warning), interpretation only possible when drive is data exchanging, e.g. EtherCAT "OP"-state  </summary>
        DriveDeviceError = 268435456,

        /// <summary> Axis is locked for motion commands (TcMc2) </summary>
        MotionCommandsLocked = 536870912,

        /// <summary> IO data invalid (e.g. 'WcState' or 'CdlState') </summary>
        IoDataInvalid = 1073741824,

        /// <summary> Axis is in a fault state </summary>
        Error = 2147483648
    }

    /// <summary>
    /// The AxisControlDWord is a 32 bit data word in the axis interface PLC->NC.
    /// </summary>
    [Flags]
    public enum NCTOPLC_AXIS_REF_OPMODE : uint
    {
        ///<summary> No active bits set. </summary>
        None = 0,

        /// <summary> Position range monitoring. </summary>
        PositionAreaMonitoring = 1,

        /// <summary> Target position window monitoring. </summary>
        TargetPositionMonitoring = 2,

        /// <summary> Loop movement. </summary>
        LoopMode = 4,

        /// <summary> Physical movement monitoring. </summary>
        MotionMonitoring = 8,

        /// <summary> PEH time monitoring. </summary>
        PEHTimeMonitoring = 16,

        /// <summary> Backlash compensation. </summary>
        BacklashCompensation = 32,

        /// <summary> Delayed error reaction of the NC. </summary>
        DelayedErrorReaction = 64,

        /// <summary> Modulo axis (modulo display). </summary>
        ModuloPositioning = 128,

        /// <summary> Simulation axis. </summary>
        SimulationAxis = 256,
        
        /// <summary> Reserved for future use. </summary>
        Unused1 = 512,

        /// <summary> Reserved for future use. </summary>
        Unused2 = 1024,

        /// <summary> Reserved for future use. </summary>
        Unused3 = 2048,

        /// <summary> Standstill monitoring. </summary>
        StopMonitoring = 4096,

        /// <summary> Reserved for future use. </summary>
        Unused4 = 8192,

        /// <summary> Reserved for future use. </summary>
        Unused5 = 16384,

        /// <summary> Reserved for future use. </summary>
        Unused6 = 32768,

        /// <summary> Lag monitoring - position. </summary>
        PositionLagMonitoring = 65536,

        /// <summary> Lag monitoring - velocity. </summary>
        VeloLagMonitoring = 131072,

        /// <summary> End position monitoring min. </summary>
        SoftLimitMinMonitoring = 262144,

        /// <summary> End position monitoring max. </summary>
        SoftLimitMaxMonitoring = 524288,

        /// <summary> Position correction ("Measuring system error compensation") </summary>
        PositionCorrection = 1048576,

        /// <summary> Allow motion commands to slave axes. </summary>
        AllowSlaveCommands = 2097152,

        /// <summary> Allow motion commands to an axis that is fed by an external setpoint generator. </summary>
        AllowExtSetAxisCommands = 4194304,

        /// <summary> Request bit for the application software (PLC code), e.g. for an "ApplicationHomingRequest". </summary>
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
