using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.ViewModel.App
{
    public partial class SearchDropDownViewModel: BaseModel
    {
        private SearchBarViewModel searchBarViewModel;
        [ObservableProperty]
        private Customer? selectedPatient;
        public ObservableCollection<Customer> FoundPatients => searchBarViewModel.FoundPatients;
        public SearchDropDownViewModel(SearchBarViewModel searchBarViewModel)
        {
            this.searchBarViewModel = searchBarViewModel;
            OnPropertyChanged(nameof(FoundPatients));
        }
        partial void OnSelectedPatientChanged(Customer? value)
        {
            if (value == null) { return; }
            ReportSuccess($"SelectedPatient {value.Name}");
            searchBarViewModel.OnPatientSelected(value.Id);
        }
    }
}