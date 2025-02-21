namespace ReminderManagerApi.Models
{
    public class ReminderModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public AttachReminderModel? Attach { get; set; }
        public UserModel User { get; set; }
    }
}
