using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task<bool> ExistsAsync(string name); 
}