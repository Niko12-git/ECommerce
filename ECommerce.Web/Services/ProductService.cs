using System.Net.Http.Json;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ProductModel>>("api/products");
        return result ?? new List<ProductModel>();
    }

    public async Task<bool> CreateAsync(CreateProductModel model)
    {
        var response = await _http.PostAsJsonAsync("api/products", model);
        return response.IsSuccessStatusCode;
    }
}