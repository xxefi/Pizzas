namespace Pizzas.Core.Dtos.Create;

public class CreateBasketDto
{
    public ICollection<CreateBasketItemDto> Items { get; set; } = [];
}