using System.Text.RegularExpressions;

namespace ReminderManagerApp.Helpers
{
    public class CpfHelper
    {
        public static readonly Regex CpfRegex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        public static bool IsCpf(string input)
        {
            input = input.Replace(".", "").Replace("-", "");
            return (input.Length == 11 && long.TryParse(input, out _));
        }
        public static string FormatCPF(string input)
        {
            if (input != null)
            {
                var text = Regex.Replace(input, @"[^0-9*]", "");
                text = text.PadRight(11);

                if (text.Length > 11)
                {
                    text = text.Remove(11);
                }

                return text.Insert(3, ".").Insert(7, ".").Insert(11, "-").TrimEnd(new char[] { ' ', '.', '-' });
            }

            return string.Empty;
        }
    }
}
