using System.Text;
using TwinCAT.Ads;

namespace TwinSharp.CNC
{
    public class ErrorManagement
    {
        private AdsClient plcClient;
        readonly uint handleErrorMessageValid;
        readonly uint handleErrorMessage;

        public ErrorManagement(AdsClient plcClient, int channelNumber)
        {
            this.plcClient = plcClient;

            var fastSettings = new NotificationSettings(AdsTransMode.OnChange, 50, 50);

            string prefix = string.Format(
                "HLI_Global_Variables.gpCh[{0}]^.mc_error", channelNumber - 1);

            string errorMessageSymbol = prefix + ".satz_r.kopf";
            handleErrorMessage = plcClient.CreateVariableHandle(errorMessageSymbol);

            string errorMessageWrittenSymbol = prefix + ".semaphor_rw";
            handleErrorMessageValid = plcClient.AddDeviceNotificationEx(errorMessageWrittenSymbol, fastSettings, null, typeof(HLI_ERROR_SATZ));
        
            plcClient.AdsNotificationEx += PlcClient_AdsNotificationEx;
        }

        private void PlcClient_AdsNotificationEx(object? sender, AdsNotificationExEventArgs e)
        {
            if(e.Handle != handleErrorMessageValid)
                return;

            ReadErrorMessage();
        }

        private void ReadErrorMessage()
        {
            var error = plcClient.ReadAny<HLI_ERROR_SATZ_KOPF>(handleErrorMessage);


            var buffer = new byte[256];
            var memory = new Memory<byte>(buffer);
            int bytesReadCount = plcClient.Read(handleErrorMessage, memory);

        }
    }
}