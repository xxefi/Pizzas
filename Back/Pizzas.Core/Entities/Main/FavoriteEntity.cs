using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Pizzas.Core.Entities.Main;

public class FavoriteEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string UserId { get; set; } = string.Empty;
    public string PizzaId { get; set; } = string.Empty;
    public virtual UserEntity User { get; set; } = null!;
    public virtual PizzaEntity Pizza { get; set; } = null!;
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RemovedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}