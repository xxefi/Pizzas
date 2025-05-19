using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;

namespace Pizzas.Core.Entities.Main;

public class BasketEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public string UserId { get; set; } = string.Empty;
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    public virtual ICollection<BasketItemEntity> Items { get; set; } = [];
    public virtual UserEntity User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}