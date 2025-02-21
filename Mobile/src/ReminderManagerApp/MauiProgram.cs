using Android.Widget;
using CommunityToolkit.Maui;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Login.v1;
using ReminderManagerApp.ViewModels.Profile.v1;
using ReminderManagerApp.ViewModels.Reminders.v1;
using ReminderManagerApp.Views.Login.v1;
using ReminderManagerApp.Views.Profile.v1;
using ReminderManagerApp.Views.Reminders.v1;

namespace ReminderManagerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("LexendDeca-SemiBold.ttf", "AppLogoFont");
                });

            return builder.Build();
        }
        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
            mauiAppBuilder.Services.AddSingleton<IDialogService, DialogService>();
            mauiAppBuilder.Services.AddSingleton<IRequestProviderService, RequestProviderService>();
            mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
            mauiAppBuilder.Services.AddSingleton<ILocalNotificationService, LocalNotificationService>();

            mauiAppBuilder.Services.AddTransient<IUserService, UserService>();
            mauiAppBuilder.Services.AddTransient<IReminderService, ReminderService>();

#if ANDROID
            mauiAppBuilder.Services.AddTransient<INotificationManagerService, Platforms.Android.NotificationManagerService>();
#endif

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            #region Login

            mauiAppBuilder.Services.AddTransient<ForgotPasswordViewModel>();
            mauiAppBuilder.Services.AddTransient<HomeNotLoggedViewModel>();
            mauiAppBuilder.Services.AddTransient<LoginViewModel>();
            mauiAppBuilder.Services.AddTransient<NotRegisteredViewModel>();
            mauiAppBuilder.Services.AddTransient<AboutViewModel>();

            #endregion

            #region Reminders

            mauiAppBuilder.Services.AddTransient<EditReminderViewModel>();
            mauiAppBuilder.Services.AddTransient<NewReminderViewModel>();
            mauiAppBuilder.Services.AddTransient<ReminderViewModel>();
            mauiAppBuilder.Services.AddTransient<CalendarViewModel>();

            #endregion

            #region Profile

            mauiAppBuilder.Services.AddTransient<UserProfileViewModel>();

            #endregion

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            #region Login

            mauiAppBuilder.Services.AddTransient<ForgotPasswordView>();
            mauiAppBuilder.Services.AddTransient<HomeNotLoggedView>();
            mauiAppBuilder.Services.AddTransient<LoginView>();
            mauiAppBuilder.Services.AddTransient<NotRegisteredView>();
            mauiAppBuilder.Services.AddTransient<AboutView>();

            #endregion

            #region Reminders

            mauiAppBuilder.Services.AddTransient<EditReminderView>();
            mauiAppBuilder.Services.AddTransient<NewReminderView>();
            mauiAppBuilder.Services.AddTransient<ReminderView>();
            mauiAppBuilder.Services.AddTransient<Views.Reminders.v1.CalendarView>();

            #endregion

            #region Profile

            mauiAppBuilder.Services.AddTransient<UserProfileView>();

            #endregion

            return mauiAppBuilder;
        }
    }
}
