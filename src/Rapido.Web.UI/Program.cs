using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Rapido.Web.Core;
using Rapido.Web.UI;
using Rapido.Web.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddCore();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IApiResponseHandler, ApiResponseHandler>();

var app = builder.Build();

var authenticationService = app.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.InitializeAsync();

await app.RunAsync();