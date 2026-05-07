using System.Net.Http.Json;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class DashboardService
{
    private readonly HttpClient _http;

    public DashboardService(HttpClient http)
    {
        _http = http;
    }

    public async Task<DashboardStats?> GetStatsAsync()
    {
        try
        {
            return await _http
                .GetFromJsonAsync<DashboardStats>("api/dashboard/stats");
        }
        catch
        {
            return null;
        }
    }
}