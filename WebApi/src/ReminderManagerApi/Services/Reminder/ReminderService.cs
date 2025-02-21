using Microsoft.EntityFrameworkCore;
using ReminderManagerApi.Data;
using ReminderManagerApi.Dto.Requests;
using ReminderManagerApi.Models;

namespace ReminderManagerApi.Services.Reminder
{
    public class ReminderService(AppDbContext context) : IReminderService
    {
        private readonly AppDbContext _context = context;
        public async Task<ResponseModel<List<ReminderModel>>> GetAllReminderByUserAsync(int idUser)
        {
            var response = new ResponseModel<List<ReminderModel>>();

            try
            {
                var reminders = await _context.Reminders
                    .Include(s => s.User)
                    .Include(s => s.Attach)
                    .Where(s => s.User.Id == idUser).ToListAsync();

                if (reminders is null)
                {
                    response.ErrorMessage = "Nenhum registro encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = reminders;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível listar todos os lembretes.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<ReminderModel>> GetReminderById(int id)
        {
            var response = new ResponseModel<ReminderModel>();

            try
            {
                var reminder = await _context.Reminders.FirstOrDefaultAsync(s => s.Id.Equals(id));

                response.Data = reminder;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível buscar o lembrete.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<ReminderModel>> CreateReminderAsync(CreateReminderDto dto)
        {
            var response = new ResponseModel<ReminderModel>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(s => s.Id.Equals(dto.UserId));

                if (user == null)
                {
                    response.ErrorMessage = "Usuário não encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Success = false;

                    return response;
                }

                var reminder = new ReminderModel()
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    CreationDate = DateTime.Now,
                    DueDate = dto.DueDate,
                    User = user,
                    Attach = dto.Attach == null ? null : new AttachReminderModel() { Name = dto.Attach.Name, Path = dto.Attach.Path }
                };

                _context.Add(reminder);
                await _context.SaveChangesAsync();

                response.Data = null;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível criar o lembrete.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<ReminderModel>> DeleteReminderAsync(int id)
        {
            var response = new ResponseModel<ReminderModel>();

            try
            {
                var reminder = await _context.Reminders
                    .Include(s => s.Attach)
                    .FirstOrDefaultAsync(s => s.Id.Equals(id));

                if (reminder == null)
                {
                    response.ErrorMessage = "Lembrete não encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Success = false;

                    return response;
                }

                _context.Remove(reminder);

                await _context.SaveChangesAsync();

                response.Data = null;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível excluir o lembrete.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<ReminderModel>> UpdateReminderAsync(int id, UpdateReminderDto dto)
        {
            var response = new ResponseModel<ReminderModel>();

            try
            {
                var reminder = await _context.Reminders.FirstOrDefaultAsync(s => s.Id.Equals(id));

                if (reminder == null)
                {
                    response.ErrorMessage = "Lembrete não encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Success = false;
                    return response;
                }

                reminder.Title = dto.Title;
                reminder.Description = dto.Description;
                reminder.DueDate = dto.DueDate;
                reminder.Attach = dto.Attach == null ? null : new AttachReminderModel() { Name = dto.Attach.Name, Path = dto.Attach.Path };

                _context.Update(reminder);
                await _context.SaveChangesAsync();

                response.Data = null;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível editar o lembrete.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
    }
}
