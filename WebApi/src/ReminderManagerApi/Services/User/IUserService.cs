using ReminderManagerApi.Dto.Requests;
using ReminderManagerApi.Dto.Responses;
using ReminderManagerApi.Models;

namespace ReminderManagerApi.Services.User
{
    public interface IUserService
    {
        Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDto dto);
        Task<ResponseModel<UserModel>> ForgotPasswordUserAsync(ForgotPasswordUserDto dto);
        Task<ResponseModel<UserModel>> DeleteUserAsync(int id);
        Task<ResponseModel<LoginUserResponse?>> LoginAsync(LoginUserDto request);
        Task<ResponseModel<GetUserResponse>> GetUserByIdAsync(int id);
        Task<ResponseModel<List<GetAllCalendarByUserIdResponse>>> GetAllCalendarByUserId(int id);
    }
}
