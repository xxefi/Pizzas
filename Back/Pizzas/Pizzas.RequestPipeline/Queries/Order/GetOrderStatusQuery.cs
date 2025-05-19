using MediatR;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Queries.Order;

public record GetOrderStatusQuery(string OrderId) : IRequest<OrderStatus>;