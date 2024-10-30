namespace Rapido.Web.Core.Users.DTO;

public sealed record AuthDto(JsonWebToken Token, string AccountType);