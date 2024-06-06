
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Fax { get; set; }

        public string NIF { get; set; }

        public string NIS { get; set; }

        public string N_A { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

    }
}
