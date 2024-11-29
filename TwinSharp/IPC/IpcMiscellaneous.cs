using TwinCAT.Ads;

namespace TwinSharp.IPC
{

    /// <summary>
    /// The IpcMiscellaneous class allows reading and writing of settings such as the startup state of the Numlock key, 
    /// CE remote display state, security wizard enabled state, auto logon username, and auto-generate certificates. 
    /// The class uses an AdsClient to communicate with the system and perform these operations.
    /// </summary>
    public class IpcMiscellaneous
    {
        internal const ushort ModuleType = 0x0100;

        readonly AdsClient client;
        readonly uint subIndex;

        internal IpcMiscellaneous(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndex = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
        }

        /// <summary>
        /// Startup Numlock State. State of the Numlock key at system start
        /// </summary>
        public bool StartupNumlockState
        {
            get => client.ReadAny<bool>(0xF302, subIndex + 0x01);
            set => client.WriteAny(0xF302, subIndex + 0x01, value);
        }

        /// <summary>
        /// CE remote display state shows whether a client is connected via CERHost. From MDP 1.6.x 
        /// (WinCE only)
        /// </summary>
        public bool CEremoteDisplayState
        {
            get => client.ReadAny<bool>(0xF302, subIndex + 0x02);
        }

        /// <summary>
        /// CE Remote Display Enabled
        /// (WinCE only)
        /// </summary>
        public bool CEremoteDisplayEnabled
        {
            get => client.ReadAny<bool>(0xF302, subIndex + 0x03);
            set => client.WriteAny(0xF302, subIndex + 0x03, value);
        }

        /// <summary>
        /// Security Wizard Enabled
        /// </summary>
        public bool SecurityWizardEnabled
        {
            get => client.ReadAny<bool>(0xF302, subIndex + 0x04);
            set => client.WriteAny(0xF302, subIndex + 0x04, value);
        }

        /// <summary>
        /// Auto Logon Username
        /// </summary>
        public string AutoLogonUsername
        {
            get => client.ReadString(0xF302, subIndex + 0x05, 80);
        }

        /// <summary>
        /// Auto Generate Certificates
        /// </summary>
        public bool AutoGenerateCertificates
        {
            get => client.ReadAny<bool>(0xF302, subIndex + 0x06);
            set => client.WriteAny(0xF302, subIndex + 0x06, value);
        }
    }
}
