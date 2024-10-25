namespace Rapido.Web.Core.Wallets.DTO;

public sealed record WalletDto(
    Guid Id,
    Guid OwnerId, 
    IEnumerable<BalanceDto> Balances,
    IEnumerable<TransferDto> Transfers, 
    double TotalBalance,
    DateTime CreatedAt);