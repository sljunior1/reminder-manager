using ReminderManagerApp.Helpers;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;

namespace ReminderManagerApp.ViewModels.Profile.v1
{
    public partial class UserProfileViewModel : ViewModelBase
    {
        #region Properties

        private string? _nameText;
        private string? _cpfText;
        private string? _emailText;
        private string? _phoneText;
        private Color _toggledColor;
        private Color _onColor;
        private bool _isToggled;

        public string NameText
        {
            get { return _nameText; }
            set { SetProperty(ref _nameText, value); }
        }
        public string CPFText
        {
            get { return _cpfText; }
            set { SetProperty(ref _cpfText, value); }
        }
        public string EmailText
        {
            get { return _emailText; }
            set { SetProperty(ref _emailText, value); }
        }
        public string PhoneText
        {
            get { return _phoneText; }
            set { SetProperty(ref _phoneText, value); }
        }
        public Color ToggledColor
        {
            get { return _toggledColor; }
            set { SetProperty(ref _toggledColor, value); }
        }
        public Color OnColor
        {
            get { return _onColor; }
            set { SetProperty(ref _onColor, value); }
        }
        public bool IsToggled
        {
            get { return _isToggled; }
            set { SetProperty(ref _isToggled, value); }
        }
        #endregion

        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;
        public UserProfileViewModel(INavigationService navigationService, ISettingsService settingsService, IUserService userService) : base(navigationService)
        {
            _settingsService = settingsService;
            _userService = userService;
            
            ToggledColor = _settingsService.CurrentTheme.Equals(AppTheme.Light) ? Colors.DodgerBlue : Colors.DarkBlue;
            OnColor = _settingsService.CurrentTheme.Equals(AppTheme.Light) ? Colors.Black : Colors.White;
            IsToggled = !_settingsService.CurrentTheme.Equals(AppTheme.Light);

        }
        public async override Task InitializeAsync()
        {
            try
            {
                await IsBusyFor(async () =>
                {
                    var response = await _userService.GetUserByIdAsync();

                    if (response != null && response.Success)
                    {
                        NameText = response.Data.Name;
                        CPFText = CpfHelper.FormatCPF(response.Data.Cpf);
                        EmailText = response.Data.Email;
                        PhoneText =PhoneNumberHelper.FormatPhoneNumber(response.Data.PhoneNumber);
                    }
                });
            }
            catch (Exception)
            {
                App.ScreenMain();
            }
        }
        public async void ChangeThemeApp(bool isToggled)
        {
            try
            {
                await IsBusyFor(async () =>
                {
                    if (isToggled)
                    {
                        _settingsService.CurrentTheme = AppTheme.Dark;
                        _settingsService.CurrentResource = (ResourceDictionary)Application.Current.Resources["DarkTheme"];
                        ToggledColor = Colors.DarkBlue;
                        OnColor = Colors.White;
                    }
                    else
                    {
                        _settingsService.CurrentTheme = AppTheme.Light;
                        _settingsService.CurrentResource = (ResourceDictionary)Application.Current.Resources["LightTheme"];
                        ToggledColor = Colors.DodgerBlue;
                        OnColor = Colors.Black;
                    }

                    App.ApplyTheme(_settingsService.CurrentTheme, _settingsService.CurrentResource);
                });
            }
            catch (Exception)
            {
                App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
            }
        }
    }
}
