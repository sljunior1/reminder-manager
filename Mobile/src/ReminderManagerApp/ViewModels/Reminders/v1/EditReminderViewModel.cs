using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Dtos;
using ReminderManagerApp.Models;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.ViewModels.Reminders.v1
{
    public partial class EditReminderViewModel : ViewModelBase
    {
        #region Properties

        private string? _titleText;
        private string? _descriptionText;
        [ObservableProperty]
        private DateTime _dateDueText = DateTime.Now;
        [ObservableProperty]
        private TimeSpan _hourDueText;
        private string? _nameFile;

        [Required(ErrorMessage = SystemMessage.FIELD_TITLE_REQUIRED)]
        public string TitleText
        {
            get { return _titleText; }
            set { SetProperty(ref _titleText, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_DESCRIPTION_REQUIRED)]
        public string DescriptionText
        {
            get { return _descriptionText; }
            set { SetProperty(ref _descriptionText, value); }
        }
        public string? NameFile
        {
            get { return _nameFile; }
            set { SetProperty(ref _nameFile, value); }
        }
        public int IdReminder { get; set; }
        public AttachReminder AttachReminder { get; set; }
        #endregion

        private readonly INavigationService _navigationService;
        private readonly IReminderService _reminderService;
        public EditReminderViewModel(INavigationService navigationService,
                                     IReminderService reminderService) : base(navigationService)
        {
            _navigationService = navigationService;
            _reminderService = reminderService;
        }
        public async override Task InitializeAsync()
        {
            await IsBusyFor(async () =>
            {
                var reminder = (Reminder)_parameters["edit"];

                if (reminder != null)
                {
                    IdReminder = reminder.Id;
                    TitleText = reminder.Title;
                    DescriptionText = reminder.Description;
                    DateDueText = new DateTime(reminder.DueDate.Year, reminder.DueDate.Month, reminder.DueDate.Day, 0, 0, 0);
                    HourDueText = new TimeSpan(reminder.DueDate.Hour, reminder.DueDate.Minute, 0);
                    AttachReminder = reminder.Attach;
                    NameFile = reminder.Attach != null ? reminder.Attach.Name : string.Empty;
                }
            });
        }
        [RelayCommand]
        public async Task BackAsync()
        {
            await _navigationService.PopModalAsync();
        }
        [RelayCommand]
        public async Task DeleteAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    var result = await _reminderService.DeleteReminderAsync(IdReminder);

                    if (result != null && result.Success)
                    {
                        App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.DELETE_REMINDER_SUCCESS, "ok");
                        App.ScreenMain();
                    }
                    else
                    {
                        App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.DELETE_REMINDER_ERROR, "ok");
                    }
                }
                catch (Exception)
                {
                    App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
                }
            });
        }
        [RelayCommand]
        public async Task EditAsync()
        {
            await IsBusyFor(async () =>
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
                        var dueDate = new DateTime(DateDueText.Year, DateDueText.Month,
                                                   DateDueText.Day, HourDueText.Hours, HourDueText.Minutes, 0);

                        var dto = new EditReminderDto()
                        {
                            Title = TitleText,
                            Description = DescriptionText,
                            DueDate = dueDate,
                            Attach = AttachReminder == null ? null : new AttachReminderDto() { Name = AttachReminder.Name, Path = AttachReminder.Path }
                        };

                        var result = await _reminderService.EditReminderAsync(IdReminder, dto);

                        if (result != null && result.Success)
                        {
                            App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.EDIT_REMINDER_SUCCESS, "ok");
                            App.ScreenMain();
                        }
                        else
                        {
                            App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.EDIT_REMINDER_ERROR, "ok");
                        }
                    }
                }
                catch (Exception)
                {
                    App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
                }
            });
        }
    }
}
