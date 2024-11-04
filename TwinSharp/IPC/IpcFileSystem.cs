using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    public class IpcFileSystem
    {
        public const ushort ModuleType = 0x0010;

        AdsClient client;
        readonly uint subIndexTable1;
        readonly uint subIndexTable2;

        internal IpcFileSystem(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
            subIndexTable2 = (uint)(mdpId << 20) | 0x80020000; //Table 0x8nn2, just add the desired subIndex later.
        }
    }
}
