namespace ReminderManagerApp
{
    public class GlobalSettings
    {
        private const string IP = "192.168.0.10";
        private const string Porta = "7130";
        public const string DefaultEndpoint = $"https://{IP}:{Porta}/api/";
        public string ExecuteAddUserEndpoint { get; set; }
        public string GetAllReminderByUserEndpoint { get; set; }
        public string ExecuteLoginEndpoint { get; set; }
        public string CreateReminderEndpoint { get; set; }
        public string DeleteReminderEndpoint { get; set; }
        public string EditReminderEndpoint { get; set; }
        public string GetUserByIdEndpoint { get; set; }
        public string GetAllCalendarByUserIdEndpoint { get; set; }
        public string ForgotPasswordUserEndpoint { get; set; }
        public static GlobalSettings Instance { get; } = new GlobalSettings();
        public GlobalSettings()
        {
            ExecuteAddUserEndpoint = $"{DefaultEndpoint}users";
            GetUserByIdEndpoint = $"{DefaultEndpoint}users/search/";
            ExecuteLoginEndpoint = $"{DefaultEndpoint}users/login";
            GetAllReminderByUserEndpoint = $"{DefaultEndpoint}reminders/search/user/";
            CreateReminderEndpoint = $"{DefaultEndpoint}reminders";
            DeleteReminderEndpoint = $"{DefaultEndpoint}reminders/";
            EditReminderEndpoint = $"{DefaultEndpoint}reminders/";
            GetAllCalendarByUserIdEndpoint = $"{DefaultEndpoint}users/calendar/";
            ForgotPasswordUserEndpoint = $"{DefaultEndpoint}users/forgot-password";

        }
    }
}
