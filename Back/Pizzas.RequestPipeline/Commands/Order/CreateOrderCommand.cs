using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Order;

public record CreateOrderCommand(CreateOrderDto Order) : IRequest<OrderDto>;