using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_N_Tier_Test.View.Sales.Customers
{
    /// <summary>
    /// Interaction logic for CreateBatchView.xaml
    /// </summary>
    public partial class CreateBatchView : UserControl
    {
        public CreateBatchView()
        {
            InitializeComponent();
        }
        private void PreviewNumericInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char last = e.Text[^1];
            e.Handled = !(last == '.') && !double.TryParse(e.Text, out _);
        }
        private void PreviewIntegerInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char last = e.Text[^1];
            e.Handled = !double.TryParse(e.Text, out _);
        }
        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(QuantityBox.Text)) QuantityBox.Text = "0";
        }

        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == System.Windows.Input.Key.Enter) { Validate(); }
            //if (e.Key == System.Windows.Input.Key.Escape) { OnActionTaken(false); }
            //if (e.Key == System.Windows.Input.Key.Delete) { RemoveBatch(); }

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            QuantityBox.Focus();
            QuantityBox.SelectAll();
        }

        private void QuantityBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == System.Windows.Input.Key.Delete) { e.Handled = false; RemoveBatch(); }
        }
    }
}
