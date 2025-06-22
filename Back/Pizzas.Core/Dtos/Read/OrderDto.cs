using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Read;

public class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string TrackingNumber { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TotalAmount { get; set; }
    public DeliveryInfoDto DeliveryInfo { get; set; } = new DeliveryInfoDto();
    public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}