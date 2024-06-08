
using System.Windows;
using System.Windows.Controls;
using WPF_N_Tier_Test.ViewModel.Sales.POS;

namespace WPF_N_Tier_Test.View.Sales.POS
{
    /// <summary>
    /// Interaction logic for PointOfSaleCartView.xaml
    /// </summary>
    public partial class PointOfSaleCartView : UserControl
    {
        public PointOfSaleCartView()
        {
            InitializeComponent();
        }
        public void BindActionEvents()
        {
            var VM = (PointOfSaleCartViewModel)DataContext;
            VM.ProductSelectionRequested += (e) => { };
            VM.ProductSelectionSubmited += (e) => { };
        }
        private void GeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            if (e.Column.Header.ToString() == "ProductId") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "TotalCost") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Model") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Category") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "UnitCost") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "UnitMetric") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "UnitPrice") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Article") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Quantity") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "discount") { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Id") { e.Cancel = true; }

            if (e.Column.Header.ToString() == "ProductName")
            {
                e.Column.DisplayIndex = 0;
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                e.Column.CellStyle = (Style)FindResource("NameCellAlt");
                e.Column.Header = "Product";
            }
            if (e.Column.Header.ToString() == "FormatedQuantity")
            {
                e.Column.DisplayIndex = 1;
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                e.Column.Header = "Quantity";
            }
            if (e.Column.Header.ToString() == "SalePrice")
            {
                e.Column.DisplayIndex = 2;
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                e.Column.Header = "Unit Price";
                e.Column.CellStyle = (Style)FindResource("QuantityCellAlt");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N2}"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }
            if (e.Column.Header.ToString() == "Discount")
            {
                e.Column.DisplayIndex = 3;
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                e.Column.Header = "Discount";
                e.Column.CellStyle = (Style)FindResource("QuantityCellAlt");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N0}%"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }
            if (e.Column.Header.ToString() == "TotalPrice")
            {
                e.Column.DisplayIndex = 4;
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                e.Column.Header = "Subtotal";
                e.Column.CellStyle = (Style)FindResource("QuantityCellAlt");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N2}"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }
            if (e.Column.Header.ToString() == "NetTotal")
            {
                e.Column.DisplayIndex = 5;
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                e.Column.Header = "Total";
                e.Column.CellStyle = (Style)FindResource("QuantityCellAlt");
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = "{0 :N2}"; dataGridTextColumn.Binding.FallbackValue = 0; }
            }



        }
    }
}
