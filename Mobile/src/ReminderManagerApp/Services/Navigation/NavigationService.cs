namespace ReminderManagerApp.Services
{
    public class NavigationService(ISettingsService settingsService,
                                   IDialogService dialogService,
                                   IServiceProvider serviceProvider) : INavigationService
    {
        private readonly ISettingsService _settingsService = settingsService;
        private readonly IDialogService _dialogService = dialogService;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
        {
            if (!string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                if (!Connectivity.Current.NetworkAccess.Equals(NetworkAccess.Internet))
                    _dialogService.DisplayAlertAsync(SystemMessage.FAIL_CONECTION, "Não foi possível se conectar ao servidor", "ok");

                var shellNavigation = new ShellNavigationState(route);

                return routeParameters != null
                    ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
                    : Shell.Current.GoToAsync(shellNavigation);
            }
            else
            {
                _dialogService.DisplayAlertAsync(SystemMessage.REMINDER_MANAGER, "Sua sessão expirou. Efetue novamente o login", "ok");
                App.ScreenLogin(_serviceProvider);

                return null;
            }
        }
        public Task PopAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
        public Task PushModalAsync(Page page)
        {
            return Shell.Current.Navigation.PushModalAsync(page);
        }
        public Task PopModalAsync()
        {
            return Shell.Current.Navigation.PopModalAsync(true);
        }
    }
}