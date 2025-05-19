using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Category;

namespace Pizzas.RequestPipeline.Handlers.Category.Queries;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoryByIdAsync(request.Id);
    }
}