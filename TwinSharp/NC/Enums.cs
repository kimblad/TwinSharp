namespace TwinSharp.NC
{
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
        Single = 0, //Single axis that is neither a master nor a slave (SINGLE)
        Master = 1, //Master axis with any number of slaves (MASTER)
        MasterSlave = 2, //Slave axis that is the master of another slave (MASTERSLAVE)
        Slave = 3, //Just a slave axis (SLAVE)
    }
}
