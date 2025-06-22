using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Update;

public class UpdatePizzaPriceDto
{
    public PizzaSize? Size { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
}