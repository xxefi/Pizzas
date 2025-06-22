using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Order;

public record GetOrdersQuery : IRequest<IEnumerable<OrderDto>>;