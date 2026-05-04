using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IProductRepository _productRepo;

    public OrderService(IOrderRepository orderRepo, IProductRepository productRepo)
    {
        _orderRepo = orderRepo;
        _productRepo = productRepo;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order(dto.CustomerId);

        foreach (var item in dto.Items)
        {
            var product = await _productRepo.GetByIdAsync(item.ProductId)
                          ?? throw new Exception($"Producto {item.ProductId} no encontrado");

            product.ReduceStock(item.Quantity);
            order.AddItem(new OrderItem(product.Id, product.Price, item.Quantity));
        }

        await _orderRepo.AddAsync(order);
        return order;
    }
    
    public async Task<Order?> GetOrderAsync(int id)
        => await _orderRepo.GetByIdAsync(id);
    
}

