using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    /// <summary>
    /// When single-step mode is active, the machine operator has the option to execute an NC program step by step. 
    /// The operator releases every NC line one by one. Comment lines or comment blocks and skipped blocks are skipped.
    /// </summary>
    public class SingleBlock
    {
        readonly AdsClient comClient;
        readonly uint indexGroup;
        internal SingleBlock(AdsClient comClient, int channelNumber)
        {
            this.comClient = comClient;

            indexGroup = 0x120100 + (uint)channelNumber;
        }

        /// <summary>
        /// This object defines whether the single block mode is active.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return comClient.ReadAny<bool>(indexGroup, 0x3);
            }
            set
            {
                comClient.WriteAny(indexGroup, 0x1, value);
            }
        }
    }
}