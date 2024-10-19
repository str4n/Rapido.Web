using System.Text.Json;
using Microsoft.JSInterop;

namespace Rapido.Web.Core.Storage;

internal sealed class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

        return json is not null ? JsonSerializer.Deserialize<T>(json) : default;
    }

    public async Task SetAsync<T>(string key, T value)
        => await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));

    public async Task RemoveAsync(string key)
        => await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
}