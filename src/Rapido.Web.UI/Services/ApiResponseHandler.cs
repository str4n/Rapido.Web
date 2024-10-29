using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Rapido.Web.Core.Auth;
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
            await _authService.SignOutAndNavigateAsync("/log-in");
            return null;
        }

        if (response.Succeeded)
        {
            return response;
        }

        return response;
    }

    public async Task<ApiResponse<T>?> HandleAsync<T>(Func<Task<ApiResponse>> request)
    {
        var response = await request() as ApiResponse<T>;
        
        if (response?.HttpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            _snackbar.Add("Your session has expired - please sign in again.", Severity.Error);
            await _authService.SignOutAndNavigateAsync("/log-in");
            return null;
        }

        if (response!.Succeeded)
        {
            return response;
        }

        return response;
    }
}