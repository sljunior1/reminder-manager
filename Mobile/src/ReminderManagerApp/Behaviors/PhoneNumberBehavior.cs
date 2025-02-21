using System.Text.RegularExpressions;

namespace ReminderManagerApp.Behaviors
{
    public class PhoneNumberBehavior : Behavior<Entry>
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
            var text = entry.Text;

            text = Regex.Replace(text, @"[^\d]", "");

            if (text.Length > 2)
                text = text.Insert(2, ")");
            if (text.Length > 8)
                text = text.Insert(8, "-");

            if (text.Length > 0)
                text = "(" + text;

            entry.Text = text;
            entry.CursorPosition = entry.Text.Length;
        }
    }
}
