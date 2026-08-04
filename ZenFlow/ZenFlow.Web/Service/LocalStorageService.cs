using System.Text.Json;
using Microsoft.JSInterop;

namespace ZenFlow.Web.Services;

public class LocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task GuardarAsync<T>(string clave, T valor)
    {
        string json = JsonSerializer.Serialize(valor, _options);

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.setItem",
            clave,
            json
        );
    }

    public async Task<T?> ObtenerAsync<T>(string clave)
    {
        string? json = await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            clave
        );

        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json, _options);
    }

    public async Task EliminarAsync(string clave)
    {
        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            clave
        );
    }
}