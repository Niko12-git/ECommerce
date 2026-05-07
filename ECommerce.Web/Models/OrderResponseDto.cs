namespace ECommerce.Web.Models;

public class OrderResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemDetailModel> Items { get; set; } = new();
}