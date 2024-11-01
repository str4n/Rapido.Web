using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Users.DTO;
using Rapido.Web.Core.Users.Requests;

namespace Rapido.Web.Core.Users.Services;

public interface IUserService
{
    Task<ApiResponse> SignUpAsync(SignUpRequest request);
    Task<ApiResponse<AuthDto?>> SignInAsync(SignInRequest request);
    Task<ApiResponse<EmailCheckDto?>> CheckEmailAsync(string email);
}