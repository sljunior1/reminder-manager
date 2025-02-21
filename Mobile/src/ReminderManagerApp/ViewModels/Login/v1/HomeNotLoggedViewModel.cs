using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;

namespace ReminderManagerApp.ViewModels.Login.v1
{
    public partial class HomeNotLoggedViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        public HomeNotLoggedViewModel(INavigationService navigationService,
                                      IServiceProvider serviceProvider) : base(navigationService)
        {
            _serviceProvider = serviceProvider;
        }
        [RelayCommand]
        public async Task LoginAsync()
        {
            App.ScreenLogin(_serviceProvider);
        }
        [RelayCommand]
        public async Task AboutAsync()
        {
            App.ScreenAbout(_serviceProvider);
        }
        [RelayCommand]
        public async Task NotRegisteredAsync()
        {
            App.ScreenNotRegistered(_serviceProvider);
        }

    }
}
