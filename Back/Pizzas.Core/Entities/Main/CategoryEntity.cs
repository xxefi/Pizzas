using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Pizzas.Core.Entities.Main;

public class CategoryEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public virtual ICollection<PizzaEntity> Pizzas { get; set; } = [];
}