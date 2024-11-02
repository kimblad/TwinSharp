using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class ErrorManagement
    {
        public event EventHandler<ErrorRecievedEventArgs>? ErrorRecieved;

        private AdsClient plcClient;
        readonly uint notificationHandleErrorMessageValid;
        readonly uint variableHandleErrorMessageValid;
        readonly uint handleErrorMessage;

        public ErrorManagement(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;

            var fastSettings = new NotificationSettings(AdsTransMode.OnChange, 50, 50);

            string prefix = string.Format(
                "HLI_Global_Variables.gpCh[{0}]^.mc_error", channelNumber - 1);

            string errorMessageSymbol = prefix + ".satz_r";
            handleErrorMessage = plcClient.CreateVariableHandle(errorMessageSymbol);

            string errorMessageWrittenSymbol = prefix + ".semaphor_rw";
            variableHandleErrorMessageValid = plcClient.CreateVariableHandle(errorMessageWrittenSymbol);


            notificationHandleErrorMessageValid = plcClient.AddDeviceNotificationEx(errorMessageWrittenSymbol, fastSettings, null, typeof(bool));
        
            plcClient.AdsNotificationEx += PlcClient_AdsNotificationEx;
        }

        private void PlcClient_AdsNotificationEx(object? sender, AdsNotificationExEventArgs e)
        {
            if(e.Handle != notificationHandleErrorMessageValid)
                return;

            bool messageIsValid = (bool)e.Value;

            if(!messageIsValid)
                return;

            ReadErrorMessage();
            AcknowledgeMessage();
        }

        private void AcknowledgeMessage()
        {
            plcClient.WriteAny(variableHandleErrorMessageValid, false);
        }

        private void ReadErrorMessage()
        {
            var error = plcClient.ReadAny<HLI_ERROR_SATZ>(handleErrorMessage);


            //TODO: Add enum for body types.
            if(error.Head.BodyType == 1)
            {
                var extendedInfo = Extensions.ByteArrayToStructure<HLI_RUMPF_NC_PROG>(error.Body.Mask.ErrorMask);
            }

            string description = "Unknown error";

            if(ErrorCodes.CodesAndDescriptions.TryGetValue(error.Head.ErrorId, out string? value))
                description = value;


            ErrorRecieved?.Invoke(this, new ErrorRecievedEventArgs(error.Head, description));
        }



    }

    public class ErrorRecievedEventArgs
    {

        public readonly HLI_ERROR_SATZ_KOPF Error;
        public readonly string Description;
        internal ErrorRecievedEventArgs(HLI_ERROR_SATZ_KOPF error, string description)
        {
            Error = error;
            Description = description;
        }
    }
}