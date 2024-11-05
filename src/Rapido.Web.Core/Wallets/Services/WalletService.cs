using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Wallets.DTO;
using Rapido.Web.Core.Wallets.Requests;

namespace Rapido.Web.Core.Wallets.Services;

internal sealed class WalletService : IWalletService
{
    private const string Path = "wallets-service";
    private readonly IHttpClient _httpClient;

    public WalletService(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ApiResponse<WalletDto?>> GetWalletAsync()
        => _httpClient.GetAsync<WalletDto?>($"{Path}/wallet");

    public Task<ApiResponse> TransferFundsAsync(TransferFundsRequest request)
        => _httpClient.PostAsync($"{Path}/transfer/name", request);
}