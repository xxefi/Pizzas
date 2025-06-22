using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto createDto);
    Task<OrderDto> GetOrderByIdAsync(string orderId);
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync();
    Task<OrderDto> CancelOrderAsync(string orderId);
    Task<OrderStatus> GetOrderStatusAsync(string orderId);
    Task<OrderDto> UpdateOrderStatusAsync(string orderId, OrderStatus newStatus);
}