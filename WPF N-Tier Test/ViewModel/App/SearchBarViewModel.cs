using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test.Modules.Helpers;
using WPF_N_Tier_Test.Service;

namespace WPF_N_Tier_Test.ViewModel.App
{
    public partial class SearchBarViewModel: BaseModel
    {
        AppWindowViewModel Parent;
        private CustomerService clientsService;

        [ObservableProperty]
        public string searchKey = "";
        [ObservableProperty]
        public ObservableCollection<Customer> foundPatients;
        public ObservableCollection<Article> foundMeds;
        SearchEngine searchEngine;

        [ObservableProperty]
        public SearchDropDownViewModel dropDownVM;
        public SearchBarViewModel(CustomerService clientsService, AppWindowViewModel parent )
        {
            this.clientsService = clientsService;
            this.Parent = parent;
        }


        partial void OnSearchKeyChanging(string value) => Query(value);
        [RelayCommand]
        public async Task Query(string value)
        {
            //DropDownVM = new(this);
            //Parent.CallBackDropDown();
            //searchEngine = new SearchEngine(value);
            //var found = await clientsService.SearchPatient(searchEngine.Subsets, 3);
            //FoundPatients = new ObservableCollection<Customer>(found);
            //string foundNames = "";
            //foreach (var patient in found) { foundNames += patient.Name; }
            //ReportSuccess($"Query invoked with Searchkey : {value} and found {foundNames}");
        }
       
        internal void OnPatientSelected(int id)
        {
            Parent.GotoPatient(id);
        }
    }
}
