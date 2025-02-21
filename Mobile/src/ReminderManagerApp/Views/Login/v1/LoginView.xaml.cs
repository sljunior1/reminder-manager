using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Login.v1;

namespace ReminderManagerApp.Views.Login.v1;

public partial class LoginView : ContentPageBase
{
    private bool _isPasswordVisible = false;
    private readonly LoginViewModel _viewModel;
    private readonly ISettingsService _settingsService;
    public LoginView(LoginViewModel viewModel, ISettingsService settingsService)
    {
        BindingContext = _viewModel = viewModel;
        _settingsService = settingsService;
        InitializeComponent();
    }
    private void imgVisiblePassword_Clicked(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        entryPassword.IsPassword = !_isPasswordVisible;
        imgVisiblePassword.Source = _isPasswordVisible ? (_settingsService.CurrentTheme.Equals(AppTheme.Light) ? "icon_eye_open_light.svg" : "icon_eye_open_dark.svg")
                                                       : (_settingsService.CurrentTheme.Equals(AppTheme.Light) ? "icon_eye_close_light.svg" : "icon_eye_close_dark.svg");
    }
}