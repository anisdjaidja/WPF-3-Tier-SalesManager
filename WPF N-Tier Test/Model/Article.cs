namespace WPF_N_Tier_Test.Model
{
    public class Article: WPF_N_Tier_Test_Data_Access.DTOs.Product
    {
        public string ProductID
        {
            get
            {
                return "PID-" + Id.ToString("D4");
            }
        }
        public string Barcode 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(customBarcode))
                    return ProductID;
                return customBarcode;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    customBarcode = value;
            }
        }
        public double MarginPrice
        {
            get
            {
                return Math.Round(SalePrice - BasePrice);
            }
        }
        public bool Available
        {
            get
            {
                return Quantity > 0;
            }
        }
        public bool LowOnQuantity { 
            get 
            { 
                if(QuantityCap <= 0) 
                    return false;
                return Quantity <= QuantityCap; 
            } 
        }
        public double GetCurrentStockValue() { return BasePrice * Quantity; }
    }
}
