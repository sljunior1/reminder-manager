namespace ReminderManagerApp.Services
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        int IdUser { get; set; }
        AppTheme CurrentTheme { get; set; }
        ResourceDictionary CurrentResource { get; set; }
    }
}
