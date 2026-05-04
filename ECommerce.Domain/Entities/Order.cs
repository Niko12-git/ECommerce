using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public decimal Total => _items.Sum(i => i.Subtotal);

    public Order(int customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Solo se puede confirmar una orden pendiente");
        Status = OrderStatus.Confirmed;
    }
}