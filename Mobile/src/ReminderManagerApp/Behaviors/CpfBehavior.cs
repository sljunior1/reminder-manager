using ReminderManagerApp.Helpers;

namespace ReminderManagerApp.Behaviors
{
    public class CpfBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            if (IsNumeric(entry.Text))
            {
                entry.Text = CpfHelper.FormatCPF(entry.Text);
                entry.CursorPosition = entry.Text.Length;
            }
        }
        private bool IsNumeric(string text)
        {
            text = text.Replace(".", "").Replace("-", "");
            return long.TryParse(text, out _);
        }
    }
}
