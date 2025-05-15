using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPizzasPageQueryHandler : IRequestHandler<GetPizzasPageQuery, PaginatedResponse<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public GetPizzasPageQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    
    public async Task<PaginatedResponse<PizzaDto>> Handle(GetPizzasPageQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetPizzasPageAsync(request.PageNumber, request.PageSize, request.TargetCurrency);
    }
}