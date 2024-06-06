using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Sockets;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.ViewModel.Sales.Customers;

namespace WPF_N_Tier_Test.ViewModel.Sales.POS
{
    public partial class PointOfSaleCartViewModel : BaseModel, IArticleSelector
    {
        private CustomerService customerService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Total), nameof(SubTotal), nameof(Bucket))]
        public Order newOrder;
        public double SubTotal => NewOrder.Amount;
        public double Total => NewOrder.Total;
        public ObservableCollection<ProductBatch>? Bucket => NewOrder?.TransactedEntities;
        [ObservableProperty]
        public ObservableCollection<Customer>? customerList;
        [ObservableProperty]
        public Customer? selectedCustomer;

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
            NewOrder = new() { Customer = null };
            NewOrder.TransactionFeaturesChanged += (o,s) => NewOrder_TransactionFeaturesChanged();
        }

        private void NewOrder_TransactionFeaturesChanged()
        {
            OnPropertyChanged(nameof(NewOrder));
            OnPropertyChanged(nameof(SubTotal));
            OnPropertyChanged(nameof(Total));
        }

        public ProductBatch? SelectedBatch
        {
            set
            {
                ReportSuccess("Product Batch selected for edition");
                Parent.EditProductBatch(value);
            }
        }

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
    }
}
