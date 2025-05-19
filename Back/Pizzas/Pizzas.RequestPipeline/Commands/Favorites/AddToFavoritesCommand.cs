using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Favorites;

public record AddToFavoritesCommand(string PizzaId, string TargetCurrency) : IRequest<FavoriteDto>;