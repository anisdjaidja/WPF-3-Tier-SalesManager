using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Threading;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Modules.Common;
using WPF_N_Tier_Test.ViewModel.App;
using WPF_N_Tier_Test_Data_Access.DataAccess;

namespace WPF_N_Tier_Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public GlobalMessageStore globalMessageStore;
        private AppWindowViewModel appWindowViewModel;
        private AppConfig _AppConfig;
        private async void OnStart(object sender, StartupEventArgs e)
        {
            //RegisterGlobalExceptionHandling();

            globalMessageStore = new GlobalMessageStore();

            _AppConfig = new();
            _AppConfig.load();

            SalesDesignTimeContextFactory contextFactory = new SalesDesignTimeContextFactory();
            appWindowViewModel = new AppWindowViewModel(contextFactory);
            MainWindow mainWindow = new MainWindow(_AppConfig, globalMessageStore, appWindowViewModel);
            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
        #region Global Exception Handeling
        private void RegisterGlobalExceptionHandling()
        {
            // this is the line you really want 
            //AppDomain.CurrentDomain.UnhandledException +=
            //    (sender, args) => CurrentDomainOnUnhandledException(args);

            // optional: hooking up some more handlers
            // remember that you need to hook up additional handlers when 
            // logging from other dispatchers, shedulers, or applications

            Current.Dispatcher.UnhandledException +=
                (sender, args) => DispatcherOnUnhandledException(args);


            //Application.Current.DispatcherUnhandledException +=
            //    (sender, args) => CurrentOnDispatcherUnhandledException(args);

            //TaskScheduler.UnobservedTaskException +=
            //    (sender, args) => TaskSchedulerOnUnobservedTaskException(args);
        }


        private async void DispatcherOnUnhandledException(DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            string errorType = args.Exception.InnerException
                is TimeoutException
                or SocketException
                or NotImplementedException
                || (args.Exception
                is TimeoutException
                or SocketException
                or NotImplementedException)
                ? "Network error" : "Unknown error";
            ReportError($"{errorType}: {args.Exception.GetType().Name}");
         

        }

        internal void AddShortcut(FavShortcut shortcut)
        {
            var identical = appWindowViewModel.Favorites.Where(x => x.Equals(shortcut)).FirstOrDefault();
            if(identical != null)
            {
                appWindowViewModel.Favorites.Remove(identical);
                _AppConfig.favShortcuts.Remove(identical);
                return;
            }
            appWindowViewModel.Favorites.Add(shortcut);
            _AppConfig.favShortcuts.Add(shortcut);
        }


        //private void FilterNetworkExceptions(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        //{
        //    if(e.Exception.GetType().FullName == "MongoDB.Driver.MongoConnectionException")
        //    {
        //        ReportError(e.Exception.Message);
        //        e.Handled = true;
        //    }
        //} 
        #endregion
    }

}
