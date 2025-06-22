namespace Pizzas.Core.Dtos.Create;

public class CreateRoleDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = []; 
}