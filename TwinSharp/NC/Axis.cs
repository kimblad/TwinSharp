using TwinCAT.Ads;

namespace TwinSharp.NC
{
    /// <summary>
    /// Represents an axis in a TwinCAT NC system, encapsulating its functions, parameters, state, cyclic process data, and associated sub-elements such as encoders, controllers, and drives.
    /// </summary>
    public class Axis
    {
        /// <summary>
        /// Creates a new axis object representation of the given ID.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id"></param>
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

        /// <summary>
        /// Gets the array of encoders associated with the axis.
        /// </summary>
        public Encoder[] Encoders { get; private set; }

        /// <summary>
        /// Gets the functions available for the axis.
        /// </summary>
        public AxisFunctions Functions { get; private set; }

        /// <summary>
        /// Gets the parameters of the axis.
        /// </summary>
        public AxisParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the state of the axis.
        /// </summary>
        public AxisState State { get; private set; }

        /// <summary>
        /// Gets the array of controllers associated with the axis.
        /// </summary>
        public Controller[] Controllers { get; private set; }

        /// <summary>
        /// Gets the array of drives associated with the axis.
        /// </summary>
        public Drive[] Drives { get; private set; }

        /// <summary>
        /// Gets the cyclic process data of the axis.
        /// </summary>
        public AxisCyclicProcessData CyclicProcessData { get; private set; }

        /// <summary>
        /// Returns the name of the axis.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Axis {Parameters.Name}";
        }

    }
}
