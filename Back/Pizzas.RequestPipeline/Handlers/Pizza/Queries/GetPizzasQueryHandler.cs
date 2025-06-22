using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPizzasQueryHandler : IRequestHandler<GetPizzasQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public GetPizzasQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    public async Task<IEnumerable<PizzaDto>> Handle(GetPizzasQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetAllPizzasAsync(request.TargetCurrency);
    }
}
