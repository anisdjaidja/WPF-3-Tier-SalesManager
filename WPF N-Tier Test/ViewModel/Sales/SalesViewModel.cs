using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.View.Register;
using WPF_N_Tier_Test.View.Sales.Customers;
using WPF_N_Tier_Test.ViewModel.Customers;
using WPF_N_Tier_Test.ViewModel.Sales.POS;

namespace WPF_N_Tier_Test.ViewModel.Sales
{
    public partial class SalesViewModel: BaseModel
    {
        private readonly StockService stockService;
        private readonly CustomerService customersService;
        public List<UIElement> Pages;
        [ObservableProperty] public UIElement? currentPage;

        public CustomersViewModel CustomersVM;
        private PointOfSaleViewModel PointOfSaleVM;

        public SalesViewModel(StockService stockservice, CustomerService customersService)
        {
            this.stockService = stockservice;
            this.customersService = customersService;
            ResolveViewModels();
            ResolvePages();
        }
        void ResolveViewModels()
        {
            CustomersVM = new CustomersViewModel(customersService, stockService);
            PointOfSaleVM = new PointOfSaleViewModel(stockService, customersService);
        }
        void ResolvePages()
        {
            Pages = new()
            {
                new CustomersPage(CustomersVM),
                null,
                null,
                null,
                null,
            };
            SwitchPage(0);
        }

        [RelayCommand]
        public void SwitchPage(object PageIDX)
        {
            CurrentPage = Pages[int.Parse(PageIDX.ToString())];
        }
    }
}
