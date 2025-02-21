using System.Text.RegularExpressions;

namespace ReminderManagerApp.Helpers
{
    public class PhoneNumberHelper
    {
        public static readonly Regex PhoneRegex = new Regex(@"^\(\d{2}\)\d{5}-\d{4}$");
        public static bool IsPhoneNumber(string input)
        {
            return PhoneRegex.IsMatch(input);
        }
        public static string FormatPhoneNumber(string input)
        {
            if (input != null)
            {
                var text = Regex.Replace(input, @"[^\d]", "");
                text = text.PadRight(11);

                if (text.Length > 11)
                {
                    text = text.Remove(11);
                }

                if (text.Length > 0)
                    text = "(" + text;

                return text.Insert(3, ")").Insert(9, "-");
            }

            return string.Empty;
        }
    }
}
