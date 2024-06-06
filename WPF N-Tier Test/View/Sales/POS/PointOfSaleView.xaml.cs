using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test.ViewModel.Sales.POS;
using static WPF_N_Tier_Test.Modules.Helpers.AnimationHelper;

namespace WPF_N_Tier_Test.View.Register
{
    /// <summary>
    /// Interaction logic for PointOfSaleView.xaml
    /// </summary>
    public partial class PointOfSaleView : UserControl
    {
        public PointOfSaleView(PointOfSaleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.BatchCreatorRequested += (a) => AnimationHelper.SlideFadePopup(QSelectorPopup, true, AnimationHelper.SlidDirection.Down);
            viewModel.BatchCreatorCollapsed += () => AnimationHelper.SlideFadePopup(QSelectorPopup, false, AnimationHelper.SlidDirection.Down);

        }
        private void PreviewNumericInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char last = e.Text[^1];
            e.Handled = !(last == '.') && !double.TryParse(e.Text, out _);
        }
        private void PastingNumeric(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!double.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
