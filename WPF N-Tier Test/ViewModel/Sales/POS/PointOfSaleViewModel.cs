using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.ViewModel.Sales.Customers;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.ViewModel.Sales.POS
{
    public partial class PointOfSaleViewModel: BaseModel, IArticleSelector
    {
        public class ProductProductBatch
        {
            public int Quantity { get; set; } = 1;

        }
        private StockService stockService;
        private CustomerService customerService;
        public SalesService salesService;
        [ObservableProperty]
        public ObservableCollection<Article> currentStock = new();


        [ObservableProperty]
        public PointOfSaleCartViewModel cartVM;
        public event Action<Article> BatchCreatorRequested;
        public event Action BatchCreatorCollapsed;

        [ObservableProperty]
        public CreateBatchViewModel? createBatchVM;

        public PointOfSaleViewModel(StockService stockservice, CustomerService customersService, SalesService salesService)
        {
            this.stockService = stockservice;
            this.salesService = salesService;
            this.customerService = customersService;
            CartVM = new PointOfSaleCartViewModel(this, customersService);
            LoadStock();
            OnPropertyChanged(nameof(CurrentStock));
            
        }


        private async void LoadStock()
        {
            var all = await stockService.GetAllStock();
            CurrentStock = new ObservableCollection<Article>(all);
        }


        public Article? SelectedArticle
        {
            get
            {
                return null;
            }
            set
            {
                ReportSuccess("article selected");
                if (CheckIfPresent(value))
                    return;
                CreateBatchVM = new CreateBatchViewModel(this, value);
                OnPropertyChanged(nameof(CreateBatchVM));
                BatchCreatorRequested.Invoke(value);
            }
        }
        private bool CheckIfPresent(Article p)
        {
            return (CartVM.Bucket?.Where(x => x.ProductId == p.Id).FirstOrDefault() != null);
        }
        private bool CheckIfPresentbatch(ProductBatch pb)
        {
            return (CartVM.Bucket?.Where(x => x.ProductId == pb.ProductId).FirstOrDefault() != null);
        }
        public void OnProductBatchCreated(ProductBatch? pb)
        {
            if (pb == null)
            {
                BatchCreatorCollapsed?.Invoke();
                return;
            }
            CartVM.OnProductBatchCreated(pb);
            BatchCreatorCollapsed?.Invoke();
            CreateBatchVM = null;
        }
        public void EditProductBatch(ProductBatch? pb)
        {
            CreateBatchVM = new CreateBatchViewModel(this, pb);
            OnPropertyChanged(nameof(CreateBatchVM));
            BatchCreatorRequested.Invoke(null);
        }
        public void OnProductBatchEdited(ProductBatch? pb, int id)
        {
                CartVM.OnProductBatchEdited(pb, 0);
                BatchCreatorCollapsed?.Invoke();
                return;
        }

        public void OnProductBatchRemoved(int id)
        {
            throw new NotImplementedException();
        }

        internal void ResetSale()
        {
            CartVM = new(this, customerService: customerService);
        }
        [RelayCommand]
        public void AddtoFavs()
        {
            AddShortcut(new FavShortcut(0, 1, "P.O.S"));
        }
    }
}
