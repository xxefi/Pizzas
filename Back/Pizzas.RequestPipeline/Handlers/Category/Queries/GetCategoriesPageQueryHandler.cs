using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Category;

namespace Pizzas.RequestPipeline.Handlers.Category.Queries;

public class GetCategoriesPageQueryHandler : IRequestHandler<GetCategoriesPageQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesPageQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesPageQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoriesPageAsync(request.PageNumber, request.PageSize);
    }
}