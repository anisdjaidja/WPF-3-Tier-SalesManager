namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public interface ITransactionBatch
    {
        public double TotalPrice { get; }
        public double TotalCost { get; }

        public void SetQuantity(double quantity);
    }
}