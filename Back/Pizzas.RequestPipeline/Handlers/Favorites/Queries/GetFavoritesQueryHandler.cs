using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Favorites;

namespace Pizzas.RequestPipeline.Handlers.Favorites.Queries;

public class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, IEnumerable<FavoriteDto>>
{
    private readonly IFavoriteService _favoriteService;

    public GetFavoritesQueryHandler(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    public async Task<IEnumerable<FavoriteDto>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        return await _favoriteService.GetFavoritesAsync(request.TargetCurrency);
    }
}