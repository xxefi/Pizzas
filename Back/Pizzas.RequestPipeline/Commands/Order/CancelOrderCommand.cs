using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Order;

public record CancelOrderCommand(string OrderId) : IRequest<OrderDto>;