using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Wraps the axis control commands of the HLI <c>lr_mc_control</c> control unit for a single CNC axis
    /// (<c>gpAx[n].lr_mc_control.*</c>), as described in section "Axis control commands" of the HLI documentation.
    ///
    /// <para>
    /// Every command in this class is an MC control unit with four elements:
    /// <list type="bullet">
    ///   <item><description><c>enable_w</c> — the PLC declares that it uses this control unit.
    ///   <b>The unit has no effect at all until this is TRUE</b>, so call the matching
    ///   <c>...Enable</c> setter (or <see cref="EnableAll"/>) once after connecting.</description></item>
    ///   <item><description><c>command_w</c> — the value commanded by the PLC.</description></item>
    ///   <item><description><c>request_r</c> — the value requested by a GUI communicating directly with the CNC.
    ///   The PLC may accept the request by copying it into <c>command_w</c>, or ignore it.</description></item>
    ///   <item><description><c>state_r</c> — the CNC acknowledgement, used to check whether the command took effect.</description></item>
    /// </list>
    /// </para>
    ///
    /// <para>
    /// Other parts of <c>lr_mc_control</c> live in their own classes:
    /// <see cref="AxisCoupling"/> (<c>axis_coupling</c>), <see cref="DistanceControl"/> (<c>distance_control</c>)
    /// and <see cref="ExternalAxisCommanding"/> (<c>add_cmd_values</c>).
    /// The axis control commands that sit under <c>ipo_mc_control</c> (feedhold, override, reduced velocity, …)
    /// are not part of this class.
    /// </para>
    ///
    /// <para>
    /// <b>Precondition:</b> <see cref="CncAxis.PlcPresent"/> must be set to <c>true</c> before the CNC
    /// accepts any HLI command for the axis.
    /// </para>
    /// </summary>
    public class AxisControlCommands : IDisposable
    {
        private readonly uint      axisNumber;
        private readonly AdsClient plcClient;
        private Dictionary<int, uint>? handles;

        internal AxisControlCommands(uint axisNumber, AdsClient plcClient)
        {
            this.axisNumber = axisNumber;
            this.plcClient  = plcClient;
            // Handles are created lazily so an axis that is never commanded from C#
            // does not pay for 56 symbol lookups at connect time.
        }

        // HLI element names, indexed by ControlUnit. Order must match the ControlUnit enum.
        private static readonly string[] UnitNames =
        {
            "release_feedhold",
            "drive_on",
            "torque_permission",
            "reference_cam",
            "follow_up",
            "gantry_on",
            "set_reference_position",
            "refpos_position",
            "clear_reference_position",
            "disable_pos_lag_mon",
            "probing_signal",
            "probing_position",
            "deactivate_axis",
            "timestamp",
        };

        private void EnsureHandles()
        {
            if (handles is not null) return;
            handles = CreateHandles(plcClient, axisNumber);
        }

        private static Dictionary<int, uint> CreateHandles(AdsClient plcClient, uint axisNumber)
        {
            var d = new Dictionary<int, uint>();
            string pfx = $"HLI_Global_Variables.gpAx[{axisNumber - 1}]^.lr_mc_control";

            for (int u = 0; u < UnitNames.Length; u++)
            {
                string up = $"{pfx}.{UnitNames[u]}";
                d[Key((ControlUnit)u, Element.Enable)]  = plcClient.CreateVariableHandle(up + ".enable_w");
                d[Key((ControlUnit)u, Element.Command)] = plcClient.CreateVariableHandle(up + ".command_w");
                d[Key((ControlUnit)u, Element.Request)] = plcClient.CreateVariableHandle(up + ".request_r");
                d[Key((ControlUnit)u, Element.State)]   = plcClient.CreateVariableHandle(up + ".state_r");
            }

            return d;
        }

        /// <summary>
        /// Declares to the CNC that the PLC uses <i>all</i> axis control commands of this axis
        /// by setting every <c>enable_w</c> to <c>true</c>.
        /// Creates the ADS variable handles on first call — this is where AdsErrorCode 1808 will surface
        /// if the HLI is not available on the target.
        /// </summary>
        public void EnableAll()
        {
            EnsureHandles();
            for (int u = 0; u < UnitNames.Length; u++)
                plcClient.WriteAny(handles![Key((ControlUnit)u, Element.Enable)], true);
        }

        #region Feed rate enable (release_feedhold)

        /// <summary>
        /// Declares that the PLC uses the axis-specific feed rate enable control unit.
        /// Writes <c>release_feedhold.enable_w</c>.
        /// </summary>
        public bool FeedEnableEnable { set => Write(ControlUnit.ReleaseFeedhold, Element.Enable, value); }

        /// <summary>
        /// Axis-specific feed rate enable. Must be set for every axis that is to be moved,
        /// otherwise no path motion occurs.
        /// <c>true</c> = drive enable, <c>false</c> = drive STOP. On a <c>true</c> → <c>false</c> transition
        /// the drive is shut down in compliance with the acceleration parameters.
        /// With SERCOS drives this is derived from bit 13 of the control word.
        /// Writes <c>release_feedhold.command_w</c>.
        /// </summary>
        public bool FeedEnableCommand
        {
            get => Read<bool>(ControlUnit.ReleaseFeedhold, Element.Command);
            set => Write(ControlUnit.ReleaseFeedhold, Element.Command, value);
        }

        /// <summary>Feed rate enable requested by a GUI. Reads <c>release_feedhold.request_r</c>.</summary>
        public bool FeedEnableRequest => Read<bool>(ControlUnit.ReleaseFeedhold, Element.Request);

        /// <summary>
        /// CNC acknowledgement of the feed rate enable.
        /// <c>true</c> = drive enable, <c>false</c> = drive STOP. Reads <c>release_feedhold.state_r</c>.
        /// </summary>
        public bool FeedEnableState => Read<bool>(ControlUnit.ReleaseFeedhold, Element.State);

        #endregion

        #region Drive ON (drive_on)

        /// <summary>
        /// Declares that the PLC uses the drive ON control unit. Writes <c>drive_on.enable_w</c>.
        /// </summary>
        public bool DriveOnEnable { set => Write(ControlUnit.DriveOn, Element.Enable, value); }

        /// <summary>
        /// Drive ON. <c>true</c> = drive ON, <c>false</c> = drive OFF. On a <c>true</c> → <c>false</c>
        /// transition the drive is shut down in the best possible manner.
        /// With SERCOS drives this is derived from bit 15 of the control word.
        /// Writes <c>drive_on.command_w</c>.
        /// </summary>
        public bool DriveOnCommand
        {
            get => Read<bool>(ControlUnit.DriveOn, Element.Command);
            set => Write(ControlUnit.DriveOn, Element.Command, value);
        }

        /// <summary>Drive ON requested by a GUI. Reads <c>drive_on.request_r</c>.</summary>
        public bool DriveOnRequest => Read<bool>(ControlUnit.DriveOn, Element.Request);

        /// <summary>
        /// CNC acknowledgement of drive ON. <c>true</c> = drive ON, <c>false</c> = drive OFF.
        /// Reads <c>drive_on.state_r</c>.
        /// </summary>
        public bool DriveOnState => Read<bool>(ControlUnit.DriveOn, Element.State);

        #endregion

        #region Controller enable (torque_permission)

        /// <summary>
        /// Declares that the PLC uses the controller enable control unit. Writes <c>torque_permission.enable_w</c>.
        /// </summary>
        public bool ControllerEnableEnable { set => Write(ControlUnit.TorquePermission, Element.Enable, value); }

        /// <summary>
        /// Controller enable — axis-specific torque connection.
        /// <c>true</c> = torque connection, <c>false</c> = drive is torque-free.
        /// With SERCOS drives this is derived from bit 14 of the control word.
        /// Writes <c>torque_permission.command_w</c>.
        /// </summary>
        public bool ControllerEnableCommand
        {
            get => Read<bool>(ControlUnit.TorquePermission, Element.Command);
            set => Write(ControlUnit.TorquePermission, Element.Command, value);
        }

        /// <summary>Controller enable requested by a GUI. Reads <c>torque_permission.request_r</c>.</summary>
        public bool ControllerEnableRequest => Read<bool>(ControlUnit.TorquePermission, Element.Request);

        /// <summary>CNC acknowledgement of the controller enable. Reads <c>torque_permission.state_r</c>.</summary>
        public bool ControllerEnableState => Read<bool>(ControlUnit.TorquePermission, Element.State);

        #endregion

        #region Reference cam (reference_cam)

        /// <summary>
        /// Declares that the PLC uses the reference cam control unit. Writes <c>reference_cam.enable_w</c>.
        /// </summary>
        public bool ReferenceCamEnable { set => Write(ControlUnit.ReferenceCam, Element.Enable, value); }

        /// <summary>
        /// Reference cam signal when homing.
        /// <c>true</c> = reference cam switched, <c>false</c> = reference cam not switched.
        /// Only acts if <c>lr_hw[i].cam_direct_access</c> (P-AXIS-00036) is 0 in the axis parameter list.
        /// <c>lr_hw[i].cam_level</c> (P-AXIS-00038) can invert this from high-active to low-active.
        /// Writes <c>reference_cam.command_w</c>.
        /// </summary>
        public bool ReferenceCamCommand
        {
            get => Read<bool>(ControlUnit.ReferenceCam, Element.Command);
            set => Write(ControlUnit.ReferenceCam, Element.Command, value);
        }

        /// <summary>Reference cam requested by a GUI. Reads <c>reference_cam.request_r</c>.</summary>
        public bool ReferenceCamRequest => Read<bool>(ControlUnit.ReferenceCam, Element.Request);

        /// <summary>CNC acknowledgement of the reference cam signal. Reads <c>reference_cam.state_r</c>.</summary>
        public bool ReferenceCamState => Read<bool>(ControlUnit.ReferenceCam, Element.State);

        #endregion

        #region Tracking mode (follow_up)

        /// <summary>
        /// Declares that the PLC uses the tracking mode control unit. Writes <c>follow_up.enable_w</c>.
        /// </summary>
        public bool TrackingModeEnable { set => Write(ControlUnit.FollowUp, Element.Enable, value); }

        /// <summary>
        /// Tracking mode. While <c>true</c> the control loop is open: the command value is set identical to
        /// the actual value read in, so the position lag becomes 0.
        /// Beware that an external force (for example the weight of the axis) can then slowly drift the axis position.
        /// Writes <c>follow_up.command_w</c>.
        /// </summary>
        public bool TrackingModeCommand
        {
            get => Read<bool>(ControlUnit.FollowUp, Element.Command);
            set => Write(ControlUnit.FollowUp, Element.Command, value);
        }

        /// <summary>Tracking mode requested by a GUI. Reads <c>follow_up.request_r</c>.</summary>
        public bool TrackingModeRequest => Read<bool>(ControlUnit.FollowUp, Element.Request);

        /// <summary>CNC acknowledgement of tracking mode. Reads <c>follow_up.state_r</c>.</summary>
        public bool TrackingModeState => Read<bool>(ControlUnit.FollowUp, Element.State);

        #endregion

        #region Run out gantry difference (gantry_on)

        /// <summary>
        /// Declares that the PLC uses the gantry difference runout control unit. Writes <c>gantry_on.enable_w</c>.
        /// </summary>
        public bool GantryDifferenceRunoutEnable { set => Write(ControlUnit.GantryOn, Element.Enable, value); }

        /// <summary>
        /// Run out gantry difference. If the axis is a gantry slave axis and both master and slave are
        /// referenced, the gantry difference is run out. <c>true</c> = runout permitted.
        /// Writes <c>gantry_on.command_w</c>.
        /// </summary>
        public bool GantryDifferenceRunoutCommand
        {
            get => Read<bool>(ControlUnit.GantryOn, Element.Command);
            set => Write(ControlUnit.GantryOn, Element.Command, value);
        }

        /// <summary>Gantry difference runout requested by a GUI. Reads <c>gantry_on.request_r</c>.</summary>
        public bool GantryDifferenceRunoutRequest => Read<bool>(ControlUnit.GantryOn, Element.Request);

        /// <summary>CNC acknowledgement of the gantry difference runout. Reads <c>gantry_on.state_r</c>.</summary>
        public bool GantryDifferenceRunoutState => Read<bool>(ControlUnit.GantryOn, Element.State);

        #endregion

        #region Accept reference position (set_reference_position)

        /// <summary>
        /// Declares that the PLC uses the accept reference position control unit.
        /// Writes <c>set_reference_position.enable_w</c>.
        /// </summary>
        public bool SetReferencePositionEnable { set => Write(ControlUnit.SetReferencePosition, Element.Enable, value); }

        /// <summary>
        /// Accept the reference position and mark the axis as referenced on a <b>rising edge</b>
        /// (<c>false</c> → <c>true</c>). The value the actual axis position is set to depends on P-AXIS-00278:
        /// <list type="bullet">
        ///   <item><description><c>ABSOLUT</c> — value of P-AXIS-00152.</description></item>
        ///   <item><description><c>OFFSET</c> — drive encoder position + P-AXIS-00279.</description></item>
        ///   <item><description><c>PLC</c> — value of <see cref="ReferencePositionCommand"/>.</description></item>
        ///   <item><description><c>PLC_OFFSET</c> — drive encoder position + <see cref="ReferencePositionCommand"/>.</description></item>
        /// </list>
        /// Works for axes with an absolute measuring system too.
        /// Writes <c>set_reference_position.command_w</c>.
        /// </summary>
        public bool SetReferencePositionCommand
        {
            get => Read<bool>(ControlUnit.SetReferencePosition, Element.Command);
            set => Write(ControlUnit.SetReferencePosition, Element.Command, value);
        }

        /// <summary>Acceptance of the reference position requested by a GUI. Reads <c>set_reference_position.request_r</c>.</summary>
        public bool SetReferencePositionRequest => Read<bool>(ControlUnit.SetReferencePosition, Element.Request);

        /// <summary>
        /// Indicates whether the reference position was set manually and the coordinate system was shifted
        /// as a result. Cleared by CNC-controlled homing (G74). Reads <c>set_reference_position.state_r</c>.
        /// </summary>
        public bool SetReferencePositionState => Read<bool>(ControlUnit.SetReferencePosition, Element.State);

        #endregion

        #region Reference position to be set (refpos_position)

        /// <summary>
        /// Declares that the PLC uses the reference position control unit. Writes <c>refpos_position.enable_w</c>.
        /// </summary>
        public bool ReferencePositionEnable { set => Write(ControlUnit.RefposPosition, Element.Enable, value); }

        /// <summary>
        /// Reference position to be set, used by <see cref="SetReferencePositionCommand"/> when P-AXIS-00278
        /// is <c>PLC</c> (this value is the setting position) or <c>PLC_OFFSET</c> (drive encoder position + this value).
        /// Unit: 0.1 µm or 10⁻⁴ °.
        /// Writes <c>refpos_position.command_w</c>.
        /// </summary>
        public int ReferencePositionCommand
        {
            get => Read<int>(ControlUnit.RefposPosition, Element.Command);
            set => Write(ControlUnit.RefposPosition, Element.Command, value);
        }

        /// <summary>
        /// Reference position requested by a GUI. Unit: 0.1 µm or 10⁻⁴ °.
        /// Reads <c>refpos_position.request_r</c>.
        /// </summary>
        public int ReferencePositionRequest => Read<int>(ControlUnit.RefposPosition, Element.Request);

        /// <summary>
        /// CNC acknowledgement of the reference position. Unit: 0.1 µm or 10⁻⁴ °.
        /// Reads <c>refpos_position.state_r</c>.
        /// </summary>
        public int ReferencePositionState => Read<int>(ControlUnit.RefposPosition, Element.State);

        #endregion

        #region Clear referencing (clear_reference_position)

        /// <summary>
        /// Declares that the PLC uses the clear referencing control unit.
        /// Writes <c>clear_reference_position.enable_w</c>.
        /// </summary>
        public bool ClearReferencePositionEnable { set => Write(ControlUnit.ClearReferencePosition, Element.Enable, value); }

        /// <summary>
        /// Clears the referenced state of an axis that was homed by setting the reference position or by G74.
        /// As long as this is <c>true</c> the axis is regarded as not referenced — which also temporarily
        /// switches off the software limit switches — and it can be marked as referenced again via
        /// <see cref="SetReferencePositionCommand"/>. Any offset activated by "set reference position" is not cleared.
        /// Writes <c>clear_reference_position.command_w</c>.
        /// </summary>
        public bool ClearReferencePositionCommand
        {
            get => Read<bool>(ControlUnit.ClearReferencePosition, Element.Command);
            set => Write(ControlUnit.ClearReferencePosition, Element.Command, value);
        }

        /// <summary>Clearing of the referencing requested by a GUI. Reads <c>clear_reference_position.request_r</c>.</summary>
        public bool ClearReferencePositionRequest => Read<bool>(ControlUnit.ClearReferencePosition, Element.Request);

        /// <summary>
        /// Indicates whether the axis is currently not referenced. Reads <c>clear_reference_position.state_r</c>.
        /// </summary>
        public bool ClearReferencePositionState => Read<bool>(ControlUnit.ClearReferencePosition, Element.State);

        #endregion

        #region Disable position lag monitoring (disable_pos_lag_mon)

        /// <summary>
        /// Declares that the PLC uses the position lag monitoring control unit.
        /// Writes <c>disable_pos_lag_mon.enable_w</c>.
        /// </summary>
        public bool DisablePositionLagMonitoringEnable { set => Write(ControlUnit.DisablePosLagMon, Element.Enable, value); }

        /// <summary>
        /// Suppresses / disables position lag monitoring while <c>true</c>.
        /// Writes <c>disable_pos_lag_mon.command_w</c>.
        /// </summary>
        public bool DisablePositionLagMonitoringCommand
        {
            get => Read<bool>(ControlUnit.DisablePosLagMon, Element.Command);
            set => Write(ControlUnit.DisablePosLagMon, Element.Command, value);
        }

        /// <summary>Suppression of position lag monitoring requested by a GUI. Reads <c>disable_pos_lag_mon.request_r</c>.</summary>
        public bool DisablePositionLagMonitoringRequest => Read<bool>(ControlUnit.DisablePosLagMon, Element.Request);

        /// <summary>CNC acknowledgement of the suppressed position lag monitoring. Reads <c>disable_pos_lag_mon.state_r</c>.</summary>
        public bool DisablePositionLagMonitoringState => Read<bool>(ControlUnit.DisablePosLagMon, Element.State);

        #endregion

        #region Probing signal (probing_signal)

        /// <summary>
        /// Declares that the PLC uses the probing signal control unit. Writes <c>probing_signal.enable_w</c>.
        /// </summary>
        public bool ProbingSignalEnable { set => Write(ControlUnit.ProbingSignal, Element.Enable, value); }

        /// <summary>
        /// Transfers the probing signal to the CNC. Requires <c>kenngr.measure.signal</c> (P-AXIS-00516)
        /// to be set to <c>PLC</c> or <c>PLC_TIMESTAMP</c> in the axis parameter list.
        /// The measured value is accepted on the edge parameterised in <c>kenngr.mess_neg_flanke</c> (P-AXIS-00518).
        /// Writes <c>probing_signal.command_w</c>.
        /// </summary>
        public bool ProbingSignalCommand
        {
            get => Read<bool>(ControlUnit.ProbingSignal, Element.Command);
            set => Write(ControlUnit.ProbingSignal, Element.Command, value);
        }

        /// <summary>Probing signal requested by a GUI. Reads <c>probing_signal.request_r</c>.</summary>
        public bool ProbingSignalRequest => Read<bool>(ControlUnit.ProbingSignal, Element.Request);

        /// <summary>CNC acknowledgement of the probing signal. Reads <c>probing_signal.state_r</c>.</summary>
        public bool ProbingSignalState => Read<bool>(ControlUnit.ProbingSignal, Element.State);

        #endregion

        #region Measured value (probing_position)

        /// <summary>
        /// Declares that the PLC uses the measured value control unit. Writes <c>probing_position.enable_w</c>.
        /// </summary>
        public bool ProbingPositionEnable { set => Write(ControlUnit.ProbingPosition, Element.Enable, value); }

        /// <summary>
        /// Measured value transferred via the HLI, used when P-AXIS-00257 or <c>#MEAS [...SIGNAL=PLC]</c>
        /// switched the probing signal source to <see cref="ProbingSignalCommand"/>.
        /// The value is passed to the decoder unchanged. If this control unit is <i>not</i> enabled, the actual
        /// axis position on the rising edge of <see cref="ProbingSignalCommand"/> is used instead.
        /// Unit: 0.1 µm or 10⁻⁴ °.
        /// Writes <c>probing_position.command_w</c>.
        /// </summary>
        public int ProbingPositionCommand
        {
            get => Read<int>(ControlUnit.ProbingPosition, Element.Command);
            set => Write(ControlUnit.ProbingPosition, Element.Command, value);
        }

        /// <summary>Measured value requested by a GUI. Unit: 0.1 µm or 10⁻⁴ °. Reads <c>probing_position.request_r</c>.</summary>
        public int ProbingPositionRequest => Read<int>(ControlUnit.ProbingPosition, Element.Request);

        /// <summary>CNC acknowledgement of the measured value. Unit: 0.1 µm or 10⁻⁴ °. Reads <c>probing_position.state_r</c>.</summary>
        public int ProbingPositionState => Read<int>(ControlUnit.ProbingPosition, Element.State);

        #endregion

        #region Deactivate axis / park (deactivate_axis)

        /// <summary>
        /// Declares that the PLC uses the axis deactivation control unit. Writes <c>deactivate_axis.enable_w</c>.
        /// </summary>
        public bool DeactivateAxisEnable { set => Write(ControlUnit.DeactivateAxis, Element.Enable, value); }

        /// <summary>
        /// Deactivates (parks) the axis. While <c>true</c> the NC kernel does not signal drive errors,
        /// does not transfer HLI control bits to the drive, and a CNC reset does not trigger a drive reset.
        /// Moving a parked axis produces error P-ERR-70265.
        /// On PROFIDRIVE drives bit 0x80 is set in control word 2 (STW2).
        /// Writes <c>deactivate_axis.command_w</c>.
        /// </summary>
        public bool DeactivateAxisCommand
        {
            get => Read<bool>(ControlUnit.DeactivateAxis, Element.Command);
            set => Write(ControlUnit.DeactivateAxis, Element.Command, value);
        }

        /// <summary>Axis deactivation requested by a GUI. Reads <c>deactivate_axis.request_r</c>.</summary>
        public bool DeactivateAxisRequest => Read<bool>(ControlUnit.DeactivateAxis, Element.Request);

        /// <summary>CNC acknowledgement of the axis deactivation. Reads <c>deactivate_axis.state_r</c>.</summary>
        public bool DeactivateAxisState => Read<bool>(ControlUnit.DeactivateAxis, Element.State);

        #endregion

        #region Timestamp (timestamp)

        /// <summary>
        /// Declares that the PLC uses the timestamp control unit. Writes <c>timestamp.enable_w</c>.
        /// </summary>
        public bool TimestampEnable { set => Write(ControlUnit.Timestamp, Element.Enable, value); }

        /// <summary>
        /// Transfers the timestamp of a digital input terminal to the CNC. Unit: ns.
        /// When used for "measure with distributed clocks timestamp", <see cref="ProbingSignalEnable"/>
        /// must be set as well. Available as of CNC Build V3.01.3079.28.
        /// Writes <c>timestamp.command_w</c>.
        /// </summary>
        public long TimestampCommand
        {
            get => Read<long>(ControlUnit.Timestamp, Element.Command);
            set => Write(ControlUnit.Timestamp, Element.Command, value);
        }

        /// <summary>Timestamp requested by a GUI. Unit: ns. Reads <c>timestamp.request_r</c>.</summary>
        public long TimestampRequest => Read<long>(ControlUnit.Timestamp, Element.Request);

        /// <summary>CNC acknowledgement of the timestamp. Unit: ns. Reads <c>timestamp.state_r</c>.</summary>
        public long TimestampState => Read<long>(ControlUnit.Timestamp, Element.State);

        #endregion

        private void Write(ControlUnit unit, Element element, object value)
        {
            EnsureHandles();
            plcClient.WriteAny(handles![Key(unit, element)], value);
        }

        private T Read<T>(ControlUnit unit, Element element)
        {
            EnsureHandles();
            return plcClient.ReadAny<T>(handles![Key(unit, element)]);
        }

        /// <summary>
        /// Disposes of the ADS variable handles.
        /// </summary>
        public void Dispose()
        {
            if (handles is null) return;
            foreach (var h in handles.Values)
                try { plcClient.DeleteVariableHandle(h); } catch { /* ignore on dispose */ }
            handles.Clear();

            GC.SuppressFinalize(this);
        }

        // Each control unit occupies 4 consecutive handle keys, one per MC control unit element.
        private static int Key(ControlUnit unit, Element element) => (int)unit * 4 + (int)element;

        // Order must match UnitNames.
        private enum ControlUnit
        {
            ReleaseFeedhold        = 0,
            DriveOn                = 1,
            TorquePermission       = 2,
            ReferenceCam           = 3,
            FollowUp               = 4,
            GantryOn               = 5,
            SetReferencePosition   = 6,
            RefposPosition         = 7,
            ClearReferencePosition = 8,
            DisablePosLagMon       = 9,
            ProbingSignal          = 10,
            ProbingPosition        = 11,
            DeactivateAxis         = 12,
            Timestamp              = 13,
        }

        private enum Element
        {
            Enable  = 0,
            Command = 1,
            Request = 2,
            State   = 3,
        }
    }
}
