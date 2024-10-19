namespace Rapido.Web.Core.Users.DTO;

public sealed record AuthDto(string Token, Guid UserId, string Email, string Role);