namespace Rapido.Web.Core.Users.DTO;

public sealed record UserDto(Guid UserId, string Email, string Role, string AccountType);