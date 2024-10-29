using Rapido.Web.Core.Users.Models;

namespace Rapido.Web.UI.Services;

public interface IAuthenticationService
{
    Task<User?> GetUserAsync();
    Task InitializeAsync();
    Task<bool?> SignInAsync(string email, string password);
    Task SignOutAndNavigateAsync(string route);
    Task NavigateIfSignedIn(string route);
}