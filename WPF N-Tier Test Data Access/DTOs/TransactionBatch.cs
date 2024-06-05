namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class TransactionBatch: ITransactionBatch
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Category { get; set; } = 0;
        public string Model { get; set; } = string.Empty;
        public string UnitMetric { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public double discount { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice => UnitPrice * Quantity;
        public virtual double UnitCost { get; set; }
        public double TotalCost => UnitCost * Quantity;

        public virtual void SetQuantity(double quantity)
        {
            if (UnitMetric == "Unit") { Quantity = (int)quantity; return; }
            Quantity = quantity;
        }
    }
}
