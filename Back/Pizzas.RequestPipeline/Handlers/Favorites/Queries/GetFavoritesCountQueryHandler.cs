using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Favorites;

namespace Pizzas.RequestPipeline.Handlers.Favorites.Queries;

public class GetFavoritesCountQueryHandler : IRequestHandler<GetFavoritesCountQuery, int>
{
    private readonly IFavoriteService _favoriteService;

    public GetFavoritesCountQueryHandler(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    public async Task<int> Handle(GetFavoritesCountQuery request, CancellationToken cancellationToken)
    {
        return await _favoriteService.GetFavoritesCountAsync();
    }
}