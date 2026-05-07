using System.Net.Http.Json;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UserModel>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<UserModel>>("api/users");
        return result ?? new List<UserModel>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserModel model)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{id}", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/users/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ActivateAsync(int id)
    {
        var response = await _http.PatchAsync($"api/users/{id}/activate", null);
        return response.IsSuccessStatusCode;
    }
}