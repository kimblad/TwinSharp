using System;
using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class CncChannel : IDisposable
    {
        public readonly int ChannelNumber;
        public readonly Dictionary<string, ObjectDescription> Descriptions;
        readonly AdsClient comClient;

        public readonly OperationModeManager OperationModeManager;
        public readonly Interpolator Interpolator;
        public readonly ControlCommands ControlCommands;
        public readonly ContourVisualization ContourVisualization;
        public readonly FeedOverride FeedOverride;
        public readonly BackwardsOnPath BackwardsOnPath;
        public readonly SingleBlock SingleBlock;
        public readonly BlockSearch BlockSearch;
        public readonly DeleteDistanceToGo DeleteDistanceToGo;
        public readonly DataStreaming DataStreaming;
        public readonly TechnologyProcesses TechnologyProcesses;

        internal CncChannel(AdsClient plcClient, AdsClient geoClient, AdsClient comClient, int channelNumber, Dictionary<string, ObjectDescription> descriptions)
        {
            this.comClient = comClient;
            ChannelNumber = channelNumber;
            this.Descriptions = descriptions;

            Interpolator = new Interpolator(geoClient, channelNumber);

            OperationModeManager = new OperationModeManager(plcClient, geoClient, comClient, channelNumber);
            ControlCommands = new ControlCommands(comClient, descriptions);
            ContourVisualization = new ContourVisualization(comClient, channelNumber);
            FeedOverride = new FeedOverride(comClient, channelNumber);
            BackwardsOnPath = new BackwardsOnPath(plcClient, channelNumber);
            SingleBlock = new SingleBlock(comClient, channelNumber);
            BlockSearch = new BlockSearch(comClient, channelNumber);
            DeleteDistanceToGo = new DeleteDistanceToGo(plcClient, channelNumber);
            DataStreaming = new DataStreaming(comClient, channelNumber);
            TechnologyProcesses = new TechnologyProcesses(plcClient, channelNumber);
        }

        /// <summary>
        /// Part of the path motion traversed in the current block in relation to the total path.
        /// If a main axis participates in the motion, the covered path motion is in relation to the block path of the first three axes. If no main axis participates in the motion, the covered path motion is the position lag with the longest motion time in relation to the block path.
        /// </summary>
        public double CoveredBlockMotionPercent
        {
            get
            {
                var obj = Descriptions["mc_cmd_bs_covered_distance_r"];
                double tens = comClient.ReadAny<double>(obj.IndexGroup, obj.IndexOffset);
                return tens / 10.0;
            }
        }

        /// <summary>
        /// Reads the current distance covered in the NC program since program start or since the last # DISTANCE PROG START CLEAR NC command. The calculation is based on the current position in the current NC block.
        /// Unit: 0.1 µm
        /// </summary>
        public double CoveredPathDistance
        {
            get
            {
                var obj = Descriptions["mc_cmd_bs_distance_prog_start_r"];
                return comClient.ReadAny<double>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// The datum indicates the NC program line which is the source of the command just processed by the interpolator.
        /// The value is derived from the number of NC program lines which the decoder has read since the NC program started. All the lines read the decoder are counted, i.e.repeatedly read lines, empty and comment lines. All commands to the interpolator resulting from decoding a NC program line are assigned to the associated line counter.
        /// </summary>
        public uint LineCountNcProgram
        {
            get
            {
                var obj = Descriptions["mc_block_count_r"];
                return comClient.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
        }


        /// <summary>
        /// Path feed that was was programmed by the F word in the NC program..
        /// Unit: 1 µm/s
        /// </summary>
        public double PathFeedProgrammed
        {
            get
            {
                var obj = Descriptions["mc_command_feedrate_r"];
                return comClient.ReadAny<double>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Path feed was programmed in the NC program<value> Weighted by the current real-time influences such as override.
        /// Unit: 1 µm/s
        /// </summary>
        public double PathFeedProgrammedWeighted
        {
            get
            {
                var obj = Descriptions["mc_active_feedrate_r"];
                return comClient.ReadAny<double>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        public void Dispose()
        {
            OperationModeManager?.Dispose();

            GC.SuppressFinalize(this);
        }
    }

    public class DataStreaming
    {
        readonly AdsClient comClient;
        readonly uint indexGroup;
        readonly uint indexOffset;
        public DataStreaming(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;

            indexGroup = 0x120100 + (uint)channelNumber;
            indexOffset = 0x90;
        }


        /// <summary>
        /// This COM interface object can write the data stream with incremental NC commands.
        /// One complete NC line must always be written.Several NC lines may also be written jointly in one write access.
        /// Each NC line must be terminated by a carriage return (ASCII value = 13) and line feed(ASCII value = 10).
        /// </summary>
        /// <param name="ncLines"></param>
        public void Write(string ncLines)
        {
            comClient.WriteAny(indexGroup, indexOffset, ncLines);
        }


    }

    public class BackwardsOnPath
    {
        readonly AdsClient plcClient;
        readonly Dictionary<Identifier, uint> variableHandles;
        public BackwardsOnPath(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            variableHandles = CreateVariableHandles(plcClient, channelNumber);
        }

        private Dictionary<Identifier, uint> CreateVariableHandles(AdsClient plcClient, int channelNumber)
        {
            var handles = new Dictionary<Identifier, uint>();
            uint handle;

            string prefix = string.Format("HLI_Global_Variables.gpCh[{0}]^.bahn_mc_control", channelNumber - 1);
            
            handle = plcClient.CreateVariableHandle(prefix + ".backward_motion.command_w");
            handles.Add(Identifier.SetSelectBackwardMotion, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".backward_motion.enable_w");
            handles.Add(Identifier.SetEnableBackwardMotion, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".backward_motion.state_r");
            handles.Add(Identifier.GetSelectBackwardMotion, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".simulate_motion.command_w");
            handles.Add(Identifier.SetSelectSimulatedMotion, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".simulate_motion.state_r");
            handles.Add(Identifier.GetSelectSimulatedMotion, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".backward_storage_off.command_w");
            handles.Add(Identifier.SetResetBackwardMotionMemory, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".backward_storage_off.state_r");
            handles.Add(Identifier.GetResetBackwardMotionMemory, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed.command_w");
            handles.Add(Identifier.SetSpecifyExternalPathVelocity, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed.state_r");
            handles.Add(Identifier.GetSpecifyExternalPathVelocity, handle);


            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed_valid.command_w");
            handles.Add(Identifier.SetActivateExternalPathVelocity, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".ext_command_speed_valid.state_r");
            handles.Add(Identifier.GetActivateExternalPathVelocity, handle);

            return handles;
        }


        /// <summary>
        /// Select/deselect backward motion on the path In basic setting, M/H functions are executed without synchronisation(MOS) in this mode.
        /// </summary>
        public bool SelectBackwardMotion
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetSelectBackwardMotion]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetSelectBackwardMotion], value);
        }


        public bool EnableInterface
        {
            set => plcClient.WriteAny(variableHandles[Identifier.SetEnableBackwardMotion], value);
        }

        /// <summary>
        /// Select/deselect simulated forward motion on the path In basic setting, M/H functions are executed without synchronisation (MOS) in this mode. Sections in the NC program can be skipped during program runtime in combination with the NC command #OPTIONAL EXECUTION.
        /// </summary>
        public bool SelectSimulatedForwardMotion
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetSelectSimulatedMotion]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetSelectSimulatedMotion], value);
        }

        /// <summary>
        /// Deselects backward motion memory No further NC block is saved in the memory. The memory is deleted. The backward motion memory can only be cleared if no NC program is active.
        /// </summary>
        public bool ResetBackwardMotionMemory
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetResetBackwardMotionMemory]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetResetBackwardMotionMemory], value);
        }

        /// <summary>
        /// External path velocity specified. The path velocity setting is activated by the control unit ext_command_speed_valid. If the velocity specified in negative, the tool moves backwards along the path
        /// Unit: 1 μm/s
        /// </summary>
        public uint ExternalPathVelocity
        {
            //TODO: Why is this an uint when documentation says it can be negative. Check with Beckhoff.
            get => plcClient.ReadAny<uint>(variableHandles[Identifier.GetSpecifyExternalPathVelocity]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetSpecifyExternalPathVelocity], value);
        }

        /// <summary>
        /// Activate the velocity commanded in the ext_command_speed control unit. To reach the commanded velocity, all axes involved in the motion are accelerated or decelerated. If this value is TRUE, the sign is considered in the current path feed (active_feed_r control unit).
        /// </summary>
        public bool ActivateExternalPathVelocity
        {
            get => plcClient.ReadAny<bool>(variableHandles[Identifier.GetActivateExternalPathVelocity]);
            set => plcClient.WriteAny(variableHandles[Identifier.SetActivateExternalPathVelocity], value);
        }


        enum Identifier
        {
            SetSelectBackwardMotion,
            GetSelectBackwardMotion,
            SetSelectSimulatedMotion,
            GetSelectSimulatedMotion,
            SetResetBackwardMotionMemory,
            GetResetBackwardMotionMemory,
            SetSpecifyExternalPathVelocity,
            GetSpecifyExternalPathVelocity,
            SetActivateExternalPathVelocity,
            GetActivateExternalPathVelocity,
            SetEnableBackwardMotion,
        }
    }

    public class FeedOverride
    {
        public readonly FeedOverrideAdresses Adresses;

        readonly AdsClient comClient;
        readonly uint indexGroup;
        internal FeedOverride(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;
            indexGroup = 0x120100 + (uint)channelNumber;

            Adresses = new FeedOverrideAdresses(indexGroup);
        }

        public ushort CommandedFeedOverride
        {
            set => comClient.WriteAny(indexGroup, 0x09, value);
            get => comClient.ReadAny<ushort>(indexGroup, 0x0A);
        }

        public ushort ActualFeedOverride
        {
            get => comClient.ReadAny<ushort>(indexGroup, Adresses.ActualFeedOverride);
        }
    }

    public class FeedOverrideAdresses
    {
        readonly uint indexGroup;
        internal FeedOverrideAdresses(uint indexGroup)
        {
            this.indexGroup = indexGroup;
        }

        public uint IndexGroup => indexGroup;

        public uint ActualFeedOverride => 0x0C;

    }

    public class OperationModeAdresses
    {
        uint geoIndexGroup;
        internal OperationModeAdresses(uint geoIndexGroup)
        {
            this.geoIndexGroup = geoIndexGroup;
        }

        public uint GeoIndexGroup => geoIndexGroup;
        public uint OperationStateActual => 0x1F;
        public uint OperationModeActual => 0x20;
    }

    public class ControlCommands
    {
        readonly AdsClient comClient;
        readonly Dictionary<string, ObjectDescription> descriptions;

        internal ControlCommands(AdsClient comClient, Dictionary<string, ObjectDescription> descriptions)
        {
            this.comClient = comClient;
            this.descriptions = descriptions;
        }

        /// <summary>
        /// Activates/deactivates skip mode at interpreter level for the NC program. The status of skip mode is only evaluated at the start of the NC program. Switchover during execution of an NC program has no effect.
        /// Skip levels active simultaneously are enabled by bitwise ORing.
        /// Example:
        /// Enable all skip levels by setting 0x3FF.
        /// </summary>
        public SkipModes SkipMode
        {
            get
            {
                var obj = descriptions["mc_command_block_ignore_r"];
                return (SkipModes)comClient.ReadAny<uint>(obj.IndexGroup, obj.IndexOffset);
            }
            set
            {
                var obj = descriptions["mc_command_block_ignore_w"];
                comClient.WriteAny(obj.IndexGroup, obj.IndexOffset, (uint)value);
            }
        }

        /// <summary>
        /// Current special channel mode such as syntax check or machining time calculation
        /// </summary>
        public ChannelMode ChannelModeActive
        {
            get
            {
                var obj = descriptions["mc_active_execution_mode_r"];
                return (ChannelMode)comClient.ReadAny<int>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Selection of a special channel mode such as syntax check or machining time calculation
        /// </summary>
        public ChannelMode ChannelModeCommanded
        {
            get
            {
                var obj = descriptions["mc_command_execution_mode_r"];
                return (ChannelMode)comClient.ReadAny<int>(obj.IndexGroup, obj.IndexOffset);
            }
            set
            {
                var obj = descriptions["mc_command_execution_mode_w"];
                comClient.WriteAny(obj.IndexGroup, obj.IndexOffset, (int)value);
            }
        }

        /// <summary>
        /// Activating/deactivating optional stop.
        /// If the function M01(optional stop) is programmed in the current block of the NC program, set this element to the value TRUE to stop at block end (ramped-down deceleration complying with the permissible accelerations).
        /// The following block can be enabled by activating the element “continue machining” if the NC kernel indicates that all axes are located within the control window by resetting the status flag wait_axes_in_position_r.
        /// </summary>
        public bool OptionalStop
        {
            get
            {
                var obj = descriptions["mc_command_M01_stop_enable_r"];
                return comClient.ReadAny<bool>(obj.IndexGroup, obj.IndexOffset);
            }
            set
            {
                var obj = descriptions["mc_command_M01_stop_enable_w"];
                comClient.WriteAny(obj.IndexGroup, obj.IndexOffset, value);
            }
        }
    }

    public class Management
    {
        readonly AdsClient geoClient;
        readonly Dictionary<string, ObjectDescription> descriptions;


        internal Management(AdsClient geoClient, Dictionary<string, ObjectDescription> descriptions)
        {
            this.geoClient = geoClient;
            this.descriptions = descriptions;
        }

        public bool PlcPresent
        {
            get
            {
                //TODO: Don't use the COM dictionary, we need another dictionary for GEO objects.
                var obj = descriptions["cnc_plc_present_r"];
                return geoClient.ReadAny<bool>(obj.IndexGroup, obj.IndexOffset);
            }
        }


    }

    public class ContourVisualization
    {
        readonly AdsClient comClient;
        readonly uint group;
        internal ContourVisualization(AdsClient comClient, int channelID)
        {
            this.comClient = comClient;
            group = 0x120100 + (uint)channelID;
        }

        /// <summary>
        /// Select nominal contour visualisation
        /// 0x0000 ISG_STANDARD Normal mode
        /// 0x0002 SOLLKON Nominal contour visualisation
        /// 0x0004 ON_LINE Online-Visu
        /// 0x0008 SYNCHK Syntax check
        /// </summary>
        public ChannelMode ExecutionMode
        {
            get => (ChannelMode)comClient.ReadAny<uint>(group, 0x40);
            set => comClient.WriteAny(group, 0x3F, (uint)value);
        }

        /// <summary>
        /// Output grid for nominal contour visualisation for linear blocks(G00/G01) in [0.1 µm]
        /// </summary>
        public uint OutputGridSize
        {
            get => comClient.ReadAny<uint>(group, 0x89);
            set => comClient.WriteAny(group, 0x8A, value);
        }

        /// <summary>
        /// Maximum relative path error in [0.1%] for nominal contour visualisation of circles or polynomials
        /// </summary>
        public double MaxRelativePathError
        {
            set => comClient.WriteAny(group, 0x8B, value);
        }

        /// <summary>
        /// Maximum absolute path error in [0.1 µm] for nominal contour visualisation of circles and polynomials
        /// </summary>
        public double MaxAbsolutePathError
        {
            set => comClient.WriteAny(group, 0x8C, value);
        }


        public byte[] DataRecordChannelFIFO
        {
            get => comClient.ReadAny<byte[]>(group, 0x2000);
        }
        public uint DataRecordCountChannelFIFO
        {
            get => comClient.ReadAny<uint>(group, 0x2001);
        }


        public int GetDataRecordFromGlobalFIFO(Memory<byte> bufferToFill)
        {
            return comClient.Read(group, 0x2002, bufferToFill);
        }


        public uint DataRecordCountGlobalFIFO
        {
            get => comClient.ReadAny<uint>(group, 0x2003);
        }


    }

    public class Interpolator
    {
        readonly uint index;
        readonly uint offset;
        readonly AdsClient geoClient;
        public readonly InterpolatorAdresses Adresses;

        internal Interpolator(AdsClient geoClient, int channelId)
        {
            GetAllAdresses(geoClient, channelId, out uint index, out uint offset);

            this.geoClient = geoClient;
            this.index = index;
            this.offset = offset;

            Adresses = new InterpolatorAdresses(index, offset);
        }

        public bool Exists
        {
            get => index != 0 || offset != 0;
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

        public ushort AxisCount
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x1);
        }


        public double MovedPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x2);
        }

        public uint SlopeBufPath
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3);
        }

        public uint SlopeBufLevel
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x4);
        }

        public uint IpoBufLevel
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x5);
        }

        public ushort CommandBFOverride
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x6);
        }

        public ushort CommandFBOverride
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x7);
        }

        public double RemainingPathOfBlock
        {
            get => geoClient.ReadAny<double>(index, offset + 0x8);
        }

        public bool BlockEndActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x9);
        }

        public byte ActualStateOfInterpolator
        {
            get => geoClient.ReadAny<byte>(index, offset + 0xA);
        }

        public uint MaximumVelocityOnPath
        {
            get => geoClient.ReadAny<uint>(index, offset + 0xF);
        }

        public uint MaximumVelocityOnPathAtBlockEnd
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x10);
        }

        public uint ActualDWord
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x11);
        }

        public ushort ActualZeroOffsetGroup
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x12);
        }

        public uint SuspendAxisOutputState
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x14);
        }

        public double ActualVelocityOnPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x15);
        }

        public double ProgrammedVelocityOnPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x16);
        }

        public bool CartesianTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x17);
        }

        public bool KinematicalTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x18);
        }

        public double BlockLengthOnPath
        {
            get => geoClient.ReadAny<double>(index, offset + 0x19);
        }

        public uint SingleStepMode
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x1A);
            set => geoClient.WriteAny(index, offset + 0x1A, value);
        }

        public double CoveredDistance
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1B);
        }

        public uint ToolLifeToolId
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x1C);
        }

        public double ToolLifeTime
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1D);
        }

        public double ToolLifeDistance
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1E);
        }

        public double ToolLifeTimeFactor
        {
            get => geoClient.ReadAny<double>(index, offset + 0x1F);
            set => geoClient.WriteAny(index, offset + 0x1F, value);
        }

        public double ToolLifeDistanceFactor
        {
            get => geoClient.ReadAny<double>(index, offset + 0x20);
            set => geoClient.WriteAny(index, offset + 0x20, value);
        }

        public int BlockNumberActual
        {
            get => geoClient.ReadAny<int>(index, Adresses.BlockNumberActual);
        }

        public double DwellTimeRemaning
        {
            get => geoClient.ReadAny<double>(index, offset + 0x22);
            set => geoClient.WriteAny(index, offset + 0x22, value);
        }

        public double DwellTimeCommanded
        {
            get => geoClient.ReadAny<double>(index, offset + 0x23);
        }

        public bool RakelTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x24);
        }

        public double Druckwinkel
        {
            get => geoClient.ReadAny<double>(index, offset + 0x25);
        }

        public double Rahmenwinkel
        {
            get => geoClient.ReadAny<double>(index, offset + 0x26);
        }

        public double WegBisSynPunkt
        {
            get => geoClient.ReadAny<double>(index, offset + 0x27);
        }

        public double ZeitBisSynPunkt
        {
            get => geoClient.ReadAny<double>(index, offset + 0x28);
        }

        public ushort GlobalEnabledAxesCount
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x29);
        }

        public int BendAngleAtBlockEnd
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2A);
        }

        public bool RapidMovementBlock
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x2B);
        }

        public double ActualToolRadius
        {
            get => geoClient.ReadAny<double>(index, offset + 0x2C);
        }

        public int ActualBlockCount
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2E);
        }

        public int LatestInputBlockCount
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2F);
        }

        public double DynamicWeightG129
        {
            get => geoClient.ReadAny<double>(index, offset + 0x30);
        }

        public double DynamicWeightG131
        {
            get => geoClient.ReadAny<double>(index, offset + 0x31);
        }

        public double DynamicWeightG133
        {
            get => geoClient.ReadAny<double>(index, offset + 0x32);
        }

        public double DynamicWeightG134
        {
            get => geoClient.ReadAny<double>(index, offset + 0x33);
        }

        public double DynamicWeightG231
        {
            get => geoClient.ReadAny<double>(index, offset + 0x34);
        }

        public double DynamicWeightG233
        {
            get => geoClient.ReadAny<double>(index, offset + 0x35);
        }

        public bool HscSurfaceActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x36);
        }

        public double HscSurfacePathDev
        {
            get => geoClient.ReadAny<double>(index, offset + 0x37);
        }

        public double HscSurfaceTrackDev
        {
            get => geoClient.ReadAny<double>(index, offset + 0x38);
        }

        public double HscSurfaceMaxAngle
        {
            get => geoClient.ReadAny<double>(index, offset + 0x39);
        }

        public bool HscSurfaceFAutoOffG00
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x3A);
        }

        public uint HscSurfaceCheckJerk
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3B);
        }

        public uint MotionCtrl
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3C);
        }

        public uint MotionCtrl2
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3D);
        }

        public uint Status
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3E);
        }

        public uint AdditionalStatus
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x3F);
        }

        public int PosLahTimeToDist
        {
            get => geoClient.ReadAny<int>(index, offset + 0x41);
        }

        public uint PositionLookaheadDistance
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x42);
            set => geoClient.WriteAny(index, offset + 0x42, value);
        }

        public double FeedOfSyncAxis
        {
            get => geoClient.ReadAny<double>(index, offset + 0x43);
        }

        public double PathfeedInSyncArea
        {
            get => geoClient.ReadAny<double>(index, offset + 0x44);
        }

        public double TotalCsMatrixEx0
        {
            get => geoClient.ReadAny<double>(index, offset + 0x45);
        }

        public double TotalCsMatrixEx1
        {
            get => geoClient.ReadAny<double>(index, offset + 0x46);
        }

        public double TotalCsMatrixEx2
        {
            get => geoClient.ReadAny<double>(index, offset + 0x47);
        }

        public double TotalCsMatrixEy0
        {
            get => geoClient.ReadAny<double>(index, offset + 0x48);
        }

        public double TotalCsMatrixEy1
        {
            get => geoClient.ReadAny<double>(index, offset + 0x49);
        }

        public double TotalCsMatrixEy2
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4A);
        }

        public double TotalCsMatrixEz0
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4B);
        }

        public double TotalCsMatrixEz1
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4C);
        }

        public double TotalCsMatrixEz2
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4D);
        }

        public double TotalCsMatrixVx
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4E);
        }

        public double TotalCsMatrixVy
        {
            get => geoClient.ReadAny<double>(index, offset + 0x4F);
        }

        public double TotalCsMatrixVz
        {
            get => geoClient.ReadAny<double>(index, offset + 0x50);
        }

        public bool MeasuringActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x52);
        }

        public uint IpoSyncWaitState
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x53);
        }

        public uint LimitingAxis
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x54);
        }

        public uint DynamicLimit
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x55);
        }

        public double DistanceFromProgramStart
        {
            get => geoClient.ReadAny<double>(index, offset + 0x56);
        }

        public bool HscFilterOn
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x57);
        }

        public uint HscFilterOrder
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x58);
        }

        public double HscFilterDelayTimeIpo
        {
            get => geoClient.ReadAny<double>(index, offset + 0x59);
        }

        public bool ManualWaitPositionInitializationDone
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x6E);
        }

        public bool CaxFunctionAligningActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x6F);
        }

        public double OverrideFeedFactor
        {
            get => geoClient.ReadAny<double>(index, offset + 0x70);
        }

        public uint ManualPcsAxesMovementLimitation
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x71);
        }

        public uint ManualAcsAxesMovementLimitation
        {
            get => geoClient.ReadAny<uint>(index, offset + 0x72);
        }
    }

    public class InterpolatorAdresses
    {
        uint index;
        uint baseOffset;
        internal InterpolatorAdresses(uint index, uint offset)
        {
            this.index = index;
            baseOffset = offset;
        }

        public uint GroupIndex => index;

        public uint BlockNumberActual => baseOffset + 0x21;


    }
}