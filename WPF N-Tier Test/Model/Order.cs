using System.Transactions;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test_Data_Access.DTOs;
namespace WPF_N_Tier_Test.Model
{
    public class Order : Transaction<ProductBatch>
    {

        public event EventHandler<bool>? TransactionFeaturesChanged;
        protected virtual void OnRiseTransactionFeaturesChanged(bool e)
        {
            // copy to avoid race condition
            EventHandler<bool> handler = TransactionFeaturesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public string TransactionID
        {
            get { return 'O' + '-' + Customer.Id + DateTime.ToString("ddMMyy") + ID; }
        }
        public Order() : base()
        {
            IsPaid = false;
            DateTime = DateTime.Now;
            TransactedEntities = new();
            TransactedEntities.CollectionChanged += (s, o) => OnRiseTransactionFeaturesChanged(true);
        }
        public string State
        {
            get
            {
                if (IsPaid)
                    return "Paid - " + PaymentDate?.ToShortDateString();
                else
                    return "Not Paid";
            }
        }
        public string Since
        {
            get
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(this.DateTime);
                return timeSpan.ToFormattedString();
            }
        }

        public bool IsPaid
        {
            get { return paid; }
            set
            {
                if (value == paid)
                    return;
                if (value)
                    PaymentDate = DateTime.Now;
                paid = value;
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                if (value == discount) return;
                if (value > 100) discount = 100;
                else if (value < 0) discount = 0;
                else discount = value;
                OnRiseTransactionFeaturesChanged(true);
            }
        }

        public double Amount { get { return TransactedEntities.ToList().Sum(a => a.NetTotal); } }
        public double Total => Amount - (Amount * (Discount / 100));
        public double GrossMargin
        {
            get
            {
                double margin = 0;
                foreach (ITransactionBatch batch in TransactedEntities) { margin += batch.TotalPrice - batch.TotalCost; }
                return margin;
            }
        }
    }
}
