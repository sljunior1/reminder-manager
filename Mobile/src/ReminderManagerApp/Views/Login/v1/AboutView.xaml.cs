using ReminderManagerApp.ViewModels.Login.v1;

namespace ReminderManagerApp.Views.Login.v1;

public partial class AboutView : ContentPageBase
{
    private readonly AboutViewModel _viewModel;
    public AboutView(AboutViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
    }
}