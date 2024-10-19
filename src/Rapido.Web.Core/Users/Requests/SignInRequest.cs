namespace Rapido.Web.Core.Users.Requests;

public sealed record SignInRequest(string Email, string Password);