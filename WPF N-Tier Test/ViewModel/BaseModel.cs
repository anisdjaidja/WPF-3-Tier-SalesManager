using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF_N_Tier_Test.ViewModel
{
    public partial class BaseModel: ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title = "Untitled ViewModel";

        public bool IsNotBusy => !IsBusy;
    }
}
