using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Commands.Order;

public record UpdateOrderStatusCommand(string OrderId, OrderStatus NewStatus) : IRequest<OrderDto>;