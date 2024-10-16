using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rapido.Web.Core;
using Rapido.Web.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });

await builder.Build().RunAsync();