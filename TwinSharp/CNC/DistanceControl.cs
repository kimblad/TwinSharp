using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Wraps the TF5200 distance control HLI for a single CNC axis.
    /// Allows enabling the interface, issuing commands (activate/freeze/deactivate),
    /// and reading state and position feedback via ADS variable handles.
    ///
    /// Two activation modes are supported:
    /// <list type="bullet">
    ///   <item><description><b>SET_POS mode</b> (<see cref="ActivateSetPos"/>): the CNC adjusts the axis
    ///   to hold the workpiece surface at a commanded absolute position.</description></item>
    ///   <item><description><b>Constant-distance mode</b> (<see cref="ActivateConstDistance"/>): the CNC
    ///   maintains a fixed gap between tool and workpiece surface.</description></item>
    /// </list>
    ///
    /// The sensor must be wired as Encoder 2 on the axis and configured in TwinCAT.
    /// Parameter P-AXIS-00328 must be set to enable the distance control function globally for the axis.
    ///
    /// Semaphore handshake: after writing a command, <c>command_semaphor_rw</c> is set to <c>true</c>.
    /// The CNC clears it back to <c>false</c> once the command is latched. In debug contexts it is
    /// acceptable not to wait for clearance before issuing the next command.
    /// </summary>
    public class DistanceControl : IDisposable
    {
        private readonly AdsClient plcClient;
        private readonly uint axisNumber;
        private Dictionary<Identifier, uint>? handles;

        internal DistanceControl(uint axisNumber, AdsClient plcClient)
        {
            this.axisNumber = axisNumber;
            this.plcClient  = plcClient;
            // Handles are created lazily in Enable() so a missing TF5200 configuration
            // does not prevent the CNC connection from being established.
        }

        private void EnsureHandles()
        {
            if (handles is not null) return;
            handles = CreateHandles(plcClient, axisNumber);
        }

        private static Dictionary<Identifier, uint> CreateHandles(AdsClient plcClient, uint axisNumber)
        {
            var d = new Dictionary<Identifier, uint>();
            string pfx = $"HLI_Global_Variables.gpAx[{axisNumber - 1}]^.lr_mc_control.distance_control";

            d[Identifier.Enable]         = plcClient.CreateVariableHandle(pfx + ".enable_w");
            d[Identifier.Semaphore]      = plcClient.CreateVariableHandle(pfx + ".command_semaphor_rw");
            d[Identifier.Transition]     = plcClient.CreateVariableHandle(pfx + ".command_w.transition");
            d[Identifier.Position]       = plcClient.CreateVariableHandle(pfx + ".command_w.reference_position");
            d[Identifier.State]          = plcClient.CreateVariableHandle(pfx + ".state_r.actual_state");
            d[Identifier.ActualPosition] = plcClient.CreateVariableHandle(pfx + ".state_r.actual_position");
            d[Identifier.ActualOffset]   = plcClient.CreateVariableHandle(pfx + ".state_r.actual_offset");

            return d;
        }

        /// <summary>
        /// Signals to the CNC that this HLI interface is present.
        /// Creates the ADS variable handles on first call — this is where AdsErrorCode 1808 will
        /// surface if TF5200 is not configured on the machine or the axis.
        /// Must be called once after connect before any distance control commands will be accepted.
        /// </summary>
        public void Enable()
        {
            EnsureHandles();
            plcClient.WriteAny(handles![Identifier.Enable], true);
        }

        /// <summary>
        /// Activates distance control in SET_POS mode. The CNC will adjust the axis so that
        /// the workpiece surface is held at <paramref name="positionMm"/> (absolute, in mm).
        /// </summary>
        public void ActivateSetPos(double positionMm)
        {
            EnsureHandles();
            int raw = (int)(positionMm * 10_000.0); // mm → 0.1 µm
            plcClient.WriteAny(handles![Identifier.Transition], (uint)DistanceControlCommand.On);
            plcClient.WriteAny(handles![Identifier.Position],   raw);
            plcClient.WriteAny(handles![Identifier.Semaphore],  true);
        }

        /// <summary>
        /// Activates distance control in constant-distance mode. The CNC will maintain a fixed
        /// gap of <paramref name="distanceMm"/> mm between tool and workpiece surface.
        /// </summary>
        public void ActivateConstDistance(double distanceMm)
        {
            EnsureHandles();
            int raw = (int)(distanceMm * 10_000.0); // mm → 0.1 µm
            plcClient.WriteAny(handles![Identifier.Transition], (uint)DistanceControlCommand.OnConstDist);
            plcClient.WriteAny(handles![Identifier.Position],   raw);
            plcClient.WriteAny(handles![Identifier.Semaphore],  true);
        }

        /// <summary>
        /// Freezes the current distance control offset. The axis stops tracking the sensor;
        /// the last-applied offset remains active. Transitions to <see cref="DistanceControlState.Freeze"/>.
        /// </summary>
        public void Freeze()
        {
            EnsureHandles();
            plcClient.WriteAny(handles![Identifier.Transition], (uint)DistanceControlCommand.Freeze);
            plcClient.WriteAny(handles![Identifier.Semaphore],  true);
        }

        /// <summary>
        /// Deactivates distance control. The offset ramps to zero and the state transitions to
        /// <see cref="DistanceControlState.TurningOff"/>, then automatically to
        /// <see cref="DistanceControlState.Inactive"/> once the ramp completes.
        /// </summary>
        public void Deactivate()
        {
            EnsureHandles();
            plcClient.WriteAny(handles![Identifier.Transition], (uint)DistanceControlCommand.Off);
            plcClient.WriteAny(handles![Identifier.Semaphore],  true);
        }

        /// <summary>
        /// References the sensor zero point. Use when the sensor is not absolute and a known
        /// reference position must be latched. Transitions to <see cref="DistanceControlState.Inactive"/>
        /// after referencing completes.
        /// </summary>
        public void Reference(double refPositionMm)
        {
            EnsureHandles();
            int raw = (int)(refPositionMm * 10_000.0);
            plcClient.WriteAny(handles![Identifier.Transition], (uint)DistanceControlCommand.Reference);
            plcClient.WriteAny(handles![Identifier.Position],   raw);
            plcClient.WriteAny(handles![Identifier.Semaphore],  true);
        }

        /// <summary>
        /// Reads the current distance control state (0 = Inactive, 1 = Active, …).
        /// Requires <see cref="Enable"/> to have been called first.
        /// </summary>
        public DistanceControlState GetState()
        {
            EnsureHandles();
            return (DistanceControlState)plcClient.ReadAny<uint>(handles![Identifier.State]);
        }

        /// <summary>
        /// Reads the workpiece surface position currently measured by the sensor, in mm.
        /// Requires <see cref="Enable"/> to have been called first.
        /// </summary>
        public double GetActualPositionMm()
        {
            EnsureHandles();
            return plcClient.ReadAny<int>(handles![Identifier.ActualPosition]) / 10_000.0;
        }

        /// <summary>
        /// Reads the position offset currently being applied to the axis by the distance
        /// controller, in mm. Zero when inactive.
        /// Requires <see cref="Enable"/> to have been called first.
        /// </summary>
        public double GetActualOffsetMm()
        {
            EnsureHandles();
            return plcClient.ReadAny<int>(handles![Identifier.ActualOffset]) / 10_000.0;
        }

        public void Dispose()
        {
            if (handles is null) return;
            foreach (var h in handles.Values)
                try { plcClient.DeleteVariableHandle(h); } catch { /* ignore on dispose */ }
            handles.Clear();
        }

        private enum Identifier
        {
            Enable,
            Semaphore,
            Transition,
            Position,
            State,
            ActualPosition,
            ActualOffset,
        }
    }

    /// <summary>State machine states reported by the TF5200 distance control module.</summary>
    public enum DistanceControlState
    {
        /// <summary>Distance control is off; no offset is applied.</summary>
        Inactive            = 0,
        /// <summary>Active in SET_POS mode — axis is tracking the surface position.</summary>
        Active              = 1,
        /// <summary>Offset is frozen at its last value; sensor is no longer tracked.</summary>
        Freeze              = 2,
        /// <summary>Transitional: offset is ramping to zero before returning to Inactive.</summary>
        TurningOff          = 3,
        /// <summary>Active in constant-distance mode — axis maintains a fixed gap.</summary>
        ActiveConstDistance = 4,
        /// <summary>Error state — invalid command transition or position control error.</summary>
        Error               = 5,
        /// <summary>Dry-run constant-distance mode — no axis motion, for filter evaluation.</summary>
        DryrunConstDistance = 6,
        /// <summary>Dry-run SET_POS mode — no axis motion, for filter evaluation.</summary>
        DryrunSetPos        = 7,
    }

    /// <summary>Command values for the <c>command_w.transition</c> field of the distance control HLI.</summary>
    public enum DistanceControlCommand
    {
        /// <summary>Deactivate distance control; offset ramps to zero.</summary>
        Off        = 0,
        /// <summary>Activate in SET_POS mode.</summary>
        On         = 1,
        /// <summary>Freeze the current offset.</summary>
        Freeze     = 2,
        /// <summary>Reference the sensor zero point.</summary>
        Reference  = 3,
        /// <summary>Activate in constant-distance mode.</summary>
        OnConstDist = 4,
    }
}
