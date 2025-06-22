using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Update;

public class UpdatePizzaDto
{
    public string? Category { get; set; }
    public string? Name { get; set; } 
    public string? Description { get; set; }
    public decimal? Rating { get; set; }
    public string? ImageUrl { get; set; }
    public bool? Stock { get; set; }
    public bool? Top { get; set; }
    public PizzaSize? Size { get; set; }
    public ICollection<UpdateIngredientDto>? Ingredients { get; set; } = [];
    public ICollection<UpdatePizzaPriceDto>? Prices { get; set; }
}