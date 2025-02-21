using ReminderManagerApp.Dtos;
using ReminderManagerApp.Responses.Responses;

namespace ReminderManagerApp.Services
{
    public interface IUserService
    {
        Task<ResponseModel<UserResponse>> ExecuteAddUserAsync(CreateUserDto dto);
        Task<ResponseModel<LoginUserResponse>> ExecuteLoginAsync(LoginUserDto dto);
        Task<ResponseModel<UserResponse>> ForgotPasswordUserAsync(ForgotPasswordUserDto dto);
        Task<ResponseModel<List<GetAllCalendarByUserIdResponse>>> GetAllCalendarByUserId();
        Task<ResponseModel<GetUserResponse>> GetUserByIdAsync();
    }
}
