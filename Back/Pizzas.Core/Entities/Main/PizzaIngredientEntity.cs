namespace Pizzas.Core.Entities.Main;

public class PizzaIngredientEntity
{
    public string PizzaId { get; set; } = string.Empty;
    public PizzaEntity Pizza { get; set; } = null!;
    public string IngredientId { get; set; } = string.Empty;
    public IngredientEntity Ingredient { get; set; } = null!;
    public decimal Quantity { get; set; }
}