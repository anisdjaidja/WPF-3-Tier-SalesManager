using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test_Data_Access.DataAccess;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Service
{
    public class CustomerService
    {
        private SalesContext dbContext;

        public CustomerService(SalesDesignTimeContextFactory contextFactory)
        {
            this.dbContext = contextFactory.CreateDbContext(null);
            Initialize();
        }
        public async void Initialize()
        {
            if (dbContext.Customers.Count() == 0)
            {
                string file = File.ReadAllText(@"E:\\Current Dev\\WPF N-Tier Test\\WPF N-Tier Test\\Service\\Seeds\\persons.json");
                var ppl = JsonConvert.DeserializeObject<Customer[]>(file);

                await dbContext.AddRangeAsync(ppl!);
                await dbContext.SaveChangesAsync();
                ReportSuccess("Seeded Customers Table with fake data");
            }
        }

        internal async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return dbContext.Customers.Select(x => CustomerFromDTO(x));
        }
        private static Customer CustomerFromDTO(Person p)
        {
            return new Customer
            {
                Id = p.Id,
                Name = p.Name,
                Company = p.Company,
                Fax = p.Fax,
                NIF = p.NIF,
                NIS = p.NIS,
                N_A = p.N_A,
                Address = p.Address,
                Phone = p.Phone,
            };
        }
        //internal async Task<IEnumerable<Customer>> SearchCustomer(List<string> subsets, int v)
        //{
        //    foreach(string subset in subsets)
        //    {

        //    }
        //    return dbContext.Customers.Select(x => x as Customer).Where(x => x.Name.Contains);
        //}



    }
}
