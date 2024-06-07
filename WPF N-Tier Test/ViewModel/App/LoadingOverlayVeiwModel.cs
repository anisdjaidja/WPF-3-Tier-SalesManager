using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows;
using WPF_N_Tier_Test.Service;

namespace WPF_N_Tier_Test.ViewModel.App
{
    public partial class LoadingOverlayVeiwModel : BaseModel
    {
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string password;
        public event Action LogedIn;
        public LoadingOverlayVeiwModel(UserService userService)
        {
            this.userService = userService;
        }
        #region Props

        [ObservableProperty] public string? connectionFeedback;
        [ObservableProperty]
        public Visibility overlayingApp = Visibility.Visible;
        private readonly UserService userService;
        #endregion
        [RelayCommand]
        public async Task Login()
        {
            IsBusy = true;
            await Task.Delay(1000);
            var result = await userService.Login(UserName, Password);
            IsBusy = false;
            if (!result)
            {
                OverlayingApp = Visibility.Hidden;
                OnPropertyChanged(nameof(OverlayingApp));
                LogedIn?.Invoke();
                ReportSuccess("Loged In sucessfuly");
            }
            else
            {
                ConnectionFeedback = "Wrong user info";
            }
        } 
    }
}
