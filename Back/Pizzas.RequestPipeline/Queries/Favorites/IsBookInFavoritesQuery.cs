using MediatR;

namespace Pizzas.RequestPipeline.Queries.Favorites;

public record IsBookInFavoritesQuery(Guid BookId) : IRequest<bool>;