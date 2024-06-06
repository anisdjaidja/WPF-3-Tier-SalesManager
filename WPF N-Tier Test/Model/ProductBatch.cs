using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Model
{
    public class ProductBatch: TransactionBatch
    {
        public ProductBatch()
        {
            
        }
        public ProductBatch(Article P)
        {
            ProductId = P.Id;
            ProductName = P.Name;
            Model = P.Model;
            Category = P.Category;
            Quantity = P.Quantity;
            UnitPrice = P.SalePrice;
            UnitCost = P.BasePrice;
            UnitMetric = P.Metric;
        }
        public string FormatedQuantity
        {
            get
            {
                return Quantity.ToString() + " " + UnitMetric;
            }
        }
        public double NetTotal => TotalPrice - TotalPrice * (Discount / 100);
        public double SalePrice
        {
            get { return UnitPrice; }
            set
            {
                if (value < 0)
                    UnitPrice = 0;
                else UnitPrice = value;
            }
        }
        public double Discount
        {
            get { return discount;}
            set
            {
                if (value > 100)
                    discount = 100;
                else if (value < 0)
                    discount = 0;
                else discount = value;
            }
        }
    }
}
