using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinSharp.EtherCAT
{
    /// <summary>
    /// EtherCAT state of a slave. The status can adopt one of the following values:
    /// </summary>
    public enum EcDeviceState : ushort
    {
        /// <summary> Init state </summary>
        Init = 0x01,

        /// <summary> Pre-operational state </summary>
        PreOp = 0x02,

        /// <summary> Bootstrap state </summary>
        BootStrap = 0x03,

        /// <summary> Safe-operational state </summary>
        SafeOp = 0x04,

        /// <summary> Operational state </summary>
        Op = 0x08,

        // Note: In addition, the following bits can be set:
        /// <summary> State machine error in the EtherCAT slave. </summary>
        Error = 0x10,

        /// <summary> Invalid vendor ID, product code, revision number or serial number. </summary>
        InvalidVprs = 0x20,

        /// <summary> Error during sending of initialization commands. </summary>
        InitCmdError = 0x40,

        /// <summary> Slave is disabled. </summary>
        Disabled = 0x80
    }

    /// <summary>
    /// Link status of an EtherCAT slave. The Link state can consist of an ORing of the following bits:
    /// </summary>
    [Flags]
    public enum EcLinkState : ushort
    {
        /// <summary> Link is ok. </summary>
        Ok = 0x00,

        /// <summary> No EtherCAT communication with the EtherCAT slave. </summary>
        NotPresent = 0x01,

        /// <summary> Error at port X (specified through EC_LINK_STATE_PORT_A/B/C/D). The port has a link, but no communication is possible via this port.. </summary>
        WithoutCommunication = 0x02,

        /// <summary> Missing link at port X (specified through EC_LINK_STATE_PORT_A/B/C/D). </summary>
        MissingLink = 0x04,

        /// <summary> Additional link at port X (specified through EC_LINK_STATE_PORT_A/B/C/D). </summary>
        AdditionalLink = 0x08,

        /// <summary> Port 0. </summary>
        PortA = 0x10,

        /// <summary> Port 1. </summary>
        PortB = 0x20,

        /// <summary> Port 2. </summary>
        PortC = 0x40,

        /// <summary> Port 3. </summary>
        PortD = 0x80,
    }
}
