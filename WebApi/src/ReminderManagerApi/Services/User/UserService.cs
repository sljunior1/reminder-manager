using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReminderManagerApi.Data;
using ReminderManagerApi.Dto.Requests;
using ReminderManagerApi.Dto.Responses;
using ReminderManagerApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReminderManagerApi.Services.User
{
    public class UserService(AppDbContext context, IConfiguration configuration) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        public async Task<ResponseModel<GetUserResponse>> GetUserByIdAsync(int id)
        {
            var response = new ResponseModel<GetUserResponse>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(s => s.Id.Equals(id));

                if (user is null)
                {
                    response.ErrorMessage = "Usuário não encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = new GetUserResponse()
                {
                    Cpf = user.Cpf,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };

                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível encontrar o usuário.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDto dto)
        {
            var response = new ResponseModel<UserModel>();

            try
            {
                var user = _context.Users.FirstOrDefaultAsync(s => s.Cpf.Equals(dto.Cpf));
                if (user != null)
                {
                    response.ErrorMessage = "Usuário já cadastrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }

                var newUser = new UserModel()
                {
                    Name = dto.Name,
                    Cpf = dto.Cpf,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = dto.Password
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();

                response.Data = null;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível criar o usuário.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<UserModel>> DeleteUserAsync(int id)
        {
            var response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(s => s.Id.Equals(id));

                if (user == null)
                {
                    response.ErrorMessage = "Usuário não encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Success = false;
                    return response;
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                response.Data = null;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível excluir o usuário.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<UserModel>> ForgotPasswordUserAsync(ForgotPasswordUserDto dto)
        {
            var response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(s => s.Email.Equals(dto.Email));

                if (user == null)
                {
                    response.ErrorMessage = "Usuário não encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Success = false;
                    return response;
                }

                user.Password = dto.Password;

                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Data = null;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível editar o usuário.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<LoginUserResponse?>> LoginAsync(LoginUserDto request)
        {
            var response = new ResponseModel<LoginUserResponse?>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                {
                    response.Success = false;
                    response.ErrorMessage = "Usuário ou Senha inválidos.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var userAccount = await _context.Users.FirstOrDefaultAsync(s => s.Cpf.Equals(request.UserName) | s.Email.Equals(request.UserName));

                if (userAccount is null || !request.Password.Equals(userAccount.Password))
                {
                    response.Success = false;
                    response.ErrorMessage = "Senha inválida.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var issuer = _configuration["JwtConfig:Issuer"];
                var audience = _configuration["JwtConfig:Audience"];
                var key = _configuration["JwtConfig:Key"];
                var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
                var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(JwtRegisteredClaimNames.Name, request.UserName)
                }),
                    Expires = tokenExpiryTimeStamp,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha512Signature),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(securityToken);

                var loginResponse = new LoginUserResponse
                {
                    AccessToken = accessToken,
                    UserName = request.UserName,
                    Name = userAccount.Name,
                    UserId = userAccount.Id,
                    ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
                };

                response.Data = loginResponse;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível autenticar o usuário.";
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                response.Success = false;
            }

            return response;
        }
        public async Task<ResponseModel<List<GetAllCalendarByUserIdResponse>>> GetAllCalendarByUserId(int id)
        {
            var response = new ResponseModel<List<GetAllCalendarByUserIdResponse>>();
            var list = new List<GetAllCalendarByUserIdResponse>();
            try
            {
                var reminders = await _context.Reminders
                    .Include(s => s.User)
                    .Where(s => s.User.Id == id).ToListAsync();

                if (reminders is null)
                {
                    response.ErrorMessage = "Nenhum registro encontrado.";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }

                foreach (var reminder in reminders)
                {
                    list.Add(new GetAllCalendarByUserIdResponse() { Title = reminder.Title, DueDate = reminder.DueDate, Description = reminder.Description });
                }

                response.Data = list;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
            }
            catch (Exception)
            {
                response.ErrorMessage = "Não foi possível listar todos os calendários do usuário.";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
    }
}
