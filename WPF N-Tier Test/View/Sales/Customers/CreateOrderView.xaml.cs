using System.Windows;
using System.Windows.Controls;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Modules.Helpers;

using WPF_N_Tier_Test.ViewModel.Customers;
using static WPF_N_Tier_Test.Modules.Helpers.AnimationHelper;

namespace WPF_N_Tier_Test.View.Sales.Customers
{
    /// <summary>
    /// Interaction logic for OrderFormView.xaml
    /// </summary>
    public partial class CreateOrderView : UserControl
    {
        public CreateOrderView(CreateOrderViewModel VM)
        {
            InitializeComponent();
            DataContext = VM;

            VM.ProductPickerRequested += () => AnimationHelper.SlideFadePopup(StockPopup, true, AnimationHelper.SlidDirection.Down);
            VM.ProductPickerCollapsed += () => AnimationHelper.SlideFadePopup(StockPopup, false, AnimationHelper.SlidDirection.Down);

            VM.BatchCreatorRequested += (e) => AnimationHelper.SlideFadePopup(AddProductPopup, true, AnimationHelper.SlidDirection.Down);
            VM.BatchCreatorCollapsed += () => AnimationHelper.SlideFadePopup(AddProductPopup, false, AnimationHelper.SlidDirection.Down);
        }
        private void GeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            string colHeader = e.Column.Header.ToString();
            List<string> toCancel = new List<string>
            {
                "Description",
                "BasePrice",
                "MarginPrice",
                "Category",
                "ProductId",
                "FormatedQuantity",
                "QuantityCap",
                "isTaxed", 
                "TaxRate",
                "MarginPrice", 
                "SalePrice", 
                "TotalCost", 
                "Model",
                "Manufacturer", 
                "Quantity", 
                "UnitMetric", 
                "UnitCost", 
                "UnitPrice", 
                "TotalPrice", 
            };


            if ( toCancel.Where(x => x == colHeader).FirstOrDefault() != null ) { e.Cancel = true; }

            if (colHeader == "ProductName") { e.Column.CellStyle = (Style)FindResource("NameCellAlt"); e.Column.Header = FindResource("Medicine"); }
            if (colHeader == "Duration") { e.Column.Header = FindResource("days"); (e.Column as DataGridTextColumn).Binding.StringFormat = "dd"; }
            if (colHeader == "MedUnit") e.Column.Header = FindResource("Type");
            if (colHeader == "DurationDesc") e.Column.Header = FindResource("days optional");
            

        }
    }
}
