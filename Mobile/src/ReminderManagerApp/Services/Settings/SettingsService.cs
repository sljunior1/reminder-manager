using Newtonsoft.Json;

namespace ReminderManagerApp.Services
{
    public class SettingsService : ISettingsService
    {
        private const string AccessToken = "access_token";
        private const string SessionIdUser = "id_user";
        private const string Theme = "theme";
        private const string Resource = "resource";
        private readonly string AccessTokenDefault = string.Empty;
        private readonly int IdUserDefault =0;
        private readonly AppTheme ThemeDefault = AppTheme.Light;
        private readonly string ResourceDefault = string.Empty;
        public string AuthAccessToken
        {
            get => Preferences.Get(AccessToken,AccessTokenDefault);
            set => Preferences.Set(AccessToken, value);
        }
        public int IdUser
        {
            get => Preferences.Get(SessionIdUser, IdUserDefault);
            set => Preferences.Set(SessionIdUser, value);
        }
        public AppTheme CurrentTheme
        {
            get => (AppTheme)Preferences.Get(Theme, (int)ThemeDefault);
            set => Preferences.Set(Theme, (int)value);
        }
        public ResourceDictionary CurrentResource
        {
            get
            {
                var json = Preferences.Get(Resource, ResourceDefault);
                return string.IsNullOrEmpty(json) ? null :
                    JsonConvert.DeserializeObject<ResourceDictionary>(json);
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                Preferences.Set(Resource, json);
            }
        }
    }
}
