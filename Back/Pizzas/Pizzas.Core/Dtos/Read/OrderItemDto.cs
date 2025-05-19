namespace Pizzas.Core.Dtos.Read;

public class OrderItemDto
{
    public string Id { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string PizzaId { get; set; } = string.Empty;
    public PizzaDto Pizza { get; set; } = new PizzaDto();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}