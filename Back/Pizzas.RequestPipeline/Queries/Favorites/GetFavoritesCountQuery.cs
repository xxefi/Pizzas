using MediatR;

namespace Pizzas.RequestPipeline.Queries.Favorites;

public record GetFavoritesCountQuery : IRequest<int>;