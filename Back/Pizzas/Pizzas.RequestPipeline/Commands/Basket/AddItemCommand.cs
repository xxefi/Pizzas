using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Commands.Basket;

public record AddItemCommand(string PizzaId, int Quantity, string TargetCurrency, PizzaSize Size) : IRequest<BasketDto>;