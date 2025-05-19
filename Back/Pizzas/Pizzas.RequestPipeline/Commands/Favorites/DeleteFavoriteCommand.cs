using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Favorites;

public record DeleteFavoriteCommand(string PizzaId) : IRequest<FavoriteDto>;