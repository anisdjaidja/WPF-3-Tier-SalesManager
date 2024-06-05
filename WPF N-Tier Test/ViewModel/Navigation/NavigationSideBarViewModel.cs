using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Service;

namespace WPF_N_Tier_Test.ViewModel.Navigation
{
    public partial class NavigationSideBarViewModel: BaseModel
    {
        #region
        [ObservableProperty]
        public int suppliersWithDebts = 0;
        [ObservableProperty]
        public double goodsInDebts = 0.0;
        #endregion
        public event Action<int> PageChanged;
        public NavigationSideBarViewModel()
        {
        }
        #region        
        [ObservableProperty]
        public int selectedPageIndex = 0;
        [ObservableProperty]
        public int highlightedPageIndex = 0;
        #endregion
        [RelayCommand]
        public void NavigateToPage(object index)
        {
            PageChanged?.Invoke(int.Parse(index.ToString()));
        }

    }
}
