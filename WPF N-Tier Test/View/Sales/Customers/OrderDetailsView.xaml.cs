using System.Windows;
using System.Windows.Controls;

namespace WPF_N_Tier_Test.View.Sales.Customers
{
    /// <summary>
    /// Interaction logic for OrderDetailsView.xaml
    /// </summary>
    public partial class OrderDetailsView : UserControl
    {
        public OrderDetailsView()
        {
            InitializeComponent();
        }
        private void SaleBucketTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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


            if (toCancel.Where(x => x == colHeader).FirstOrDefault() != null) { e.Cancel = true; }

            if (colHeader == "ProductName") { e.Column.CellStyle = (Style)FindResource("NameCellAlt"); e.Column.Header = FindResource("Medicine"); }
            if (colHeader == "Duration") { e.Column.Header = FindResource("days"); (e.Column as DataGridTextColumn).Binding.StringFormat = "dd"; }
            if (colHeader == "MedUnit") e.Column.Header = FindResource("Type");
            if (colHeader == "DurationDesc") e.Column.Header = FindResource("days optional");


        }
    }
}
