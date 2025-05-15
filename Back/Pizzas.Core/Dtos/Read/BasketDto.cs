namespace Pizzas.Core.Dtos.Read;

public class BasketDto
{
    public string Id { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public ICollection<BasketItemDto> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}