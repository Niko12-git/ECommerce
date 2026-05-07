namespace ECommerce.Web.Models;

public class DashboardModel
{
    public int TotalProducts { get; set; }
    public int TotalUsers { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int ActiveUsers { get; set; }
    public int PendingOrders { get; set; }
    public List<RecentOrderModel> RecentOrders { get; set; } = new();
}

public class RecentOrderModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}