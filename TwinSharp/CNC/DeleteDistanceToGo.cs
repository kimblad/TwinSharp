using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class DeleteDistanceToGo
    {
        private AdsClient plcClient;
        private int channelNumber;

        Dictionary<Identifier, uint> variableHandles;
        public DeleteDistanceToGo(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;
            this.channelNumber = channelNumber;

            variableHandles = CreateVariableHandles(channelNumber);
        }

        /// <summary>
        /// The rising edge of the commanded value has the effect that the CNC channel is decelerated to feed velocity 0. Then a linear motion is executed to the target position of the next motion block (short cut). The command only affects motion blocks.
        /// </summary>
        public bool Delete
        {
            get
            {
                uint handle = variableHandles[Identifier.DeleteDistanceToGoState];
                return plcClient.ReadAny<bool>(handle);
            }
            set
            {
                uint handle = variableHandles[Identifier.DeleteDistanceToGoCommand];
                plcClient.WriteAny(handle, value);
            }
        }

        public bool InterfaceExists
        {
            set
            {
                uint handle = variableHandles[Identifier.InterfaceExists];
                plcClient.WriteAny(handle, value);
            }
        }

        private Dictionary<Identifier, uint> CreateVariableHandles(int channelNumber)
        {
            var handles = new Dictionary<Identifier, uint>();

            uint handle;

            string prefix = string.Format("HLI_Global_Variables.gpCh[{0}]^.bahn_mc_control", channelNumber - 1);

            handle = plcClient.CreateVariableHandle(prefix + ".delete_distance_to_go.command_w");
            handles.Add(Identifier.DeleteDistanceToGoCommand, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".delete_distance_to_go.state_r");
            handles.Add(Identifier.DeleteDistanceToGoState, handle);

            handle = plcClient.CreateVariableHandle(prefix + ".delete_distance_to_go.enable_w");
            handles.Add(Identifier.InterfaceExists, handle);

            return handles;
        }

        private enum Identifier
        {
            DeleteDistanceToGoCommand,
            DeleteDistanceToGoState,
            InterfaceExists,
        }
    }
}