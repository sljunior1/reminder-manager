using ReminderManagerApp.Dtos;
using ReminderManagerApp.Models;
using ReminderManagerApp.Responses.Responses;

namespace ReminderManagerApp.Services
{
    public class ReminderService(IRequestProviderService requestProviderService,
                                 ISettingsService settingsService) : IReminderService
    {
        private readonly IRequestProviderService _requestProviderService = requestProviderService;
        private readonly ISettingsService _settingsService = settingsService;

        public async Task<ResponseModel<List<Reminder>>> GetAllReminderByUserAsync(int userId)
        {
            var uri = GlobalSettings.Instance.GetAllReminderByUserEndpoint + $"{userId}";
            return await _requestProviderService.GetAsync<ResponseModel<List<Reminder>>>(uri, _settingsService.AuthAccessToken);
        }
        public async Task<ResponseModel<Reminder>> CreateReminderAsync(CreateReminderDto dto)
        {
            var uri = GlobalSettings.Instance.CreateReminderEndpoint;
            return await _requestProviderService.PostAsync<ResponseModel<Reminder>, CreateReminderDto>(uri, dto, _settingsService.AuthAccessToken);
        }
        public async Task<ResponseModel<Reminder>> DeleteReminderAsync(int idReminder)
        {
            var uri = GlobalSettings.Instance.DeleteReminderEndpoint + idReminder;
            return await _requestProviderService.DeleteAsync<ResponseModel<Reminder>>(uri, _settingsService.AuthAccessToken);
        }
        public async Task<ResponseModel<Reminder>> EditReminderAsync(int idReminder, EditReminderDto dto)
        {
            var uri = GlobalSettings.Instance.EditReminderEndpoint + idReminder;
            return await _requestProviderService.PatchAsync<ResponseModel<Reminder>, EditReminderDto>(uri, dto, _settingsService.AuthAccessToken);
        }
    }
}
