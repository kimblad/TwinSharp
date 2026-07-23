using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Allows insertion of CNC stop marks into a running NC program at runtime via the HLI
    /// <c>bahn_mc_control.insert_cmd</c> control unit.
    /// <para>
    /// In POSITION mode (logicalAxisNumber &gt; 0 in <see cref="InsertAtPosition"/>) the CNC
    /// stops the first time the specified axis reaches the commanded position. The stop behaves
    /// like M00 — the operator presses Continue to resume.
    /// </para>
    /// <para>Requires <c>P-STUP-00033</c> (fb_storage_size) to be non-zero on the machine.</para>
    /// </summary>
    public class InsertStopCommand : IDisposable
    {
        readonly AdsClient plcClient;
        readonly Dictionary<Identifier, uint> handles;

        internal InsertStopCommand(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            handles = CreateHandles(plcClient, channelNumber);
        }

        private static Dictionary<Identifier, uint> CreateHandles(AdsClient plcClient, int channelNumber)
        {
            var d = new Dictionary<Identifier, uint>();
            string pfx = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.bahn_mc_control.insert_cmd";

            d[Identifier.Enable]      = plcClient.CreateVariableHandle(pfx + ".enable_w");
            d[Identifier.Semaphore]   = plcClient.CreateVariableHandle(pfx + ".command_semaphor_rw");
            d[Identifier.DistOrPos]   = plcClient.CreateVariableHandle(pfx + ".command_w.dist_or_pos");
            d[Identifier.RelAbsMode]  = plcClient.CreateVariableHandle(pfx + ".command_w.rel_abs_mode");
            d[Identifier.AxisNr]      = plcClient.CreateVariableHandle(pfx + ".command_w.axis_nr");
            d[Identifier.MFunctionNr] = plcClient.CreateVariableHandle(pfx + ".command_w.m_function_nr");
            d[Identifier.StateValue]    = plcClient.CreateVariableHandle(pfx + ".state_r.state");
            d[Identifier.DistOfNextStop] = plcClient.CreateVariableHandle(pfx + ".state_r.distance_of_next_stop");

            return d;
        }

        /// <summary>
        /// Signals to the CNC that this interface is present. Must be called once after connect
        /// before any insert commands will be acknowledged.
        /// </summary>
        public void Enable() =>
            plcClient.WriteAny(handles[Identifier.Enable], true);

        /// <summary>
        /// Inserts a one-shot absolute stop (M00) the first time <paramref name="logicalAxisNumber"/>
        /// reaches <paramref name="positionMm"/>. If the program is already past that position the
        /// stop is triggered immediately.
        /// </summary>
        /// <param name="positionMm">Target axis position in mm.</param>
        /// <param name="logicalAxisNumber">Logical axis number (&gt; 0). Use the X-axis logical
        /// number for a marker-based stop.</param>
        public void InsertAtPosition(double positionMm, uint logicalAxisNumber)
        {
            if (logicalAxisNumber == 0)
                throw new ArgumentException("Logical axis number must be > 0 for POSITION mode.", nameof(logicalAxisNumber));

            double distOrPos = positionMm * 10_000.0; // mm → 0.1 µm

            plcClient.WriteAny(handles[Identifier.DistOrPos],   distOrPos);
            plcClient.WriteAny(handles[Identifier.RelAbsMode],  (ushort)1); // absolute one-shot (TwinCAT UINT = 2 bytes)
            plcClient.WriteAny(handles[Identifier.AxisNr],      (ushort)logicalAxisNumber);
            plcClient.WriteAny(handles[Identifier.MFunctionNr], (ushort)0); // M00

            // Signal new command to the CNC.
            plcClient.WriteAny(handles[Identifier.Semaphore], true);
        }

        /// <summary>
        /// Inserts a one-shot M00 stop after the CNC's path interpolator has traveled
        /// <paramref name="remainingMm"/> more mm from its current position.
        /// Uses DISTANCE mode (<c>axis_nr = 0</c>) with <c>rel_abs_mode = 2</c>
        /// (one-off relative distance from the current interpolator position).
        /// </summary>
        /// <param name="remainingMm">Distance to travel before stopping, in mm.</param>
        public void InsertAtRelativeDistance(double remainingMm)
        {
            // Re-assert enable in case the PLC application has reset it since connect.
            plcClient.WriteAny(handles[Identifier.Enable], true);

            double distRaw = remainingMm * 10_000.0; // mm → 0.1 µm

            plcClient.WriteAny(handles[Identifier.DistOrPos],   distRaw);
            plcClient.WriteAny(handles[Identifier.RelAbsMode],  (ushort)2); // one-off relative distance
            plcClient.WriteAny(handles[Identifier.AxisNr],      (ushort)0); // axis_nr = 0 → DISTANCE mode
            plcClient.WriteAny(handles[Identifier.MFunctionNr], (ushort)0); // M00

            plcClient.WriteAny(handles[Identifier.Semaphore], true);
        }

        /// <summary>
        /// Inserts a one-shot M00 stop at an absolute cumulative path distance
        /// of <paramref name="absoluteDistMm"/> mm from the program start.
        /// Uses DISTANCE mode (<c>axis_nr = 0</c>) with <c>rel_abs_mode = 1</c>
        /// (one-off absolute distance from the program start).
        /// </summary>
        /// <param name="absoluteDistMm">Absolute path distance from program start in mm.</param>
        public void InsertAtAbsoluteDistance(double absoluteDistMm)
        {
            // Re-assert enable in case the PLC application has reset it since connect.
            plcClient.WriteAny(handles[Identifier.Enable], true);

            double distRaw = absoluteDistMm * 10_000.0; // mm → 0.1 µm

            plcClient.WriteAny(handles[Identifier.DistOrPos],   distRaw);
            plcClient.WriteAny(handles[Identifier.RelAbsMode],  (ushort)1); // one-off absolute distance from program start
            plcClient.WriteAny(handles[Identifier.AxisNr],      (ushort)0); // axis_nr = 0 → DISTANCE mode
            plcClient.WriteAny(handles[Identifier.MFunctionNr], (ushort)0); // M00

            plcClient.WriteAny(handles[Identifier.Semaphore], true);
        }

        /// <summary>
        /// Cancels any pending insert stop that has not yet been reached.
        /// Sending dist=0 with rel_abs_mode=0 (relative iterative) instructs the CNC to clear the stop.
        /// </summary>
        public void Clear()
        {
            plcClient.WriteAny(handles[Identifier.DistOrPos],   0.0);
            plcClient.WriteAny(handles[Identifier.RelAbsMode],  (ushort)0); // relative iterative — dist=0 clears
            plcClient.WriteAny(handles[Identifier.AxisNr],      (ushort)0);
            plcClient.WriteAny(handles[Identifier.MFunctionNr], (ushort)0);

            plcClient.WriteAny(handles[Identifier.Semaphore], true);
        }

        /// <summary>
        /// Reads the current insert-command state.
        /// 0 = no stop commanded; 1 = stop commanded but not yet reached.
        /// </summary>
        public int GetState() =>
            plcClient.ReadAny<int>(handles[Identifier.StateValue]);

        /// <summary>
        /// Reads whether the control unit is enabled (enable_w).
        /// </summary>
        public bool GetEnable() =>
            plcClient.ReadAny<bool>(handles[Identifier.Enable]);

        /// <summary>
        /// Reads the current semaphore value.
        /// TRUE means the CNC has not yet consumed the last command.
        /// FALSE means the CNC has read and processed it.
        /// </summary>
        public bool GetSemaphore() =>
            plcClient.ReadAny<bool>(handles[Identifier.Semaphore]);

        /// <summary>
        /// Reads the absolute path distance (dist_prog_start) at which the CNC will trigger
        /// the next stop, in mm. Returns -1 if no stop has been found yet in the look-ahead
        /// buffer (the stop may still be found as more blocks are pre-processed).
        /// </summary>
        public double GetDistanceOfNextStopMm() =>
            plcClient.ReadAny<double>(handles[Identifier.DistOfNextStop]) / 10_000.0;

        public void Dispose()
        {
            foreach (var h in handles.Values)
                try { plcClient.DeleteVariableHandle(h); } catch { /* ignore on dispose */ }
            handles.Clear();
        }

        enum Identifier
        {
            Enable,
            Semaphore,
            DistOrPos,
            RelAbsMode,
            AxisNr,
            MFunctionNr,
            StateValue,
            DistOfNextStop,
        }
    }
}
