using ReminderManagerApp.Models;
using ReminderManagerApp.Services;
using ReminderManagerApp.ViewModels.Base;
using System.Globalization;
using Plugin.Maui.Calendar.Models;

namespace ReminderManagerApp.ViewModels.Reminders.v1
{
    public partial class CalendarViewModel : ViewModelBase
    {
        #region Properties
        public EventCollection Events { get; set; } = new EventCollection();
        public CultureInfo Culture => new CultureInfo("pt-BR");

        #endregion

        private readonly IUserService _userService;
        public CalendarViewModel(INavigationService navigationService,
                                 IUserService userService) : base(navigationService)
        {
            _userService = userService;
        }

        public async Task<EventCollection> LoadEvents()
        {
            try
            {
                var response = await _userService.GetAllCalendarByUserId();

                if (response != null && response.Success)
                {
                    var groupItems = response.Data
                        .GroupBy(s => s.DueDate.Date)
                        .ToDictionary(
                         s => s.Key,
                         s => s.ToList()
                         );

                    var lista = new List<EventModel>();

                    foreach (var item in groupItems)
                    {
                        lista = new List<EventModel>();

                        if (!Events.ContainsKey(item.Key))
                        {
                            for (int i = 0; i < item.Value.Count; i++)
                            {
                                lista.Add(new EventModel() { Name = item.Value[i].Title, Description = item.Value[i].Description });
                            }

                            Events[item.Key] = lista;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return Events;
        }
    }
}
