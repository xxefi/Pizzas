using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.PizzaPrices;

namespace Pizzas.RequestPipeline.Handlers.PizzaPrices.Queries;

public class GetPizzaPricesHandler : IRequestHandler<GetPizzaPricesQuery, IEnumerable<PizzaPriceDto>>
{
    private readonly IPizzaPriceService _pizzaPriceService;

    public GetPizzaPricesHandler(IPizzaPriceService pizzaPriceService)
    {
        _pizzaPriceService = pizzaPriceService;
    }
    public async Task<IEnumerable<PizzaPriceDto>> Handle(GetPizzaPricesQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaPriceService.GetPriceAsync(request.PizzaId, request.Size, request.Currency);
    }
}