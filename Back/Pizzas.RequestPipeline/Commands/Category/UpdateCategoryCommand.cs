using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Category;

public record UpdateCategoryCommand(string Id, UpdateCategoryDto Category) : IRequest<CategoryDto>;