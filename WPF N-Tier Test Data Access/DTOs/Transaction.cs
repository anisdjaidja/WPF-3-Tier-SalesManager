

using System.Collections.ObjectModel;

namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class Transaction<T> where T : ITransactionBatch
    {
        public int ID { get; set; }
        public Transaction()
        {
            DateTime = DateTime.Now;
            TransactedEntities.CollectionChanged += (s, o) => OnRiseTransactionFeaturesChanged(true);
        }
        public virtual string TransactionID
        {
            get { return "T" + '-' + DateTime.ToString("ddMMyy") + ID; }
        }
        public virtual string State   {get { 
                return "Pending";
            }
        }
        public DateTime DateTime { get; set; }
        public bool paid = false;
        public bool validated = false;
        public bool shipped = false;
        public double discount;
        public double transport = 0;
        

        public ObservableCollection<T> TransactedEntities { get; set; } = new();
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


        public DateTime? ShipmentDate { get; set; }
        public DateTime? ValidationDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        
    }
}
