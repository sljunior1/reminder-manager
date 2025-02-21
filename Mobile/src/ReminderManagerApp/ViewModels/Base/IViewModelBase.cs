using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Services;

namespace ReminderManagerApp.ViewModels.Base
{
    public interface IViewModelBase : IQueryAttributable
    {
        public INavigationService NavigationService { get; }
        public IAsyncRelayCommand InitializeAsyncCommand { get; }
        public IAsyncRelayCommand FinishAsyncCommand { get; }
        public bool IsBusy { get; }
        public bool IsInitialized { get; }
        Task InitializeAsync();
        Task FinishAsync();
    }
}
