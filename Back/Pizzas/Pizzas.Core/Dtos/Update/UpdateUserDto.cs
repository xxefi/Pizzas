namespace Pizzas.Core.Dtos.Update;

public class UpdateUserDto
{
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool Verified { get; set; }
}