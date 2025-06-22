using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Favorites;

namespace Pizzas.RequestPipeline.Handlers.Favorites.Queries;

public class GetFavoritesPageQueryHandler : IRequestHandler<GetFavoritesPageQuery, PaginatedResponse<FavoriteDto>>
{
    private readonly IFavoriteService _favoriteService;

    public GetFavoritesPageQueryHandler(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    public async Task<PaginatedResponse<FavoriteDto>> Handle(GetFavoritesPageQuery request, CancellationToken cancellationToken)
    {
        return await _favoriteService.GetFavoritesPageAsync(request.PageNumber, request.PageSize, request.TargetCurrency);
    }
}