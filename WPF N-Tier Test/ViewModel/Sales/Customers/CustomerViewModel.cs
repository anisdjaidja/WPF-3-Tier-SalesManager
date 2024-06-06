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

        public CustomersViewModel(CustomerService customersService, StockService stockService)
        {
            Title = "Patient";
            this.customersService = customersService;
            this.stockService = stockService;
            LoadAllCustomers();
  
        }
        public async void LoadAllCustomers()
        {
            IsBusy = true;
            var cs = await customersService.GetAllCustomers();
            Customers = new ObservableCollection<Customer>(cs);
            OnPropertyChanged(nameof(Customers));
            IsBusy = false;
        }
        #region Customers
        [ObservableProperty]
        public ObservableCollection<Customer>? customers;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCustomerSelected),
            nameof(CurrentOrders),
            nameof(IsOrderTableVisible), 
            nameof(SelectedPerson),
            nameof(CustomerName))]
        public Customer? selectedCustomer;

        public Customer? SelectedPerson => SelectedCustomer;
        public string? PersonDecoratorName { get; set; } = "Customer";
        public string? CustomerName => SelectedCustomer?.Name;
        public bool IsCustomerSelected => SelectedCustomer != null;
        public Visibility IsOrderTableVisible => IsCustomerSelected ? Visibility.Visible : Visibility.Collapsed;

        #endregion

        #region Orders
        public ObservableCollection<Order>? CurrentOrders => SelectedCustomer?.Orders;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOrderSelected),
            nameof(IsEligibleToBill)
            )]
        public Order? selectedOrder;
        public bool IsOrderSelected => SelectedOrder != null;
        public bool IsEligibleToBill => (SelectedOrder?.IsPaid )?? false;


        public OrderDetailsViewModel? OrderDetailsVM { get; private set; }
        [ObservableProperty]
        public CreateOrderViewModel? createOrderVM;
        [ObservableProperty]
        public UserControl? createOrderV;
        #endregion

        public event Action OrderDetailsRequested;
        public event Action OrderDetailsClosed; 

        public void GotoPatient(int PatientID)
        {
            SelectedCustomer = Customers.Where(x => x.Id == PatientID).FirstOrDefault();
        }

        private void Facturate(FactureType type)
        {
            FacturationSpace facturation = new FacturationSpace(SelectedOrder, SelectedCustomer);
            string title = Current.FindResource("Prescription") + " " + SelectedOrder.TransactionID + " - " + Current.FindResource("Patient") + ": " + SelectedCustomer.Name;
            ChildWindow box = new ChildWindow(facturation, title);
            box.Show();
        }

        [RelayCommand]
        public void PrintOrderProforma()
        {
            Facturate(FactureType.Proforma);
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
            CollectionViewSource.GetDefaultView(Customers).Refresh();
            CollectionViewSource.GetDefaultView(CurrentOrders).Refresh();
        }
    }
}
