using Microsoft.Identity.Client.NativeInterop;
using System.Windows;
using System.Windows.Controls;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test.ViewModel.Customers;

namespace WPF_N_Tier_Test.View.Sales.Customers
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : UserControl
    {
        public CustomersPage(CustomersViewModel VM)
        {
            DataContext = VM;
            InitializeComponent();
            

            VM.OrderDetailsRequested += () => AnimationHelper.SlideFadePopup(OrderDetailsPopup, true, AnimationHelper.SlidDirection.Down);
            VM.OrderDetailsClosed += () => AnimationHelper.SlideFadePopup(OrderDetailsPopup, false, AnimationHelper.SlidDirection.Down);   
        }
        
        
        private void GeneratingOrderColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            string colHeader = e.Column.Header.ToString();
            List<string> toCancel = new List<string>
            {
                "ID",
                "IsTaxed",
                "SoldProducts",
                "Margin",
     
                "Validated",
                "Paid",
                "PaymentDate",
                "ValidationDate",
                "Customer",
                "TVA","Discount",
                "TransactedEntities",
                "IsPaid", "IsShipped",
                "IsValidated",
                "GrossMargin",
                "Tax",
                "ShipmentDate",
                "IsBiologicalTest"
            };

            if (toCancel.Contains(colHeader)) { e.Cancel = true; }

            if (colHeader == "TransactionID") 
            { 
                e.Column.DisplayIndex = 0; e.Column.Header = "OID"; 
            }
            if (colHeader == "Since")
            {
                e.Column.DisplayIndex = 1;
                e.Column.Header = FindResource("Since");
            }
            if (colHeader == "DateTime")
            {
                e.Column.DisplayIndex = 2;
                e.Column.Header = FindResource("Date");
                e.Column.Header = "SubTotal";

            }
            if (colHeader == "discount")
            {
                e.Column.DisplayIndex = 3; e.Column.Header = "Discount";
                e.Column.CellStyle = (Style)FindResource("QuantityCell");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N0}%"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }
            if (colHeader == "Amount")
            {
                e.Column.DisplayIndex = 4; e.Column.Header = "SubTotal"; 
                e.Column.CellStyle = (Style)FindResource("QuantityCell");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N2}"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }
            if (colHeader == "Total")
            {
                e.Column.DisplayIndex = 5; e.Column.Header = "Total";
                e.Column.CellStyle = (Style)FindResource("QuantityCell");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N2}"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }
            if (colHeader == "State")
            {
                e.Column.DisplayIndex = 6; e.Column.Header = "State";
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            AnimationHelper.SlideFadeBorder(ToolBarContainer, 
                AnimationHelper.SlidDirection.Left, 5, 300, 
                System.Windows.Media.Animation.EasingMode.EaseOut);
        }
    }
}
