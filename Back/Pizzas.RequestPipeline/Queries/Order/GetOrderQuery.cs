using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Order;

public record GetOrderQuery(string OrderId) : IRequest<OrderDto>;