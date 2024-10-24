using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rapido.Web.Core.Auth;
using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Storage;
using Rapido.Web.Core.Users.Services;

namespace Rapido.Web.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddScoped<ILocalStorageService, LocalStorageService>()
            .AddScoped<IUserService, UserService>()
            .AddTransient<IHttpClient, CustomHttpClient>();

        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        
        return services;
    }
}