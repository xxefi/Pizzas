using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Category;

public record GetCategoriesPageQuery(int PageNumber, int PageSize) : IRequest<IEnumerable<CategoryDto>>;