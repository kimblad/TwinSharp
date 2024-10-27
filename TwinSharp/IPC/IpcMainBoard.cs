using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// This module provides mainboard information, such as type, serial number, production date, boot count, operating time, and temperature.
    /// This module is not supported by all devices, since it requires a special BIOS.
    /// </summary>
    public class IpcMainBoard
    {
        public const ushort ModuleType = 0x001C;

        AdsClient client;
        readonly uint subIndexTable1;
        readonly uint subIndexTable2;

        internal IpcMainBoard(AdsClient client, ushort mdpId)
        {
            this.client = client;

            // Shift 20 bit and make or-Operation with (0x8nn10000) to get the mdpAddr with the id at position nn
            subIndexTable1 = (uint)(mdpId << 20) | 0x80010000; //Table 0x8nn1, just add the desired subIndex later.
            subIndexTable2 = (uint)(mdpId << 20) | 0x80020000; //Table 0x8nn2, just add the desired subIndex later.
        }

        public string Type
        {
            get => client.ReadString(0xF302, subIndexTable1 + 01, 80);
        }

        public string SerialNumber
        {
            get => client.ReadString(0xF302, subIndexTable1 + 02, 80);
        }

        public string ProductionDate
        {
            get => client.ReadString(0xF302, subIndexTable1 + 03, 80);
        }

        public uint BootCount
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 04);
        }

        public uint OperatingTimeMinutes
        {
            get => client.ReadAny<uint>(0xF302, subIndexTable1 + 05);
        }

        /// <summary>
        /// Lowest measured temperature.
        /// </summary>
        public int MinBoardTemperatureCelsius
        {
            get => client.ReadAny<int>(0xF302, subIndexTable1 + 06);
        }

        /// <summary>
        /// Highest measured temperature.
        /// </summary>
        public int MaxBoardTemperatureCelsius
        {
            get => client.ReadAny<int>(0xF302, subIndexTable1 + 07);
        }

        /// <summary>
        /// Lowest measured voltage.
        /// </summary>
        public int MinInputMilliVolts
        {
            get => client.ReadAny<int>(0xF302, subIndexTable1 + 08);
        }

        /// <summary>
        /// Highest measured voltage.
        /// </summary>
        public int MaxInputMilliVolts
        {
            get => client.ReadAny<int>(0xF302, subIndexTable1 + 09);
        }

        public short TemperatureCelsius
        {
            get => client.ReadAny<short>(0xF302, subIndexTable1 + 10);
        }

        public byte MainBoardRevision
        {
            get => client.ReadAny<byte>(0xF302, subIndexTable2 + 01);
        }

        public byte BiosMajorVersion
        {
            get => client.ReadAny<byte>(0xF302, subIndexTable2 + 02);
        }

        public byte BiosMinorVersion
        {
            get => client.ReadAny<byte>(0xF302, subIndexTable2 + 03);
        }

        public string BiosVersion
        {
            get => client.ReadString(0xF302, subIndexTable2 + 04, 80);
        }
    }
}
