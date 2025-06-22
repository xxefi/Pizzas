using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Favorites;

namespace Pizzas.RequestPipeline.Handlers.Favorites.Commands;

public class AddToFavoritesCommandHandler : IRequestHandler<AddToFavoritesCommand, FavoriteDto>
{
    private readonly IFavoriteService _favoriteService;

    public AddToFavoritesCommandHandler(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    public async Task<FavoriteDto> Handle(AddToFavoritesCommand request, CancellationToken cancellationToken)
    {
        return await _favoriteService.AddToFavoritesAsync(request.PizzaId, request.TargetCurrency);
    }
}