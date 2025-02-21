namespace ReminderManagerApp.Dtos
{
    public class EditReminderDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public AttachReminderDto? Attach { get; set; }
    }
}
