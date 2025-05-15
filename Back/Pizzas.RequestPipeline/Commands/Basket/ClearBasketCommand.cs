using MediatR;

namespace Pizzas.RequestPipeline.Commands.Basket;

public record ClearBasketCommand(string TargetCurrency) : IRequest<bool>;