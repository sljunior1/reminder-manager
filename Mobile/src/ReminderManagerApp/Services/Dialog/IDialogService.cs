namespace ReminderManagerApp.Services
{
    public interface IDialogService
    {
        Task DisplayAlertAsync(string title, string message, string buttonlabel);
        Task<bool> DisplayAlertAsync(string title, string message, string acceptbutton, string cancelbutton);
    }
}
