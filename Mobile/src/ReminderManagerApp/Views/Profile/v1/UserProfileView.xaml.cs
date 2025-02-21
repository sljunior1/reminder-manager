using ReminderManagerApp.ViewModels.Profile.v1;

namespace ReminderManagerApp.Views.Profile.v1;

public partial class UserProfileView : ContentPageBase
{
    private readonly UserProfileViewModel _viewModel;
    public UserProfileView(UserProfileViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var theme = sender as Switch;
        _viewModel.ChangeThemeApp(theme.IsToggled);
    }
}