namespace ReminderManagerApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ReminderId { get; set; }
        public bool? IsNotified { get; set; }
        public DateTime? NotificationDate { get; set; }
    }
}
