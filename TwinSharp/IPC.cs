using TwinCAT.Ads;

namespace TwinSharp
{
    public class IPC : IDisposable
    {
        public IpcNIC[] NICs;
        public IpcTime? Time;
        public IpcTwinCAT? TwinCAT;
        public IpcCpu? Cpu;
        public IpcMemory? Memory;
        public IpcDisplayDevice[] DisplayDevices;
        public IpcOperatingSystem? OperatingSystem;
        public IpcFan[] Fans;
        public IpcMainBoard? MainBoard;
        public IpcUPS? UPS;
        public IpcMiscellaneous? Miscellaneous;


        AdsClient client;

        public IPC(AmsNetId target)
        {     
            client = new AdsClient();

            client.Connect(target, AmsPort.SystemService);

            //Some modules exists more then once due to hardware config.
            var nics = new List<IpcNIC>();
            var displays = new List<IpcDisplayDevice>();
            var fans = new List<IpcFan>();

            var unkown = new List<uint>();
            //Read all available MDP modules
            // Reads the numbers of modules. First ushort in list is the count of items 
            ushort mdpModuleCount = client.ReadAny<ushort>(0xF302,     // CoE = CAN over EtherCAT (profil definition)
                                                    0xF0200000); // index to get modul ID List - Flag and Subindex 0


            // Iterate through the list of modules to get the index and the type of each module
            for (int i = 0; i < mdpModuleCount + 1; i++)
            {
                uint subIndex = (uint)(0xF0200000 + i);
                // Composition of the MDPModule number and read the numbers of modules
                uint mdpModule = client.ReadAny<uint>(0xF302, subIndex); // get modul ID List at subindex i

                // Composition of the Type and ID
                // make &-Operation with 0xFFFF0000 and shift 16 bit to get the type from the high word
                ushort mdpType = (ushort)((mdpModule & 0xFFFF0000) >> 16);

                // make &-Operation with 0x0000FFFF  to get the id from the low word
                ushort mdpId = (ushort)(mdpModule & 0x0000FFFF);

                switch (mdpType)
                {
                    case IpcNIC.ModuleType:
                        nics.Add(new IpcNIC(client, mdpId));
                        break;
                    case IpcTime.ModuleType:
                        Time = new IpcTime(client, mdpId);
                        break;
                    case IpcTwinCAT.ModuleType:
                        TwinCAT = new IpcTwinCAT(client, mdpId);
                        break;
                    case IpcCpu.ModuleType:
                        Cpu = new IpcCpu(client, mdpId);
                        break;
                    case IpcMemory.ModuleType:
                        Memory = new IpcMemory(client, mdpId);
                        break;
                    case IpcDisplayDevice.ModuleType:
                        displays.Add(new IpcDisplayDevice(client, mdpId));
                        break;
                    case IpcOperatingSystem.ModuleType:
                        OperatingSystem = new IpcOperatingSystem(client, mdpId);
                        break;
                    case IpcFan.ModuleType:
                        fans.Add(new IpcFan(client, mdpId));
                        break;
                    case IpcMainBoard.ModuleType:
                        MainBoard = new IpcMainBoard(client, mdpId);
                        break;
                    case IpcUPS.ModuleType:
                        UPS = new IpcUPS(client, mdpId);
                        break;
                    case IpcMiscellaneous.ModuleType:
                        Miscellaneous = new IpcMiscellaneous(client, mdpId);
                        break;
                    default:
                        unkown.Add(mdpType);
                        break;
                }
            }

            NICs = nics.ToArray();
            DisplayDevices = displays.ToArray();
            Fans = fans.ToArray();
        }


        public void Dispose()
        {
            client?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
