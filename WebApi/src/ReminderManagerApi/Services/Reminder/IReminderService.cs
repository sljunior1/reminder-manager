using ReminderManagerApi.Dto.Requests;
using ReminderManagerApi.Models;

namespace ReminderManagerApi.Services.Reminder
{
    public interface IReminderService
    {
        Task<ResponseModel<List<ReminderModel>>> GetAllReminderByUserAsync(int idUser);
        Task<ResponseModel<ReminderModel>> GetReminderById(int id);
        Task<ResponseModel<ReminderModel>> CreateReminderAsync(CreateReminderDto dto);
        Task<ResponseModel<ReminderModel>> UpdateReminderAsync(int id, UpdateReminderDto dto);
        Task<ResponseModel<ReminderModel>> DeleteReminderAsync(int id);
    }
}
