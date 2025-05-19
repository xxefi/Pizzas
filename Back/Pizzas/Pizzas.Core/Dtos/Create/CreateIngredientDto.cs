namespace Pizzas.Core.Dtos.Create;

public class CreateIngredientDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}