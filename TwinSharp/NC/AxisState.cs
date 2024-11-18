using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class AxisState
    {
        readonly AdsClient client;
        readonly uint indexGroup;


        internal AxisState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4100 + id;
        }

        public NCAXISSTATE_ONLINESTRUCT OnlineData
        {
            get => client.ReadAny<NCAXISSTATE_ONLINESTRUCT>(indexGroup, 0x00);
        }

        public uint ErrorCode
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        public uint CycleCounter
        {
            get => client.ReadAny<uint>(indexGroup, 0x09);
        }

        public double SetPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x0A);
        }

        public double SetPositionModulo
        {
            get => client.ReadAny<double>(indexGroup, 0x0B);
        }

        public int SetModuloRotation
        {
            get => client.ReadAny<int>(indexGroup, 0x0C);
        }

        public double SetTravelDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x0D);
        }

        public double SetVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x0E);
        }

        public double SetAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x0F);
        }

        public double SetJerk
        {
            get => client.ReadAny<double>(indexGroup, 0x10);
        }

        public double SetTorque
        {
            get => client.ReadAny<double>(indexGroup, 0x11);
        }

        public double SetCouplingFactor
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
        }

        public double ExpectedTargetPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x13);
        }


        private void GetRemainingTravelDistanceAndTime(out double distance, out double time)
        {
            var buffer = new Memory<byte>(new byte[16]);

            client.Read(indexGroup, 0x14, buffer);

            var ms = new MemoryStream(buffer.ToArray());
            var br = new BinaryReader(ms);

            distance = br.ReadDouble();
            time = br.ReadDouble();
        }

        public double RemainingTravelDistance
        {
            get
            {
                GetRemainingTravelDistanceAndTime(out double distance, out _);
                return distance;
            }
        }

        public double RemaniningTravelTime
        {
            get
            {
                GetRemainingTravelDistanceAndTime(out _, out double time);
                return time;
            }
        }

        public int SetCommandNumber
        {
            get => client.ReadAny<int>(indexGroup, 0x15);
        }

        public double PositioningTimeLastMotionCommand
        {
            get => client.ReadAny<double>(indexGroup, 0x16);
        }

        public double SetVelocityOverride
        {
            get => client.ReadAny<double>(indexGroup, 0x17);
        }

        public double UncorrectedSetPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x1A);
        }

        public double UncorrectedSetTravelDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x1D);
        }

        public double UncorrectedSetVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x1E);
        }

        public double UncorrectedSetAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x1F);
        }

        public uint CouplingState
        {
            //TODO: this is probably an enum, find the enum in InfoSys
            get => client.ReadAny<uint>(indexGroup, 0x20);
        }

        public uint CouplingTableIndex
        {
            get => client.ReadAny<uint>(indexGroup, 0x21);
        }

        public uint DelayedErrorCode
        {
            get => client.ReadAny<uint>(indexGroup, 0x29);
        }

        public uint InitializeCommandCounter
        {
            get => client.ReadAny<uint>(indexGroup, 0x2D);
        }

        public uint ResetCommandCounter
        {
            get => client.ReadAny<uint>(indexGroup, 0x2D);
        }

        public double SetTorqueChange
        {
            get => client.ReadAny<double>(indexGroup, 0x30);
        }

        public double TorqueOffset
        {
            get => client.ReadAny<double>(indexGroup, 0x31);
            set => client.WriteAny(indexGroup, 0x31, value);
        }

        public double ActualPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x10002);
        }

        public double ActualPositionModulo
        {
            get => client.ReadAny<double>(indexGroup, 0x10003);
        }

        public int ActualRotationModulo
        {
            get => client.ReadAny<int>(indexGroup, 0x10004);
        }

        public double ActualVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x10005);
        }

        public double ActualAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x10006);
        }

        public double LagErrorPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x2000D);
        }

        public double LagErrorPositionWithDeadTimeCompensation
        {
            get => client.ReadAny<double>(indexGroup, 0x2000F);
        }

        public double LagErrorPeakMaximum
        {
            get => client.ReadAny<double>(indexGroup, 0x20010);
        }

        public double LagErrorPeakMinimum
        {
            get => client.ReadAny<double>(indexGroup, 0x20011);
        }


    }
}
