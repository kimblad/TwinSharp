using TwinCAT.Ads;

namespace TwinSharp.NC
{

    /// <summary>
    /// The EncoderFunctions class provides methods to interact with and control encoder devices via the TwinCAT ADS protocol.
    /// It includes functionalities to set and reinitialize the actual position of the encoder, activate and deactivate touch probes and external latches,
    /// and set external latch events. The class uses an AdsClient to communicate with the encoder and sends commands using specific index groups and offsets.
    /// </summary>
    public class EncoderFunctions
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal EncoderFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x5200 + id;
        }

        /// <summary>
        /// Set actual position encoder/axis. Caution when using!
        /// </summary>
        /// <param name="actualPositionType"></param>
        /// <param name="position"></param>
        public void SetActualPosition(ActualPositionType actualPositionType, double position)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)actualPositionType);
            bw.Write(position);

            client.WriteAny(indexGroup, 0x1A, ms.ToArray());

        }

        /// <summary>
        /// Re-initialization of the actual encoder position.
        /// Note: Takes effect for reference system „ABSOLUTE MULTITURN RANGE(with single overflow)“ and „ABSOLUTE SINGLETURN RANGE(with single overflow)“.
        /// NEW from TC3
        /// </summary>
        public void ReInitActualEncoderPosition()
        {
            client.WriteAny(indexGroup, 0x1B, true);
        }

        /// <summary>
        /// Function group "TouchProbeV2": SERCOS/SoE, EtherCAT/CoE(CANopen DS402), SoftDrive(TCom), MDP 511 (EL5101, EL5151, EL5021, EL7041, EL7342)
        /// </summary>
        /// <param name="probeUnit"></param>
        /// <param name="signalEdge"></param>
        /// <param name="probeMode"></param>
        /// <param name="signalSource"></param>
        public void ActivateTouchProbe(uint probeUnit, SignalEdge signalEdge, ProbeMode probeMode, uint signalSource)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(probeUnit);
            bw.Write((uint)signalEdge);
            bw.Write((uint)probeMode);
            bw.Write(signalSource);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(0); //Reserved by TwinCAT

            client.WriteAny(indexGroup, 0x200, ms.ToArray());
        }

        /// <summary>
        /// Activate  "External Latch" or activate "measuring probe function" (typically rising edge)
        /// KL5101,SERCOS,AX2xxx,PROFIDrive 
        /// </summary>
        public void ActivateExternalLatchRisingEdge()
        {
            client.WriteAny(indexGroup, 0x201, true);
        }

        /// <summary>
        /// Activate  "External Latch" 1 to 4 or activate "measuring probe function" 1 to 4 (typically rising edge)
        /// CANopen
        /// </summary>
        /// <param name="latch1"></param>
        /// <param name="latch2"></param>
        /// <param name="latch3"></param>
        /// <param name="latch4"></param>
        public void ActivateExternalLatchRisingEdgeCANopen(bool latch1, bool latch2, bool latch3, bool latch4)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(latch1 ? 1u : 0u);
            bw.Write(latch2 ? 1u : 0u);
            bw.Write(latch3 ? 1u : 0u);
            bw.Write(latch4 ? 1u : 0u);

            client.WriteAny(indexGroup, 0x201, ms.ToArray());
        }

        /// <summary>
        /// Activate  "External Latch" or activate "measuring probe function" (falling edge)
        /// KL5101,SERCOS,AX2xxx,PROFIDrive 
        /// </summary>
        public void ActivateExternalLatchFallingEdge()
        {
            client.WriteAny(indexGroup, 0x202, true);
        }

        /// <summary>
        /// Activate  "External Latch" 1 to 4 or activate "measuring probe function" 1 to 4 (falling edge)
        /// CANopen
        /// </summary>
        /// <param name="latch1"></param>
        /// <param name="latch2"></param>
        /// <param name="latch3"></param>
        /// <param name="latch4"></param>
        public void ActivateExternalLatchFallingEdgeCANopen(bool latch1, bool latch2, bool latch3, bool latch4)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(latch1 ? 1u : 0u);
            bw.Write(latch2 ? 1u : 0u);
            bw.Write(latch3 ? 1u : 0u);
            bw.Write(latch4 ? 1u : 0u);

            client.WriteAny(indexGroup, 0x202, ms.ToArray());
        }

        /// <summary>
        /// Deactivate "touch probe" (external latch)
        /// Function group "TouchProbeV2": SERCOS/SoE, EtherCAT/CoE(CANopen DS402), SoftDrive(TCom), MDP 511 (EL5101, EL5151, EL5021, EL7041, EL7342)
        /// </summary>
        /// <param name="probeUnit"></param>
        /// <param name="signalEdge"></param>
        public void DeactivateTouchProbe(uint probeUnit, SignalEdge signalEdge)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(probeUnit);
            bw.Write((uint)signalEdge);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(0); //Reserved by TwinCAT

            client.WriteAny(indexGroup, 0x205, ms.ToArray());
        }

        /// <summary>
        /// Deactivate "external latch" or deactivate "measuring probe function"
        /// KL5101,SERCOS,AX2xxx,PROFIDrive 
        /// </summary>
        public void DeactivateExternalLatch()
        {
            client.WriteAny(indexGroup, 0x205, true);
        }

        /// <summary>
        /// Deactivate "external latch" or deactivate "measuring probe function"
        /// CANopen 
        /// </summary>
        /// <param name="latch1"></param>
        /// <param name="latch2"></param>
        /// <param name="latch3"></param>
        /// <param name="latch4"></param>
        public void DeactivateExternalLatchCANopen(bool latch1, bool latch2, bool latch3, bool latch4)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(latch1 ? 1u : 0u);
            bw.Write(latch2 ? 1u : 0u);
            bw.Write(latch3 ? 1u : 0u);
            bw.Write(latch4 ? 1u : 0u);

            client.WriteAny(indexGroup, 0x205, ms.ToArray());
        }

        /// <summary>
        /// Set "External latch event" and "External latch position"
        /// KL5101,SERCOS,AX2xxx,PROFIDrive 
        /// Only for EtherCAT.
        /// </summary>
        public void SetExternalLatchEvent(double position)
        {
            client.WriteAny(indexGroup, 0x210, position);
        }
    }
}
