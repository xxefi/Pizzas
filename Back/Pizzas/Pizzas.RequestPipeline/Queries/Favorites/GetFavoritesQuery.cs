using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Favorites;

public record GetFavoritesQuery(string TargetCurrency) : IRequest<IEnumerable<FavoriteDto>>;