using System.IO;
using System.Windows;
using System.Windows.Controls;
using WPF_N_Tier_Test.Modules.Common;
using WPF_N_Tier_Test.ViewModel.App;
using static WPF_N_Tier_Test.AppConfig;

namespace WPF_N_Tier_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppConfig _CONFIG;
        public MainWindow(AppConfig config, GlobalMessageStore globalMessageStore, AppWindowViewModel VM)
        {
            InitializeComponent();
            DataContext = VM;
            VM.SearchDropdownInvoked += () => SearchBarV.DropDown();
            FeedbackBox.Hook(globalMessageStore);
            _CONFIG = config;
            _CONFIG.load();
            //HandleTheme();
            //HandleColors();
            _CONFIG.ApplyState(this);
            //HandleLanguage();
        }

        #region WindowState
        private void MinimizeBTN_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void MaximizeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                _CONFIG.windowState = WindowState.Minimized;
            }
            else
            {
                _CONFIG.windowState = WindowState.Normal;
                WindowState = WindowState.Normal;

            }
        }
        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
            App.Current.Shutdown();
        }
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //AskForRating();
            _CONFIG.Getstate(this);
            _CONFIG.save();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                AppBorder.BorderThickness = new Thickness(7);
                AppBorder.CornerRadius = new CornerRadius(0);
            }
            else
            {
                AppBorder.BorderThickness = new Thickness(0.4, 0.4, 0.4, 0);
                AppBorder.CornerRadius = new CornerRadius(8);
            }
            _CONFIG.Getstate(this);
        }
        #endregion

        #region Theme
        private void HandleTheme()
        {
            UpdateApplicationTheme(_CONFIG.AppTheme);
            //LangBox.SelectedIndex = (int)_CONFIG.SystemLanguage;
        }
        private static ResourceDictionary? LoadThemeResourceDictionary(Theme? theme = null)
        {
            try
            {
                // if is null or blank string, set lang as default.
                theme ??= AppConfig.Theme.Default;
                Uri themeUri = new($@"\UI\Theme\{theme}.xaml", UriKind.Relative);
                return (ResourceDictionary)Application.LoadComponent(themeUri);
            }
            // The file does not exist.
            catch (IOException)
            {
                return null;
            }
        }
        public void UpdateApplicationTheme(Theme? theme = null)
        {
            _CONFIG.AppTheme = theme ?? AppConfig.Theme.Default;
            ResourceDictionary? themeResource = LoadThemeResourceDictionary(theme) ??
                                                    LoadThemeResourceDictionary();
            // If you have used other themes, clear it first.
            // Since the dictionary are cleared, the output of debugging will warn "Resource not found",
            // but it is not a problem in most case.

            Application.Current.Resources.MergedDictionaries.RemoveAt(1);
            // Add new language.
            Application.Current.Resources.MergedDictionaries.Insert(1, themeResource);
        }
        #endregion

        #region Colors
        private void HandleColors()
        {
            UpdateApplicationColors(_CONFIG.AppColors);
            //LangBox.SelectedIndex = (int)_CONFIG.SystemLanguage;
        }
        private static ResourceDictionary? LoadColorsResourceDictionary(AppConfig.Colors? colors = null)
        {
            try
            {
                // if is null or blank string, set lang as default.
                colors ??= AppConfig.Colors.Orange;
                Uri colorsUri = new($@"\UI\Colors\{colors}.xaml", UriKind.Relative);
                return (ResourceDictionary)Application.LoadComponent(colorsUri);
            }
            // The file does not exist.
            catch (IOException)
            {
                return null;
            }
        }
        public void UpdateApplicationColors(AppConfig.Colors? colors = null)
        {
            _CONFIG.AppColors = colors ?? AppConfig.Colors.Orange;
            ResourceDictionary? colorsResource = LoadColorsResourceDictionary(colors) ??
                                                    LoadColorsResourceDictionary();
            // If you have used other themes, clear it first.
            // Since the dictionary are cleared, the output of debugging will warn "Resource not found",
            // but it is not a problem in most case.

            Application.Current.Resources.MergedDictionaries.RemoveAt(2);
            // Add new language.
            Application.Current.Resources.MergedDictionaries.Insert(2, colorsResource);
        }
        #endregion

        #region Language

        private void HandleLanguage()
        {
            UpdateApplicationLanguage(_CONFIG.AppLanguage);
            LangBox.SelectedIndex = (int)_CONFIG.AppLanguage;
        }
        private void LanguageSwitched(object sender, RoutedEventArgs e)
        {
            string? selected = ((ComboBoxItem)sender).Content.ToString();
            UpdateApplicationLanguage(lang: (Language)Enum.Parse(typeof(Language), selected ?? "English"));
        }
        private static ResourceDictionary? LoadLanguageResourceDictionary(Language? lang = null)
        {
            try
            {
                // if is null or blank string, set lang as default.
                lang ??= AppConfig.Language.English;
                Uri langUri = new($@"\UI\Language\{lang}.xaml", UriKind.Relative);
                return (ResourceDictionary)Application.LoadComponent(langUri);
            }
            // The file does not exist.
            catch (IOException)
            {
                return null;
            }
        }

        /// <summary>
        /// Update the Application Language to <see cref="MainWindowViewModel.SelectedLanguage"/>.
        /// </summary>
        public void UpdateApplicationLanguage(Language? lang = null)
        {
            _CONFIG.AppLanguage = lang ?? AppConfig.Language.English;
            ResourceDictionary? langResource = LoadLanguageResourceDictionary(lang) ??
                                                    LoadLanguageResourceDictionary();
            // If you have used other languages, clear it first.
            // Since the dictionary are cleared, the output of debugging will warn "Resource not found",
            // but it is not a problem in most case.

            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            // Add new language.
            Application.Current.Resources.MergedDictionaries.Insert(0, langResource);

        }








        #endregion
    }
}