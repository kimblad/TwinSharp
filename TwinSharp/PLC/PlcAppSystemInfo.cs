using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.PLC
{
    /// <summary>
    /// The PlcAppSystemInfo class provides access to various system information and status variables
    /// of a TwinCAT PLC application. It uses an AdsClient to read and write these variables, which include
    /// object ID, flags, ADS port, boot data status, application timestamp, and more.
    /// </summary>
    public class PlcAppSystemInfo
    {
        readonly AdsClient client;
        readonly Dictionary<string, uint> variableHandles;

        internal PlcAppSystemInfo(AdsClient client)
        {
            this.client = client;
            variableHandles = new Dictionary<string, uint>();
        }


        /// <summary>
        /// Object ID of the PLC project instance.
        /// </summary>
        public ulong ObjId
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.ObjId");
                return client.ReadAny<ulong>(handle);
            }
        }

        /// <summary>
        /// TwinCAT internal use.
        /// </summary>
        public uint Flags
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.Flags");
                return client.ReadAny<uint>(handle);
            }
        }

        /// <summary>
        /// ADS port of the PLC application.
        /// </summary>
        public uint AdsPort
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.AdsPort");
                return client.ReadAny<uint>(handle);
            }
        }

        /// <summary>
        /// PERSISTENT variables: LOADED (without error).
        /// </summary>
        public bool BootDataLoaded
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.BootDataLoaded");
                return client.ReadAny<bool>(handle);
            }
        }

        /// <summary>
        /// PERSISTENT variables: INVALID (the back-up copy was loaded, since no valid file was present).
        /// </summary>
        public bool OldBootData
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.OldBootData");
                return client.ReadAny<bool>(handle);
            }
        }

        /// <summary>
        /// Time at which the PLC application was compiled
        /// </summary>
        public DateTime AppTimestamp
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.AppTimestamp");
                return client.ReadDateTime(handle);
            }
        }

        /// <summary>
        /// The flag can be set and prevents that the outputs are zeroed when a breakpoint is reached. In this case the task continues to run. Only the execution of the PLC code is interrupted.
        /// </summary>
        public bool KeepOutputsOnBP
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.KeepOutputsOnBP");
                return client.ReadAny<bool>(handle);
            }
            set
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.KeepOutputsOnBP");
                client.WriteAny(handle, value);
            }
        }

        /// <summary>
        /// This variable has the value TRUE if a shutdown of the TwinCAT system is in progress. Some parts of the TwinCAT system may already have been shut down.
        /// </summary>
        public bool ShutdownInProgress
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.ShutdownInProgress");
                return client.ReadAny<bool>(handle);
            }
        }

        /// <summary>
        /// This variable has the value TRUE if not all licenses that are provided by license dongles have been validated yet.
        /// </summary>
        public bool LicensesPending
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.LicensesPending");
                return client.ReadAny<bool>(handle);
            }
        }

        /// <summary>
        /// This variable has the value TRUE if Windows is in a BSOD. 
        /// </summary>
        public bool BSODOccured
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.BSODOccured");
                return client.ReadAny<bool>(handle);
            }
        }


        /// <summary>
        /// Number of tasks in the runtime system
        /// </summary>
        public ulong TaskCnt
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.TaskCnt");
                return client.ReadAny<ulong>(handle);
            }
        }

        /// <summary>
        /// Number of online changes since the last complete download
        /// </summary>
        public ulong OnlineChangeCnt
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.OnlineChangeCnt");
                return client.ReadAny<ulong>(handle);
            }
        }


        /// <summary>
        /// Name generated by TwinCAT, which contains the port.
        /// </summary>
        public string AppName
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.AppName");
                return client.ReadAnyString(handle, 63, Encoding.ASCII);
            }
        }

        /// <summary>
        /// Name of the project.
        /// </summary>
        public string ProjectName
        {
            get
            {
                uint handle = GetOrCreateVariableHandle("TwinCAT_SystemInfoVarList._AppInfo.ProjectName");
                return client.ReadAnyString(handle, 63, Encoding.ASCII);
            }
        }


        private uint GetOrCreateVariableHandle(string symbol)
        {
            if (variableHandles.TryGetValue(symbol, out uint handle))
                return handle;

            //Symbol and handle do not exist, create them
            handle = client.CreateVariableHandle(symbol);
            variableHandles.Add(symbol, handle);
            return handle;
        }
    }
}
