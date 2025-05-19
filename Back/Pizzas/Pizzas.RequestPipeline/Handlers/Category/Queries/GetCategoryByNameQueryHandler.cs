using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Category;

namespace Pizzas.RequestPipeline.Handlers.Category.Queries;

public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, CategoryDto>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryByNameQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<CategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoryByNameAsync(request.Name);
    }
}