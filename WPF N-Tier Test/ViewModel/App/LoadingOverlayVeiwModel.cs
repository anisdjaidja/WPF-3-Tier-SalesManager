using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_N_Tier_Test.ViewModel.App
{
    public partial class LoadingOverlayVeiwModel : BaseModel
    {

        #region Props

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(OverlayingApp))] public string? connectionFeedback;
        public Visibility OverlayingApp => IsBusy ? Visibility.Visible : Visibility.Collapsed;
        #endregion

        public async Task<bool> ConnectToDB()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(OverlayingApp));
            //MongoClient client = new MongoClient();
            //int retryDelay = 5; //in seconds
            //bool connected = false;
            //while (!connected)
            //{
            //    ConnectionFeedback = Current.TryFindResource("Connecting to servers")?.ToString() ?? "Connecting to servers";
            //    try
            //    {
            //        MongoClientSettings settings = null;
            //        await Task.Run(new Action(()=>
            //        {
            //            var app = Application.Current as WPF_N_Tier_Test.App;
            //            settings = MongoClientSettings.FromConnectionString(connectionString: app._AppConfig.DbConnectionString);
            //            // Set the ServerApi field of the settings object to Stable API version 1
            //            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            //            settings.ConnectTimeout = TimeSpan.FromSeconds(1);
            //            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(1);
            //            settings.RetryWrites = true;
            //            settings.RetryReads = true;
            //        })); 

            //        // Create a new client and connect to the server
            //        client = new MongoClient(settings);
            //        await client.ListDatabaseNamesAsync();
            //        connected = true;
            //        //await client.GetDatabase("WPF_N_Tier_TestDb").RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
            //        ReportSuccess($"Successfully connected to servers");

            //    }
            //    catch(Exception ex) 
            //    {
            //        //if (retryDelay < 20) { retryDelay = (int)Math.Round(retryDelay * 1.5); }
            //        ReportNetworkError($"{ex.GetType().Name.ToLower()}");
            //        for(int i = retryDelay; i > 0; i--)
            //        {
            //            ConnectionFeedback = $"{Current.TryFindResource("Connection failed")}, {Current.TryFindResource("retrying in")} {i} {Current.TryFindResource("seconds")}";
            //            await Task.Delay(1 * 1000); // convert to miliseconds
            //        }

            //    }
            //}
            await Task.Delay(millisecondsDelay: 3000);
            IsBusy = false;
            OnPropertyChanged(nameof(OverlayingApp));

            return true;
        }
    }
}
