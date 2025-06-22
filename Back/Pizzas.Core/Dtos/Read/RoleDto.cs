namespace Pizzas.Core.Dtos.Read;

public class RoleDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<PermissionDto> Permissions { get; set; } = [];
}