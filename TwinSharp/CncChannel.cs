﻿using System.Net;
using System.Xml.Linq;
using TwinCAT.Ads;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.PlcOpen;

namespace TwinSharp
{
    public class CncChannel
    {
        readonly int Id;
        readonly Dictionary<string, ObjectDescription> descriptions;
        readonly AdsClient comClient;

        public readonly Interpolator Interpolator;
        public readonly ControlCommands ControlCommands;
        public readonly ContourVisualization ContourVisualization;


        internal CncChannel(AdsClient geoClient, AdsClient comClient, int id, Dictionary<string, ObjectDescription> descriptions)
        {
            this.comClient = comClient;
            Id = id;
            this.descriptions = descriptions;

            Interpolator = new Interpolator(geoClient, id);

            ControlCommands = new ControlCommands(comClient, descriptions);
            ContourVisualization = new ContourVisualization(comClient, id);
        }

        /// <summary>
        /// Part of the path motion traversed in the current block in relation to the total path.
        /// If a main axis participates in the motion, the covered path motion is in relation to the block path of the first three axes. If no main axis participates in the motion, the covered path motion is the position lag with the longest motion time in relation to the block path.
        /// </summary>
        public double CoveredBlockMotionPercent
        {
            get
            {
                var obj = descriptions["mc_cmd_bs_covered_distance_r"];
                double tens = comClient.ReadAny<double>(obj.IndexGroup, obj.IndexOffset);
                return tens / 10.0;
            }
        }
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
        public ChannelModes ChannelModeActive
        {
            get
            {
                var obj = descriptions["mc_active_execution_mode_r"];
                return (ChannelModes)comClient.ReadAny<int>(obj.IndexGroup, obj.IndexOffset);
            }
        }

        /// <summary>
        /// Selection of a special channel mode such as syntax check or machining time calculation
        /// </summary>
        public ChannelModes ChannelModeCommanded
        {
            get
            {
                var obj = descriptions["mc_command_execution_mode_r"];
                return (ChannelModes)comClient.ReadAny<int>(obj.IndexGroup, obj.IndexOffset);
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
                //ToDo: Don't use the COM dictionary, we need another dictionary for GEO objects.
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
        public uint ExecutionMode
        {
            get => comClient.ReadAny<uint>(group, 0x40);
            set => comClient.WriteAny(group, 0x3F, value);
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

        public byte[] DataRecordGlobalFIFO
        {
            get => comClient.ReadAny<byte[]>(group, 0x2002);
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

        internal Interpolator(AdsClient geoClient, int channelId)
        {
            GetAllAdresses(geoClient, channelId, out uint index, out uint offset);

            this.geoClient = geoClient;
            this.index = index;
            this.offset = offset;   
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
            get => geoClient.ReadAny<int>(index, offset + 0x21);
        }

        public double DwellTimeRemaning
        {
            get => geoClient.ReadAny<double>(index, offset + 0x22);
            set => geoClient.WriteAny(index, offset + 0x22, value);
        }

        public double DwellTimeCommanded
        {
            get => geoClient.ReadAny<double>(index, offset + 0x23);
            set => geoClient.WriteAny(index, offset + 0x23, value);
        }

        public bool RakelTransformationActive
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x24);
            set => geoClient.WriteAny(index, offset + 0x24, value);
        }

        public double Druckwinkel
        {
            get => geoClient.ReadAny<double>(index, offset + 0x25);
            set => geoClient.WriteAny(index, offset + 0x25, value);
        }

        public double Rahmenwinkel
        {
            get => geoClient.ReadAny<double>(index, offset + 0x26);
            set => geoClient.WriteAny(index, offset + 0x26, value);
        }

        public double WegBisSynPunkt
        {
            get => geoClient.ReadAny<double>(index, offset + 0x27);
            set => geoClient.WriteAny(index, offset + 0x27, value);
        }

        public double ZeitBisSynPunkt
        {
            get => geoClient.ReadAny<double>(index, offset + 0x28);
            set => geoClient.WriteAny(index, offset + 0x28, value);
        }

        public ushort GlobalEnabledAxesCount
        {
            get => geoClient.ReadAny<ushort>(index, offset + 0x29);
            set => geoClient.WriteAny(index, offset + 0x29, value);
        }

        public int BendAngleAtBlockEnd
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2A);
            set => geoClient.WriteAny(index, offset + 0x2A, value);
        }

        public bool RapidMovementBlock
        {
            get => geoClient.ReadAny<bool>(index, offset + 0x2B);
            set => geoClient.WriteAny(index, offset + 0x2B, value);
        }

        public double ActualToolRadius
        {
            get => geoClient.ReadAny<double>(index, offset + 0x2C);
            set => geoClient.WriteAny(index, offset + 0x2C, value);
        }

        public int ActualBlockCount
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2E);
            set => geoClient.WriteAny(index, offset + 0x2E, value);
        }

        public int LatestInputBlockCount
        {
            get => geoClient.ReadAny<int>(index, offset + 0x2F);
            set => geoClient.WriteAny(index, offset + 0x2F, value);
        }

        public double DynamicWeightG129
        {
            get => geoClient.ReadAny<double>(index, offset + 0x30);
            set => geoClient.WriteAny(index, offset + 0x30, value);
        }

        public double DynamicWeightG131
        {
            get => geoClient.ReadAny<double>(index, offset + 0x31);
            set => geoClient.WriteAny(index, offset + 0x31, value);
        }

        public double DynamicWeightG133
        {
            get => geoClient.ReadAny<double>(index, offset + 0x32);
            set => geoClient.WriteAny(index, offset + 0x32, value);
        }

        public double DynamicWeightG134
        {
            get => geoClient.ReadAny<double>(index, offset + 0x33);
            set => geoClient.WriteAny(index, offset + 0x33, value);
        }

        public double DynamicWeightG231
        {
            get => geoClient.ReadAny<double>(index, offset + 0x34);
            set => geoClient.WriteAny(index, offset + 0x34, value);
        }

        public double DynamicWeightG233
        {
            get => geoClient.ReadAny<double>(index, offset + 0x35);
            set => geoClient.WriteAny(index, offset + 0x35, value);
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



    public class Adress
    {
        public readonly uint Group;
        public readonly uint Offset;

        public Adress(uint group, uint offset)
        {
            Group = group;
            Offset = offset;
        }
    }
}