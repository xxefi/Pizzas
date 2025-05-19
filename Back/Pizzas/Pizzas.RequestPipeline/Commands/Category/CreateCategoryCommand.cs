using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Category;

public record CreateCategoryCommand(CreateCategoryDto Category) : IRequest<CategoryDto>;