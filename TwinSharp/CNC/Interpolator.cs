using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The Interpolator class provides an interface to interact with a CNC interpolator via an AdsClient.
    /// It allows reading and writing various properties related to the interpolator's state, such as axis count,
    /// moved path, velocity, tool life, and more. The class also provides addresses for device notifications
    /// and methods to retrieve interpolator addresses and properties.
    /// </summary>
    public class Interpolator
    {
        readonly uint index;
        readonly uint offset;
        readonly AdsClient geoClient;

        internal Interpolator(AdsClient geoClient, int channelId)
        {
            GetAllAdresses(geoClient, channelId, out uint index, out uint offset);

            this.geoClient = geoClient;
            this.index = index;
            this.offset = offset;

            Adresses = new InterpolatorAdresses(index, offset);
        }



        private void GetAllAdresses(AdsClient geoClient, int channelId, out uint interpolatorGroup, out uint interpolatorOffset)
        {
            const uint baseIndex = 0x121300; //Base adress for interpolator

            //To determine which instances of a class exist, you can query the object address of the first element
            //(IndexOffset = 0x0) by means of a READ & WRITE access.

            //Add the attribute signalling that we want to read the objects adress.
            uint existsIndex = baseIndex + 0x600;

            //We will write channel ID and 0 to the adress.
            var writeBuffer = new byte[8];
            var ms = new MemoryStream(writeBuffer);
            using var bw = new BinaryWriter(ms);
            bw.Write(channelId);
            bw.Write(0);
            var writeMemory = new ReadOnlyMemory<byte>(writeBuffer);

            //Create the buffer for where ADS will write the returned adress.
            var readBuffer = new byte[8];
            var readMemory = new Memory<byte>(readBuffer);

            var bytesRead = geoClient.ReadWrite(existsIndex, 0x0, readBuffer, writeBuffer);

            ms = new MemoryStream(readBuffer);
            using var br = new BinaryReader(ms);

            interpolatorGroup = br.ReadUInt32();
            interpolatorOffset = br.ReadUInt32();



            //The IndexGroup and the IndexOffset are returned.
            //No instance of the class exists if (0, 0) is returned as the adress.
            if (interpolatorGroup == 0 && interpolatorOffset == 0)
                return;

            //We know the adress of the interpolator class.
            //Now we can find the number of element types of it.
            //The number of existing element types of an instance can be queried.
            //Use the value attribute (0x0) of the first element (IndexOffset = 0).

            //Adding 0 (attribute for value) to interolatorIndexGroup is silly :)

            uint objectCount = geoClient.ReadAny<uint>(interpolatorGroup, 0x0);
        }

        /// <summary>
        /// Adresses for the interpolator class. Use this if you want to add device notifications for any of the properties.
        /// </summary>
        public InterpolatorAdresses Adresses { get; private set; }


        /// <summary>
        /// Number of axes.
        /// </summary>
        public ushort AxisCount
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x1);
        }


        /// <summary>
        /// Length of moved path. 
        /// Unit: [0.1 µm]
        /// </summary>
        public double MovedPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x2);
        }

        /// <summary>
        /// Length of slope buffer path.
        /// Unit: [0.1 µm]
        /// </summary>
        public uint SlopeBufPath
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3);
        }

        /// <summary>
        /// Slope buffer level.
        /// </summary>
        public uint SlopeBufLevel
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x4);
        }

        /// <summary>
        /// Interpolator buffer level.
        /// </summary>
        public uint IpoBufLevel
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x5);
        }

        /// <summary>
        /// Commanded BF override.
        /// </summary>
        public ushort CommandBFOverride
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x6);
        }

        /// <summary>
        /// Commanded FB overide.
        /// </summary>
        public ushort CommandFBOverride
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x7);
        }


        /// <summary>
        /// Length of the remaining path of the block.
        /// Unit: [0.1 µm]
        /// </summary>
        public double RemainingPathOfBlock
        {
            get => geoClient.ReadAny<double>(index, offset + 0x8);
        }


        /// <summary>
        /// Block end active.
        /// </summary>
        public bool BlockEndActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x9);
        }


        /// <summary>
        /// Actual state of the interpolator.
        /// </summary>
        public byte ActualStateOfInterpolator
        {
            get => geoClient.ReadAny<byte>(index, offset + 0xA);
        }

        /// <summary>
        /// Maximum velocity on path.
        /// </summary>
        public uint MaximumVelocityOnPath
        {
            get => geoClient.ReadAny<uint>(index, offset + 0xF);
        }

        /// <summary>
        /// Maximum velocity on path at block end.
        /// </summary>
        public uint MaximumVelocityOnPathAtBlockEnd
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x10);
        }

        /// <summary>
        /// Actual DWord.
        /// </summary>
        public uint ActualDWord
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x11);
        }

        /// <summary>
        /// Actual zero offset group.
        /// </summary>
        public ushort ActualZeroOffsetGroup
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x12);
        }

        /// <summary>
        /// Suspend axis output state.
        /// </summary>
        public uint SuspendAxisOutputState
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x14);
        }

        /// <summary>
        /// Actual velocity on path.
        /// Unit: [µm/s]
        /// </summary>
        public double ActualVelocityOnPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x15);
        }


        /// <summary>
        /// Programmed velocity on path.
        /// Unit: [µm/s]
        /// </summary>
        public double ProgrammedVelocityOnPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x16);
        }

        /// <summary>
        /// Cartesian transformation active.
        /// </summary>
        public bool CartesianTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x17);
        }

        /// <summary>
        /// Kinematical transformation active.
        /// </summary>
        public bool KinematicalTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x18);
        }

        /// <summary>
        /// Block length on path.
        /// Unit: [0.1 µm]
        /// </summary>
        public double BlockLengthOnPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x19);
        }

        /// <summary>
        /// Single step mode active.
        /// </summary>
        public uint SingleStepMode
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x1A);
            set => geoClient.WriteAny(index, offset + 0x1A, value);
        }

        /// <summary>
        /// Covered distance.
        /// </summary>
        public double CoveredDistance
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1B);
        }


        /// <summary>
        /// Tool life tool ID.
        /// </summary>
        public uint ToolLifeToolId
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x1C);
        }

        /// <summary>
        /// Tool life time.
        /// Unit: seconds
        /// </summary>
        public double ToolLifeTime
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1D);
        }

        /// <summary>
        /// Tool life distance.
        /// Unit: mm
        /// </summary>
        public double ToolLifeDistance
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1E);
        }

        /// <summary>
        /// Tool life time factor.
        /// </summary>
        public double ToolLifeTimeFactor
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1F);
            set => geoClient.WriteAny(index, offset + 0x1F, value);
        }

        /// <summary>
        /// Tool life distance factor.
        /// </summary>
        public double ToolLifeDistanceFactor
        {
            get => geoClient.ReadAny<double>(index, offset + 0x20);
            set => geoClient.WriteAny(index, offset + 0x20, value);
        }

        /// <summary>
        /// Actual block number.
        /// </summary>
        public int BlockNumberActual
        {
            get => geoClient.ReadAny<int>(index, Adresses.BlockNumberActual);
        }

        /// <summary>
        /// Remaining G04 dwell time.
        /// Unit: seconds.
        /// </summary>
        public double DwellTimeRemaning
        {
            get => geoClient.ReadAny<double>(index, offset + 0x22);
            set => geoClient.WriteAny(index, offset + 0x22, value);
        }

        /// <summary>
        /// Commanded G04 dwell time.
        /// Unit: seconds.
        /// </summary>
        public double DwellTimeCommanded
        {
            get => geoClient.ReadAny<double>(index, offset + 0x23);
        }

        /// <summary>
        /// Rakel transformation active.
        /// </summary>
        public bool RakelTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x24);
        }

        /// <summary>
        /// Pressure angle.
        /// </summary>
        public double Druckwinkel
        {
            get => geoClient.ReadAny<double>(index, offset + 0x25);
        }

        /// <summary>
        /// Frame angle.
        /// </summary>
        public double Rahmenwinkel
        {
            get => geoClient.ReadAny<double>(index, offset + 0x26);
        }

        /// <summary>
        /// Weg bis Syn. Punkt.
        /// </summary>
        public double WegBisSynPunkt
        {
            get => geoClient.ReadAny<double>(index, offset + 0x27);
        }

        /// <summary>
        /// Zeit bis Syn. Punkt.
        /// </summary>
        public double ZeitBisSynPunkt
        {
            get => geoClient.ReadAny<double>(index, offset + 0x28);
        }

        /// <summary>
        /// Global enabled axes count.
        /// </summary>
        public ushort GlobalEnabledAxesCount
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x29);
        }

        /// <summary>
        /// Bend angle at block end.
        /// </summary>
        public int BendAngleAtBlockEnd
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2A);
        }

        /// <summary>
        /// Is current block a rapid G0 movement.
        /// </summary>
        public bool RapidMovementBlock
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x2B);
        }

        /// <summary>
        /// Actual tool radius.
        /// Unit: [0.1 µm]
        /// </summary>
        public double ActualToolRadius
        {
            get => geoClient.ReadAny<double>(index, offset + 0x2C);
        }

        /// <summary>
        /// Actual block count.
        /// </summary>
        public int ActualBlockCount
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2E);
        }

        /// <summary>
        /// Latest input block count.
        /// </summary>
        public int LatestInputBlockCount
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2F);
        }

        /// <summary>
        /// Dynamic weight G129.
        /// Unit: %
        /// </summary>
        public double DynamicWeightG129
        {
            get => geoClient.ReadAny<double>(index, offset + 0x30);
        }

        /// <summary>
        /// Dynamic weight G131.
        /// Unit: %
        /// </summary>
        public double DynamicWeightG131
        {
            get => geoClient.ReadAny<double>(index, offset + 0x31);
        }

        /// <summary>
        /// Dynamic weight G133.
        /// Unit: %
        /// </summary>
        public double DynamicWeightG133
        {
            get => geoClient.ReadAny<double>(index, offset + 0x32);
        }

        /// <summary>
        /// Dynamic weight G134.
        /// Unit: %
        /// </summary>
        public double DynamicWeightG134
        {
            get => geoClient.ReadAny<double>(index, offset + 0x33);
        }

        /// <summary>
        /// Dynamic weight G231.
        /// Unit: %
        /// </summary>
        public double DynamicWeightG231
        {
            get => geoClient.ReadAny<double>(index, offset + 0x34);
        }

        /// <summary>
        /// Dynamic weight G233.
        /// Unit: %
        /// </summary>
        public double DynamicWeightG233
        {
            get => geoClient.ReadAny<double>(index, offset + 0x35);
        }

        /// <summary>
        /// HSC surface active.
        /// </summary>
        public bool HscSurfaceActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x36);
        }

        /// <summary>
        /// HSC surface path deviation.
        /// Unit: [mm]
        /// </summary>
        public double HscSurfacePathDev
        {
            get => geoClient.ReadAny<double>(index, offset + 0x37);
        }

        /// <summary>
        /// HSC surface track deviation.
        /// Unit: [deg]
        /// </summary>
        public double HscSurfaceTrackDev
        {
            get => geoClient.ReadAny<double>(index, offset + 0x38);
        }

        /// <summary>
        /// HSC surface max angle.
        /// Unit: [deg]
        /// </summary>
        public double HscSurfaceMaxAngle
        {
            get => geoClient.ReadAny<double>(index, offset + 0x39);
        }

        /// <summary>
        /// HSC surface F auto off G00.
        /// </summary>
        public bool HscSurfaceFAutoOffG00
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x3A);
        }

        /// <summary>
        /// HSC surface check jerk.
        /// </summary>
        public uint HscSurfaceCheckJerk
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3B);
        }

        /// <summary>
        /// Motion control.
        /// </summary>
        public uint MotionCtrl
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3C);
        }

        /// <summary>
        /// Motion control 2.
        /// </summary>
        public uint MotionCtrl2
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3D);
        }

        /// <summary>
        /// Status.
        /// </summary>
        public uint Status
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3E);
        }

        /// <summary>
        /// Additional status.
        /// </summary>
        public uint AdditionalStatus
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3F);
        }

        /// <summary>
        /// Position lookahead time to distance.
        /// </summary>
        public int PosLahTimeToDist
        {
            get => geoClient.ReadAny<int>(index, offset + 0x41);
        }

        /// <summary>
        /// Position lookahead distance.
        /// </summary>
        public uint PositionLookaheadDistance
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x42);
            set => geoClient.WriteAny(index, offset + 0x42, value);
        }

        /// <summary>
        /// Feed of sync axis.
        /// Unit: [µm/s]
        /// </summary>
        public double FeedOfSyncAxis
        {
            get => geoClient.ReadAny<double>(index, offset + 0x43);
        }

        /// <summary>
        /// Path feed in sync area.
        /// Unit: [µm/s]
        /// </summary>
        public double PathfeedInSyncArea
        {
            get => geoClient.ReadAny<double>(index, offset + 0x44);
        }

        /// <summary>
        /// Total CS matrix ex0.
        /// </summary>
        public double TotalCsMatrixEx0
        {
            get => geoClient.ReadAny<double>(index, offset + 0x45);
        }

        /// <summary>
        /// Total CS matrix ex1.
        /// </summary>
        public double TotalCsMatrixEx1
        {
            get => geoClient.ReadAny<double>(index, offset + 0x46);
        }

        /// <summary>
        /// Total CS matrix ex2.
        /// </summary>
        public double TotalCsMatrixEx2
        {
            get => geoClient.ReadAny<double>(index, offset + 0x47);
        }

        /// <summary>
        /// Total CS matrix ey0.
        /// </summary>
        public double TotalCsMatrixEy0
        {
            get => geoClient.ReadAny<double>(index, offset + 0x48);
        }

        /// <summary>
        /// Total CS matrix ey1.
        /// </summary>
        public double TotalCsMatrixEy1
        {
            get => geoClient.ReadAny<double>(index, offset + 0x49);
        }

        /// <summary>
        /// Total CS matrix ey2.
        /// </summary>
        public double TotalCsMatrixEy2
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4A);
        }

        /// <summary>
        /// Total CS matrix ez0.
        /// </summary>
        public double TotalCsMatrixEz0
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4B);
        }

        /// <summary>
        /// Total CS matrix ez1.
        /// </summary>
        public double TotalCsMatrixEz1
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4C);
        }

        /// <summary>
        /// Total CS matrix ez2.
        /// </summary>
        public double TotalCsMatrixEz2
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4D);
        }

        /// <summary>
        /// Total CS matrix vx.
        /// Unit: [0.1 µm]
        /// </summary>
        public double TotalCsMatrixVx
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4E);
        }

        /// <summary>
        /// Total CS matrix vy.
        /// Unit: [0.1 µm]
        /// </summary>
        public double TotalCsMatrixVy
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4F);
        }

        /// <summary>
        /// Total CS matrix vz.
        /// Unit: [0.1 µm]
        /// </summary>
        public double TotalCsMatrixVz
        {
            get => geoClient.ReadAny<double>(index, offset + 0x50);
        }

        /// <summary>
        /// Measuring active.
        /// </summary>
        public bool MeasuringActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x52);
        }

        /// <summary>
        /// Interpolator sync wait state.
        /// </summary>
        public uint IpoSyncWaitState
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x53);
        }

        /// <summary>
        /// Limiting axis.
        /// </summary>
        public uint LimitingAxis
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x54);
        }

        /// <summary>
        /// Dynamic limit.
        /// </summary>
        public uint DynamicLimit
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x55);
        }

        /// <summary>
        /// Distance from program start.
        /// </summary>
        public double DistanceFromProgramStart
        {
            get => geoClient.ReadAny<double>(index, offset + 0x56);
        }

        /// <summary>
        /// HSC filter on.
        /// </summary>
        public bool HscFilterOn
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x57);
        }

        /// <summary>
        /// HSC filter order.
        /// </summary>
        public uint HscFilterOrder
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x58);
        }

        /// <summary>
        /// HSC filter delay time Ipo.
        /// Unit: seconds.
        /// </summary>
        public double HscFilterDelayTimeIpo
        {
            get => geoClient.ReadAny<double>(index, offset + 0x59);
        }

        /// <summary>
        /// Manual: wait position initialization done.
        /// </summary>
        public bool ManualWaitPositionInitializationDone
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x6E);
        }

        /// <summary>
        /// Cax function aligning active.
        /// </summary>
        public bool CaxFunctionAligningActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x6F);
        }

        /// <summary>
        /// Override feed factor.
        /// </summary>
        public double OverrideFeedFactor
        {
            get => geoClient.ReadAny<double>(index, offset + 0x70);
        }

        /// <summary>
        /// Manual: PCS axes movement limitation.
        /// </summary>
        public uint ManualPcsAxesMovementLimitation
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x71);
        }

        /// <summary>
        /// Manual: ACS axes movement limitation.
        /// </summary>
        public uint ManualAcsAxesMovementLimitation
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x72);
        }
    }

    /// <summary>
    /// Adresses for the interpolator class.
    /// </summary>
    public class InterpolatorAdresses
    {
        uint index;
        uint baseOffset;
        internal InterpolatorAdresses(uint index, uint offset)
        {
            this.index = index;
            baseOffset = offset;
        }

        /// <summary>
        /// Group index for interpolator adresses.
        /// </summary>
        public uint GroupIndex => index;

        /// <summary>
        /// Sub index for block number actual.
        /// </summary>
        public uint BlockNumberActual => baseOffset + 0x21;
    }
}