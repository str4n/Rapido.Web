using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Customers.DTO;
using Rapido.Web.Core.Customers.Requests;

namespace Rapido.Web.Core.Customers.Services;

internal sealed class CustomerService : ICustomerService
{
    private const string Path = "customers-service";
    private readonly IHttpClient _httpClient;

    public CustomerService(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ApiResponse> CompleteIndividualCustomerAsync(CompleteIndividualCustomerRequest request)
        => _httpClient.PostAsync($"{Path}/individual/complete", request);

    public Task<ApiResponse> CompleteCorporateCustomerAsync(CompleteCorporateCustomerRequest request)
        => _httpClient.PostAsync($"{Path}/corporate/complete", request);

    public Task<ApiResponse<NameCheckDto?>> CheckNameAsync(string name)
        => _httpClient.GetAsync<NameCheckDto>($"{Path}/customers/check-name/{name}");
}