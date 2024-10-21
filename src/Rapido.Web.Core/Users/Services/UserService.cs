using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Users.DTO;
using Rapido.Web.Core.Users.Requests;

namespace Rapido.Web.Core.Users.Services;

internal sealed class UserService : IUserService
{
    private readonly IHttpClient _httpClient;

    public UserService(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ApiResponse> SignUpAsync(SignUpRequest request)
        => _httpClient.PostAsync("users-service/sign-up", request);

    public Task<ApiResponse<AuthDto?>> SignInAsync(SignInRequest request)
        => _httpClient.PostAsync<AuthDto>("users-service/sign-in", request);
}