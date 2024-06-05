using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_N_Tier_Test.Model;


namespace WPF_N_Tier_Test.ViewModel.Customers
{
    public partial class PickProductViewModel : BaseModel
    {
        CreateOrderViewModel Parent;
        [ObservableProperty]
        public ObservableCollection<Article>? stock;
        [ObservableProperty]
        public Article? selectedProduct;
        public PickProductViewModel(CreateOrderViewModel parent)
        {
            Parent = parent;
            LoadStock();
        }
        public async void LoadStock()
        {
            IsBusy = true;
            var cs = await Parent.Parent.stockService.GetAllStock();
            Stock = new ObservableCollection<Article>(cs);
            OnPropertyChanged(nameof(Stock));
            IsBusy = false;
        }
        [RelayCommand]
        public void SelectProduct(Article product)
        {
            if(SelectedProduct != null)
                Parent.OnProductPicked(SelectedProduct);
        }
    }
}