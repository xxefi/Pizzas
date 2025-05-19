using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Ingredient;

public record CreateIngredientCommand(CreateIngredientDto Ingredient) : IRequest<IngredientDto>;