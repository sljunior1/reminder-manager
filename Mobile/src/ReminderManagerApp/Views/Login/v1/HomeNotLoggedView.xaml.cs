using ReminderManagerApp.ViewModels.Login.v1;

namespace ReminderManagerApp.Views.Login.v1;

public partial class HomeNotLoggedView : ContentPageBase
{
    private readonly HomeNotLoggedViewModel _viewModel;
    public HomeNotLoggedView(HomeNotLoggedViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
    }
}