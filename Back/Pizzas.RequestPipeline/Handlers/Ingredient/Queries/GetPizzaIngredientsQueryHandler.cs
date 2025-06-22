using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Ingredient;

namespace Pizzas.RequestPipeline.Handlers.Ingredient.Queries;

public class GetPizzaIngredientsQueryHandler : IRequestHandler<GetPizzaIngredientsQuery, IEnumerable<IngredientDto>>
{
    private readonly IIngredientService _ingredientService;

    public GetPizzaIngredientsQueryHandler(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    public async Task<IEnumerable<IngredientDto>> Handle(GetPizzaIngredientsQuery request, CancellationToken cancellationToken)
    {
        return await _ingredientService.GetPizzaIngredientsAsync(request.PizzaId);
    }
}