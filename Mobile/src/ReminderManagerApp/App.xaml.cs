using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Login.v1;
using ReminderManagerApp.Views.Login.v1;

namespace ReminderManagerApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider, ISettingsService settingsService)
        {
            InitializeComponent();
            ConfigureTheme(settingsService);
            Initialize(serviceProvider, settingsService.CurrentTheme);
        }
        private void ConfigureTheme(ISettingsService settingsService)
        {
            var resource = settingsService.CurrentResource is null ?
                (ResourceDictionary)Application.Current.Resources["LightTheme"] : settingsService.CurrentResource;

            Application.Current.UserAppTheme = settingsService.CurrentTheme;
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
        private void Initialize(IServiceProvider serviceProvider, AppTheme currentTheme)
        {
            var mainPage = serviceProvider.GetRequiredService<HomeNotLoggedView>();
            var viewModel = serviceProvider.GetRequiredService<HomeNotLoggedViewModel>();

            mainPage.BindingContext = viewModel;
            MainPage = new NavigationPage(mainPage)
            {
                BarBackgroundColor = currentTheme.Equals(AppTheme.Light) ? Colors.White : Colors.Black,
            };
        }
        public static async void ScreenLogin(IServiceProvider serviceProvider)
        {
            var mainPage = serviceProvider.GetRequiredService<LoginView>();
            var viewModel = serviceProvider.GetRequiredService<LoginViewModel>();

            await Current.MainPage.Navigation.PushAsync(new NavigationPage(mainPage)
            {
                BarBackgroundColor = Colors.DodgerBlue,
                BarTextColor = Colors.White
            });
        }
        public static async void ScreenAbout(IServiceProvider serviceProvider)
        {
            var mainPage = serviceProvider.GetRequiredService<AboutView>();
            var viewModel = serviceProvider.GetRequiredService<AboutViewModel>();

            await Current.MainPage.Navigation.PushAsync(new NavigationPage(mainPage)
            {
                BarBackgroundColor = Colors.DodgerBlue,
                BarTextColor = Colors.White,
            });
        }
        public static async void ScreenNotRegistered(IServiceProvider serviceProvider)
        {
            var mainPage = serviceProvider.GetRequiredService<NotRegisteredView>();
            var viewModel = serviceProvider.GetRequiredService<NotRegisteredViewModel>();

            await Current.MainPage.Navigation.PushAsync(new NavigationPage(mainPage)
            {
                BarBackgroundColor = Colors.DodgerBlue,
                BarTextColor = Colors.White
            });
        }
        public static async void ScreenForgotPassword(IServiceProvider serviceProvider)
        {
            var mainPage = serviceProvider.GetRequiredService<ForgotPasswordView>();
            var viewModel = serviceProvider.GetRequiredService<ForgotPasswordViewModel>();

            await Current.MainPage.Navigation.PushAsync(new NavigationPage(mainPage)
            {
                BarBackgroundColor = Colors.DodgerBlue,
                BarTextColor = Colors.White
            });
        }
        public static void ScreenMain()
        {
            Current.MainPage = new AppShell();
        }
        public static void ApplyTheme(AppTheme selectTheme, ResourceDictionary currentResource)
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(currentResource);
            Application.Current.UserAppTheme = selectTheme;
        }
    }
}
