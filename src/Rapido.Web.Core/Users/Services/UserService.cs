using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Users.DTO;
using Rapido.Web.Core.Users.Requests;

namespace Rapido.Web.Core.Users.Services;

internal sealed class UserService : IUserService
{
    private const string Path = "users-service";
    private readonly IHttpClient _httpClient;

    public UserService(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ApiResponse> SignUpAsync(SignUpRequest request)
        => _httpClient.PostAsync($"{Path}/sign-up", request);

    public Task<ApiResponse<AuthDto?>> SignInAsync(SignInRequest request)
        => _httpClient.PostAsync<AuthDto>($"{Path}/sign-in", request);

    public Task<ApiResponse<EmailCheckDto?>> CheckEmailAsync(string email)
        => _httpClient.GetAsync<EmailCheckDto>($"{Path}/users/check-email/{email}");
}