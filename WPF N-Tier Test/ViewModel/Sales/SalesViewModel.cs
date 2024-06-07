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
    public partial class SalesViewModel: BaseModel, INavigationViewModel
    {
        private readonly StockService stockService;
        private readonly CustomerService customersService;
        public readonly SalesService salesService;
        public List<UIElement> Pages;
        [ObservableProperty] public UIElement? currentPage;

        public CustomersViewModel CustomersVM;
        private PointOfSaleViewModel PointOfSaleVM;

        

        public SalesViewModel(StockService stockservice, CustomerService customersService, SalesService salesService)
        {
            this.stockService = stockservice;
            this.customersService = customersService;
            this.salesService = salesService;
            ResolveViewModels();
            ResolvePages();
        }
        void ResolveViewModels()
        {
            CustomersVM = new CustomersViewModel(customersService, stockService, salesService);
            PointOfSaleVM = new PointOfSaleViewModel(stockService, customersService, salesService);
        }
        void ResolvePages()
        {
            Pages = new()
            {
                new CustomersPage(CustomersVM),
                new PointOfSaleView(PointOfSaleVM),
                null,
                null,
                null,
            };
            SwitchPage(0);
        }

        [RelayCommand]
        public void SwitchPage(object PageIDX)
        {
            NavigateTo(int.Parse(PageIDX.ToString()));
        }
        public void NavigateTo(int pageIndex)
        {
            CurrentPage = Pages[pageIndex];
        }

        public void NavigateToTab(int idx, int tabIdx)
        {
            throw new NotImplementedException();
        }
    }
}
