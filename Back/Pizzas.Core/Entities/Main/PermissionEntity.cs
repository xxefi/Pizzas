using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Pizzas.Core.Entities.Main;

public class PermissionEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public virtual ICollection<RoleEntity> Roles { get; set; } = [];
    public virtual ICollection<UserEntity> Users { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}