using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPizzaPriceQueryHandler : IRequestHandler<GetPizzaPriceQuery, decimal>
{
    private readonly IPizzaService _pizzaService;

    public GetPizzaPriceQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<decimal> Handle(GetPizzaPriceQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetPizzaPriceAsync(request.Id, request.Size, request.Currency);
    }
}