using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Pizza;

namespace Pizzas.RequestPipeline.Handlers.Pizza.Queries;

public class GetPizzasByCategoryQueryHandler : IRequestHandler<GetPizzasByCategoryQuery, PaginatedResponse<PizzaDto>>
{
    private readonly IPizzaService _pizzaService;

    public GetPizzasByCategoryQueryHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<PaginatedResponse<PizzaDto>> Handle(GetPizzasByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _pizzaService.GetPizzasByCategoryAsync(request.PageNumber, request.PageSize, 
            request.CategoryName, request.TargetCurrency);
    }
}