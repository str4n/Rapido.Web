namespace Rapido.Web.Core.Wallets.Requests;

public sealed record TransferFundsRequest(
    Guid OwnerId, 
    string ReceiverName, 
    string TransferName,
    string Currency, 
    double Amount);