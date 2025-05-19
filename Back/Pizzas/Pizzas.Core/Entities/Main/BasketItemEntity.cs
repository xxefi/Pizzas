using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Entities.Main;

public class BasketItemEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public string PizzaId { get; set; } = string.Empty;
    public string BasketId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public PizzaSize Size { get; set; }
    public virtual PizzaEntity Pizza { get; set; } = null!;
    public virtual BasketEntity Basket { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}