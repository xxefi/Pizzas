using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Favorites;

public record RemoveFromFavoritesCommand(string Id) : IRequest<FavoriteDto>;