using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Category;

namespace Pizzas.RequestPipeline.Handlers.Category.Queries;

public class GetCategoryPizzasCountQueryHandler : IRequestHandler<GetCategoryPizzasCountQuery, int>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryPizzasCountQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<int> Handle(GetCategoryPizzasCountQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoryPizzasCountAsync(request.CategoryId);
    }
}