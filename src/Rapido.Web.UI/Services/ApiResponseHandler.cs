using System.Net;
using MudBlazor;
using Rapido.Web.Core.Clients;

namespace Rapido.Web.UI.Services;

internal sealed class ApiResponseHandler : IApiResponseHandler
{
    private readonly IAuthenticationService _authService;
    private readonly ISnackbar _snackbar;

    public ApiResponseHandler(IAuthenticationService authService, ISnackbar snackbar)
    {
        _authService = authService;
        _snackbar = snackbar;
    }


    public async Task<ApiResponse?> HandleAsync(Func<Task<ApiResponse>> request)
    {
        var response = await request();
        
        if (response.HttpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            _snackbar.Add("Your session has expired - please sign in again.", Severity.Error);
            await _authService.SignOutAsync();
            return null;
        }

        if (response.Succeeded)
        {
            return response;
        }

        return null;
    }
}