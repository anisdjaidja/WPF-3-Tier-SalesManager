using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test_Data_Access.DataAccess;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Service
{
    public class StockService
    {
        private SalesContext dbContext;

        public StockService(SalesContext dbContext)
        {
            this.dbContext = dbContext;
            Initialize();
        }

        public async void Initialize()
        {
            if (dbContext.Products.Count() == 0)
            {
                string file = File.ReadAllText(@"E:\\Current Dev\\WPF N-Tier Test\\WPF N-Tier Test\\Service\\Seeds\\products.json");
                var ppl = JsonConvert.DeserializeObject<Article[]>(file);
                
                await dbContext.AddRangeAsync(ppl);
                await dbContext.SaveChangesAsync();
            }
        }
        internal async Task<IEnumerable<Article>> GetAllAvailableProductsinStock()
        {
            return dbContext.Products.Select(x => x as Article);
        }

        internal async Task<IEnumerable<Article>> GetAllStock()
        {
            return new List<Article>();
        }
    }
}
