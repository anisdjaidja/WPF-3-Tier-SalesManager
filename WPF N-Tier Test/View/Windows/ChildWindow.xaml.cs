using System.Windows;

namespace WPF_N_Tier_Test.Views.Windows
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        public ChildWindow(dynamic content, string title)
        {
            InitializeComponent();
            MainFrame.Child = content;
            Title = title;
            TitleBox.Text = title;
        }
        private void MinimizeBTN_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void MaximizeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                //_AppConfig.windowState = WindowState.Minimized;
            }
            else
            {
                //_AppConfig.windowState = WindowState.Normal;
                WindowState = WindowState.Normal;

            }
        }
        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
