using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Wallets.DTO;

namespace Rapido.Web.Core.Wallets.Services;

public interface IWalletService
{
    Task<ApiResponse<WalletDto?>> GetWalletAsync();
}