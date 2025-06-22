using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using NanoidDotNet;

namespace Pizzas.Core.Entities.Main;

public class UserEntity
{
    public string Id { get; set; } = Nanoid.Generate(size: 24);
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
    public bool Verified { get; set; }
    public virtual RoleEntity Role { get; set; } = null!;
    public virtual ICollection<ReviewEntity> Reviews { get; set; } = [];
    public virtual ICollection<OrderEntity> Orders { get; set; } = [];
    public virtual ICollection<AddressEntity> Addresses { get; set; } = [];
    public virtual ICollection<DeliveryInfoEntity> DeliveryInfos { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}