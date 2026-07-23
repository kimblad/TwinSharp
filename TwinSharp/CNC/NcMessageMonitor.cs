using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// Monitors NC-program messages sent to the PLC via <c>#MSG PLC "text"</c> or
    /// <c>#MSGINFO PLC "text"</c> in the NC program.
    /// <para>
    /// When the CNC places a message in the HLI <c>msg_mc_control</c> control unit, this class
    /// fires <see cref="MessageReceived"/> with the message text and type, then acknowledges the
    /// semaphore so the CNC can deliver the next message.
    /// </para>
    /// <para>
    /// Call <see cref="Enable"/> once after connecting to register the PLC as present on this
    /// control unit. Without it the CNC will not deliver messages.
    /// </para>
    /// </summary>
    public class NcMessageMonitor : IDisposable
    {
        /// <summary>Fires on the ADS callback thread when the NC program sends a message to the PLC.</summary>
        public event EventHandler<NcMessageEventArgs>? MessageReceived;

        readonly AdsClient plcClient;
        readonly string semaphoreSymbol;
        readonly uint handleEnable;
        readonly uint handleSemaphore;
        readonly uint handleData;
        uint notificationHandle;

        internal NcMessageMonitor(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;

            string prefix = $"HLI_Global_Variables.gpCh[{channelNumber - 1}]^.msg_mc_control";
            semaphoreSymbol = prefix + ".request_semaphor_rw";

            handleEnable    = plcClient.CreateVariableHandle(prefix + ".enable_w");
            handleSemaphore = plcClient.CreateVariableHandle(prefix + ".request_semaphor_rw");
            handleData      = plcClient.CreateVariableHandle(prefix + ".request_r");
        }

        /// <summary>
        /// Registers the PLC as present on the <c>msg_mc_control</c> control unit and starts
        /// monitoring for incoming messages. The ADS device notification is only registered here —
        /// not in the constructor — so no CNC resources are consumed until this is called.
        /// </summary>
        public void Enable()
        {
            plcClient.WriteAny(handleEnable, true);

            // Slow (200 ms) notification — operator messages are not time-critical
            var slowSettings = new NotificationSettings(AdsTransMode.OnChange, 200, 200);
            notificationHandle = plcClient.AddDeviceNotificationEx(
                semaphoreSymbol, slowSettings, null, typeof(bool));

            plcClient.AdsNotificationEx += OnAdsNotification;
        }

        private void OnAdsNotification(object? sender, AdsNotificationExEventArgs e)
        {
            if (e.Handle != notificationHandle)
                return;

            bool semaphore = (bool)e.Value;
            if (!semaphore)
                return;   // CNC cleared the semaphore — no new message

            var msg = plcClient.ReadAny<HLI_MSG_SENDUNG>(handleData);

            // Clear the semaphore so the CNC can deliver the next message
            plcClient.WriteAny(handleSemaphore, false);

            MessageReceived?.Invoke(this, new NcMessageEventArgs(
                msg.Inhalt ?? string.Empty,
                msg.Typ == HliConstants.HLI_MESSAGE_TYPE_INFO ||
                msg.Typ == HliConstants.HLI_MESSAGE_TYPE_INFO_SYN_ACKN));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            plcClient.AdsNotificationEx -= OnAdsNotification;
            try { plcClient.DeleteDeviceNotification(notificationHandle); } catch { }
            try { plcClient.DeleteVariableHandle(handleEnable);    } catch { }
            try { plcClient.DeleteVariableHandle(handleSemaphore); } catch { }
            try { plcClient.DeleteVariableHandle(handleData);      } catch { }
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>Event arguments for <see cref="NcMessageMonitor.MessageReceived"/>.</summary>
    public class NcMessageEventArgs : EventArgs
    {
        /// <summary>Message text from the NC program.</summary>
        public string Contents { get; }

        /// <summary>True if the message was sent via <c>#MSGINFO</c>, false for <c>#MSG</c>.</summary>
        public bool IsInfo { get; }

        internal NcMessageEventArgs(string contents, bool isInfo)
        {
            Contents = contents;
            IsInfo   = isInfo;
        }
    }
}
