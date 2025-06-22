using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Entities.Main;

public class PizzaEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Rating { get; set; } 
    public string ImageUrl { get; set; } = string.Empty;
    public bool Stock { get; set; }
    public bool Top { get; set; }
    public PizzaSize Size { get; set; }
    public virtual CategoryEntity Category { get; set; } = null!;
    public virtual ICollection<PizzaPriceEntity> Prices { get; set; } = [];
    public virtual ICollection<PizzaIngredientEntity> PizzaIngredients { get; set; } = [];
    public virtual ICollection<ReviewEntity> Reviews { get; set; } = [];
    public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = []; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}