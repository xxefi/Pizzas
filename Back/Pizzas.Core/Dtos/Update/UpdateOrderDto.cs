using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Update;

public class UpdateOrderDto
{
    public OrderStatus? Status { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string? AddressId { get; set; }
    public ICollection<UpdateOrderItemDto>? Items { get; set; }
    public UpdateDeliveryInfoDto? DeliveryInfo { get; set; }
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}