using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test_Data_Access.DTOs;
namespace WPF_N_Tier_Test.Model
{
    public class Order : WPF_N_Tier_Test_Data_Access.DTOs.Transaction<ProductBatch>
    {
        public Customer Customer { get; set; }
        public override string TransactionID
        {
            get { return 'O' + '-' + Customer.Id + DateTime.ToString("ddMMyy") + ID; }
        }
        public Order() : base()
        {
            IsPaid = false;
        }
        public override string State
        {
            get
            {
                if (IsPaid)
                    return "Paid - " + PaymentDate?.ToShortDateString();
                else if (IsValidated)
                    return "Not Paid";
                else
                    return "Proforma";
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

        public bool IsShipped
        {
            get { return shipped; }
            set
            {
                if (value == shipped)
                    return;
                if (value)
                    ShipmentDate = DateTime.Now;
                shipped = value;
                OnRiseTransactionFeaturesChanged(true);
            }
        }

        public bool IsValidated
        {
            get { return validated; }
            set
            {
                if (value == validated)
                    return;
                if (value)
                    ValidationDate = DateTime.Now;
                validated = value;
                OnRiseTransactionFeaturesChanged(true);
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

        public double Amount { get { return TransactedEntities.ToList().Sum(a => a.TotalPrice); } }
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
