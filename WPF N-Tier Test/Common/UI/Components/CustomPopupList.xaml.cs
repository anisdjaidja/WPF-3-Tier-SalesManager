using System.Windows;
using System.Windows.Controls;

namespace WPF_N_Tier_Test.Common.UI.Components
{
    /// <summary>
    /// Interaction logic for CustomPopupList.xaml
    /// </summary>
    public partial class CustomPopupList : UserControl
    {
        public CustomPopupList(List<string> Items)
        {
            InitializeComponent();
            foreach (var item in Items) {
                var b = new Button { Content = item, Style = (Style)FindResource("LongMenuBtn") };
                b.Click += (s, e) => { OnChoiceTaken(MenuStack.Children.IndexOf(b)); };
                MenuStack.Children.Add(b); }
            //this.LostFocus += (s, e) => { OnChoiceTaken(-1); };
        }
        public event EventHandler<int> ChoiceTaken;

        public virtual void OnChoiceTaken(int e)
        {
            EventHandler<int> handler = ChoiceTaken;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
