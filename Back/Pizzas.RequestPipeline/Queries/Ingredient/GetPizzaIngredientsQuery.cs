using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Ingredient;

public record GetPizzaIngredientsQuery(string PizzaId) : IRequest<IEnumerable<IngredientDto>>;