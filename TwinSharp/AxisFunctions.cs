using TwinCAT.Ads;

namespace TwinSharp
{
    public class AxisFunctions
    {
        readonly uint indexGroup;
        readonly AdsClient client;
        internal AxisFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4200 + id;
        }

        public void Reset()
        {
            client.WriteAny(indexGroup, 0x01, true);
        }

        public void Stop()
        {
            client.WriteAny(indexGroup, 0x02, true);
        }

        public void ClearAxisTask()
        {
            client.WriteAny(indexGroup, 0x03, true);
        }

        public void EmergencyStopWithControlledRamp(double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(deceleration);
            bw.Write(jerk);

            client.WriteAny(indexGroup, 0x04, ms.ToArray());
        }

        public void StopWithControlledRamp(double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(deceleration);
            bw.Write(jerk);

            client.WriteAny(indexGroup, 0x05, ms.ToArray());
        }

        public void OrientedStop(double moduloEndPosition, double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(moduloEndPosition);
            bw.Write(deceleration);
            bw.Write(jerk);

            client.WriteAny(indexGroup, 0x09, ms.ToArray());
        }

        public void ReferenceAxis()
        {
            client.WriteAny(indexGroup, 0x10, true);
        }

        public void NewEndPositionAxis(EndPositionType endPositionType, double newEndPosition)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)endPositionType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(newEndPosition);

            client.WriteAny(indexGroup, 0x11, ms.ToArray());
        }



        public void SetExternalAxisError(uint errorCode)
        {
            client.WriteAny(indexGroup, 0x19, errorCode);
        }

        public void SetActualAxisPosition(ActualPositionType actualPositionType, double actualPosition)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)actualPositionType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(actualPosition);

            client.WriteAny(indexGroup, 0x1A, ms.ToArray());
        }

        public void StandardAxisStart(GroupAxisStartType startType, double endPosition, double velocity)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(endPosition);
            bw.Write(velocity);

            client.WriteAny(indexGroup, 0x20, ms.ToArray());
        }

        public void StartReversingOperation(GroupAxisStartType startType, double targePosition1, double targetPosition2, double velocity, double idleSeconds)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);
            bw.Write((uint)startType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(targePosition1);
            bw.Write(targetPosition2);
            bw.Write(velocity);
            bw.Write(idleSeconds);

            client.WriteAny(indexGroup, 0x25, ms.ToArray());
        }

        public void StartDriveOutput(DriveOutputStartType startType, double value)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(value);

            client.WriteAny(indexGroup, 0x26, ms.ToArray());
        }

        public void StopDriveOutput()
        {
            client.WriteAny(indexGroup, 0x27, true);
        }

        public void ChangeDriveOutput(DriveOutputStartType startType, double newValue)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(newValue);

            client.WriteAny(indexGroup, 0x28, ms.ToArray());
        }

        public void DeactivateCompleteAxis()
        {
            client.WriteAny(indexGroup, 0x50, true);
        }

        public void ActivateCompleteAxis()
        {
            client.WriteAny(indexGroup, 0x51, true);
        }

        public void DeactivateDriveOutput()
        {
            client.WriteAny(indexGroup, 0x60, true);
        }

        public void ActivateDriveOutput()
        {
            client.WriteAny(indexGroup, 0x61, true);
        }

        public void ReleaseParkingBrake(ushort release)
        {
            client.WriteAny(indexGroup, 0x62, release);
        }
    }
}
