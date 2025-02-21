using System.Text.Json.Serialization;

namespace ReminderManagerApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public ICollection<ReminderModel> Reminders { get; set; }
    }
}
