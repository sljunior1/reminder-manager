using System.Text.RegularExpressions;

namespace ReminderManagerApp.Helpers
{
    public class EmailHelper
    {
        public static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        public static bool IsEmail(string input)
        {
            return EmailRegex.IsMatch(input);
        }
    }
}
