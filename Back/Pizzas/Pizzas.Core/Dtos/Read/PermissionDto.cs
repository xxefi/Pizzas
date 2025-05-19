namespace Pizzas.Core.Dtos.Read;

public class PermissionDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<RoleDto> Roles { get; set; } = null!;
    public ICollection<UserDto> Users { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}