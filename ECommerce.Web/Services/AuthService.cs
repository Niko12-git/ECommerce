using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<bool> LoginAsync(LoginModel model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", model);
        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (result is null) return false;

        // Guardar token y datos del usuario en localStorage
        await _localStorage.SetItemAsync("token", result.Token);
        await _localStorage.SetItemAsync("userName", result.Name);
        await _localStorage.SetItemAsync("userRole", result.Role);
        await _localStorage.SetItemAsync("userEmail", result.Email);
        await _localStorage.SetItemAsync("userId", result.UserId.ToString());
        
        // Agregar el token a todas las peticiones futuras
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

        return true;
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", model);
        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (result is null) return false;

        await _localStorage.SetItemAsync("token", result.Token);
        await _localStorage.SetItemAsync("userName", result.Name);
        await _localStorage.SetItemAsync("userRole", result.Role);
        await _localStorage.SetItemAsync("userEmail", result.Email);
        await _localStorage.SetItemAsync("userId", result.UserId.ToString());
        
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("token");
        await _localStorage.RemoveItemAsync("userName");
        await _localStorage.RemoveItemAsync("userRole");
        await _localStorage.RemoveItemAsync("userEmail");
        _http.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("token");
        return !string.IsNullOrEmpty(token);
    }

    public async Task<string> GetUserNameAsync()
        => await _localStorage.GetItemAsync<string>("userName") ?? string.Empty;

    public async Task<string> GetUserRoleAsync()
        => await _localStorage.GetItemAsync<string>("userRole") ?? string.Empty;

    public async Task InitializeAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("token");
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
    public async Task<string> GetUserEmailAsync()
        => await _localStorage.GetItemAsync<string>("userEmail") ?? string.Empty;
    
    public async Task<int> GetUserIdAsync()
    {
        var id = await _localStorage.GetItemAsync<string>("userId");
        return int.TryParse(id, out var userId) ? userId : 0;
    }
    
}

