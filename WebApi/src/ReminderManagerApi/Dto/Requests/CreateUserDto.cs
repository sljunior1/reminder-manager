namespace ReminderManagerApi.Dto.Requests
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
