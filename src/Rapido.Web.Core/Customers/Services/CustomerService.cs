using Rapido.Web.Core.Clients;
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
}