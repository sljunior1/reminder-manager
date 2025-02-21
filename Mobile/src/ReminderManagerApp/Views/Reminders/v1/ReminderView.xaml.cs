using ReminderManagerApp.Models;
using ReminderManagerApp.ViewModels.Reminders.v1;

namespace ReminderManagerApp.Views.Reminders.v1;

public partial class ReminderView : ContentPageBase
{
    private readonly ReminderViewModel _viewModel;
    public ReminderView(ReminderViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        searchBar.Text = string.Empty;
        base.OnAppearing();
    }
    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
            ReminderCollectionView.ItemsSource = _viewModel.ListReminder;
        else
            ReminderCollectionView.ItemsSource = _viewModel.ListReminder.Where(s => s.Title.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                                                                    s.Description.ToLower().Contains(e.NewTextValue.ToLower()));
    }
    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var collection = e.CurrentSelection.FirstOrDefault() as Reminder;

            if (collection == null)
            {
                return;
            }
            _viewModel.EditReminderAsync(collection);
        }
        catch (Exception)
        {
            return;
        }
    }
}