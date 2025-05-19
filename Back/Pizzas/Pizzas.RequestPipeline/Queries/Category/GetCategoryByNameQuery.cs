using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Category;

public record GetCategoryByNameQuery(string Name) : IRequest<CategoryDto>;