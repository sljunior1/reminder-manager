using System.Text.Json.Serialization;

namespace ReminderManagerApp.Dtos
{
    public class CreateUserDto
    {
        public CreateUserDto(string nameText, string cPFText, string emailText, string phoneText, string passwordText)
        {
            Name = nameText;
            Cpf = cPFText.Replace(".","").Replace("-","");
            Email = emailText;
            PhoneNumber = phoneText.Replace("(","").Replace(")","").Replace("-","");
            Password = passwordText;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
