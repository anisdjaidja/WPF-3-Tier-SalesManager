using Newtonsoft.Json;
using System.IO;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test_Data_Access.DataAccess;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Service
{
    public class StockService
    {
        private SalesContext dbContext;

        public StockService(SalesDesignTimeContextFactory contextFactory)
        {
            this.dbContext = contextFactory.CreateDbContext(null);
            Initialize();
        }

        public async void Initialize()
        {
            if (dbContext.Products.Count() == 0)
            {
                string file = File.ReadAllText(@"E:\\Current Dev\\WPF N-Tier Test\\WPF N-Tier Test\\Service\\Seeds\\products.json");
                var prods = JsonConvert.DeserializeObject<Article[]>(file);
                
                await dbContext.AddRangeAsync(prods!);
                await dbContext.SaveChangesAsync();
                ReportSuccess("Seeded Products Table with fake data");
            }
        }
        internal async Task<IEnumerable<Article>> GetAllAvailableProductsinStock()
        {
            return dbContext.Products.Select(x => x as Article).Where(x => x.Available);
        }

        internal async Task<IEnumerable<Article>> GetAllStock()
        {
            return dbContext.Products.Select(x => ArticleFromDTO(x));
        }

        private static Article ArticleFromDTO (Product p)
        {
            return new Article
            {
                Id = p.Id,
                Name = p.Name,
                Manufacturer = p.Manufacturer,
                Model = p.Model,
                Quantity = p.Quantity,
                QuantityCap = p.QuantityCap,
                Description = p.Description,
                Category = p.Category,
                BasePrice = p.BasePrice,
                SalePrice = p.SalePrice,
                customBarcode = p.customBarcode,
                Metric = p.Metric,
            };
        }
    }
}
