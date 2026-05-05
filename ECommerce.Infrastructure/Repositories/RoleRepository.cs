using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _ctx;

    public RoleRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Role?> GetByIdAsync(int id)
        => await _ctx.Roles.FindAsync(id);

    public async Task<Role?> GetByNameAsync(string name)
        => await _ctx.Roles.FirstOrDefaultAsync(r => r.Name == name);

    public async Task<IEnumerable<Role>> GetAllAsync()
        => await _ctx.Roles.ToListAsync();

    public async Task AddAsync(Role role)
    {
        _ctx.Roles.Add(role);
        await _ctx.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string name)
        => await _ctx.Roles.AnyAsync(r => r.Name == name);
}