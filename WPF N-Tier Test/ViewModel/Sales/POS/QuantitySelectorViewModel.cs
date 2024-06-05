using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.ViewModel.Sales.POS
{
    public class QuantitySelectorViewModel
    {
        private Article product;

        public QuantitySelectorViewModel(PointOfSaleCartViewModel pointOfSaleCartViewModel, Article product)
        {
            this.product = product;
        }
    }
}