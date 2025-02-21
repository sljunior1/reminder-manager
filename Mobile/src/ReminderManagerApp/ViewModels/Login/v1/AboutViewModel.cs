using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;

namespace ReminderManagerApp.ViewModels.Login.v1
{
    public partial class AboutViewModel : ViewModelBase
    {
        #region Properties

        private string _version;
        private string _versionPlatformAndroid;
        private string _description;
        public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }
        public string VersionPlatformAndroid
        {
            get { return _versionPlatformAndroid; }
            set { SetProperty(ref _versionPlatformAndroid, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        #endregion

        public AboutViewModel(INavigationService navigationService) : base(navigationService)
        {
            _version = $"Versão do aplicativo: {VersionTracking.CurrentVersion}";
            _versionPlatformAndroid = $"Versão do Android: {DeviceInfo.VersionString}";
            _description = "Este aplicativo foi desenvolvido para atender ao desafio técnico proposto pela Movisis, como parte do processo seletivo para a posição de desenvolvedor .NET MAUI.";
        }
    }
}
