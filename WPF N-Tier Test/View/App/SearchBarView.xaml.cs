using System.Windows.Controls;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test.ViewModel.App;

namespace WPF_N_Tier_Test.View.App
{
    /// <summary>
    /// Interaction logic for SearchBarView.xaml
    /// </summary>
    public partial class SearchBarView : UserControl
    {
        public SearchBarView()
        {
            InitializeComponent();
        }
        public void DropDown()
        {
            AnimationHelper.SlideFadePopup(DropPopup, true, AnimationHelper.SlidDirection.Down);
        }
    }
}
