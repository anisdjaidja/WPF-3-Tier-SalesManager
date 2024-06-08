using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;

namespace WPF_N_Tier_Test.ViewModel.Sales.POS
{
    public partial class PointOfSaleCartViewModel : BaseModel, IArticleSelector
    {
        private CustomerService customerService;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Total), nameof(SubTotal), nameof(Bucket), nameof(IsCustomerSelected), nameof(IsEligibleToCheckout))]
        public Order newOrder;
        public double SubTotal => NewOrder.Amount;
        public double Total => NewOrder.Total;
        public ObservableCollection<ProductBatch>? Bucket => NewOrder?.TransactedEntities;

        [ObservableProperty]
        public ObservableCollection<Customer>? customerList;
        public Customer? SelectedCustomer
        {
            set
            {
                NewOrder.Customer = value;
                OnPropertyChanged(nameof(IsCustomerSelected));
                OnPropertyChanged(nameof(IsEligibleToCheckout));
            }
        }
        public bool IsCustomerSelected => NewOrder.Customer != null;
        public bool IsEligibleToCheckout => IsCustomerSelected && (Bucket.Count >= 1);

        [ObservableProperty]
        public Article selectedProduct;

        public event Action<QuantitySelectorViewModel> ProductSelectionRequested;
        public event Action<bool> ProductSelectionSubmited;

        PointOfSaleViewModel Parent;
        public PointOfSaleCartViewModel(PointOfSaleViewModel parent, CustomerService customerService)
        {
            Parent = parent;
            this.customerService = customerService;
            CustomerList = new(customerService.GetAllCustomers().Result.ToList());
            NewOrder = new();
            NewOrder.TransactionFeaturesChanged += (o, s) => NewOrder_TransactionFeaturesChanged();
            
        }

        [RelayCommand]
        private void NewOrder_TransactionFeaturesChanged()
        {
            OnPropertyChanged(nameof(NewOrder));
            OnPropertyChanged(nameof(SubTotal));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(IsEligibleToCheckout));

        }

        public ProductBatch? SelectedBatch
        {
            set
            {
                ReportSuccess("Product Batch selected for edition");
                Parent.EditProductBatch(value);
            }
        }

        public PointOfSaleViewModel PointOfSaleViewModel { get; }
        public CustomerService CustomersService { get; }

        public void OnProductBatchCreated(ProductBatch? pb)
        {
            if (pb != null)
                Bucket?.Add(pb);
            NewOrder_TransactionFeaturesChanged();
            ProductSelectionSubmited?.Invoke(true);
        }

        public void OnProductBatchEdited(ProductBatch? pb, int id)
        {
            if (pb != null)
            {
                int order = NewOrder.TransactedEntities.IndexOf(pb);
                NewOrder.TransactedEntities.Remove(NewOrder.TransactedEntities.Where(x => x.ProductId == pb.ProductId).FirstOrDefault());
                NewOrder.TransactedEntities.Insert(order, pb);
                ReportSuccess("Product batch edited");
                NewOrder_TransactionFeaturesChanged();
            }
        }

        public void OnProductBatchRemoved(int id)
        {
            NewOrder.TransactedEntities.Remove(NewOrder.TransactedEntities.Where(x => x.ProductId == id).FirstOrDefault());
            NewOrder_TransactionFeaturesChanged();

        }
        [RelayCommand]
        public async Task CheckoutOrder()
        {
            if (!IsCustomerSelected)
            {
                ReportError("Please select a customer for the order");
                return;
            }
            var result = await Parent.salesService.INSERT_Order(NewOrder);
            if (result)
            {
                ReportSuccess("Order created !");
                Parent.ResetSale();
            }
                
        }
    }
}
