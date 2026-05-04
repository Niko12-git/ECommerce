using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    Task AddAsync(Order order);
}