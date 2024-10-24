using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Rapido.Web.Core.Storage;
using Rapido.Web.Core.Users.Models;

namespace Rapido.Web.Core.Auth;

public sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "JwtAuth";
    
    private readonly ILocalStorageService _localStorageService;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var user = await _localStorageService.GetAsync<User>("user");

            if (user is null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
            }, AuthenticationType));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch 
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(User? user)
    {
        ClaimsPrincipal claimsPrincipal;

        if (user is not null)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
            }, AuthenticationType));
        }
        else
        {
            claimsPrincipal = _anonymous;
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}