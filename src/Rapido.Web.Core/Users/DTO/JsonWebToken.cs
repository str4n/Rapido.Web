namespace Rapido.Web.Core.Users.DTO;

public sealed record JsonWebToken(string AccessToken, string RefreshToken, Guid UserId, string Email, string Role, DateTime Expires);