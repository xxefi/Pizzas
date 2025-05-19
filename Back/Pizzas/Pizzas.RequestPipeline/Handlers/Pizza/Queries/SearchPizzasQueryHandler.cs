using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class SearchPizzasQueryHandler : IRequestHandler<SearchPizzasQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public SearchPizzasQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<IEnumerable<PizzaDto>> Handle(SearchPizzasQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.SearchPizzasAsync(request.SearchTerm, request.TargetCurrency);
    }
}