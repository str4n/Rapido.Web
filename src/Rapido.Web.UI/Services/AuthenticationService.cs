using Microsoft.AspNetCore.Components;
using Rapido.Web.Core.Storage;
using Rapido.Web.Core.Users.Services;

namespace Rapido.Web.UI.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly ILocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;

    public AuthenticationService(IUserService userService, ILocalStorageService localStorageService, 
        NavigationManager navigationManager)
    {
        _userService = userService;
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
    }
    
    public async Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool?> LogInAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task LogOutAsync()
    {
        throw new NotImplementedException();
    }
}