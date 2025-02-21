namespace ReminderManagerApp.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
        public int ReminderId { get; set; }
    }
}
