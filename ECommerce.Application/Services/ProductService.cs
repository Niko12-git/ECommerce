using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repo;

    // Principio D de SOLID — depende de la interfaz, no de la implementación
    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await _repo.GetAllAsync();

    public async Task<Product?> GetProductAsync(int id)
        => await _repo.GetByIdAsync(id);

    public async Task CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product(dto.Name, dto.Price, dto.Stock);
        await _repo.AddAsync(product);
    }
}