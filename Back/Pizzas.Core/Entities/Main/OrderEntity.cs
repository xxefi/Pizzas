using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Entities.Main;

public class OrderEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public string UserId { get; set; } = string.Empty; 
    public string AddressId { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = $"ORD-{DateTime.UtcNow.Millisecond.ToString().PadLeft(4, '0')}";
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public string TrackingNumber { get; set; } = Nanoid.Generate(size: 24);
    public string Currency { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TotalAmount { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public virtual AddressEntity Address { get; set; } = null!;
    public virtual ICollection<OrderItemEntity> Items { get; set; } = [];
    public virtual DeliveryInfoEntity DeliveryInfo { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}