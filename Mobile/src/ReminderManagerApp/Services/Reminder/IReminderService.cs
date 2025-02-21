using ReminderManagerApp.Dtos;
using ReminderManagerApp.Models;
using ReminderManagerApp.Responses.Responses;

namespace ReminderManagerApp.Services
{
    public interface IReminderService
    {
        Task<ResponseModel<List<Reminder>>> GetAllReminderByUserAsync(int userId);
        Task<ResponseModel<Reminder>> CreateReminderAsync(CreateReminderDto dto);
        Task<ResponseModel<Reminder>> DeleteReminderAsync(int idReminder);
        Task<ResponseModel<Reminder>> EditReminderAsync(int idReminder, EditReminderDto dto);
    }
}
