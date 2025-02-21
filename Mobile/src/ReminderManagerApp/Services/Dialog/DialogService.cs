namespace ReminderManagerApp.Services
{
    public class DialogService : IDialogService
    {
        public Task DisplayAlertAsync(string title, string message, string buttonlabel)
        {
            return Shell.Current.DisplayAlert(title, message, buttonlabel);
        }
        public Task<bool> DisplayAlertAsync(string title, string message, string acceptbutton, string cancelbutton)
        {
            return Shell.Current.DisplayAlert(title, message, acceptbutton, cancelbutton);
        }
    }
}
