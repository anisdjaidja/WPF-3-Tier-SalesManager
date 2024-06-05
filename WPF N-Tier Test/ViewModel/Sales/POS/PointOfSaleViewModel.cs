using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.ViewModel.Sales.Customers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WPF_N_Tier_Test.ViewModel.Sales.POS
{
    public partial class PointOfSaleViewModel: BaseModel, IArticleSelector
    {
        public class ProductProductBatch
        {
            public int Quantity { get; set; } = 1;

        }
        private StockService stockService;
        [ObservableProperty]
        public ObservableCollection<Article> currentStock = new();


        [ObservableProperty]
        public PointOfSaleCartViewModel cartVM;
        public event Action<Article> BatchCreatorRequested;
        public event Action BatchCreatorCollapsed;

        [ObservableProperty]
        public CreateBatchViewModel? createBatchVM;

        public PointOfSaleViewModel(StockService stockservice, CustomerService customersService)
        {
            this.stockService = stockservice;
            CartVM = new PointOfSaleCartViewModel(customersService);
            LoadStock();
            OnPropertyChanged(nameof(CurrentStock));
        }


        private async void LoadStock()
        {
            var all = await stockService.GetAllAvailableProductsinStock();
            CurrentStock = new ObservableCollection<Article>(all);
        }

        [RelayCommand]
        public void OnProductPicked(Article p)
        {
            if (CheckIfPresent(p))
                return;
            CreateBatchVM = new CreateBatchViewModel(this, p);
            BatchCreatorRequested.Invoke(p);
        }
        private bool CheckIfPresent(Article p)
        {
            return (CartVM.Bucket?.Where(x => x.ProductId == p.Id).FirstOrDefault() != null);
        }

        public void OnProductBatchCreated(ProductBatch? pb)
        {
            if (pb == null)
            {
                BatchCreatorCollapsed?.Invoke();
                return;
            }
            CartVM.OnProductBatchCreated(pb);
            ReportSuccess();
            BatchCreatorCollapsed?.Invoke();
            CreateBatchVM = null;
        }

        public void OnProductBatchEdited(ProductBatch? pb, int id)
        {
            throw new NotImplementedException();
        }

        public void OnProductBatchRemoved(int id)
        {
            throw new NotImplementedException();
        }
    }
}
