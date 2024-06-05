

using System.ComponentModel.DataAnnotations;

namespace WPF_N_Tier_Test_Data_Access.DTOs
{
    public class Product
    {
        [Key]
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public double Quantity { get { return 1; } set {  } }

        public double QuantityCap { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public int Category { get; set; } = 0;

        public double BasePrice { get; set; } = 0;

        public double SalePrice { get; set; } = 0;

        public string customBarcode = string.Empty;

        public string Metric = "Unit";
    }
}
