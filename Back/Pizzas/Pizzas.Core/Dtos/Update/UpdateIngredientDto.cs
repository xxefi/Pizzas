namespace Pizzas.Core.Dtos.Update;

public class UpdateIngredientDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}