using MediatR;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Favorites;

public record GetFavoritesPageQuery(int PageNumber, int PageSize, string TargetCurrency) : IRequest<PaginatedResponse<FavoriteDto>>;