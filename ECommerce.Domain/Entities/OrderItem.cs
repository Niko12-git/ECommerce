namespace ECommerce.Domain.Entities;

public class OrderItem
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public decimal Subtotal => UnitPrice * Quantity;

    public OrderItem(int productId, decimal unitPrice, int quantity)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}