using CommunityToolkit.Mvvm.Input;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using ReminderManagerApp.Dtos;
using ReminderManagerApp.Services;
using ReminderManagerApp.Validations;
using ReminderManagerApp.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.ViewModels.Login.v1
{
    public partial class LoginViewModel : ViewModelBase
    {
        #region Properties

        private string _userText;
        private string _passwordText;

        [CpfOrEmailValidation]
        public string UserText
        {
            get { return _userText; }
            set { SetProperty(ref _userText, value); }
        }
        [Required(ErrorMessage = "Senha deve ser preenchida.")]
        public string PasswordText
        {
            get { return _passwordText; }
            set { SetProperty(ref _passwordText, value); }
        }
        #endregion

        private readonly IServiceProvider _serviceProvider;
        private readonly IUserService _userService;
        private readonly ISettingsService _settingsService;
        public LoginViewModel(INavigationService navigationService,
                              IServiceProvider serviceProvider,
                              IUserService userService,
                              ISettingsService settingsService) : base(navigationService)
        {
            _serviceProvider = serviceProvider;
            _userService = userService;
            _settingsService = settingsService;

            _userText = string.Empty;
            _passwordText = string.Empty;
        }
        public override async Task InitializeAsync()
        {
            await IsBusyFor(async () =>
            {
                if (!string.IsNullOrEmpty(_settingsService.AuthAccessToken))
                    await LoginWithBiometricAsync();
            });
        }
        [RelayCommand]
        public async Task ForgotPasswordAsync()
        {
            App.ScreenForgotPassword(_serviceProvider);
        }
        [RelayCommand]
        public async Task LoginAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    var validationContext = new ValidationContext(this);
                    var validationResult = new List<ValidationResult>();

                    if (!Validator.TryValidateObject(this, validationContext, validationResult, true))
                    {
                        var errorMessage = validationResult.FirstOrDefault()?.ErrorMessage;

                        App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, errorMessage, "ok");
                    }
                    else
                    {
                        var dto = new LoginUserDto()
                        {
                            UserName = UserText,
                            Password = PasswordText,
                        };

                        var response = await _userService.ExecuteLoginAsync(dto);
                        if (response != null && response.Success)
                        {
                            _settingsService.AuthAccessToken = response.Data.AccessToken;
                            _settingsService.IdUser = response.Data.UserId;

                            App.ScreenMain();
                        }
                        else
                        {
                            App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, response.ErrorMessage, "ok");
                        }
                    }
                }
                catch (Exception)
                {
                    App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
                }
            });
        }
        private async Task LoginWithBiometricAsync()
        {
            if (await CrossFingerprint.Current.IsAvailableAsync())
            {
                if (!string.IsNullOrEmpty(_settingsService.AuthAccessToken))
                {
                    var request = new AuthenticationRequestConfiguration("Autenticação Biométrica", "Autentique-se para continuar");
                    var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                    if (result.Authenticated)
                        App.ScreenMain();
                    else
                        return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
