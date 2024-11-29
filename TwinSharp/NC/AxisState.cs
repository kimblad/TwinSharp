using System.Collections.Generic;
using TwinCAT.Ads;
using TwinCAT.PlcOpen;

namespace TwinSharp.NC
{

    /// <summary>
    /// Represents the state of an NC axis. 
    /// Provides access to various properties and methods to read and manipulate 
    /// the state of the axis, including position, velocity, acceleration, torque, 
    /// and error codes.
    /// </summary>
    public class AxisState
    {
        readonly AdsClient client;
        readonly uint indexGroup;


        internal AxisState(AdsClient client, uint id)
        {
            this.client = client;
            indexGroup = 0x4100 + id;
        }

        /// <summary>
        /// The online data of the axis.
        /// </summary>
        public NCAXISSTATE_ONLINESTRUCT OnlineData
        {
            get => client.ReadAny<NCAXISSTATE_ONLINESTRUCT>(indexGroup, 0x00);
        }

        /// <summary>
        /// Axis state error code.
        /// </summary>
        public uint ErrorCode
        {
            get => client.ReadAny<uint>(indexGroup, 0x01);
        }

        /// <summary>
        /// Set cycle counter
        /// (SAF timestamp)
        /// </summary>
        public uint CycleCounter
        {
            get => client.ReadAny<uint>(indexGroup, 0x09);
        }

        /// <summary>
        /// Set position
        /// </summary>
        public double SetPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x0A);
        }

        /// <summary>
        /// Modulo set position
        /// </summary>
        public double SetPositionModulo
        {
            get => client.ReadAny<double>(indexGroup, 0x0B);
        }

        /// <summary>
        /// Modulo set rotation
        /// </summary>
        public int SetModuloRotation
        {
            get => client.ReadAny<int>(indexGroup, 0x0C);
        }

        /// <summary>
        /// Set travel direction [-1.0, 0.0, 1.0]
        /// </summary>
        public double SetTravelDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x0D);
        }

        /// <summary>
        /// Set velocity
        /// </summary>
        public double SetVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x0E);
        }

        /// <summary>
        /// Set acceleration
        /// </summary>
        public double SetAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x0F);
        }

        /// <summary>
        /// Set jerk (time derivative of the set acceleration) 
        /// </summary>
        public double SetJerk
        {
            get => client.ReadAny<double>(indexGroup, 0x10);
        }

        /// <summary>
        /// Set torque (rot. motor) or set force(linear motor) ("SetTorque")
        /// </summary>
        public double SetTorque
        {
            get => client.ReadAny<double>(indexGroup, 0x11);
        }

        /// <summary>
        /// Set coupling factor (set gear ratio)
        /// </summary>
        public double SetCouplingFactor
        {
            get => client.ReadAny<double>(indexGroup, 0x12);
        }

        /// <summary>
        /// Expected target position
        /// </summary>
        public double ExpectedTargetPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x13);
        }

        /// <summary>
        /// Remaining travel time and distance (SERVO).
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time">Remaining travel time secodns</param>
        private void GetRemainingTravelDistanceAndTime(out double distance, out double time)
        {
            var buffer = new Memory<byte>(new byte[16]);

            client.Read(indexGroup, 0x14, buffer);

            var ms = new MemoryStream(buffer.ToArray());
            var br = new BinaryReader(ms);

            time = br.ReadDouble();
            distance = br.ReadDouble();
        }

        /// <summary>
        /// Remaining travel distance e.g. mm (SERVO).
        /// </summary>
        public double RemainingTravelDistance
        {
            get
            {
                GetRemainingTravelDistanceAndTime(out double distance, out _);
                return distance;
            }
        }

        /// <summary>
        /// Remaining travel time seconds (SERVO).
        /// </summary>
        public double RemaniningTravelTime
        {
            get
            {
                GetRemainingTravelDistanceAndTime(out _, out double time);
                return time;
            }
        }

        /// <summary>
        /// Set command number ("CmdNo")
        /// </summary>
        public int SetCommandNumber
        {
            get => client.ReadAny<int>(indexGroup, 0x15);
        }

        /// <summary>
        /// Positioning time of the last motion command
        /// (start → target position window)
        /// </summary>
        public double PositioningTimeLastMotionCommand
        {
            get => client.ReadAny<double>(indexGroup, 0x16);
        }

        /// <summary>
        /// Set override value for velocity [0.0…1.0] 1.0=100%
        /// </summary>
        public double SetVelocityOverride
        {
            get => client.ReadAny<double>(indexGroup, 0x17);
        }

        /// <summary>
        /// Uncorrected set position
        /// </summary>
        public double UncorrectedSetPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x1A);
        }

        /// <summary>
        /// Uncorrected set travel direction [-1.0, 0.0, 1.0]
        /// </summary>
        public double UncorrectedSetTravelDirection
        {
            get => client.ReadAny<double>(indexGroup, 0x1D);
        }

        /// <summary>
        /// Uncorrected set velocity e.g. mm/s
        /// </summary>
        public double UncorrectedSetVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x1E);
        }

        /// <summary>
        /// Uncorrected set acceleration e.g. mm/s^2
        /// </summary>
        public double UncorrectedSetAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x1F);
        }

        /// <summary>
        /// Coupling state
        /// </summary>
        public uint CouplingState
        {
            //TODO: this is probably an enum, find the enum in InfoSys
            get => client.ReadAny<uint>(indexGroup, 0x20);
        }

        /// <summary>
        /// Coupling table index
        /// </summary>
        public uint CouplingTableIndex
        {
            get => client.ReadAny<uint>(indexGroup, 0x21);
        }

        /// <summary>
        /// Delayed error code (error pre-warning) in case of a delayed error reaction (see bit ErrorPropagationDelayed)
        /// </summary>
        public uint DelayedErrorCode
        {
            get => client.ReadAny<uint>(indexGroup, 0x29);
        }

        /// <summary>
        /// Counter for initialization command (InitializeCommandCounter)
        /// </summary>
        public uint InitializeCommandCounter
        {
            get => client.ReadAny<uint>(indexGroup, 0x2D);
        }

        /// <summary>
        /// Counter for reset command (ResetCommandCounter)
        /// </summary>
        public uint ResetCommandCounter
        {
            get => client.ReadAny<uint>(indexGroup, 0x2D);
        }

        /// <summary>
        /// Set torque change (rot. motor) or set force change (linear motor) (time derivative of the set torque or set force)
        /// </summary>
        public double SetTorqueChange
        {
            get => client.ReadAny<double>(indexGroup, 0x30);
        }

        /// <summary>
        /// Additive set torque (rot. motor) or additive set force (linear motor) for pre-control.
        /// ("TorqueOffset")
        /// </summary>
        public double TorqueOffset
        {
            get => client.ReadAny<double>(indexGroup, 0x31);
            set => client.WriteAny(indexGroup, 0x31, value);
        }

        /// <summary>
        /// Actual position (charge with actual position compensation value) ("ActPos")
        /// </summary>
        public double ActualPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x10002);
        }

        /// <summary>
        /// Modulo actual position "ActPosModulo"
        /// </summary>
        public double ActualPositionModulo
        {
            get => client.ReadAny<double>(indexGroup, 0x10003);
        }

        /// <summary>
        /// Modulo actual rotation
        /// </summary>
        public int ActualRotationModulo
        {
            get => client.ReadAny<int>(indexGroup, 0x10004);
        }

        /// <summary>
        /// Optional: Actual velocity "ActVelo"
        /// </summary>
        public double ActualVelocity
        {
            get => client.ReadAny<double>(indexGroup, 0x10005);
        }

        /// <summary>
        /// Optional: Actual acceleration "ActAcc"
        /// </summary>
        public double ActualAcceleration
        {
            get => client.ReadAny<double>(indexGroup, 0x10006);
        }

        /// <summary>
        /// Lag error position(without dead time compensation)
        /// </summary>
        public double LagErrorPosition
        {
            get => client.ReadAny<double>(indexGroup, 0x2000D);
        }

        /// <summary>
        /// Lag error position(with dead time compensation) "PosDiff"
        /// </summary>
        public double LagErrorPositionWithDeadTimeCompensation
        {
            get => client.ReadAny<double>(indexGroup, 0x2000F);
        }

        /// <summary>
        /// Peak hold value for maximum negative lag error of the position
        /// </summary>
        public double LagErrorPeakMaximum
        {
            get => client.ReadAny<double>(indexGroup, 0x20010);
        }

        /// <summary>
        /// Peak hold value for minimum positive lag error of the position
        /// </summary>
        public double LagErrorPeakMinimum
        {
            get => client.ReadAny<double>(indexGroup, 0x20011);
        }
    }
}
