using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IIngredientService
{
    Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
    Task<IEnumerable<IngredientDto>> GetPizzaIngredientsAsync(string pizzaId);
    Task<IngredientDto> CreateIngredientAsync(CreateIngredientDto ingredientDto);
}