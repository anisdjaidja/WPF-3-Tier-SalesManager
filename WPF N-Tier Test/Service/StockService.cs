using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.Service
{
    public class StockService
    {
        internal async Task<IEnumerable<Article>> GetAllAvailableProductsinStock()
        {
            return new List<Article>();
        }

        internal async Task<IEnumerable<Article>> GetAllStock()
        {
            return new List<Article>();
        }
    }
}
