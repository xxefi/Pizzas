namespace Pizzas.Core.Dtos.Update;

public class UpdateRoleDto
{
    public string? Name { get; set; }
    public ICollection<UpdatePermissionDto>? PermissionIds { get; set; }
}