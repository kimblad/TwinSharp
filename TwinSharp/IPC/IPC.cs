using TwinCAT.Ads;

namespace TwinSharp.IPC
{
    /// <summary>
    /// The IPC class represents a Beckhoff Industrial PC (IPC) and provides access to various hardware modules such as network cards, CPU, memory, display devices, operating system, fans, mainboard, UPS, and miscellaneous modules.
    /// It uses the AdsClient to connect to the IPC and read the available MDP modules, initializing the corresponding module objects based on their types.
    /// The class implements IDisposable to ensure proper disposal of the AdsClient.
    /// </summary>
    public class IPC : IDisposable
    {
        readonly AdsClient client;



        /// <summary>
        /// Creates an representation of a Beckhoff IPC.
        /// </summary>
        /// <param name="target">Target where to find IPC. Use AmsNetId.Local for local access.</param>
        public IPC(AmsNetId target)
        {
            client = new AdsClient();
            client.Connect(target, AmsPort.SystemService);

            //Some modules exists more then once due to hardware config.
            var nics = new List<IpcNIC>();
            var displays = new List<IpcDisplayDevice>();
            var fans = new List<IpcFan>();

            var unknown = new List<uint>();
            //Read all available MDP modules
            // Reads the numbers of modules. First ushort in list is the count of items 
            ushort mdpModuleCount = client.ReadAny<ushort>(0xF302, 0xF0200000); // index to get modul ID List - Flag and Subindex 0


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
                    case IpcUps.ModuleType:
                        UPS = new IpcUps(client, mdpId);
                        break;
                    case IpcMiscellaneous.ModuleType:
                        Miscellaneous = new IpcMiscellaneous(client, mdpId);
                        break;
                    default:
                        unknown.Add(mdpType);
                        break;
                }
            }

            NICs = nics.ToArray();
            DisplayDevices = displays.ToArray();
            Fans = fans.ToArray();
        }


        /// <summary>
        /// Represents the network cards of the IPC.
        /// </summary>
        public IpcNIC[] NICs { get; private set; }

        /// <summary>
        /// Module for viewing and setting the time on the IPC.
        /// </summary>
        public IpcTime? Time { get; private set; }

        /// <summary>
        /// Represents the TwinCAT module of the IPC.
        /// </summary>
        public IpcTwinCAT? TwinCAT { get; private set; }

        /// <summary>
        /// Represents the CPU module of the IPC.
        /// </summary>
        public IpcCpu? Cpu { get; private set; }

        /// <summary>
        /// Represents the memory module of the IPC.
        /// </summary>
        public IpcMemory? Memory { get; private set; }

        /// <summary>
        /// Represents the display devices of the IPC.
        /// </summary>
        public IpcDisplayDevice[] DisplayDevices { get; private set; }

        /// <summary>
        /// Represents the operating system module of the IPC.
        /// </summary>
        public IpcOperatingSystem? OperatingSystem { get; private set; }

        /// <summary>
        /// Represents the fans of the IPC.
        /// </summary>
        public IpcFan[] Fans { get; private set; }

        /// <summary>
        /// Represents the mainboard module of the IPC.
        /// </summary>
        public IpcMainBoard? MainBoard { get; private set; }

        /// <summary>
        /// Represents the UPS module of the IPC.
        /// </summary>
        public IpcUps? UPS { get; private set; }

        /// <summary>
        /// Represents the miscellaneous module of the IPC.
        /// </summary>
        public IpcMiscellaneous? Miscellaneous { get; private set; }


        /// <summary>
        /// Disposes the ads client.
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
