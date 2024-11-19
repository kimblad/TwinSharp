using TwinCAT.Ads;

namespace TwinSharp.NC
{
    public class Axis
    {
        public readonly AxisFunctions Functions;
        public readonly AxisParameters Parameters;
        public readonly AxisState State;
        public readonly AxisCyclicProcessData CyclicProcessData;

        public readonly Encoder[] Encoders;
        public readonly Controller[] Controllers;
        public readonly Drive[] Drives;
        public Axis(AdsClient client, uint id)
        {
            Functions = new AxisFunctions(client, id);
            Parameters = new AxisParameters(client, id);
            State = new AxisState(client, id);
            CyclicProcessData = new AxisCyclicProcessData(client, id);


            //Create the sub elements for encoders, controllers and drives.
            //Read the sub elements corresponding ID and instanciate relevant objects using that ID.
            Parameters.ReadAllSubElements(out var encoderIDs, out var controllerIDs, out var driveIDs);

            Encoders = new Encoder[encoderIDs.Length];
            for (int i = 0; i < Encoders.Length; i++)
            {
                Encoders[i] = new Encoder(client, encoderIDs[i]);
            }

            Controllers = new Controller[controllerIDs.Length];
            for (int i = 0; i < Controllers.Length; i++)
            {
                Controllers[i] = new Controller(client, controllerIDs[i]);
            }

            Drives = new Drive[driveIDs.Length];
            for (int i = 0; i < Drives.Length; i++)
            {
                Drives[i] = new Drive(client, driveIDs[i]);
            }
        }


        public override string ToString()
        {
            return $"Axis {Parameters.Name}";
        }

    }
}
