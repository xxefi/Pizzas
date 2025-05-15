using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPopularPizzasQueryHandler : IRequestHandler<GetPopularPizzasQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public GetPopularPizzasQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<IEnumerable<PizzaDto>> Handle(GetPopularPizzasQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetPopularPizzasAsync(request.TargetCurrency);
    }
}