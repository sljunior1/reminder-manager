using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Dtos;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.ViewModels.Login.v1
{
    public partial class ForgotPasswordViewModel : ViewModelBase
    {
        #region Properties

        private string? _newPasswordText;
        private string? _newPasswordConfirmText;
        private string? _emailText;

        [Required(ErrorMessage = SystemMessage.FIELD_NEW_PASSWORD_REQUIRED)]
        public string? NewPasswordText
        {
            get { return _newPasswordText; }
            set { SetProperty(ref _newPasswordText, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_NEW_PASSWORD_CONFIRM_REQUIRED)]
        public string? NewPasswordConfirmText
        {
            get { return _newPasswordConfirmText; }
            set { SetProperty(ref _newPasswordConfirmText, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_EMAIL_REQUIRED)]
        public string? EmailText
        {
            get { return _emailText; }
            set { SetProperty(ref _emailText, value); }
        }
        #endregion

        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        public ForgotPasswordViewModel(INavigationService navigationService,
                                       IUserService userService,
                                       IServiceProvider serviceProvider) : base(navigationService)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;
        }
        [RelayCommand]
        public async Task ChangeNewPasswordAsync()
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
                    if (NewPasswordText.Equals(NewPasswordConfirmText))
                    {
                        await IsBusyFor(async () =>
                        {
                            ForgotPasswordUserDto dto = new ForgotPasswordUserDto { Password = NewPasswordText,Email = EmailText };

                            var response = await _userService.ForgotPasswordUserAsync(dto);

                            if (response == null || !response.Success)
                            {
                                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.CHANGE_PASSWORD_REMINDER_ERROR, "ok");
                            }
                            else
                            {
                                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.CHANGE_PASSWORD_REMINDER_SUCCESS, "ok");
                                App.ScreenLogin(_serviceProvider);
                            }
                        });
                    }
                    else
                    {
                        App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FIELD_PASSWORD_CONFIRM_VALID, "ok");
                    }
                }
            }
            catch (Exception)
            {
                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
            }
        }
        [RelayCommand]
        public async Task CleanAsync()
        {
            NewPasswordText = string.Empty;
            NewPasswordConfirmText = string.Empty;
        }
    }
}
