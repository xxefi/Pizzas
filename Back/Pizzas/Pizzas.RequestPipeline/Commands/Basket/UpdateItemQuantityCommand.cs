using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Basket;

public record UpdateItemQuantityCommand(string BasketItemId, int Quantity, string TargetCurrency)
    : IRequest<BasketDto>;