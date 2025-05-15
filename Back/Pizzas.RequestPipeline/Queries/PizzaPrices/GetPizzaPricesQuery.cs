using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Queries.PizzaPrices;

public record GetPizzaPricesQuery(string PizzaId, PizzaSize Size, string Currency)
    : IRequest<IEnumerable<PizzaPriceDto>>;