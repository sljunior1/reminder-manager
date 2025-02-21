using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Dtos;
using ReminderManagerApp.Services;
using ReminderManagerApp.Validations;
using ReminderManagerApp.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.ViewModels.Login.v1
{
    public partial class NotRegisteredViewModel : ViewModelBase
    {
        #region Properties

        private string _nameText;
        private string _cpfText;
        private string _emailText;
        private string _phoneText;
        private string _passwordText;
        private string _passwordConfirmText;

        [Required(ErrorMessage = SystemMessage.FIELD_NAME_REQUIRED)]
        public string NameText
        {
            get { return _nameText; }
            set { SetProperty(ref _nameText, value); }
        }
        [CpfValidation]
        public string CPFText
        {
            get { return _cpfText; }
            set { SetProperty(ref _cpfText, value); }
        }
        [EmailValidation]
        public string EmailText
        {
            get { return _emailText; }
            set { SetProperty(ref _emailText, value); }
        }
        [PhoneNumberValidation]
        public string PhoneText
        {
            get { return _phoneText; }
            set { SetProperty(ref _phoneText, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_PASSWORD_REQUIRED)]
        public string PasswordText
        {
            get { return _passwordText; }
            set { SetProperty(ref _passwordText, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_PASSWORD_CONFIRM_REQUIRED)]
        public string PasswordConfirmText
        {
            get { return _passwordConfirmText; }
            set { SetProperty(ref _passwordConfirmText, value); }
        }
        #endregion

        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        public NotRegisteredViewModel(INavigationService navigationService,
                                      IUserService userService,
                                      IServiceProvider serviceProvider) : base(navigationService)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;

            _nameText = string.Empty;
            _cpfText = string.Empty;
            _emailText = string.Empty;
            _phoneText = string.Empty;
            _passwordText = string.Empty;
            _passwordConfirmText = string.Empty;
        }
        [RelayCommand]
        public async Task RegisterUserAsync()
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
                    if (PasswordText.Equals(PasswordConfirmText))
                    {
                        await IsBusyFor(async () =>
                        {
                            CreateUserDto dto = new CreateUserDto(NameText, CPFText, EmailText, PhoneText, PasswordText);

                            var response = await _userService.ExecuteAddUserAsync(dto);

                            if (response == null || !response.Success)
                            {
                                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.EXECUTE_REGISTER_INVALID, "ok");
                            }
                            else
                            {
                                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.CREATE_USER_SUCCESS, "ok");
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
            NameText = string.Empty;
            CPFText = string.Empty;
            EmailText = string.Empty;
            PhoneText = string.Empty;
            PasswordText = string.Empty;
            PasswordConfirmText = string.Empty;
        }
    }
}
