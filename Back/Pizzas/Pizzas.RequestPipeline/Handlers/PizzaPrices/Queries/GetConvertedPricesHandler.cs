using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Enums;
using Pizzas.RequestPipeline.Queries.PizzaPrices;

namespace Pizzas.RequestPipeline.Handlers.PizzaPrices.Queries;

public class GetConvertedPricesHandler : IRequestHandler<GetConvertedPricesQuery, 
    (Dictionary<PizzaSize, decimal>, Dictionary<PizzaSize, decimal>, string)>
{
    private readonly IPizzaPriceService _pizzaPriceService;

    public GetConvertedPricesHandler(IPizzaPriceService pizzaPriceService)
    {
        _pizzaPriceService = pizzaPriceService;
    }
    public async Task<(Dictionary<PizzaSize, decimal>, Dictionary<PizzaSize, decimal>, string)> 
        Handle(GetConvertedPricesQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaPriceService.GetConvertedPricesAsync(request.PizzaId, request.Currency);
    }
}