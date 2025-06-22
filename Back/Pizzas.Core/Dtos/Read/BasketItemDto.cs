namespace Pizzas.Core.Dtos.Read;

public class BasketItemDto
{
    public string Id { get; set; } = string.Empty;
    public string PizzaId { get; set; } = string.Empty;
    public string PizzaName { get; set; } = string.Empty; 
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}