using ReminderManagerApp.Views.Profile.v1;
using ReminderManagerApp.Views.Reminders.v1;

namespace ReminderManagerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            InitializeRoutes();
        }
        private void InitializeRoutes()
        {
            Routing.RegisterRoute(nameof(ReminderView), typeof(ReminderView));
            Routing.RegisterRoute(nameof(NewReminderView), typeof(NewReminderView));
            Routing.RegisterRoute(nameof(EditReminderView), typeof(EditReminderView));
            Routing.RegisterRoute(nameof(CalendarView), typeof(CalendarView));
            Routing.RegisterRoute(nameof(UserProfileView), typeof(UserProfileView));
        }
    }
}
