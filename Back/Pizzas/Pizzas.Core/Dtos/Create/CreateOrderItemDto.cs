namespace Pizzas.Core.Dtos.Create;

public class CreateOrderItemDto
{
    public string PizzaId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}