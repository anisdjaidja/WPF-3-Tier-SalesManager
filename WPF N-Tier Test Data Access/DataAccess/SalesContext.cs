using Microsoft.EntityFrameworkCore;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test_Data_Access.DataAccess
{
     public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Customers { get; set; }
        public DbSet<Transaction<TransactionBatch>> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
