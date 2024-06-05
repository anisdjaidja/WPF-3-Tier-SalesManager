using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.ViewModel.Customers;
using WPF_N_Tier_Test.ViewModel.Sales.POS;

namespace WPF_N_Tier_Test.ViewModel.Sales.Customers
{
    public partial class CreateBatchViewModel: BaseModel
    {
        IArticleSelector Parent;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Quantity), nameof(ProductName), nameof(Discount), nameof(SalePrice))]
        private ProductBatch? currentBatch;
        
        string? ProductName => CurrentBatch?.ProductName;
        
        double? Quantity => CurrentBatch?.Quantity;

        double? Discount => CurrentBatch?.Discount;

        double? SalePrice => CurrentBatch?.UnitPrice;

        public CreateBatchViewModel(IArticleSelector parent, Article p)
        {
            Parent = parent;
            CurrentBatch = new ProductBatch(p);
        }
        public CreateBatchViewModel(IArticleSelector parent, ProductBatch pb)
        {
            Parent = parent;
            CurrentBatch = pb;
        }
        [RelayCommand]
        public void Up()
        {
            CurrentBatch?.SetQuantity(CurrentBatch.Quantity + 1);
        }
        [RelayCommand]
        public void Down()
        {
            CurrentBatch?.SetQuantity(CurrentBatch.Quantity - 1);
        }
        [RelayCommand]
        public void Remove()
        {
            Parent.OnProductBatchRemoved(CurrentBatch.ProductId);
        }
        [RelayCommand]
        public void Accept()
        {
            if (Quantity <= 0) { ReportError(); return; } // Selected lower or equals Zero
            Parent.OnProductBatchCreated(CurrentBatch);
        }
        [RelayCommand]
        public void Cancel()
        {
            Parent.OnProductBatchCreated(null);
        }
    }
}
