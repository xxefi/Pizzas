using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Order;

public record GetUserOrdersQuery : IRequest<IEnumerable<OrderDto>>;