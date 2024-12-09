using TwinCAT.Ads;

namespace TwinSharp.NC
{

    /// <summary>
    /// The Encoder class represents an encoder device and provides access to its functions, parameters, and state.
    /// It uses an AdsClient to communicate with the encoder via the TwinCAT ADS protocol.
    /// The class contains three main properties:
    /// - Functions: Provides methods to interact with and control the encoder.
    /// - Parameters: Allows reading and writing various encoder settings.
    /// - State: Provides access to various encoder states and properties.
    /// </summary>
    public class Encoder
    {
        internal Encoder(AdsClient client, uint id)
        {
            Functions = new EncoderFunctions(client, id);
            Parameters = new EncoderParameters(client, id);
            State = new EncoderState(client, id);
        }

        /// <summary>
        /// The EncoderFunctions class provides methods to interact with and control encoder devices via the TwinCAT ADS protocol.
        /// It includes functionalities to set and reinitialize the actual position of the encoder, activate and deactivate touch probes and external latches,
        /// and set external latch events. The class uses an AdsClient to communicate with the encoder and sends commands using specific index groups and offsets.
        /// </summary>
        public EncoderFunctions Functions { get; private set; }

        /// <summary>
        /// The EncoderParameters class provides an interface to interact with encoder parameters via an AdsClient.
        /// It allows reading and writing various encoder settings such as ID, name, type, scaling factor, position offset,
        /// count direction, modulo factor, mode, soft end monitoring, soft end positions, evaluation direction, and filter times.
        /// The class uses an AdsClient to communicate with the encoder and retrieve or update these parameters.
        /// </summary>
        public EncoderParameters Parameters { get; private set; }

        /// <summary>
        /// The EncoderState class provides access to various encoder states and properties 
        /// through an AdsClient instance. It allows reading and writing of encoder-related 
        /// data such as error codes, actual positions, velocities, accelerations, and other 
        /// relevant metrics.
        /// </summary>
        public EncoderState State { get; private set; }
    }
}
