using ReminderManagerApp.ViewModels.Reminders.v1;

namespace ReminderManagerApp.Views.Reminders.v1;

public partial class NewReminderView : ContentPageBase
{
    private readonly NewReminderViewModel _viewModel;
    public NewReminderView(NewReminderViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
    }
}