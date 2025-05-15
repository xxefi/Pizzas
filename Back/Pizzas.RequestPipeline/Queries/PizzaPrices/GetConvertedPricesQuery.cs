using MediatR;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Queries.PizzaPrices;

public record GetConvertedPricesQuery(string PizzaId, string Currency) 
    : IRequest<(Dictionary<PizzaSize, decimal>, Dictionary<PizzaSize, decimal>, string)>;