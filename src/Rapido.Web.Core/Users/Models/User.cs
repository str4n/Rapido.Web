namespace Rapido.Web.Core.Users.Models;

public sealed class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string JwtToken { get; set; }
    public bool IsAdmin => Role == "admin";
}