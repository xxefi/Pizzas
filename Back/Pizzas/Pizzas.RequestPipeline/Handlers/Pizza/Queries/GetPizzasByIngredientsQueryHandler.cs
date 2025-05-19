using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPizzasByIngredientsQueryHandler : IRequestHandler
    <GetPizzasByIngredientsQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public GetPizzasByIngredientsQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    
    public async Task<IEnumerable<PizzaDto>> Handle(GetPizzasByIngredientsQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetPizzasByIngredientsAsync(request.Ingredients, request.Currency);
    }
}