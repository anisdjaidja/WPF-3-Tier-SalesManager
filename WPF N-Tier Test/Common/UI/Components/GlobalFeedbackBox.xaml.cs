using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WPF_N_Tier_Test.Modules.Common;

namespace WPF_N_Tier_Test.Common.UI.Components
{
    /// <summary>
    /// Interaction logic for GlobalFeedbackBox.xaml
    /// </summary>
    public partial class GlobalFeedbackBox : System.Windows.Controls.UserControl
    {
        public GlobalFeedbackBox()
        {
            InitializeComponent();
            Hide();
        }

        public void Hook(GlobalMessageStore _globalMessageStore) {

            _globalMessageStore.CurrentMessageChanged += GlobalMessageStore_CurrentMessageChanged;
            _globalMessageStore.MessageTypeChanged += GlobalMessageStore_MessageTypeChanged;
        }

        private void GlobalMessageStore_MessageTypeChanged(object? sender, GlobalMessageType e)
        {
            if (e == GlobalMessageType.Error)
            {
                MessageBox.Foreground = Brushes.White;
                MainBorder.Background = Brushes.IndianRed;
            }
            else {
                MessageBox.Foreground = Brushes.White;
                MainBorder.Background = Brushes.DodgerBlue;
            }
        }

        private void GlobalMessageStore_CurrentMessageChanged(object? sender, string e)
        {
            MessageBox.Text = e;
            Show();

            //await Task.Delay(2000);
            Storyboard storyboard = new();
            DoubleAnimation fadeAnimation = new()
            {
                Duration = new Duration(TimeSpan.FromSeconds(6)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn },
                From = 1,
                To = 0,
                FillBehavior = FillBehavior.HoldEnd
            };
            storyboard.Children.Add(fadeAnimation);
            Storyboard.SetTarget(fadeAnimation, this);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));

            storyboard.Completed += (s, o) => {
               
            };

            storyboard.Begin();
        }

        public void Show()
        {
            this.Opacity = 1;
        }
        public void Hide()
        {
            this.Opacity = 0;
        }
    }
}
