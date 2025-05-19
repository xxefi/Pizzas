namespace Pizzas.Core.Dtos.Update;

public class UpdateBasketDto
{
    public ICollection<UpdateBasketItemDto> Items { get; set; } = [];
}