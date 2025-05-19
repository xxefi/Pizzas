using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;

namespace Pizzas.Core.Entities.Main;

public class IngredientEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public virtual ICollection<PizzaIngredientEntity> PizzaIngredients { get; set; } = [];
}