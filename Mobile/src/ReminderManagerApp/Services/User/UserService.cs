using ReminderManagerApp.Dtos;
using ReminderManagerApp.Responses.Responses;

namespace ReminderManagerApp.Services
{
    public class UserService(IRequestProviderService requestProviderService,
                             ISettingsService settingsService) : IUserService
    {
        private readonly IRequestProviderService _requestProviderService = requestProviderService;
        private readonly ISettingsService _settingsService = settingsService;
        public async Task<ResponseModel<UserResponse>> ExecuteAddUserAsync(CreateUserDto dto)
        {
            var uri = GlobalSettings.Instance.ExecuteAddUserEndpoint;
            return await _requestProviderService.PostAsync<ResponseModel<UserResponse>, CreateUserDto>(uri, dto);
        }
        public async Task<ResponseModel<LoginUserResponse>> ExecuteLoginAsync(LoginUserDto dto)
        {
            if (Helpers.CpfHelper.IsCpf(dto.UserName))
                dto.UserName = dto.UserName.Replace(".", "").Replace("-", "");

            var uri = GlobalSettings.Instance.ExecuteLoginEndpoint;
            return await _requestProviderService.PostAsync<ResponseModel<LoginUserResponse>, LoginUserDto>(uri, dto);
        }
        public async Task<ResponseModel<GetUserResponse>> GetUserByIdAsync()
        {
            var uri = GlobalSettings.Instance.GetUserByIdEndpoint + _settingsService.IdUser;
            return await _requestProviderService.GetAsync<ResponseModel<GetUserResponse>>(uri, _settingsService.AuthAccessToken);
        }
        public async Task<ResponseModel<List<GetAllCalendarByUserIdResponse>>> GetAllCalendarByUserId()
        {
            var uri = GlobalSettings.Instance.GetAllCalendarByUserIdEndpoint + _settingsService.IdUser;
            return await _requestProviderService.GetAsync<ResponseModel<List<GetAllCalendarByUserIdResponse>>>(uri, _settingsService.AuthAccessToken);
        }
        public async Task<ResponseModel<UserResponse>> ForgotPasswordUserAsync(ForgotPasswordUserDto dto)
        {
            var uri = GlobalSettings.Instance.ForgotPasswordUserEndpoint;
            return await _requestProviderService.PatchAsync<ResponseModel<UserResponse>, ForgotPasswordUserDto>(uri, dto);
        }
    }
}
