using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Create;

public class CreatePizzaDto
{
    public string CategoryName = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Rating { get; set; } = 5;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Stock { get; set; }
    public bool Top { get; set; }
    public PizzaSize Size { get; set; }
    public ICollection<CreateIngredientDto> Ingredients { get; set; } = [];
    public ICollection<CreatePizzaPriceDto> Prices { get; set; } = [];
}