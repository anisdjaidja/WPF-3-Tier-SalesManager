using System.Windows.Controls;
using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.Common.UI.Components
{
    /// <summary>
    /// Interaction logic for ProductCardS.xaml
    /// </summary>
    public partial class ProductCardS : UserControl
    {
        public Article product;
        public ProductCardS(Article product)
        {
            InitializeComponent();
            this.product = product;
            TitleBox.Text = product.Name;
            //CategoryLable.Content = categoryDB.GetNameByID(product.Category);
            QuantityBox.Content = string.Format("{0:N1}" , product.Quantity);
            SPrice.Content = string.Format("{0:N2} DA", product.SalePrice);

            //ImageBox.Source = product.Image;
        }
    }
}
