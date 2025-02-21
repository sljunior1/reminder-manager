namespace ReminderManagerApp.Services
{
    public interface ILocalNotificationService
    {
        Task ScheduleNotification(string title, string description, DateTime notifyTime);
    }
}
