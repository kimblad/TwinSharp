using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The CncChannel class represents a CNC channel and provides various functionalities to manage and control the CNC operations.
    /// It includes properties and methods to interact with different aspects of the CNC, such as operation modes, the interpolator, control commands, contour visualization, feed override, path motion, single block mode, block search, distance to go, data streaming, technology processes, error management, zero offsets, and manual operation.
    /// The class also provides properties to read the covered block motion percentage, covered path distance, line count of the NC program, and path feed rates.
    /// </summary>
    public class CncChannel : IDisposable
    {
        /// <summary>
        /// Gets a dictionary with object descriptions from the COM task.
        /// </summary>
        public readonly Dictionary<string, ObjectDescription> Descriptions;
        readonly AdsClient comClient;


        internal CncChannel(AdsClient plcClient, AdsClient geoClient, AdsClient sdaClient, AdsClient comClient, int channelNumber, Dictionary<string, ObjectDescription> descriptions)
        {
            this.comClient = comClient;
            ChannelNumber = channelNumber;
            this.Descriptions = descriptions;

            Interpolator = new Interpolator(geoClient, channelNumber);

            OperationModeManager = new OperationModeManager(plcClient, channelNumber);
            ControlCommands = new ControlCommands(comClient, descriptions);
            ContourVisualization = new ContourVisualization(comClient, channelNumber);
            FeedOverride = new FeedOverride(comClient, channelNumber);
            BackwardsOnPath = new BackwardsOnPath(plcClient, channelNumber);
            SingleBlock = new SingleBlock(comClient, channelNumber);
            BlockSearch = new BlockSearch(comClient, channelNumber);
            DeleteDistanceToGo = new DeleteDistanceToGo(plcClient, channelNumber);
            DataStreaming = new DataStreaming(comClient, channelNumber);
            TechnologyProcesses = new TechnologyProcesses(plcClient, channelNumber);
            ErrorManager = new ErrorManagement(plcClient, channelNumber);
            ZeroOffsets = new ZeroOffsets(sdaClient, channelNumber);
            ManualOperation = new ManualOperation(plcClient, channelNumber);
        }

        /// <summary>
        /// The unique channel number of this CNC channel.
        /// </summary>
        public int ChannelNumber { get; private set; }

        /// <summary>
        /// Gets an object that  provides an methods and properties to manage and control the operation modes and states
        /// </summary>
        public OperationModeManager OperationModeManager { get; private set; }

        /// <summary>
        /// Provides functions to control the interpolator of this channel. Such as the interpolator's state, axis count,
        /// moved path, velocity, tool life, and more.
        /// </summary>
        public Interpolator Interpolator { get; private set; }

        /// <summary>
        /// Provides functions to control the CNC channel. Such as enablind and disabling skip modes. Control of optional stop.
        /// </summary>
        public ControlCommands ControlCommands { get; private set; }

        /// <summary>
        /// Provides tools to visualize the contour of NC programs.
        /// </summary>
        public ContourVisualization ContourVisualization { get; private set; }

        /// <summary>
        /// Provides tools to read and control the feed override of this channel.
        /// </summary>
        public FeedOverride FeedOverride { get; private set; }

        /// <summary>
        /// Provides functions for forward/backward motion on the path. Permits backward motion along the originally programmed path by means of a real-time signal when the NC program is active.
        /// </summary>
        public BackwardsOnPath BackwardsOnPath { get; private set; }

        /// <summary>
        /// Object that controls single-step mode. When it is active, the machine operator has the option to execute an NC program step by step. The operator releases every NC line one by one. Comment lines or comment blocks and skipped blocks are skipped
        /// </summary>
        public SingleBlock SingleBlock { get; private set; }

        /// <summary>
        /// Provides a tool so the operator can start machining at what is called the continuation position at any point in the program. After a program is interrupted (e.g. tool breakage), this is a quick method to reactivate machining at the point of interruption.
        /// The continuation position can be defined using a number of different block search types(file offset, block counter, block number, etc.). 
        /// </summary>
        public BlockSearch BlockSearch { get; private set; }

        /// <summary>
        /// Provides function to control the “Delete distance to go” function. It interrupts the actual path motion and starts a short cut by straight line to the target position of next block. The distance to go of the current (interrupted) block is then deleted.
        /// </summary>
        public DeleteDistanceToGo DeleteDistanceToGo { get; private set; }

        /// <summary>
        /// Provides a tool to use Data Streaming. With the incremental specification of motion commands (streaming), the CAD/CAM system or the PLC stipulates the next path segment to be travelled (or even several segments).
        /// In this way, motion information not previously specified can still be modified until shortly before entering the command.
        /// </summary>
        public DataStreaming DataStreaming { get; private set; }

        /// <summary>
        /// Gets an object that provides functions to subscribe to and acknowledge technology processes of this channel. Such as M, H, T and S functions/codes.
        /// </summary>
        public TechnologyProcesses TechnologyProcesses { get; private set; }

        /// <summary>
        /// Gets an object useful for error management. You can subscribe to recieve error notifications, reads error messages, and provides functions to acknowledge error. 
        /// When an error is received, it triggers the ErrorRecieved event with detailed error information.
        /// </summary>
        public ErrorManagement ErrorManager { get; private set; }

        /// <summary>
        /// Provides functions to control the zero offsets of this channel. Such as setting, reading, and resetting zero offsets.
        /// </summary>
        public ZeroOffsets ZeroOffsets { get; private set; }

        /// <summary>
        /// Provides a class that contains settings and functions for the manual operation of the CNC.
        /// Such as keys, rapid keys and hand wheels.
        /// </summary>
        public ManualOperation ManualOperation { get; private set; }

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
        /// Path feed was programmed in the NC program Weighted by the current real-time influences such as override.
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


        /// <summary>
        /// Dispose the CNC channel.
        /// </summary>
        public void Dispose()
        {
            OperationModeManager?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}