using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPizzaQueryHandler : IRequestHandler<GetPizzaQuery, PizzaDto>
{
    private readonly IPizzaService _pizzaService;

    public GetPizzaQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<PizzaDto> Handle(GetPizzaQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetPizzaByIdAsync(request.Id, request.TargetCurrency);
    }
}