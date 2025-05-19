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

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IMapper mapper,
        IValidator<CreateCategoryDto> createCategoryValidator,
        IValidator<UpdateCategoryDto> updateCategoryValidator,
        ICategoryRepository categoryRepository,
        IPizzaRepository pizzaRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryValidator = updateCategoryValidator;
        _categoryRepository = categoryRepository;
        _pizzaRepository = pizzaRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(string id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto?> GetCategoryByNameAsync(string name)
    {
        var category = (await _categoryRepository.FindAsync(c => c.Name == name))
            .FirstOrDefault()
            .EnsureFound("CategoryNotFound");
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        await _createCategoryValidator.ValidateAndThrowAsync(createCategoryDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var category = _mapper.Map<CategoryEntity>(createCategoryDto);
            await _categoryRepository.AddAsync(category);
            
            return _mapper.Map<CategoryDto>(category);
        });
    }

    public async Task<CategoryDto> UpdateCategoryAsync(string id, UpdateCategoryDto updateCategoryDto)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        
        if (existingCategory.Name != updateCategoryDto.Name && await ExistsByNameAsync(updateCategoryDto.Name))
            throw new PizzasException(ExceptionType.CredentialsAlreadyExists, "CategoryNameAlreadyExists");
        
        await _updateCategoryValidator.ValidateAndThrowAsync(updateCategoryDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateCategoryDto, existingCategory);
            await _categoryRepository.UpdateAsync(new[] { existingCategory });
            
            return _mapper.Map<CategoryDto>(existingCategory);
        });
    }

    public async Task<bool> DeleteCategoryAsync(string id)
    {
        var hasPizzas = await _pizzaRepository.AnyAsync(b => b.CategoryId == id);
        if (hasPizzas)
            throw new PizzasException(ExceptionType.OperationFailed, "CategoryHasPizzas");
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _categoryRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<bool> ExistsByNameAsync(string name)
        => await _categoryRepository.AnyAsync(c => c.Name == name);

    public async Task<int> GetCategoriesCountAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Count();
    }
    
    public async Task<IEnumerable<CategoryDto>> GetCategoriesPageAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");

        var categories = await _categoryRepository.GetAllAsync();
        var pagedCategories = categories
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return _mapper.Map<IEnumerable<CategoryDto>>(pagedCategories);
    }

    public async Task<int> GetCategoryPizzasCountAsync(string categoryId)
    {
        var books = await _pizzaRepository.FindAsync(b => b.CategoryId == categoryId);
        return books.Count;
    }
}