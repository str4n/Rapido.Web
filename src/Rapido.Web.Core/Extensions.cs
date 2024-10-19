using Microsoft.Extensions.DependencyInjection;
using Rapido.Web.Core.Clients;
using Rapido.Web.Core.Storage;

namespace Rapido.Web.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddScoped<ILocalStorageService, LocalStorageService>()
            .AddScoped<IHttpClient, CustomHttpClient>();
        
        return services;
    }
}