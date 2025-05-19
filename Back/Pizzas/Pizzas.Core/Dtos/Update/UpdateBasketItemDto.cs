namespace Pizzas.Core.Dtos.Update;

public class UpdateBasketItemDto
{
    public string PizzaId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}