namespace ReminderManagerApp.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);
        Task PopAsync();
        Task PushModalAsync(Page page);
        Task PopModalAsync();
    }
}
