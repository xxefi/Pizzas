using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Ingredient;

public record GetAllIngredientsQuery : IRequest<IEnumerable<IngredientDto>>;