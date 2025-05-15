using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Read;

public class PizzaPriceDto
{
    public string Id { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public string PizzaId { get; set; } = string.Empty;
}