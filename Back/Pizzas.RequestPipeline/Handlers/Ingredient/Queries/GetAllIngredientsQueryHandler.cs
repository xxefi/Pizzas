using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Ingredient;

namespace Pizzas.RequestPipeline.Handlers.Ingredient.Queries;

public class GetAllIngredientsQueryHandler : IRequestHandler<GetAllIngredientsQuery, IEnumerable<IngredientDto>>
{
    private readonly IIngredientService _ingredientService;

    public GetAllIngredientsQueryHandler(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
    {
        return await _ingredientService.GetAllIngredientsAsync();
    }
}