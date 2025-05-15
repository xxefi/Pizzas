using MediatR;

namespace Pizzas.RequestPipeline.Queries.Basket;

public record GetItemsCountQuery : IRequest<int>;