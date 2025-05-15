namespace Pizzas.Core.Dtos.Create;

public class CreateBasketItemDto
{
    public string PizzaId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; } = string.Empty;
}