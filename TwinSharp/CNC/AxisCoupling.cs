using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Wraps the HLI axis coupling interface for a single CNC axis
    /// (<c>gpAx[n].lr_mc_control.axis_coupling</c>).
    ///
    /// Axis coupling (electronic gearing) makes a target axis follow one or more source axes
    /// according to a specified coupling factor.  Typical uses include tandem/gantry axes and
    /// secondary cutting heads (MultiJet).
    ///
    /// <para>
    /// <b>Precondition:</b> all involved axes must be at standstill (velocity = 0) before calling
    /// <see cref="SetCouplings"/> or <see cref="RemoveCouplings"/>.  The CNC will raise error 70200
    /// if this is violated.
    /// </para>
    ///
    /// <para>
    /// <b>NC programmability:</b> if the coupled target axis also needs to respond to motion
    /// commands in the NC program, include a self-coupling entry in the descriptor list
    /// (source axis = target axis, mode = <see cref="AxisCouplingMode.Direct"/>).
    /// </para>
    ///
    /// Semaphore handshake: after writing descriptors, <c>semaphor_rw</c> is set to <c>true</c>.
    /// The CNC latches the command and clears the semaphore automatically.
    /// </summary>
    public class AxisCoupling : IDisposable
    {
        private readonly uint      axisNumber;
        private readonly AdsClient plcClient;
        private Dictionary<Identifier, uint>? handles;

        internal AxisCoupling(uint axisNumber, AdsClient plcClient)
        {
            this.axisNumber = axisNumber;
            this.plcClient  = plcClient;
        }

        private void EnsureHandles()
        {
            if (handles is not null) return;
            handles = CreateHandles(plcClient, axisNumber);
        }

        private static Dictionary<Identifier, uint> CreateHandles(AdsClient plcClient, uint axisNumber)
        {
            var d = new Dictionary<Identifier, uint>();
            string pfx = $"HLI_Global_Variables.gpAx[{axisNumber - 1}]^.lr_mc_control.axis_coupling";

            d[Identifier.Enable]     = plcClient.CreateVariableHandle(pfx + ".enable_w");

            // Descriptor slots 0–3 (HLI_AX_COUPLING_MAXIDX = 3)
            for (int i = 0; i <= HliConstants.HLI_AX_COUPLING_MAXIDX; i++)
            {
                string dp = $"{pfx}.command_w.desc[{i}]";
                d[(Identifier)(i * 4 + (int)Identifier.Desc0_AxNr)]      = plcClient.CreateVariableHandle(dp + ".ax_nr");
                d[(Identifier)(i * 4 + (int)Identifier.Desc0_Mode)]      = plcClient.CreateVariableHandle(dp + ".mode");
                d[(Identifier)(i * 4 + (int)Identifier.Desc0_FractNum)]  = plcClient.CreateVariableHandle(dp + ".fract_num");
                d[(Identifier)(i * 4 + (int)Identifier.Desc0_FractDenom)]= plcClient.CreateVariableHandle(dp + ".fract_denom");
            }

            d[Identifier.Semaphore]  = plcClient.CreateVariableHandle(pfx + ".command_w.semaphor_rw");
            d[Identifier.ActiveState]= plcClient.CreateVariableHandle(pfx + ".state_r.active_r");

            return d;
        }

        /// <summary>
        /// Signals to the CNC that this HLI interface is present.
        /// Must be called once after connect before any coupling commands will be accepted.
        /// </summary>
        public void Enable()
        {
            EnsureHandles();
            plcClient.WriteAny(handles![Identifier.Enable], true);
        }

        /// <summary>
        /// Activates axis coupling with the specified descriptor list.
        /// Writes each entry's fields, writes <see cref="AxisCouplingMode.Inactive"/> as the
        /// terminator immediately after the last entry, then pulses the semaphore.
        /// </summary>
        /// <param name="specs">
        /// Ordered list of coupling descriptors. Must not exceed
        /// <see cref="HliConstants.HLI_AX_COUPLING_MAXIDX"/> + 1 entries (= 4).
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="specs"/> has more than 4 entries (no room for the terminator).
        /// </exception>
        public void SetCouplings(AxisCouplingEntry[] specs)
        {
            if (specs.Length > HliConstants.HLI_AX_COUPLING_MAXIDX + 1)
                throw new ArgumentOutOfRangeException(nameof(specs),
                    $"Maximum {HliConstants.HLI_AX_COUPLING_MAXIDX + 1} coupling descriptors allowed.");

            EnsureHandles();

            for (int i = 0; i < specs.Length; i++)
            {
                var e = specs[i];
                // ax_nr and mode are PLC INT (16-bit signed); fract fields are already short.
                plcClient.WriteAny(handles![(Identifier)(i * 4 + (int)Identifier.Desc0_AxNr)],      (short)e.SourceAxisNumber);
                plcClient.WriteAny(handles![(Identifier)(i * 4 + (int)Identifier.Desc0_Mode)],      (short)(int)e.Mode);
                plcClient.WriteAny(handles![(Identifier)(i * 4 + (int)Identifier.Desc0_FractNum)],  e.FractNumerator);
                plcClient.WriteAny(handles![(Identifier)(i * 4 + (int)Identifier.Desc0_FractDenom)],e.FractDenominator);
            }

            // Terminator: only the mode field needs to be INACTIVE; the CNC stops reading there.
            plcClient.WriteAny(handles![(Identifier)(specs.Length * 4 + (int)Identifier.Desc0_Mode)], (short)AxisCouplingMode.Inactive);

            plcClient.WriteAny(handles![Identifier.Semaphore], true);
        }

        /// <summary>
        /// Removes all couplings from this axis by writing a single INACTIVE descriptor
        /// at index 0 and pulsing the semaphore.
        /// All involved axes must be at standstill.
        /// </summary>
        public void RemoveCouplings()
        {
            EnsureHandles();
            plcClient.WriteAny(handles![(Identifier)((int)Identifier.Desc0_Mode)], (short)AxisCouplingMode.Inactive);
            plcClient.WriteAny(handles![Identifier.Semaphore], true);
        }

        /// <summary>
        /// Returns <c>true</c> when axis coupling is currently active on this axis
        /// (<c>state_r.active_r</c>).
        /// </summary>
        public bool IsActive()
        {
            EnsureHandles();
            return plcClient.ReadAny<bool>(handles![Identifier.ActiveState]);
        }

        public void Dispose()
        {
            if (handles is null) return;
            foreach (var h in handles.Values)
                try { plcClient.DeleteVariableHandle(h); } catch { /* ignore on dispose */ }
            handles.Clear();
        }

        // Each descriptor slot occupies 4 consecutive enum values starting at Desc0_AxNr.
        // Slot i uses identifiers (i*4 + Desc0_AxNr) through (i*4 + Desc0_FractDenom).
        private enum Identifier
        {
            Desc0_AxNr       = 0,
            Desc0_Mode       = 1,
            Desc0_FractNum   = 2,
            Desc0_FractDenom = 3,
            // Desc1 = 4..7, Desc2 = 8..11, Desc3 = 12..15 (computed at runtime)
            Semaphore        = 16,
            ActiveState      = 17,
            Enable           = 18,
        }
    }

    /// <summary>Coupling mode for one descriptor entry in an axis coupling specification.</summary>
    public enum AxisCouplingMode
    {
        /// <summary>Marks the end of the descriptor list. All following entries are also inactive.</summary>
        Inactive = 0,
        /// <summary>Coupling factor = 0. The axis is held stationary; it can still be a source for others.</summary>
        Zero     = 1,
        /// <summary>Coupling factor = 1:1. The target follows the source 1:1.</summary>
        Direct   = 2,
        /// <summary>Coupling factor = −1:1. The target mirrors the source.</summary>
        Mirror   = 3,
        /// <summary>Coupling factor = <c>FractNumerator / FractDenominator</c>.</summary>
        Fract    = 4,
    }

    /// <summary>
    /// Describes one source-axis relationship in an axis coupling command.
    /// Pass an array of these to <see cref="AxisCoupling.SetCouplings"/>.
    /// </summary>
    public readonly struct AxisCouplingEntry
    {
        /// <summary>Logical axis number of the source axis (1-based).</summary>
        public int              SourceAxisNumber { get; init; }
        /// <summary>Coupling mode.</summary>
        public AxisCouplingMode Mode             { get; init; }
        /// <summary>Numerator of the coupling factor. Only used when <see cref="Mode"/> is <see cref="AxisCouplingMode.Fract"/>.</summary>
        public short            FractNumerator   { get; init; }
        /// <summary>Denominator of the coupling factor. Must not be 0. Only used when <see cref="Mode"/> is <see cref="AxisCouplingMode.Fract"/>.</summary>
        public short            FractDenominator { get; init; }
    }
}
