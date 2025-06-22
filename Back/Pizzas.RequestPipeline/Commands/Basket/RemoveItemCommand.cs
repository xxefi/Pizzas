using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Basket;

public record RemoveItemCommand(string BasketItemId, string TargetCurrency) : IRequest<BasketDto>;