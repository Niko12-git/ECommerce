namespace ECommerce.Web.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => UnitPrice * Quantity;
}

public class CartSummary
{
    public List<CartItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Subtotal);
    public int TotalItems => Items.Sum(i => i.Quantity);
}

public class CreateOrderModel
{
    public int CustomerId { get; set; }
    public List<OrderItemModel> Items { get; set; } = new();
}

public class OrderItemModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderItemDetailModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}

public class DashboardStats
{
    public int TotalUsers { get; set; }
    public int ActiveUsers { get; set; }
    public int TotalProducts { get; set; }
    public int LowStockProducts { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int PendingOrders { get; set; }
    public List<OrderResponseDto> RecentOrders { get; set; } = new();
}