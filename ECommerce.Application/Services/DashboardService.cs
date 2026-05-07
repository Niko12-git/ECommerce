using ECommerce.Application.DTOs;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class DashboardService
{
    private readonly IProductRepository _productRepo;
    private readonly IUserRepository _userRepo;
    private readonly IOrderRepository _orderRepo;

    public DashboardService(
        IProductRepository productRepo,
        IUserRepository userRepo,
        IOrderRepository orderRepo)
    {
        _productRepo = productRepo;
        _userRepo = userRepo;
        _orderRepo = orderRepo;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var products = await _productRepo.GetAllAsync();
        var users = await _userRepo.GetAllAsync();
        var orders = await _orderRepo.GetAllAsync();

        var ordersList = orders.ToList();
        var usersList = users.ToList();
        var productsList = products.ToList();

        var recentOrders = ordersList
            .OrderByDescending(o => o.CreatedAt)
            .Take(5)
            .Select(o => new RecentOrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                Status = o.Status.ToString(),
                Total = o.Total,
                CreatedAt = o.CreatedAt
            }).ToList();

        return new DashboardDto
        {
            TotalProducts = productsList.Count,
            TotalUsers = usersList.Count,
            TotalOrders = ordersList.Count,
            TotalRevenue = ordersList.Sum(o => o.Total),
            ActiveUsers = usersList.Count(u => u.IsActive),
            PendingOrders = ordersList.Count(o => o.Status == OrderStatus.Pending),
            RecentOrders = recentOrders
        };
    }
}