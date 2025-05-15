using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.PizzaPrices;

public record UpdatePizzaPricesCommand(string PizzaId, IEnumerable<UpdatePizzaPriceDto> Prices)
    : IRequest<PizzaPriceDto>;