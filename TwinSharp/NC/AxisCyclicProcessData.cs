using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The AxisCyclicProcessData class provides properties to interact with the cyclic process data of an axis in a TwinCAT NC system.
    /// It uses an AdsClient to read and write various control and status parameters of the axis, such as control word, controller enable,
    /// feed enable, referencing cam, velocity override, operation mode, actual position correction value, and external controller component.
    /// If the axis is linked to a PLC object, most of these values will be refused.
    /// </summary>
    public class AxisCyclicProcessData
    {
        readonly uint indexGroup;
        readonly AdsClient client;
        internal AxisCyclicProcessData(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4300 + id;
        }

        /// <summary>
        /// Control double word
        /// </summary>
        public int ControlWord
        {
            get => client.ReadAny<int>(indexGroup, 0x01);
            set => client.WriteAny(indexGroup, 0x01, value);
        }

        /// <summary>
        /// Controller enable
        /// </summary>
        public bool ControllerEnable
        {
            get 
            {
                ushort number = client.ReadAny<ushort>(indexGroup, 0x02);
                return number == 1;
            }
            set
            {
                ushort number = value ? (ushort)1 : (ushort)0;
                client.WriteAny(indexGroup, 0x02, number);
            }
        }

        /// <summary>
        /// Feed enable plus
        /// </summary>
        public bool FeedEnablePlus
        {
            get
            {
                ushort number = client.ReadAny<ushort>(indexGroup, 0x03);
                return number == 1;
            }
            set
            {
                ushort number = value ? (ushort)1 : (ushort)0;
                client.WriteAny(indexGroup, 0x03, number);
            }
        }

        /// <summary>
        /// Feed enable minus
        /// </summary>
        public bool FeedEnableMinus
        {
            get
            {
                ushort number = client.ReadAny<ushort>(indexGroup, 0x04);
                return number == 1;
            }
            set
            {
                ushort number = value ? (ushort)1 : (ushort)0;
                client.WriteAny(indexGroup, 0x04, number);
            }
        }

        /// <summary>
        /// Referencing cam
        /// </summary>
        public bool ReferencingCam
        {
            get
            {
                ushort number = client.ReadAny<ushort>(indexGroup, 0x07);
                return number == 1;
            }
            set
            {
                ushort number = value ? (ushort)1 : (ushort)0;
                client.WriteAny(indexGroup, 0x07, number);
            }
        }

        /// <summary>
        /// Velocity override (1000000 == 100%)
        /// </summary>
        public uint VelocityOverride
        {
            get => client.ReadAny<uint>(indexGroup, 0x21);
            set => client.WriteAny(indexGroup, 0x21, value);
        }

        /// <summary>
        /// Operation mode axis
        /// </summary>
        public uint OperationMode
        {
            get => client.ReadAny<uint>(indexGroup, 0x22);
            set => client.WriteAny(indexGroup, 0x22, value);
        }

        /// <summary>
        /// Actual position correction value (measurement system error correction)
        /// </summary>
        public double ActualPositionCorrectionValue
        {
            get => client.ReadAny<double>(indexGroup, 0x25);
            set => client.WriteAny(indexGroup, 0x25, value);
        }

        /// <summary>
        /// External controller component (position controller component)
        /// </summary>
        public double ExternalControllerComponent
        {
            get => client.ReadAny<double>(indexGroup, 0x26);
            set => client.WriteAny(indexGroup, 0x26, value);
        }

    }
}
