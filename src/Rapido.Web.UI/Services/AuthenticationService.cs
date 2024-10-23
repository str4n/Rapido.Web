using Microsoft.AspNetCore.Components;
using Rapido.Web.Core.Storage;
using Rapido.Web.Core.Users.Models;
using Rapido.Web.Core.Users.Requests;
using Rapido.Web.Core.Users.Services;

namespace Rapido.Web.UI.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly ILocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;
    private User? _user;

    public AuthenticationService(IUserService userService, ILocalStorageService localStorageService, 
        NavigationManager navigationManager)
    {
        _userService = userService;
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
    }

    public Task<User?> GetUserAsync()
        => Task.FromResult(_user);

    public async Task InitializeAsync()
        => _user = await _localStorageService.GetAsync<User>("user");

    public async Task<bool?> SignInAsync(string email, string password)
    {
        var request = new SignInRequest(email, password);
        var response = await _userService.SignInAsync(request);

        if (response?.HttpResponse is null)
        {
            return null;
        }

        if (response.Succeeded is false)
        {
            return false;
        }

        _user = new User
        {
            Id = response.Value!.UserId.ToString(),
            Email = response.Value.Email,
            Role = response.Value.Role,
            AccessToken = response.Value.AccessToken,
            RefreshToken = response.Value.RefreshToken,
            TokenExpires = response.Value.Expires
        };

        await _localStorageService.SetAsync("user", _user);

        return true;
    }

    public async Task SignOutAsync()
    {
        _user = null;

        await _localStorageService.RemoveAsync("user");
        _navigationManager.NavigateTo("log-in");
    }
}