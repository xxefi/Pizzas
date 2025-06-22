using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Ingredient;

namespace Pizzas.RequestPipeline.Handlers.Ingredient.Commands;

public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, IngredientDto>
{
    private readonly IIngredientService _ingredientService;

    public CreateIngredientCommandHandler(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }
    public async Task<IngredientDto> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        return await _ingredientService.CreateIngredientAsync(request.Ingredient);
    }
}