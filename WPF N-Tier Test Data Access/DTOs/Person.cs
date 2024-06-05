
using System.ComponentModel.DataAnnotations;

namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Fax { get; set; } = string.Empty;

        public string NIF { get; set; } = string.Empty;

        public string NIS { get; set; } = string.Empty;

        public string N_A { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

    }
}
