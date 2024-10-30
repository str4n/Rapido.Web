using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Rapido.Web.Core.Auth;
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
    private readonly CustomAuthenticationStateProvider? _authenticationStateProvider;
    private User? _user;

    public AuthenticationService(IUserService userService, ILocalStorageService localStorageService, 
        NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
    {
        _userService = userService;
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
        _authenticationStateProvider = authenticationStateProvider as CustomAuthenticationStateProvider;
    }

    public Task<User?> GetUserAsync()
        => Task.FromResult(_user);

    public async Task InitializeAsync()
    {
        _user = await _localStorageService.GetAsync<User>("user");
        _authenticationStateProvider?.UpdateAuthenticationState(_user);
    }

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
            Id = response.Value!.Token.UserId.ToString(),
            Email = response.Value.Token.Email,
            Role = response.Value.Token.Role,
            AccountType = response.Value.AccountType,
            AccessToken = response.Value.Token.AccessToken,
            RefreshToken = response.Value.Token.RefreshToken,
            TokenExpires = response.Value.Token.Expires
        };
        
        await _localStorageService.SetAsync("user", _user);
        await _authenticationStateProvider?.UpdateAuthenticationState(_user)!;

        return true;
    }

    public async Task SignOutAndNavigateAsync(string route)
    {
        _user = null;

        await _localStorageService.RemoveAsync("user");
        await _authenticationStateProvider?.UpdateAuthenticationState(null)!;
        _navigationManager.NavigateTo(route);
    }

    public async Task NavigateIfSignedIn(string route)
    {
        var state = await _authenticationStateProvider?.GetAuthenticationStateAsync()!;

        if (state.User.Identity is null)
        {
            return;
        }

        var isAuthenticated = state.User.Identity.IsAuthenticated && _user is not null;

        if (isAuthenticated)
        {
            _navigationManager.NavigateTo(route);
        }
    }
}