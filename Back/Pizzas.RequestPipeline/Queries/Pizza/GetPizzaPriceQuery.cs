using MediatR;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPizzaPriceQuery(string Id, PizzaSize Size, string Currency) : IRequest<decimal>;