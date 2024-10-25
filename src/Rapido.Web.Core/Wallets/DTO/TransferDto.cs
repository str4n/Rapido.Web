namespace Rapido.Web.Core.Wallets.DTO;

public sealed record TransferDto(
    Guid Id, 
    string Name, 
    double Amount, 
    string Currency, 
    string Type, 
    DateTime CreatedAt);