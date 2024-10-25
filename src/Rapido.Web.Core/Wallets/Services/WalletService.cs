using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Wallets.DTO;

namespace Rapido.Web.Core.Wallets.Services;

internal sealed class WalletService : IWalletService
{
    private const string Path = "wallets-service";
    private readonly IHttpClient _httpClient;

    public WalletService(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<WalletDto?>> GetWalletAsync()
        => await _httpClient.GetAsync<WalletDto?>($"{Path}/wallet");
}