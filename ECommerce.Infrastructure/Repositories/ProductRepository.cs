using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _ctx;

    public ProductRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Product?> GetByIdAsync(int id)
        => await _ctx.Products.FindAsync(id);

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _ctx.Products.ToListAsync();

    public async Task AddAsync(Product product)
    {
        _ctx.Products.Add(product);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _ctx.Products.Update(product);
        await _ctx.SaveChangesAsync();
    }
}