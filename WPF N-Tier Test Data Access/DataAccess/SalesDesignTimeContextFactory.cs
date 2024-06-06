using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_N_Tier_Test_Data_Access.DataAccess
{
    public class SalesDesignTimeContextFactory : IDesignTimeDbContextFactory<SalesContext>
    {
        public SalesContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder<SalesContext>().UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NTierTestDB;Integrated Security=True;").Options;
            return new SalesContext(options);
        }
    }
}
