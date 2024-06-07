using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_N_Tier_Test.Common.UI.Workspaces;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.View.Sales.Customers;
using WPF_N_Tier_Test.ViewModel.Sales.Customers;
using WPF_N_Tier_Test.Views.Windows;

namespace WPF_N_Tier_Test.ViewModel.Customers
{
    public partial class CustomersViewModel: BaseModel
    {
        public CustomerService customersService;
        public StockService stockService;
        public SalesService salesService;


        public CustomersViewModel(CustomerService customersService, StockService stockService, SalesService salesService)
        {
            Title = "Patient";
            this.customersService = customersService;
            this.stockService = stockService;
            this.salesService = salesService;
            LoadSales();
        }

        public async void LoadSales()
        {
            IsBusy = true;
            var cs = await salesService.GetAllOrders();
            CurrentOrders = new (cs);
            OnPropertyChanged(nameof(CurrentOrders));
            IsBusy = false;
        }

        #region Orders
        [ObservableProperty]
        public ObservableCollection<Order>? currentOrders;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOrderSelected), nameof(CustomerName))]
        public Order? selectedOrder;
        public bool IsOrderSelected => SelectedOrder != null;
        public string? CustomerName => SelectedOrder?.Customer?.Name;
        public Visibility IsOrderTableVisible => CurrentOrders?.Count > 0 ? Visibility.Visible : Visibility.Collapsed;


        public OrderDetailsViewModel? OrderDetailsVM { get; private set; }
        #endregion

        public event Action OrderDetailsRequested;
        public event Action OrderDetailsClosed;

        private void Facturate(FactureType type)
        {
            //FacturationSpace facturation = new FacturationSpace(SelectedOrder);
            //string title = Current.FindResource("Prescription") + " " + SelectedOrder.TransactionID + " - " + Current.FindResource("Patient") + ": " + SelectedCustomer.Name;
            //ChildWindow box = new ChildWindow(facturation, title);
            //box.Show();
        }
        [RelayCommand]
        public void PrintOrderReceipt()
        {
            Facturate(FactureType.Receipt);
        }



        [RelayCommand]
        public void ShowOrderDetails()
        {
            OrderDetailsVM = new(this);
            OnPropertyChanged(nameof(OrderDetailsVM));
            OrderDetailsRequested?.Invoke();
        }
        public void OnOrderModificationCommited(bool StatusSignal = true)
        {
            if (!StatusSignal) { OrderDetailsClosed?.Invoke(); }
            CollectionViewSource.GetDefaultView(CurrentOrders).Refresh();
        }
    }
}
