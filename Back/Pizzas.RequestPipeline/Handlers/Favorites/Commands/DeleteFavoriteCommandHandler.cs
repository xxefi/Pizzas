using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Favorites;

namespace Pizzas.RequestPipeline.Handlers.Favorites.Commands;

public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, FavoriteDto>
{
    private readonly IFavoriteService _favoriteService;

    public DeleteFavoriteCommandHandler(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    public async Task<FavoriteDto> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        return await _favoriteService.RemoveFromFavoritesAsync(request.PizzaId);
    }
}