using ReminderManagerApp.ViewModels.Reminders.v1;

namespace ReminderManagerApp.Views.Reminders.v1;

public partial class CalendarView : ContentPageBase
{
    private readonly CalendarViewModel _viewModel;
    public CalendarView(CalendarViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
        calendar.Loaded += Calendar_Loaded;
    }

    private async void Calendar_Loaded(object? sender, EventArgs e)
    {
        calendar.Events.Clear();
        calendar.Events = await _viewModel.LoadEvents(); 
    }
}