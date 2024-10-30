namespace Rapido.Web.Core.Users.Models;

public sealed class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string AccountType { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpires { get; set; }
    public bool IsAdmin => Role == "admin";
}