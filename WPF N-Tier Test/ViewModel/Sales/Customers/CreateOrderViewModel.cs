using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.ViewModel.Sales.Customers;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.ViewModel.Customers
{
    public partial class CreateOrderViewModel : BaseModel
    {
        public CustomersViewModel Parent;

        public Customer? CustomerOrdering => (Parent.SelectedPerson as Customer);
        [ObservableProperty]
        public ObservableCollection<Article>? currentStock;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Discount), nameof(Total), nameof(Bucket))]
        public Order newOrder;
        [ObservableProperty]
        public double subTotal = 0;
        [ObservableProperty]
        public double discount = 0;
        [ObservableProperty]
        public double total = 0;

        public ObservableCollection<ProductBatch>? Bucket => NewOrder?.TransactedEntities;
        public event Action ProductPickerRequested;
        public event Action ProductPickerCollapsed;

        public event Action<Article> BatchCreatorRequested;
        public event Action BatchCreatorCollapsed;

        [ObservableProperty]
        public CreateBatchViewModel? createBatchVM;
        [ObservableProperty]
        public PickProductViewModel? pickProductVM;
        public CreateOrderViewModel(CustomersViewModel Parent)
        {
            this.Parent = Parent;

            LoadStock();
            
            OnPropertyChanged(nameof(CurrentStock));
            NewOrder = new() { Customer = CustomerOrdering };
            NewOrder.TransactionFeaturesChanged += OnNewOrderEdited;
        }
        private async void LoadStock()
        {
            var all = await Parent.stockService.GetAllAvailableProductsinStock();
            CurrentStock = new ObservableCollection<Article>(all);
        }
        private void OnNewOrderEdited(object? sender, bool e)
        {
            SubTotal = NewOrder.Amount;
            Discount = NewOrder.Discount;
            Total = NewOrder.Total;
        }

        [RelayCommand]
        private async Task PlaceOrder()
        {
            if (!string.IsNullOrWhiteSpace(OtherTest))
                Bucket.Add(new ProductBatch { ProductName =  OtherTest });
            //Parent.PlaceNewOrder();
        }
        [RelayCommand]
        private void CancelOrder()
        {
            //Parent.OnEndCreateOrder(false);
        }
        [RelayCommand]
        private void AddProduct()
        {
            PickProductVM = new PickProductViewModel(this);
            ProductPickerRequested.Invoke();
        }
        [RelayCommand]
        public void OnProductPicked(Article p)
        {
            //ProductPickerCollapsed.Invoke();
            //CreateBatchVM = new CreateBatchViewModel(this, p);
            //BatchCreatorRequested.Invoke(p);
            //PickProductVM = null;
        }
        #region Tests
        [ObservableProperty]
        public string otherTest;
        public System.Collections.IList SelectedTests
        {
            get
            {
                return Bucket;
            }
            set
            {
                Bucket.Clear();
                foreach (Product model in value)
                {
                    Bucket.Add(new ProductBatch { ProductName = model.Name });
                }
            }
        }
        [RelayCommand]
        public void OnTestPicked(Object p)
        {
            ProductBatch pb = new ProductBatch() { ProductName = p.ToString() };
            Bucket?.Add(pb);
            OnPropertyChanged(nameof(Bucket));
            ReportSuccess();
        } 
        #endregion
        [RelayCommand]
        public void OnProductBatchCreated(ProductBatch? pb)
        {
            if (pb == null)
            {
                BatchCreatorCollapsed?.Invoke();
                return;
            }
            Bucket?.Add(pb);
            ReportSuccess();
            BatchCreatorCollapsed?.Invoke();
            CreateBatchVM = null;
        }
        [RelayCommand]
        public void OnProductBatchRemove(int id)
        {
            var pb = Bucket?.Where(x => x.ProductId == id).FirstOrDefault();
            if (pb == null)
            {
                ReportError("Product not on the order list");
                return;
            }
            Bucket.Remove(pb);
            ReportSuccess();
            BatchCreatorCollapsed?.Invoke();
        }

    }
}
