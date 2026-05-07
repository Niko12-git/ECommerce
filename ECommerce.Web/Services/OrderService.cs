using System.Net.Http.Json;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class OrderService
{
    private readonly HttpClient _http;

    public OrderService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> CreateOrderAsync(CreateOrderModel model)
    {
        var response = await _http.PostAsJsonAsync("api/orders", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<OrderResponseDto>> GetMyOrdersAsync(int customerId)
    {
        var result = await _http
            .GetFromJsonAsync<List<OrderResponseDto>>(
                $"api/orders/my-orders/{customerId}");
        return result ?? new List<OrderResponseDto>();
    }
}