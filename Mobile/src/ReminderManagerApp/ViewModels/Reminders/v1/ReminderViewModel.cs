using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Models;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;
using ReminderManagerApp.Views.Reminders.v1;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ReminderManagerApp.ViewModels.Reminders.v1
{
    public partial class ReminderViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Properties

        private const int PageSize = 5;
        private int _currentPage = 0;
        private bool _canLoadMore = true;
        private bool _isLoading = false;
        private ObservableCollection<Reminder> _listReminder;

        public ObservableCollection<Reminder> ListReminder
        {
            get { return _listReminder; }
            set { SetProperty(ref _listReminder, value); }
        }
        public bool CanLoadMore
        {
            get { return _canLoadMore; }
            set { SetProperty(ref _canLoadMore, value); }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        #endregion

        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IReminderService _reminderService;
        private readonly ISettingsService _settingsService;
        public ReminderViewModel(INavigationService navigationService,
                                 IServiceProvider serviceProvider,
                                 IReminderService reminderService,
                                 ISettingsService settingsService) : base(navigationService)
        {
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            _reminderService = reminderService;
            _settingsService = settingsService;

            ListReminder = new ObservableCollection<Reminder>();
        }
        public override async Task InitializeAsync()
        {
            await LoadMoreReminder();
        }
        [RelayCommand]
        public async Task AddNewReminderAsync()
        {
            await _navigationService.NavigateToAsync(nameof(NewReminderView));
        }
        [RelayCommand]
        public async Task LoadMoreReminderAsync()
        {
            await LoadMoreReminder();
        }
        [RelayCommand]
        public async Task EditReminderAsync(Reminder reminder)
        {
            await IsBusyFor(async () =>
            {
                _parameters = new Dictionary<string, object>()
                {
                    {"edit",reminder }
                };

                await _navigationService.NavigateToAsync(nameof(EditReminderView), _parameters);
            });
        }
        private async Task LoadMoreReminder()
        {
            try
            {
                await IsBusyFor(async () =>
                {
                    IsLoading = true;
                    CanLoadMore = false;

                    var getReminders = await GetReminders(_currentPage, PageSize);

                    if (getReminders != null)
                    {
                        foreach (var item in getReminders)
                        {
                            ListReminder.Add(item);
                        }
                        _currentPage++;
                    }

                    IsLoading = false;
                    CanLoadMore = ListReminder.Count > 0;
                });
            }
            catch (Exception)
            {
                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
                IsLoading = false;
                CanLoadMore = false;
            }       
        }
        private async Task<List<Reminder>> GetReminders(int pageIndex, int pageSize)
        {
            var allReminders = await _reminderService.GetAllReminderByUserAsync(_settingsService.IdUser);

            if (!allReminders.Success || allReminders.Data.Count == 0)
                return null;

            allReminders.Data = allReminders.Data.OrderByDescending(s => s.CreationDate).ToList();

            return allReminders.Data.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}