using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Category;

public record GetCategoryQuery(string Id) : IRequest<CategoryDto>;