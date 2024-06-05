using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.ViewModel.Sales
{
    public interface IArticleSelector
    {
        public void OnProductBatchCreated(ProductBatch? pb);
        public void OnProductBatchEdited(ProductBatch? pb, int id);
        public void OnProductBatchRemoved(int id);

    }
}
