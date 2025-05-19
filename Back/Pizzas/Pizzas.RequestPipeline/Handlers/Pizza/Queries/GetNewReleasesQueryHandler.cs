using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetNewReleasesQueryHandler : IRequestHandler<GetNewReleasesQuery, IEnumerable<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public GetNewReleasesQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<IEnumerable<PizzaDto>> Handle(GetNewReleasesQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetNewReleasesAsync(request.TargetCurrency);
    }
}