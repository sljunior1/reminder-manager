using ReminderManagerApp.Helpers;

namespace ReminderManagerApp.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(string name, string cpf, string email, string phone, string password)
        {
            this.Name = name;
            this.Cpf = cpf;
            this.Email = email;
            this.PhoneNumber = phone;
            this.Password = password;
        }
        private string RemoveSpecialCharacters(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }
        internal void IdentifierType(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                this.Cpf = CpfHelper.IsCpf(input) ? RemoveSpecialCharacters(input) : string.Empty;
                this.Email = EmailHelper.IsEmail(input) ? input : string.Empty;
            }
        }
    }
}
