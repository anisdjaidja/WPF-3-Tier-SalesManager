﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using WPF_N_Tier_Test.Service;
using WPF_N_Tier_Test.View.Sales;
using WPF_N_Tier_Test.ViewModel.Navigation;
using WPF_N_Tier_Test.ViewModel.Sales;
using WPF_N_Tier_Test_Data_Access.DataAccess;


namespace WPF_N_Tier_Test.ViewModel.App
{
    public partial class AppWindowViewModel: BaseModel
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

        public SalesDesignTimeContextFactory factory { get; }

        #endregion

        #endregion
        public AppWindowViewModel(SalesDesignTimeContextFactory contextFactory)
        {
            this.factory = contextFactory;
            ResolveDependencies();
            
        }
        public async void Reload()
        {
            ResolveDependencies(true);
        }
        async void ResolveDependencies(bool connectionOnly = false)
        {
            await ResolveDatabaseConnection();
            if (connectionOnly)
                return;
            ResolveServices();
            ResolveViewModels();
            ResolveWorkspaces();
        }
        async Task ResolveDatabaseConnection()
        {
            LoadingOverlayVM = new LoadingOverlayVeiwModel();
            OnPropertyChanged(nameof(LoadingOverlayVM));
            //DBclient = await LoadingOverlayVM.ConnectToDB();
        }
        void ResolveServices()
        {
            //var app = Application.Current as WPF_N_Tier_Test.App;
            clientsService = new(factory);
            stockService = new(factory);
            //supplyService = new(DBclient);
            //memberService = new(DBclient);
            //subscriptionPlansService = new(DBclient!.GetDatabase(app._AppConfig.DbName));
        }
        void ResolveViewModels()
        {
            salesViewModel = new(stockService, clientsService);
            NavigationSideBarVM = new ();
            NavigationSideBarVM.PageChanged += (e) => Navigate(e);
            SearchBarVM = new(clientsService, this);
        }
        void ResolveWorkspaces()
        {
            WorkSpaces = new()
            {
                new SalesView(salesViewModel),
            };
            Navigate(0);
        }
        public void Navigate(int workspaceIDX)
        {
            CurrentWorkspace = WorkSpaces?[workspaceIDX];
        }

        internal void CallBackDropDown()
        {
            SearchDropdownInvoked.Invoke();
        }
        public void GotoPatient(int id)
        {
            Navigate(1);
            salesViewModel.CustomersVM.GotoPatient(id);
            
        }
    }
}
