using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Create;

public class CreatePizzaPriceDto
{
    public PizzaSize Size { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public string PizzaId { get; set; } = string.Empty;
}