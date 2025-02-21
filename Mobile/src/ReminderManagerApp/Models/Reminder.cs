using ReminderManagerApp.Dtos;

namespace ReminderManagerApp.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateFormat
        {
            get
            {
                return DueDate.ToString("dd/MM/yyyy HH:mm");
            }
        }
        public DateTime CreationDate { get; set; }
        public AttachReminder Attach { get; set; }
        public UserDto User { get; set; }
    }
}
