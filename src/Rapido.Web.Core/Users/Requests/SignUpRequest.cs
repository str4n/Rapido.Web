namespace Rapido.Web.Core.Users.Requests;

public sealed record SignUpRequest(string Email, string Password, string AccountType);