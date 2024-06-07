

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class Transaction<T> where T : ITransactionBatch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        [ForeignKey(nameof(CustomerId))]
        public Person Customer { get; set; }
        public DateTime DateTime { get; set; }
        public bool paid = false;
        public double discount { get; set; }
        public ObservableCollection<T> TransactedEntities { get; set; }
        public DateTime? PaymentDate { get; set; }

    }
}
