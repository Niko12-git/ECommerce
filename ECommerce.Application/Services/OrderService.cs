using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Application.DTOs;
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
    
    public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        => await _orderRepo.GetByCustomerIdAsync(customerId);
    
    public async Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerDtoAsync(int customerId)
    {
        var orders = await _orderRepo.GetByCustomerIdAsync(customerId);
        return await MapOrdersToDtoAsync(orders);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersDtoAsync()
    {
        var orders = await _orderRepo.GetAllAsync();
        return await MapOrdersToDtoAsync(orders);
    }

    private async Task<IEnumerable<OrderResponseDto>> MapOrdersToDtoAsync(
        IEnumerable<ECommerce.Domain.Entities.Order> orders)
    {
        var result = new List<OrderResponseDto>();

        foreach (var order in orders)
        {
            var items = new List<OrderItemResponseDto>();

            foreach (var item in order.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                items.Add(new OrderItemResponseDto
                {
                    ProductId = item.ProductId,
                    ProductName = product?.Name ?? $"Producto #{item.ProductId}",
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Subtotal = item.Subtotal
                });
            }

            result.Add(new OrderResponseDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Total = order.Total,
                Items = items
            });
        }

        return result;
    }
}
