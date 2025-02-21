namespace ReminderManagerApp.Services
{
    public class LocalNotificationService(INotificationManagerService notificationManagerService) : ILocalNotificationService
    {
        private readonly INotificationManagerService _notificationManagerService = notificationManagerService;
        public async Task ScheduleNotification(string title, string description, DateTime notifyTime)
        {
            _notificationManagerService.SendNotification(title, description, notifyTime);
        }
    }
}

