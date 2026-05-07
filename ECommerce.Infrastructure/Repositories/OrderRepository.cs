using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _ctx;

    public OrderRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Order?> GetByIdAsync(int id)
        => await _ctx.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        => await _ctx.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();

    public async Task AddAsync(Order order)
    {
        _ctx.Orders.Add(order);
        await _ctx.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Order>> GetAllAsync()
        => await _ctx.Orders
            .Include(o => o.Items)
            .ToListAsync();
}