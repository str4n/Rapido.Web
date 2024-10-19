namespace Rapido.Web.UI.Services;

public interface IAuthenticationService
{
    Task InitializeAsync();
    Task<bool?> LogInAsync(string email, string password);
    Task LogOutAsync();
}