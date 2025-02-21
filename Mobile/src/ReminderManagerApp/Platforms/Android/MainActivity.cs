using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ReminderManagerApp.Services;
using Plugin.Fingerprint;

namespace ReminderManagerApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreateNotificationFromIntent(Intent);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
        }
        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);
            CreateNotificationFromIntent(intent);
        }
        static void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(Platforms.Android.NotificationManagerService.TitleKey);
                string message = intent.GetStringExtra(Platforms.Android.NotificationManagerService.MessageKey);

                var service = IPlatformApplication.Current.Services.GetService<INotificationManagerService>();
                service.ReceiveNotification(title, message);
            }
        }
    }
}
