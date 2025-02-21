using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Services;

namespace ReminderManagerApp.ViewModels.Base
{
    public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        public INavigationService NavigationService { get; }
        public bool IsBusy => Interlocked.Read(ref _isBusy) > 0;
        public IAsyncRelayCommand InitializeAsyncCommand { get; }
        public IAsyncRelayCommand FinishAsyncCommand { get; }
        public IDictionary<string, object>? _parameters;
        private long _isBusy;
        [ObservableProperty]
        private bool _isInitialized;
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            InitializeAsyncCommand = new AsyncRelayCommand(
                async () =>
                {
                    await IsBusyFor(InitializeAsync);
                },
                AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);

            FinishAsyncCommand = new AsyncRelayCommand(
                async () =>
                {
                    await IsBusyFor(FinishAsync);
                },
                AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
        }
        public virtual void ApplyQueryAttributes(IDictionary<string,object> query)
        {
            _parameters = query;
        }
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
        public virtual Task FinishAsync()
        {
            return Task.CompletedTask;
        }
        protected async Task IsBusyFor(Func<Task> unitOfWork)
        {
            Interlocked.Increment(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));

            try
            {
                await unitOfWork();
            }
            finally
            {
                Interlocked.Decrement(ref _isBusy);
                OnPropertyChanged(nameof(IsBusy));
            }
        }
    }
}
