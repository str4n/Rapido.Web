namespace Rapido.Web.Core.Users.DTO;

public sealed record AuthDto(string AccessToken, string RefreshToken, Guid UserId, string Email, string Role, DateTime Expires);