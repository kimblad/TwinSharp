using TwinCAT.Ads;

namespace TwinSharp.NC
{

    /// <summary>
    /// Represents the parameters of a controller, providing properties to get and set various control parameters
    /// such as ID, name, type, and control weights. This class interacts with an AdsClient to read and write
    /// parameter values from a specified index group.
    /// </summary>
    public class ControllerParameters
    {
        readonly AdsClient client;
        readonly uint indexGroup;

        internal ControllerParameters(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x6000 + id;
        }

        /// <summary>
        /// Controller ID
        /// </summary>
        public uint ID
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Controller name
        /// </summary>
        public string Name
        {
            get => client.ReadString(indexGroup, 0x02, 30);
        }

        /// <summary>
        /// Controller type
        /// </summary>
        public ControllerType Type
        {
            get => (ControllerType)client.ReadAny<uint>(indexGroup, 0x03);
        }


        /// <summary>
        /// Weight of the velocity pre control (standard value: 1.0 = 100 %) [0.0 ... 1.0]
        /// </summary>
        public double VelocityPreControlWeight
        {
            get => client.ReadAny<double>(indexGroup, 0x0B);
            set => client.WriteAny(indexGroup, 0x0B, value);
        }


        /// <summary>
        /// Maximum output limitation ( ) for controller total output. (Standard value: 0.5 == 50%)
        /// </summary>
        public double MaxOutputLimitation
        {
            get => client.ReadAny<double>(indexGroup, 0x100);
            set => client.WriteAny(indexGroup, 0x100, value);
        }


        /// <summary>
        /// Proportional amplification factor kp resp. kv
        /// </summary>
        public double ProportionalGainKpOrKv
        {
            get => client.ReadAny<double>(indexGroup, 0x102);
            set => client.WriteAny(indexGroup, 0x102, value);
        }


        /// <summary>
        /// Integral action time Tn. Position control. [0.0 ... 60.0]. Seconds.
        /// </summary>
        public double IntegralActionTimeTn
        {
            get => client.ReadAny<double>(indexGroup, 0x103);
            set => client.WriteAny(indexGroup, 0x103, value);
        }


        /// <summary>
        /// Derivative action time Tv. Position control. [0.0 ... 60.0]. Seconds.
        /// </summary>
        public double DerivativeActionTimeTv
        {
            get => client.ReadAny<double>(indexGroup, 0x104);
            set => client.WriteAny(indexGroup, 0x104, value);
        }


        /// <summary>
        /// Damping time Td. Position control. [0.0 ... 60.0]. Seconds.
        /// </summary>
        public double DampingTimeTd
        {
            get => client.ReadAny<double>(indexGroup, 0x105);
            set => client.WriteAny(indexGroup, 0x105, value);
        }
    }
}
