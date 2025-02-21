using ReminderManagerApp.ViewModels.Base;

namespace ReminderManagerApp.Views
{
    public class ContentPageBase : ContentPage
    {
        public ContentPageBase()
        {
            NavigationPage.SetBackButtonTitle(this,string.Empty);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is not IViewModelBase ivmb)
                return;

            await ivmb.InitializeAsyncCommand.ExecuteAsync(null);
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is not IViewModelBase ivmb)
                return;

            await ivmb.FinishAsyncCommand.ExecuteAsync(null);
        }
    }
}
