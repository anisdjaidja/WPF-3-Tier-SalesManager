using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.ViewModel.Customers;

namespace WPF_N_Tier_Test.ViewModel.Sales.Customers
{
    public partial class OrderDetailsViewModel : BaseModel
    {
        CustomersViewModel Parent;
        
        public Order? SelectedOrder => Parent.SelectedOrder;
        public int OID => (SelectedOrder?.ID) ?? -1;
        public string? OrderID => (SelectedOrder?.TransactionID) ?? "Unknown";
        public int CustomerID => (Parent.SelectedPerson as Customer)?.Id ?? -1;
        public double? Total => SelectedOrder?.Total;
        public ObservableCollection<ProductBatch>? curretProductBatches => SelectedOrder?.TransactedEntities;
        public bool IsOrderEligibleToPay => !SelectedOrder?.IsPaid ?? false && (!SelectedOrder?.IsPaid ?? false);
        public bool IsOrderEligibleToRevoke => SelectedOrder?.IsPaid ?? false;

        public string? InitialDate => SelectedOrder?.DateTime.ToString("dd/MM/yy H:mm") ?? "Unknown";
        public string? PaymentDate => SelectedOrder?.PaymentDate?.ToString("dd/MM/yy H:mm") ?? "_ _";

        public string TypeTitle
        {
            get
            {
                return Current.FindResource("Order").ToString();
            }
        }
        public OrderDetailsViewModel(CustomersViewModel parent)
        {
            Parent = parent;
        }


        [RelayCommand]
        public async Task MarkOrderPaid()
        {
            //if (!IsOrderEligibleToPay)
            //    return;
            //Parent.IsBusy = true;
            //var result = await Parent.customersService.PAY_Order(OID);
            //if (result != null)
            //{
            //    Parent.SelectedOrder.IsPaid = true;
            //    OnPropertyChanged(nameof(PaymentDate));
            //    OnPropertyChanged(nameof(IsOrderEligibleToValidate));
            //    OnPropertyChanged(nameof(IsOrderEligibleToPay));
            //    OnPropertyChanged(nameof(IsOrderEligibleToRevoke));
            //    Parent.OnOrderModificationCommited();
            //}
            //Parent.IsBusy = false;
        }
        [RelayCommand]
        public async Task RevokeOrder()
        {
            //if (!IsOrderEligibleToRevoke)
            //    return;
            //Parent.IsBusy = true;
            //bool result = await Parent.customersService.REVOKE_Order(OID);
            //if (result)
            //{
            //    Parent.SelectedCustomer.Orders.Remove(SelectedOrder);
            //    OnPropertyChanged();
            //    Parent.OnOrderModificationCommited();
            //}
            //Parent.IsBusy = false;
        }
    }
}
