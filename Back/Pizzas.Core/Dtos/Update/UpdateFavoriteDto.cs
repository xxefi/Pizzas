namespace Pizzas.Core.Dtos.Update;

public class UpdateFavoriteDto
{
    public Guid PizzaId { get; set; }
    public bool IsInFavorite { get; set; }
}