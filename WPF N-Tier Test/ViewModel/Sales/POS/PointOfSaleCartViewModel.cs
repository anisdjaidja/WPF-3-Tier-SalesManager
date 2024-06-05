using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Sockets;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;

namespace WPF_N_Tier_Test.ViewModel.Sales.POS
{
    public partial class PointOfSaleCartViewModel : BaseModel, IArticleSelector
    {
        private CustomerService customerService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Total), nameof(Bucket))]
        public Order newOrder;
        [ObservableProperty]
        public double subTotal = 0;
        [ObservableProperty]
        public double total = 0;
        public ObservableCollection<ProductBatch>? Bucket => NewOrder?.TransactedEntities;

        [ObservableProperty]
        public Article selectedProduct;

        public event Action<QuantitySelectorViewModel> ProductSelectionRequested;
        public event Action<bool> ProductSelectionSubmited;


        public PointOfSaleCartViewModel(CustomerService customerService)
        {
            this.customerService = customerService;
            NewOrder = new() { Customer = null };
            NewOrder.TransactionFeaturesChanged += OnNewOrderEdited;
        }
        private void OnNewOrderEdited(object? sender, bool e)
        {
            SubTotal = NewOrder.Amount;
            Total = NewOrder.Total;
        }

        public void OnProductSelection(Article product)
        {
            ProductSelectionRequested.Invoke(new QuantitySelectorViewModel(this, product));
        }

        public void OnProductBatchCreated(ProductBatch? pb)
        {
            if (pb != null)
                Bucket?.Add(pb);
            ProductSelectionSubmited.Invoke(true);
        }

        public void OnProductBatchEdited(ProductBatch? pb, int id)
        {
            if (pb != null)
            {
                Bucket.Remove(Bucket.Where(x => x.ProductId == id).FirstOrDefault());
                Bucket.Add(pb);
            }
            ProductSelectionSubmited.Invoke(true);
        }

        public void OnProductBatchRemoved(int id)
        {
            Bucket.Remove(Bucket.Where(x => x.ProductId == id).FirstOrDefault());
            ProductSelectionSubmited.Invoke(false);
        }
    }
}
