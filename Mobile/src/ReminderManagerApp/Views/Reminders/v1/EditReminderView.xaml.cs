using ReminderManagerApp.ViewModels.Reminders.v1;

namespace ReminderManagerApp.Views.Reminders.v1;

public partial class EditReminderView : ContentPageBase
{
    private readonly EditReminderViewModel _viewModel;
    public EditReminderView(EditReminderViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
    }
}