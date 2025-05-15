namespace Pizzas.Core.Dtos.Read;

public class AddBasketItemDto
{
    public string PizzaId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string TargetCurrency { get; set; } = string.Empty;
}