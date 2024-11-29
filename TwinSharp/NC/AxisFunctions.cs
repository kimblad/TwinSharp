using System;
using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// The AxisFunctions class provides a set of methods to control and manage the behavior of an axis in a TwinCAT NC system.
    /// It allows for operations such as resetting, stopping, referencing, and setting positions of the axis.
    /// Additionally, it supports advanced operations like controlled ramps, emergency stops, reversing operations, and sinus oscillation sequences.
    /// The class interacts with the TwinCAT system using an AdsClient to send commands and data to the specified axis.
    /// </summary>

    public class AxisFunctions
    {
        readonly uint indexGroup;
        readonly AdsClient client;
        internal AxisFunctions(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4200 + id;
        }

        /// <summary>
        /// Reset axis
        /// </summary>
        public void Reset()
        {
            client.WriteAny(indexGroup, 0x01, true);
        }

        /// <summary>
        /// Stop axis
        /// </summary>
        public void Stop()
        {
            client.WriteAny(indexGroup, 0x02, true);
        }

        /// <summary>
        /// Clear axis (task)
        /// </summary>
        public void ClearAxisTask()
        {
            client.WriteAny(indexGroup, 0x03, true);
        }

        /// <summary>
        /// Emergency stop with controlled ramp
        /// </summary>
        /// <param name="deceleration">Deceleration (must be greater than or equal to the original deceleration)</param>
        /// <param name="jerk">Jerk (must greater than or equal to the original jerk)</param>
        public void EmergencyStopWithControlledRamp(double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(deceleration);
            bw.Write(jerk);

            client.WriteAny(indexGroup, 0x04, ms.ToArray());
        }

        /// <summary>
        /// Parameterizable stop (with controlled ramp). Only for PTP axes.
        /// </summary>
        /// <param name="deceleration"></param>
        /// <param name="jerk"></param>
        public void StopWithControlledRamp(double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(deceleration);
            bw.Write(jerk);

            client.WriteAny(indexGroup, 0x05, ms.ToArray());
        }

        /// <summary>
        /// Oriented stop (oriented end position). Only for PTP axes.
        /// </summary>
        /// <param name="moduloEndPosition"></param>
        /// <param name="deceleration"></param>
        /// <param name="jerk"></param>
        public void OrientedStop(double moduloEndPosition, double deceleration, double jerk)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write(moduloEndPosition);
            bw.Write(deceleration);
            bw.Write(jerk);

            client.WriteAny(indexGroup, 0x09, ms.ToArray());
        }

        /// <summary>
        /// Reference axis (calibration).
        /// </summary>
        public void ReferenceAxis()
        {
            client.Write(indexGroup, 0x10);
        }

        /// <summary>
        /// Set new end position (axis).
        /// </summary>
        /// <param name="endPositionType"></param>
        /// <param name="newEndPosition">New end position (target position)</param>
        public void NewEndPositionAxis(EndPositionType endPositionType, double newEndPosition)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)endPositionType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(newEndPosition);

            client.WriteAny(indexGroup, 0x11, ms.ToArray());
        }


        /// <summary>
        /// Set external axis error (runtime error)
        /// </summary>
        /// <param name="errorCode"></param>
        public void SetExternalAxisError(uint errorCode)
        {
            client.WriteAny(indexGroup, 0x19, errorCode);
        }

        /// <summary>
        /// Set actual axis position
        /// </summary>
        /// <param name="actualPositionType"></param>
        /// <param name="actualPosition"></param>
        public void SetActualAxisPosition(ActualPositionType actualPositionType, double actualPosition)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)actualPositionType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(actualPosition);

            client.WriteAny(indexGroup, 0x1A, ms.ToArray());
        }

        /// <summary>
        /// Set actual axis position on the fly (in motion of the axis)
        /// </summary>
        /// <param name="positionType"></param>
        /// <param name="controlword">Control double word, e.g. for "clearing the lag error</param>
        /// <param name="newActualPosition"></param>
        public void SetActualPositionOnTheFly(ActualPositionType positionType, int controlword, double newActualPosition)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)positionType);
            bw.Write(controlword);
            bw.Write((double)0.0);
            bw.Write(newActualPosition);
            bw.Write((uint)0);
            bw.Write((uint)0);

            client.WriteAny(indexGroup, 0x1F, ms.ToArray());
        }

        /// <summary>
        /// Standard axis start.
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="endPosition"></param>
        /// <param name="velocity"></param>
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

        /// <summary>
        /// Extended axis start.
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="targetPosition"></param>
        /// <param name="requireVelocity"></param>
        /// <param name="acceleration">0 if internal TwinCAT acceleration should be used.</param>
        /// <param name="deceleration">0 if internal TwiNCAT deceleration should be used.</param>
        /// <param name="jerk">0 if internal TwinCAT jerk should be used.</param>
        public void ExtendedAxisStart(GroupAxisStartType startType, double targetPosition, double requireVelocity, double acceleration = 0, double deceleration = 0, double jerk = 0)
        {
            //TwinCAT wants 3 ints that are 1 if no external acceleration, deceleration or jerk is used

            int useDefaultAcceleration = acceleration <= 0 ? 1 : 0;
            int useDefaultDeceleration = deceleration <= 0 ? 1 : 0;
            int useDefaultJerk = jerk <= 0 ? 1 : 0;

            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write((uint)0);
            bw.Write(targetPosition);
            bw.Write(requireVelocity);
            bw.Write(useDefaultAcceleration);
            bw.Write((uint)0);
            bw.Write(acceleration);
            bw.Write(useDefaultDeceleration);
            bw.Write((uint)0);
            bw.Write(deceleration);
            bw.Write(useDefaultJerk);
            bw.Write((uint)0);
            bw.Write(jerk);


            client.WriteAny(indexGroup, 0x21, ms.ToArray());
        }

        /// <summary>
        /// Start reversing operation for positioning (SERVO).
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="targePosition1"></param>
        /// <param name="targetPosition2"></param>
        /// <param name="velocity"></param>
        /// <param name="idleSeconds"></param>
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

        /// <summary>
        /// Start drive output.
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="value"></param>
        public void StartDriveOutput(DriveOutputStartType startType, double value)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(value);

            client.WriteAny(indexGroup, 0x26, ms.ToArray());
        }

        /// <summary>
        /// Stop drive output.
        /// </summary>
        public void StopDriveOutput()
        {
            client.Write(indexGroup, 0x27);
        }

        /// <summary>
        /// Change the drive output.
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="newValue">Required output value (e.g. %)</param>
        public void ChangeDriveOutput(DriveOutputStartType startType, double newValue)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write(0); //Reserved by TwinCAT
            bw.Write(newValue);

            client.WriteAny(indexGroup, 0x28, ms.ToArray());
        }


        /// <summary>
        /// Start reversing operation with velocity jumps (SERVO):
        /// (can be used to determine the velocity step response)
        /// </summary>
        /// <param name="startType"></param>
        /// <param name="velocity1"></param>
        /// <param name="velocity2"></param>
        /// <param name="travelSeconds"></param>
        /// <param name="idleSeconds"></param>
        /// <param name="repetitionCount"></param>
        public void StartReversingOperationVelocityJumps(GroupAxisStartType startType, double velocity1, double velocity2, double travelSeconds, double idleSeconds, uint repetitionCount)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)startType);
            bw.Write((uint)0);
            bw.Write(velocity1);
            bw.Write(velocity2);
            bw.Write(travelSeconds);
            bw.Write(idleSeconds);
            bw.Write(repetitionCount);
            bw.Write((uint)0);

            client.WriteAny(indexGroup, 0x32, ms.ToArray());
        }

        /// <summary>
        /// Sine oscillation sequence
        /// - used as single sinus oscillation(sinus generator)
        /// - used as sinus oscillation sequence(e.g. for bode plot)
        /// </summary>
        /// <param name="baseAmplitude"></param>
        /// <param name="baseFrequency"></param>
        /// <param name="startAmplitude"></param>
        /// <param name="feedConstantMotor"></param>
        /// <param name="startFrequency"></param>
        /// <param name="stopFrequency"></param>
        /// <param name="stepDurationSeconds"></param>
        /// <param name="stepCycles"></param>
        public void StartSinusOscillationSequence(double baseAmplitude, double baseFrequency, double startAmplitude, double feedConstantMotor, double startFrequency, double stopFrequency, double stepDurationSeconds, uint stepCycles)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((uint)GroupAxisStartType.AbsoluteStart); //Fixed to this according to TwinCAT documentation
            bw.Write((uint)0);
            bw.Write(baseAmplitude);
            bw.Write(baseFrequency);
            bw.Write(startAmplitude);
            bw.Write(feedConstantMotor);
            bw.Write(startFrequency);
            bw.Write(stopFrequency);
            bw.Write(stepDurationSeconds);
            bw.Write(stepCycles);
            bw.Write((uint)1);

            client.WriteAny(indexGroup, 0x33, ms.ToArray());
        }

        /// <summary>
        /// Deactivate complete axis (disable)
        /// </summary>
        public void DeactivateCompleteAxis()
        {
            client.WriteAny(indexGroup, 0x50, true);
        }

        /// <summary>
        /// Activate complete axis (enable)
        /// </summary>
        public void ActivateCompleteAxis()
        {
            client.WriteAny(indexGroup, 0x51, true);
        }

        /// <summary>
        /// Deactivate drive output (disable).
        /// </summary>
        public void DeactivateDriveOutput()
        {
            client.WriteAny(indexGroup, 0x60, true);
        }

        /// <summary>
        /// Activate drive output (enable).
        /// </summary>
        public void ActivateDriveOutput()
        {
            client.WriteAny(indexGroup, 0x61, true);
        }

        /// <summary>
        /// Release parking brake?
        /// 0: automatic activation(default) 
        /// 1: mandatorily always released 
        /// Note: Reset to '0' when resetting the axis!
        /// </summary>
        /// <param name="release"></param>
        public void ReleaseParkingBrake(ushort release)
        {
            client.WriteAny(indexGroup, 0x62, release);
        }
    }
}
