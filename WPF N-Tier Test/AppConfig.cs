using Newtonsoft.Json;
using System.Data.Common;
using System.IO;
using System.Windows;
using WPF_N_Tier_Test.Modules.Helpers;

namespace WPF_N_Tier_Test
{
    
    public class AppConfig
    {
        //public const string DbConnectionString = "mongodb+srv://tajirdev:tajirtajir0a@basictajircluster.yrgpzez.mongodb.net/?retryWrites=true&w=majority";
        //public const string DbConnectionString = "mongodb+srv://tajirdev:tajirtajir0a@basictajircluster.yrgpzez.mongodb.net/";
        public string ServerUrl = "localhost:27017";
        public string DbConnectionString {
            get { return $"mongodb://{ServerUrl}/"; }
        }
        public string DbName = "WPF_N_Tier_TestDocDB";
        private string FilePath = DirectorySurfer.GetAppConfig();
        public Theme AppTheme = Theme.Default;
        public Colors AppColors = Colors.Orange;
        public Language AppLanguage = Language.English;
        public enum Language
        {
            English,
            Francais,
            العربية,
        }
        public enum Theme
        {
            Default,
            Dark,
        }
        public enum Colors
        {
            Orange,
            Teal,
            Money,
        }


        #region WindowState

        public double windowHeight = double.NaN;
        public double windowWidth = double.NaN;
        public double windowTop = double.NaN;
        public double windowLeft = double.NaN;
        public WindowState windowState = WindowState.Normal;

        public void ApplyState(Window window)
        {
            window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            window.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            if (windowHeight == double.NaN) { window.Height = SystemParameters.FullPrimaryScreenHeight * 0.80; }
            else { window.Height = windowHeight; }
            if (windowWidth == double.NaN) { window.Width = SystemParameters.FullPrimaryScreenWidth * 0.80; }
            else { window.Width = windowWidth; }
            if (windowTop != double.NaN) { window.Top = windowTop; }
            if (windowLeft != double.NaN) { window.Left = windowLeft; }
            window.WindowState = windowState;

        }
        public void Getstate(Window window)
        {
            if (window == null) { return; }
            windowTop = window.Top;
            windowLeft = window.Left;
            windowHeight = window.Height;
            windowWidth = window.Width;
            windowState = window.WindowState;
        }

        #endregion

        #region I.O Operations
        public void save()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            // If directory does not exist, create it
            if (!Directory.Exists(DirectorySurfer.GetAppDocDir())) { Directory.CreateDirectory(DirectorySurfer.GetAppDocDir()); }
            File.WriteAllText(FilePath, jsonString);
        }
        public bool load()
        {
            AppConfig? temp;
            try
            {
                if (!File.Exists(FilePath)) { throw new FileNotFoundException("NO EXISTING APPCONFIG FILE FOUND");}
                using StreamReader r = new(FilePath);
                string json = r.ReadToEnd();
                temp = JsonConvert.DeserializeObject<AppConfig>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            }
            catch 
            { 
                save();return false;
            }
                
            if (temp != null)
            {
                AppTheme = temp.AppTheme;
                AppColors = temp.AppColors;
                AppLanguage = temp.AppLanguage;
                windowTop = temp.windowTop;
                windowLeft = temp.windowLeft;
                windowHeight = temp.windowHeight; 
                windowWidth = temp.windowWidth;
                windowState = temp.windowState;
                return true;
            }
            return false;
        }
        #endregion
    }
}
