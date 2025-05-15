using MediatR;

namespace Pizzas.RequestPipeline.Commands.Category;

public record DeleteCategoryCommand(string Id) : IRequest<bool>;