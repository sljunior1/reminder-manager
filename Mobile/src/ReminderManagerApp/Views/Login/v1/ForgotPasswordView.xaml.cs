using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Login.v1;

namespace ReminderManagerApp.Views.Login.v1;

public partial class ForgotPasswordView : ContentPageBase
{
	private readonly ForgotPasswordViewModel _viewModel;
    private readonly ISettingsService _settingsService;
    private bool _isPasswordVisible = false;
    private bool _isPasswordConfirmVisible = false;
    public ForgotPasswordView(ForgotPasswordViewModel viewModel, ISettingsService settingsService)
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
    private void imgVisibleConfirmPassword_Clicked(object sender, EventArgs e)
    {
        _isPasswordConfirmVisible = !_isPasswordConfirmVisible;
        entryConfirmPassword.IsPassword = !_isPasswordConfirmVisible;
        imgVisibleConfirmPassword.Source = _isPasswordConfirmVisible ? (_settingsService.CurrentTheme.Equals(AppTheme.Light) ? "icon_eye_open_light.svg" : "icon_eye_open_dark.svg")
                                                                     : (_settingsService.CurrentTheme.Equals(AppTheme.Light) ? "icon_eye_close_light.svg" : "icon_eye_close_dark.svg");
    }
}