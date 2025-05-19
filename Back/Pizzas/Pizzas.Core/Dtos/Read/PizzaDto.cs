using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Read;

public class PizzaDto
{
    public string Id { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Rating { get; set; } = 5;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Stock { get; set; }
    public bool Top { get; set; }
    public string Size { get; set; } = string.Empty;
    public ICollection<PizzaPriceDto> Prices { get; set; } = [];
    public ICollection<IngredientDto> Ingredients { get; set; } = [];
}