using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Favorites;

namespace Pizzas.RequestPipeline.Handlers.Favorites.Commands;

public class RemoveFromFavoritesCommandHandler : IRequestHandler<RemoveFromFavoritesCommand, FavoriteDto>
{
    private readonly IFavoriteService _favoriteService;

    public RemoveFromFavoritesCommandHandler(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    
    public async Task<FavoriteDto> Handle(RemoveFromFavoritesCommand request, CancellationToken cancellationToken)
    {
        return await _favoriteService.RemoveFromFavoritesAsync(request.Id);
    }
}