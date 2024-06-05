

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class Transaction<T> where T : ITransactionBatch
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Person Customer { get; set; }
        public DateTime DateTime { get; set; }
        public bool paid = false;
        public bool validated = false;
        public bool shipped = false;
        public double discount { get; set; }
        public ObservableCollection<T> TransactedEntities { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public DateTime? ValidationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
