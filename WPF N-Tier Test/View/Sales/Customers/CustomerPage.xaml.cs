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
            InitializeComponent();
            DataContext = VM;

            VM.OrderDetailsRequested += () => AnimationHelper.SlideFadePopup(OrderDetailsPopup, true, AnimationHelper.SlidDirection.Down);
            VM.OrderDetailsClosed += () => AnimationHelper.SlideFadePopup(OrderDetailsPopup, false, AnimationHelper.SlidDirection.Down);   
        }
        
        
        private void GeneratingCustomerColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            if (e.Column.Header.ToString() == "Id") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "ProformaCount") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "ConfirmedCount") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Company") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "N_A") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "NIS") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "NIF") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Fax") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Expenditure") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Payement") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Debt") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Name") { 
                e.Column.DisplayIndex = 0; 
                e.Column.CellStyle = (Style)FindResource("NameCell"); 
                e.Column.Header = FindResource("Name");
                e.Column.HeaderStyle = (Style)TryFindResource("NameColHeader");
                
            }
            if (e.Column.Header.ToString() == "Phone") { e.Column.Header = FindResource("Phone"); }
            if (e.Column.Header.ToString() == "BirthDate") { e.Column.Header = FindResource("BirthDate"); (e.Column as DataGridTextColumn).Binding.StringFormat = "dd / MM / yyyy"; }
            if (e.Column.Header.ToString() == "Gender") { e.Column.Header = FindResource("Gendre"); }
            if (e.Column.Header.ToString() == "Age") { e.Column.Header = FindResource("Age"); }
            if (e.Column.Header.ToString() == "Address") { e.Column.Header = FindResource("Address"); }
            if (e.Column.Header.ToString() == "orders") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "EligibleToDelete") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Orders") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "OrdersCount") { e.Cancel = true; }
        }
        private void GeneratingOrderColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            string colHeader = e.Column.Header.ToString();
            List<string> toCancel = new List<string>
            {
                "ID",
                "TotalAmount",
                "IsTaxed",
                "SoldProducts",
                "Margin",
                "CustomerID","Validated","Paid","PaymentDate","ValidationDate",
                "Total","Customer",
                "TVA","Discount","TransactedEntities", "IsPaid", "IsShipped", "IsValidated", "Amount", "GrossMargin", "Tax", "ShipmentDate", "State", "IsBiologicalTest"
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
            if (colHeader == "Description")
            {
                e.Column.DisplayIndex = 2;
                e.Column.Header = FindResource("Illness");
            }
            if (colHeader == "DateTime")
            {
                e.Column.DisplayIndex = 3;
                e.Column.Header = FindResource("Date");
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
