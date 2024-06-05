using System.Windows;

namespace WPF_N_Tier_Test.Modules.Common
{
    public static class GlobalAppAPI
    {
        public static void ReportActionStatus(bool success, string? sMessage = "Sucess", string? fMessage = "Canceled")
        {
            var app = Application.Current as App;
            if (success) app.globalMessageStore.SetCurrentMessage(sMessage, Modules.Common.GlobalMessageType.Status);
            else app.globalMessageStore.SetCurrentMessage(fMessage, Modules.Common.GlobalMessageType.Status);
        }
        public static void ReportOperationStatus(bool success, string? sMessage = "Sucess", string? fMessage = "Failed, possible error or rule violation")
        {
            var app = Application.Current as App;
            if (success) app.globalMessageStore.SetCurrentMessage(sMessage, Modules.Common.GlobalMessageType.Status);
            else app.globalMessageStore.SetCurrentMessage(fMessage, Modules.Common.GlobalMessageType.Error);
        }
        public static void ReportSuccess(string? sMessage = "Sucess")
        {
            var app = Application.Current as App;
            app.globalMessageStore.SetCurrentMessage(sMessage, Modules.Common.GlobalMessageType.Status);
        }
        public static void ReportError (string fMessage = "Error or rule violation")
        {
            var app = Application.Current as App;
            app.globalMessageStore.SetCurrentMessage(fMessage, Modules.Common.GlobalMessageType.Error);
        }
        public static void ReportNetworkError (string errorMssg = "")
        {
            var app = Application.Current as App;
            string mssg = Current.TryFindResource("Network issue").ToString() ?? "Network issue";
            app.globalMessageStore.SetCurrentMessage(mssg + ", "+ errorMssg, Modules.Common.GlobalMessageType.Error);
        }
    }
}
