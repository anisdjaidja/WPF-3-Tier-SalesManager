
using System.Windows;
using System.Windows.Controls;
using WPF_N_Tier_Test.ViewModel.Customers;

namespace WPF_N_Tier_Test.View.Sales.Customers
{
    /// <summary>
    /// Interaction logic for PickProductView.xaml
    /// </summary>
    public partial class PickProductView : UserControl
    {
        public PickProductView()
        {
            InitializeComponent();
        }

        private void GeneratingColumnsAlt(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //...
            string colHeader = e.Column.Header.ToString();
            List<string> toCancel = new List<string>
            {
                "Description",
                "BasePrice",
                "MarginPrice",
                "Category",
                "Id",
                "QuantityCap",
                "MarginPrice", "SalePrice","TotalCost", "Model","Manufacturer", "Quantity","Duration","LowOnQuantity","Barcode",
            };


            if (toCancel.Contains(colHeader)) { e.Cancel = true; }
            if (e.Column.Header.ToString() == "Name") { e.Column.Header = FindResource("Name"); e.Column.CellStyle = (Style)FindResource("NameCell"); }
            if (e.Column.Header.ToString() == "Image")
            {
                FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
                image.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding(e.PropertyName));

                e.Column = new DataGridTemplateColumn
                {
                    CellTemplate = new DataTemplate() { VisualTree = image },
                    Header = e.PropertyName
                };
                e.Column.Header = string.Empty;
                e.Column.DisplayIndex = 0;
                e.Column.CellStyle = (Style)FindResource("ImageCellAlt");
            }
        }
    }
}
