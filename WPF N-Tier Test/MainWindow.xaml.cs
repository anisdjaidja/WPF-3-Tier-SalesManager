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
            FeedbackBox.Hook(globalMessageStore);
            _CONFIG = config;
            _CONFIG.load();
            _CONFIG.ApplyState(this);
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
    }
}