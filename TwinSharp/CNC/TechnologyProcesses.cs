using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// The TechnologyProcesses class manages the interaction with CNC technology units via an ADS client.
    /// It handles the acknowledgment of M and H codes, processes notifications from the CNC, and provides methods to read and acknowledge technology units.
    /// </summary>
    public class TechnologyProcesses : IDisposable
    {
        /// <summary>
        /// Event that is triggered when a M code needs to be acknowledged.
        /// </summary>
        public event EventHandler<McodeNeedsAcknowledgeEventArgs>? McodeNeedsAcknowledge;

        /// <summary>
        /// Event that is triggered when a H code needs to be acknowledged.
        /// </summary>
        public event EventHandler<HcodeNeedsAcknowledgeEventArgs>? HcodeNeedsAcknowledge;

        readonly AdsClient plcClient;
        readonly int channelNumber;


        readonly Dictionary<Identifier, uint> variableHandles;
        readonly int HLI_TU_CH_STD_SYNC_MAXIDX;
        readonly int HLI_TECH_UNIT_CH_MAXIDX;

        const int sizeOfOneUnit = 50;
        readonly uint notificationHandle; //ADS handle that recieves notifications.

        internal TechnologyProcesses(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;

            variableHandles = CreateVariableHandles(channelNumber);

            HLI_TU_CH_STD_SYNC_MAXIDX = MaxArrayLengthStandardSynch; //Typically maximum 19 M/H/T/S codes can be programmed in one NC block.
            HLI_TECH_UNIT_CH_MAXIDX = TechUnitChannelLength;

            //Add some notification when technology units change in CNC.
            var settingsFast = new NotificationSettings(AdsTransMode.OnChange, 25, 25);


            notificationHandle = plcClient.AddDeviceNotificationEx(
                string.Format("HLI_Global_Variables.gpCh[{0}]^.techno_unit_std_sync", channelNumber - 1),
                settingsFast, null, typeof(TECHNO_UNIT_CH[]), [HLI_TU_CH_STD_SYNC_MAXIDX]);

            plcClient.AdsNotificationEx += PlcClient_AdsNotificationEx;
        }


        private void PlcClient_AdsNotificationEx(object? sender, AdsNotificationExEventArgs e)
        {
            if(e.Handle != notificationHandle)
                return;

            int acknowledgeCount = CountToBeAcknowledgedInThisBlock;

            if (acknowledgeCount == 0)
                return;

            var technoUnits = (TECHNO_UNIT_CH[])e.Value;


            for (int i = 0; i < acknowledgeCount; i++)
            {
                var unit = technoUnits[i];

                //M and H codes contain the same additional info. Read it.
                if (unit.TechnologyType == TechnologyFunction.HLI_INTF_M_FKT)
                {
                    var mHProcess = ReadMHinfo(unit.MSTH_PROCESS_CH);
                    //Confirm to CNC that we have read all data.
                    string symbol = string.Format("HLI_Global_Variables.gpCh[{0}]^.techno_unit_std_sync[{1}].please_rw", channelNumber - 1, i);
                    uint handle = plcClient.CreateVariableHandle(symbol);
                    plcClient.WriteAny(handle, false);

                    TriggerMcodeNeedsAck(mHProcess, i);
                }
                else if (unit.TechnologyType == TechnologyFunction.HLI_INTF_H_FKT)
                {
                    var mHProcess = ReadMHinfo(unit.MSTH_PROCESS_CH);

                    TriggerHcodeNeedsAck(mHProcess, i);
                }
            }
        }

        private void TriggerMcodeNeedsAck(HLI_M_H_PROZESS unitInfo, int indexToAck)
        {
            var ea = new McodeNeedsAcknowledgeEventArgs(indexToAck, unitInfo);
            McodeNeedsAcknowledge?.Invoke(this, ea);
        }

        private void TriggerHcodeNeedsAck(HLI_M_H_PROZESS unitInfo, int indexToAck)
        {
            var ea = new HcodeNeedsAcknowledgeEventArgs(indexToAck, unitInfo);
            HcodeNeedsAcknowledge?.Invoke(this, ea);
        }

        private Dictionary<Identifier, uint> CreateVariableHandles(int channelNumber)
        {
            string prefix = string.Format("HLI_Global_Variables.gpCh[{0}]^", channelNumber - 1);
            var handles = new Dictionary<Identifier, uint>();
            uint handle;

            handle = plcClient.CreateVariableHandle("HLI_Global_Constants.HLI_TU_CH_STD_SYNC_MAXIDX");
            handles.Add(Identifier.MaxSynchArrayLength, handle);

            handle = plcClient.CreateVariableHandle("HLI_Global_Constants.HLI_TECH_UNIT_CH_MAXIDX");
            handles.Add(Identifier.TechUnitChannelLength, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".techno_unit_std_sync");
            handles.Add(Identifier.GetBlockByBlockSynchTechnologies, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".used_units_std_sync_r");
            handles.Add(Identifier.NumberOfBlocksToBeAcknowledged, handle);

            return handles;
        }

        private int MaxArrayLengthStandardSynch
        {
            get => plcClient.ReadAny<int>(variableHandles[Identifier.MaxSynchArrayLength]);
        }

        private int TechUnitChannelLength
        {
            get => plcClient.ReadAny<int>(variableHandles[Identifier.TechUnitChannelLength]);
        }





        /// <summary>
        /// Array of M/H/S/T technology functions with block-by-block synchronisation.
        /// </summary>
        /// <returns></returns>
        public TECHNO_UNIT_CH[] GetBlockByBlockSynchTechnologies()
        {
            var technoUnits = plcClient.ReadAny<TECHNO_UNIT_CH[]>(
                variableHandles[Identifier.GetBlockByBlockSynchTechnologies], [HLI_TU_CH_STD_SYNC_MAXIDX]);

            return technoUnits;
        }

        /// <summary>
        /// Acknowledge a technology unit. This is necessary to inform the CNC that processing of the NC program should continue.
        /// </summary>
        /// <param name="unitsIndex"></param>
        public void AcknowledgeTecnologyUnit(int unitsIndex)
        {
            //string symbol = string.Format("HLI_Global_Variables.gpCh[{0}]^.techno_unit_std_sync[{1}].please_rw", channelNumber - 1, unitsIndex);
            //uint handle = plcClient.CreateVariableHandle(symbol);

            string symbol2 = string.Format("HLI_Global_Variables.gpCh[{0}]^.techno_unit_std_sync[{1}].done_w", channelNumber - 1, unitsIndex);
            uint handle2 = plcClient.CreateVariableHandle(symbol2);

            //plcClient.WriteAny(handle, false);
            plcClient.WriteAny(handle2, true);
        }

        /// <summary>
        /// The technology unit contains a byte[] (MSTH_PROCESS_CH) with additional information.
        /// This byte[] can be cast to a HLI_M_H_PROZESS struct if the technology unit is a M or H function.
        /// </summary>
        private HLI_M_H_PROZESS ReadMHinfo(byte[] bytes)
        {
            var ms = new MemoryStream(bytes);
            using var br = new BinaryReader(ms);

            HLI_M_H_PROZESS mh = new HLI_M_H_PROZESS();
            mh.Number = br.ReadUInt32();
            mh.ExpectedTime = br.ReadInt32();
            mh.BlockNumber = br.ReadUInt32();
            mh.ProgramRow = br.ReadUInt32();
            mh.additionalValue = br.ReadInt32();
            mh.nr_late_sync = br.ReadUInt16();
            mh.fill_up_1 = br.ReadUInt16();
            mh.synchMode = br.ReadUInt32();
            mh.fill_up_2 = br.ReadInt32();

            return mh;
        }

        /// <summary>
        /// Dispose the object. Deletes device notifications and handles on ADS side.
        /// </summary>
        public void Dispose()
        {
            plcClient.DeleteDeviceNotification(notificationHandle);

            foreach (var handle in variableHandles.Values)
            {
                plcClient.DeleteVariableHandle(handle);
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Number of entries in the array ATechnoUnitChannel_Std ( = number of technology functions to be acknowledged in this block)
        /// </summary>
        public ushort CountToBeAcknowledgedInThisBlock
        {
            get => plcClient.ReadAny<ushort>(variableHandles[Identifier.NumberOfBlocksToBeAcknowledged]);
        }

        enum Identifier
        {
            MaxSynchArrayLength,
            TechUnitChannelLength,
            GetBlockByBlockSynchTechnologies,
            NumberOfBlocksToBeAcknowledged
        }
    }

    /// <summary>
    /// Event args supplied when a H code needs to be acknowledged.
    /// </summary>
    public class HcodeNeedsAcknowledgeEventArgs
    {
        /// <summary> Index of technology to acknowledge when you are done processing this H-code.</summary>
        public readonly int IndexToAcknowledge;

        /// <summary> Description of the H-code. </summary>
        public readonly HLI_M_H_PROZESS MhProcess;

        internal HcodeNeedsAcknowledgeEventArgs(int indexToAck, HLI_M_H_PROZESS mHProcess)
        {
            this.MhProcess = mHProcess;
        }
    }

    /// <summary>
    /// Event args supplied when a M code needs to be acknowledged.
    /// </summary>
    public class McodeNeedsAcknowledgeEventArgs
    {
        /// <summary> Index of technology to acknowledge when you are done processing this M-code.</summary>
        public readonly int IndexToAcknowledge;

        /// <summary> Description of the M-code. </summary>
        public readonly HLI_M_H_PROZESS MhProcess;
        
        internal McodeNeedsAcknowledgeEventArgs(int indexToAck, HLI_M_H_PROZESS mhProcess)
        {
            IndexToAcknowledge = indexToAck;
            MhProcess = mhProcess;
        }
    }
}