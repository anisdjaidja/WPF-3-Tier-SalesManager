using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test.ViewModel.Navigation;

namespace WPF_N_Tier_Test.View.Navigation
{
    /// <summary>
    /// Interaction logic for NavigationSideBarView.xaml
    /// </summary>
    public partial class NavigationSideBarView : UserControl
    {
        public NavigationSideBarView() //NavigationSideBarViewModel VM
        {
            InitializeComponent();
            //DataContext = VM;
            //InventoryBrief.DataContext = VM;
            //CustomerBrief.DataContext = VM;
        }

        private void Home_btn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
