using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Data;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.View.Sales;
using WPF_N_Tier_Test.ViewModel.Navigation;
using WPF_N_Tier_Test.ViewModel.Sales;
using WPF_N_Tier_Test.ViewModel.Sales.Customers;
using WPF_N_Tier_Test_Data_Access.DataAccess;


namespace WPF_N_Tier_Test.ViewModel.App
{
    public partial class AppWindowViewModel: BaseModel, INavigationViewModel
    {
        #region Fields
        public List<UIElement>? WorkSpaces;
        [ObservableProperty] public UIElement? currentWorkspace;
        public event Action SearchDropdownInvoked;
        #region ViewModels
        public SalesViewModel salesViewModel;
        [ObservableProperty] public LoadingOverlayVeiwModel loadingOverlayVM;
        [ObservableProperty] public SearchBarViewModel searchBarVM;
        [ObservableProperty] public NavigationSideBarViewModel navigationSideBarVM;
        #endregion

        #region Services
        CustomerService clientsService;
        StockService stockService;
        SalesService salesService;
        UserService userService;
        public SalesDesignTimeContextFactory factory { get; }

        #endregion
        [ObservableProperty]
        public ObservableCollection<FavShortcut> favorites;
        public FavShortcut? SelectedShortcut
        {
            get
            {
                return null;
            }
            set
            {
                if(value != null) {
                    ReportSuccess("Going to favorite page shortcut");
                    NavigateToTab(value.PageIndex, value.TabIndex);
                }
                CollectionViewSource.GetDefaultView(Favorites).Refresh();
                OnPropertyChanged(nameof(Favorites));
            }
        }
        #endregion
        public AppWindowViewModel(SalesDesignTimeContextFactory contextFactory)
        {
            this.factory = contextFactory;
            ResolveServices();
            LoadingOverlayVM = new LoadingOverlayVeiwModel(userService);
            OnPropertyChanged(nameof(LoadingOverlayVM));
            LoadingOverlayVM.LogedIn += LoadingOverlayVM_LogedIn;

        }
        private void LoadingOverlayVM_LogedIn()
        {
            LoadingOverlayVM = null;
            ResolveViewModels();
            ResolveWorkspaces();
        }
        void ResolveServices()
        {
            clientsService = new(factory);
            stockService = new(factory);
            salesService = new(factory);
            userService = new(factory);
        }
        void ResolveViewModels()
        {
            salesViewModel = new(stockService, clientsService, salesService);
            NavigationSideBarVM = new ();
            NavigationSideBarVM.PageChanged += (e) => NavigateTo(e);
            SearchBarVM = new(clientsService, this);
        }
        void ResolveWorkspaces()
        {
            WorkSpaces = new()
            {
                new SalesView(salesViewModel),
            };

            NavigateTo(0);
        }
        public void NavigateTo(int pageIndex)
        {
            CurrentWorkspace = WorkSpaces?[pageIndex];
        }
        public void NavigateToTab(int idx, int tabIdx)
        {
            NavigateTo(idx);
            OnPropertyChanged(nameof(CurrentWorkspace));
            (salesViewModel).NavigateTo(tabIdx);
        }
        internal void CallBackDropDown()
        {
            SearchDropdownInvoked.Invoke();
        }

        
    }
}
