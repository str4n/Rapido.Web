using Rapido.Web.Core.Clients;

namespace Rapido.Web.UI.Services;

public interface IApiResponseHandler
{
    Task<ApiResponse?> HandleAsync(Func<Task<ApiResponse>> request);
}