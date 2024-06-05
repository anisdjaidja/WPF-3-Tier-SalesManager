using System.Windows;
using System.Windows.Controls;

namespace WPF_N_Tier_Test.View.App
{
    /// <summary>
    /// Interaction logic for SearchDropDownView.xaml
    /// </summary>
    public partial class SearchDropDownView : UserControl
    {
        public SearchDropDownView()
        {
            InitializeComponent();
        }
        private void GeneratingCustomerColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            string colHeader = e.Column.Header.ToString();
            List<string> toCancel = new List<string>
            {
                "Expenses",
                "Payement",
                "Fax",
                "Orders",
                "Company",
                "Id",
                "Expenditure",
                "Address",
                "EligibleToDelete",
                "NIF",
                "ProformaCount",
                "ConfirmedCount",
                "NIS",
                "N_A",
                "Phone", "Debt", "OrdersCount",
                "Age", "Gender","BirthDate",

            };

            if (toCancel.Contains(colHeader)) { e.Cancel = true; }
            if (colHeader == "Name")
            {
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                e.Column.CellStyle = (Style)FindResource("NameCellSearch");
            }
        }

        private void GeneratingStockColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...

            string colHeader = e.Column.Header.ToString();
            List<string> toCancel = new List<string>
            { "QuantityCap",
                "BasePrice",
                "Image",
                "Id",
                "BasePrice",
                "Manufacturer",
                "Model",
                "MarginPrice",
                "Model",
                "Category",
                "Description", };

            if (toCancel.Contains(colHeader)) { e.Cancel = true; }

            if (colHeader == "Name")
            {
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                e.Column.DisplayIndex = 0;
                e.Column.CellStyle = (Style)FindResource("NameCellSearch");
            }
            if (colHeader == "Quantity")
            {
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                e.Column.DisplayIndex = 1;
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = FindResource("Quantity") + ": {0:N1}"; }
            }
            if (colHeader == "SalePrice")
            {
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                e.Column.DisplayIndex = 2;
                DataGridTextColumn? dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null) { dataGridTextColumn.Binding.StringFormat = FindResource("Price") + ": {0:N2} DA"; }
            }
        }
    }
}
