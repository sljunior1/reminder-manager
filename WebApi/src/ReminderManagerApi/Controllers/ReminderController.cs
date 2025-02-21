using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReminderManagerApi.Dto.Requests;
using ReminderManagerApi.Models;
using ReminderManagerApi.Services.Reminder;

namespace ReminderManagerApi.Controllers
{
    [Route("api/reminders")]
    [ApiController]
    [Authorize]
    public class ReminderController(IReminderService reminderService) : ControllerBase
    {
        private readonly IReminderService _reminderService = reminderService;

        [HttpGet("search/user/{userId}")]
        public async Task<ActionResult<ResponseModel<List<ReminderModel>>>> GetAllReminderByUserAsync(int userId)
        {
            var response = await _reminderService.GetAllReminderByUserAsync(userId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ReminderModel>>> GetReminderById(int id)
        {
            var response = await _reminderService.GetReminderById(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<ReminderModel>>> CreateReminderAsync(CreateReminderDto dto)
        {
            var response = await _reminderService.CreateReminderAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<ReminderModel>>> DeleteReminderAsync(int id)
        {
            var response = await _reminderService.DeleteReminderAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseModel<ReminderModel>>> UpdateReminderAsync(int id,UpdateReminderDto dto)
        {
            var response = await _reminderService.UpdateReminderAsync(id,dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
