using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(string id);
    Task<CategoryDto?> GetCategoryByNameAsync(string name);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<CategoryDto> UpdateCategoryAsync(string id, UpdateCategoryDto updateCategoryDto);
    Task<bool> DeleteCategoryAsync(string id);
    Task<bool> ExistsByNameAsync(string name);
    Task<int> GetCategoriesCountAsync();
    Task<IEnumerable<CategoryDto>> GetCategoriesPageAsync(int pageNumber, int pageSize);
    Task<int> GetCategoryPizzasCountAsync(string categoryId);
}