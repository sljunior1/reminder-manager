using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReminderManagerApp.Controls;
using ReminderManagerApp.Dtos;
using ReminderManagerApp.Models;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.ViewModels.Reminders.v1
{
    public partial class NewReminderViewModel : ViewModelBase
    {
        #region Properties

        private string? _nameFile;
        private string? _titleText;
        private string? _descriptionText;
        [ObservableProperty]
        private DateTime _dateDueText = DateTime.Now;
        [ObservableProperty]
        private TimeSpan _hourDueText;
        public string? NameFile
        {
            get { return _nameFile; }
            set { SetProperty(ref _nameFile, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_TITLE_REQUIRED)]
        public string? TitleText
        {
            get { return _titleText; }
            set { SetProperty(ref _titleText, value); }
        }
        [Required(ErrorMessage = SystemMessage.FIELD_DESCRIPTION_REQUIRED)]
        public string? DescriptionText
        {
            get { return _descriptionText; }
            set { SetProperty(ref _descriptionText, value); }
        }
        public AttachReminder? AttachReminder { get; set; }

        #endregion

        private readonly INavigationService _navigationService;
        private readonly ILocalNotificationService _localNotificationService;
        private readonly IReminderService _reminderService;
        private readonly ISettingsService _settingsService;
        public NewReminderViewModel(INavigationService navigationService,
                                    ILocalNotificationService localNotificationService,
                                    IReminderService reminderService,
                                    ISettingsService settingsService) : base(navigationService)
        {
            _navigationService = navigationService;
            _localNotificationService = localNotificationService;
            _reminderService = reminderService;
            _settingsService = settingsService;

            AttachReminder = new AttachReminder();
        }
        [RelayCommand]
        public async Task BackAsync()
        {
            await IsBusyFor(async () =>
            {
                await _navigationService.PopModalAsync();
            });
        }
        [RelayCommand]
        public async Task SaveAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    AttachReminderDto attach = null;

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

                        if (AttachReminder != null)
                        {
                            if (!string.IsNullOrEmpty(AttachReminder.Name) && !string.IsNullOrEmpty(AttachReminder.Path))
                                attach = new AttachReminderDto() { Name = AttachReminder.Name, Path = AttachReminder.Path };
                        }

                        var dto = new CreateReminderDto()
                        {
                            Title = TitleText,
                            Description = DescriptionText,
                            DueDate = dueDate,
                            UserId = _settingsService.IdUser,
                            Attach = attach
                        };

                        var response = await _reminderService.CreateReminderAsync(dto);

                        if (response != null && response.Success)
                        {
                            await _localNotificationService.ScheduleNotification(dto.Title, dto.Description, dto.DueDate);

                            App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.CREATE_REMINDER_SUCCESS, "ok");
                            App.ScreenMain();
                        }
                        else
                        {
                            App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.CREATE_REMINDER_ERROR, "ok");
                        }
                    }
                }
                catch (Exception)
                {
                    App.Current?.MainPage?.DisplayAlert(SystemMessage.REMINDER_MANAGER, SystemMessage.FRIENDLY_MESSAGE, "ok");
                }
            });
        }
        [RelayCommand]
        public async Task AttachFileAsync()
        {
            await IsBusyFor(async () =>
            {
                try
                {
                    var result = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Por favor, selecione o arquivo",
                        FileTypes = CustomFilePickerFileType.ImagesAndDocuments
                    });

                    if (result != null)
                    {
                        var fileBytes = await File.ReadAllBytesAsync(result.FullPath);

                        AttachReminder = new AttachReminder()
                        {
                            Name = result.FileName,
                            Path = result.FullPath
                        };

                        NameFile = AttachReminder.Name;
                    }
                }
                catch (Exception)
                {
                    return;
                }
            });
        }
        [RelayCommand]
        public async Task CleanAsync()
        {
            NameFile = string.Empty;
            TitleText = string.Empty;
            DescriptionText = string.Empty;
            AttachReminder = new AttachReminder();
        }
    }
}
