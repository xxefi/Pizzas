namespace Pizzas.Core.Dtos.Read;

public class FavoriteDto
{
    public DateTime AddedAt { get; set; }
    public PizzaDto Pizza { get; set; } = null!;
}