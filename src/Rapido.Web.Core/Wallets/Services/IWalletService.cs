using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Wallets.DTO;
using Rapido.Web.Core.Wallets.Requests;

namespace Rapido.Web.Core.Wallets.Services;

public interface IWalletService
{
    Task<ApiResponse<WalletDto?>> GetWalletAsync();
    Task<ApiResponse> TransferFundsAsync(TransferFundsRequest request);
}