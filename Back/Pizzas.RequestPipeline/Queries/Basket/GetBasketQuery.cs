using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Basket;

public record GetBasketQuery(string TargetCurrency) : IRequest<BasketDto>;