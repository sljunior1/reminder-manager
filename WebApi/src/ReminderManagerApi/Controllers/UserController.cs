using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReminderManagerApi.Dto.Requests;
using ReminderManagerApi.Dto.Responses;
using ReminderManagerApi.Models;
using ReminderManagerApi.Services.User;

namespace ReminderManagerApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserModel>>> CreateUserAsync(CreateUserDto dto)
        {
            var response = await _userService.CreateUserAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<UserModel>>> LoginAsync(LoginUserDto dto)
        {
            var response = await _userService.LoginAsync(dto);
            return response.Success ? Ok(response) : Unauthorized(response);
        }
        [HttpGet("search/{id}")]
        public async Task<ActionResult<ResponseModel<GetUserResponse>>> GetUserByIdAsync(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [AllowAnonymous]
        [HttpPatch("forgot-password")]
        public async Task<ActionResult<ResponseModel<UserModel>>> ForgotPasswordUserAsync(ForgotPasswordUserDto dto)
        {
            var response = await _userService.ForgotPasswordUserAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> DeleteUserAsync(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("calendar/{id}")]
        public async Task<ActionResult<ResponseModel<List<GetAllCalendarByUserIdResponse>>>> GetAllCalendarByUserId(int id)
        {
            var response = await _userService.GetAllCalendarByUserId(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
    