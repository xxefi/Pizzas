using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Pizzas.Core.Entities.Main;

public class OrderItemEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string OrderId { get; set; } = string.Empty;
    public string PizzaId { get; set; } = string.Empty;
    public virtual OrderEntity Order { get; set; } = null!;
    public virtual PizzaEntity Pizza { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}