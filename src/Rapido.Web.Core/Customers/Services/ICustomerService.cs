using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Customers.Requests;

namespace Rapido.Web.Core.Customers.Services;

public interface ICustomerService
{
    Task<ApiResponse> CompleteIndividualCustomerAsync(CompleteIndividualCustomerRequest request);
}