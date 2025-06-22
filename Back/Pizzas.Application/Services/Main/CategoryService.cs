using AutoMapper;
using FluentValidation;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Main;

public class CategoryService(
    IMapper mapper,
    IValidator<CreateCategoryDto> createCategoryValidator,
    IValidator<UpdateCategoryDto> updateCategoryValidator,
    IPermissionService permissionService,
    ICategoryRepository categoryRepository,
    IPizzaRepository pizzaRepository,
    IUnitOfWork unitOfWork)
    : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(string id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        return mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto?> GetCategoryByNameAsync(string name)
    {
        var category = (await categoryRepository.FindAsync(c => c.Name == name))
            .FirstOrDefault()
            .EnsureFound("CategoryNotFound");
        return mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        await permissionService.EnsurePermissionAsync("category_create");
        await createCategoryValidator.ValidateAndThrowAsync(createCategoryDto);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            var category = mapper.Map<CategoryEntity>(createCategoryDto);
            await categoryRepository.AddAsync(category);
            return mapper.Map<CategoryDto>(category);
        });
    }

    public async Task<CategoryDto> UpdateCategoryAsync(string id, UpdateCategoryDto updateCategoryDto)
    {
        await permissionService.EnsurePermissionAsync("category_update");

        var existingCategory = await categoryRepository.GetByIdAsync(id);
        if (existingCategory.Name != updateCategoryDto.Name && await ExistsByNameAsync(updateCategoryDto.Name))
            throw new PizzasException(ExceptionType.CredentialsAlreadyExists, "CategoryNameAlreadyExists");
        
        await updateCategoryValidator.ValidateAndThrowAsync(updateCategoryDto);

        return await unitOfWork.StartTransactionAsync(async () =>
        {
            mapper.Map(updateCategoryDto, existingCategory);
            await categoryRepository.UpdateAsync(new[] { existingCategory });
            return mapper.Map<CategoryDto>(existingCategory);
        });
    }

    public async Task<bool> DeleteCategoryAsync(string id)
    {
        await permissionService.EnsurePermissionAsync("category_delete");

        var hasPizzas = await pizzaRepository.AnyAsync(b => b.CategoryId == id);
        if (hasPizzas)
            throw new PizzasException(ExceptionType.OperationFailed, "CategoryHasPizzas");
        
        return await unitOfWork.StartTransactionAsync(async () =>
        {
            await categoryRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<bool> ExistsByNameAsync(string name)
        => await categoryRepository.AnyAsync(c => c.Name == name);

    public async Task<int> GetCategoriesCountAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Count();
    }
    
    public async Task<IEnumerable<CategoryDto>> GetCategoriesPageAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");

        var categories = await categoryRepository.GetAllAsync();
        var pagedCategories = categories
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return mapper.Map<IEnumerable<CategoryDto>>(pagedCategories);
    }

    public async Task<int> GetCategoryPizzasCountAsync(string categoryId)
    {
        var pizzas = await pizzaRepository.FindAsync(b => b.CategoryId == categoryId);
        return pizzas.Count;
    }
}