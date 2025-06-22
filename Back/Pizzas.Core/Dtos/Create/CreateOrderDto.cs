using Pizzas.Core.Enums;

namespace Pizzas.Core.Dtos.Create;

public class CreateOrderDto
{
    public string AddressId { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public string Currency { get; set; } = string.Empty;
    public ICollection<CreateOrderItemDto> Items { get; set; } = [];
}