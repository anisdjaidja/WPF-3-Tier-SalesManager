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

        private ProductBatch? currentBatch;
        [ObservableProperty]
        private bool edition = false;
        public CreateBatchViewModel(IArticleSelector parent, Article p)
        {
            Parent = parent;
            CurrentBatch = new ProductBatch(p);

        }
        public CreateBatchViewModel(IArticleSelector parent, ProductBatch pb)
        {
            Parent = parent;
            CurrentBatch = pb;
            Edition = true;
        }
        [RelayCommand]
        public void Up()
        {
            CurrentBatch?.SetQuantity(CurrentBatch.Quantity + 1);
            OnPropertyChanged(nameof(CurrentBatch));
        }
        [RelayCommand]
        public void Down()
        {
            CurrentBatch?.SetQuantity(CurrentBatch.Quantity - 1);
            OnPropertyChanged(nameof(CurrentBatch));
        }
        [RelayCommand]
        public void Remove()
        {
            Parent.OnProductBatchRemoved(CurrentBatch.ProductId);
        }
        [RelayCommand]
        public void Accept()
        {
            if (CurrentBatch?.Quantity <= 0) { ReportError(); return; } // Selected lower or equals Zero
            if(Edition)
            {
                Parent.OnProductBatchEdited(CurrentBatch, 0);
                return;
            }
            Parent.OnProductBatchCreated(CurrentBatch);
        }
        [RelayCommand]
        public void Cancel()
        {
            Parent.OnProductBatchCreated(null);
        }
    }
}
