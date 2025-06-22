using AutoMapper;
using FluentValidation;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Core.Enums;

namespace Pizzas.Application.Services.Main;

public class PizzaService : IPizzaService
{
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IPizzaPriceRepository _pizzaPriceRepository;
    private readonly IPizzaPriceService _pizzaPriceService;
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CreatePizzaDto> _createPizzaValidator;
    private readonly IValidator<UpdatePizzaDto> _updatePizzaValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PizzaService(IPizzaRepository pizzaRepository, IIngredientRepository ingredientRepository,
        IPizzaPriceRepository pizzaPriceRepository,
        IPizzaPriceService pizzaPriceService, ICategoryService categoryService, IValidator<CreatePizzaDto> createPizzaValidator, 
        IValidator<UpdatePizzaDto> updatePizzaValidator, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _pizzaRepository = pizzaRepository;
        _ingredientRepository = ingredientRepository;
        _pizzaPriceRepository = pizzaPriceRepository;
        _pizzaPriceService = pizzaPriceService;
        _categoryService = categoryService;
        _createPizzaValidator = createPizzaValidator;
        _updatePizzaValidator = updatePizzaValidator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PizzaDto>> GetAllPizzasAsync(string targetCurrency)
    {
        var pizzas = await _pizzaRepository.GetAllAsync();
        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(pizzas);

        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);
          
            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value; 
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<PizzaDto> GetPizzaByIdAsync(string pizzaId, string targetCurrency)
    {
        var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);
        var pizzaDto = _mapper.Map<PizzaDto>(pizza);

        var (originalPrices, discountPrices, _) = 
            await _pizzaPriceService.GetConvertedPricesAsync(pizzaId, targetCurrency);

        var priceIndex = 0;
        foreach (var price in pizzaDto.Prices)
        {
            if (originalPrices.Count > priceIndex)
                price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value; 
            if (discountPrices.Count > priceIndex)
                price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

            priceIndex++;
        }

        return pizzaDto;
    }

    public async Task<IEnumerable<PizzaDto>> GetPizzasByIngredientsAsync(IEnumerable<string> ingredients, string targetCurrency)
    {
        var pizzas = await _pizzaRepository.FindAsync(p =>
            p.PizzaIngredients.Any(pi => ingredients.Contains(pi.Ingredient.Name)));
        
        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(pizzas);

        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value; 
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<decimal> GetPizzaPriceAsync(string pizzaId, PizzaSize size, string targetCurrency)
    {
        var (originalPrices, discountPrices, _) =
            await _pizzaPriceService.GetConvertedPricesAsync(pizzaId, targetCurrency);

        var originalPrice = originalPrices.GetValueOrDefault(size, 0);
        var discountPrice = discountPrices.GetValueOrDefault(size, 0);

        return discountPrice > 0 ? discountPrice : originalPrice;
    }

    public async Task<PaginatedResponse<PizzaDto>> GetPizzasPageAsync(int pageNumber, int pageSize, string targetCurrency)
    {
        var totalPizzas = await _pizzaRepository.GetAllAsync();
        
        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");
       
        int totalItems = totalPizzas.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var pagedPizzas = totalPizzas
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(pagedPizzas);
        
        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);
            
            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value; 
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }
        
        return new PaginatedResponse<PizzaDto>
        {
            Data = pizzaDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedResponse<PizzaDto>> GetPizzasByCategoryAsync(int pageNumber, int pageSize, string categoryName, string targetCurrency)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");

        var totalPizzas = await _pizzaRepository.FindAsync(p => p.Category.Name == categoryName);

        int totalItems = totalPizzas.Count;
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagedPizzas = totalPizzas
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(pagedPizzas);

        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return new PaginatedResponse<PizzaDto>
        {
            Data = pizzaDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<PizzaDto>> GetTopRatedPizzasAsync(int count, string targetCurrency)
    {
        var topRatedPizzas = await _pizzaRepository.FindAsync(p => p.Rating >= (decimal)4.5);

        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(topRatedPizzas.Take(count));

        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<IEnumerable<PizzaDto>> GetRecommendedPizzasAsync(string targetCurrency)
    {
        var recommendedPizzas = await _pizzaRepository.GetAllAsync(); 

        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(recommendedPizzas);

        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<IEnumerable<PizzaDto>> GetPopularPizzasAsync(string targetCurrency)
    {
        var pizzas = await _pizzaRepository.GetAllAsync();
        
        var popularPizzas = pizzas
            .OrderByDescending(b => b.Rating)
            .Take(6)
            .ToList();
        
        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(popularPizzas);
        
        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<IEnumerable<PizzaDto>> GetNewReleasesAsync(string targetCurrency)
    {
        var newPizzas = await _pizzaRepository.FindAsync
            (p => p.CreatedAt >= DateTime.Now.AddMonths(-1)); 

        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(newPizzas.Take(6));
        
        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<IEnumerable<PizzaDto>> SearchPizzasAsync(string searchTerm, string targetCurrency)
    {
        var pizzas = await _pizzaRepository.FindAsync(p => p.Name.Contains(searchTerm)
                                                           || p.Description.Contains(searchTerm));
        var pizzaDtos = _mapper.Map<IEnumerable<PizzaDto>>(pizzas);

        foreach (var pizzaDto in pizzaDtos)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(pizzaDto.Id, targetCurrency);

            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }

        return pizzaDtos;
    }

    public async Task<PizzaDto> CreatePizzaAsync(CreatePizzaDto createPizzaDto)
    {
        var existingPizza = await _pizzaRepository.FindAsync(p => p.Name == createPizzaDto.Name);
        if (existingPizza is not null)
            throw new PizzasException(ExceptionType.Conflict, "PizzaAlreadyExists");
        var category = await _categoryService.GetCategoryByNameAsync(createPizzaDto.CategoryName);
        await _createPizzaValidator.ValidateAndThrowAsync(createPizzaDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var pizzaEntity = _mapper.Map<PizzaEntity>(createPizzaDto);
            
            pizzaEntity.PizzaIngredients = new List<PizzaIngredientEntity>();
            pizzaEntity.CategoryId = category!.Id;

            foreach (var ingredientDto in createPizzaDto.Ingredients)
            {
                var existingIngredient = (await _ingredientRepository
                        .FindAsync(i => i.Name == ingredientDto.Name))
                    .FirstOrDefault()
                    .EnsureFound("IngredientNotFound");
                
                await _ingredientRepository.UpdateAsync([existingIngredient]);
                
                pizzaEntity.PizzaIngredients.Add(new PizzaIngredientEntity
                {
                    IngredientId = existingIngredient.Id,
                    PizzaId = pizzaEntity.Id,
                    Quantity = ingredientDto.Quantity
                });
            }

            await _pizzaRepository.AddAsync(pizzaEntity);

            return _mapper.Map<PizzaDto>(pizzaEntity);
        });
    }


    public async Task<PizzaDto> UpdatePizzaAsync(string id, UpdatePizzaDto updatePizzaDto)
    {
        var category = await _categoryService.GetCategoryByNameAsync(updatePizzaDto.Category);
        await _updatePizzaValidator.ValidateAndThrowAsync(updatePizzaDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var pizzaEntity = await _pizzaRepository.GetByIdAsync(id);

            _mapper.Map(updatePizzaDto, pizzaEntity);
            pizzaEntity.CategoryId = category!.Id;
            pizzaEntity.UpdatedAt = DateTime.UtcNow;

            pizzaEntity.PizzaIngredients.Clear();
            pizzaEntity.Prices.Clear();

            var ingredientNames = updatePizzaDto.Ingredients.Select(i => i.Name).ToList();
            var existingIngredients = await _ingredientRepository.FindAsync(i => ingredientNames.Contains(i.Name));

            foreach (var dto in updatePizzaDto.Ingredients)
            {
                var ingredient = existingIngredients.FirstOrDefault(i => i.Name == dto.Name)
                    .EnsureFound("IngredientNotFound");

                pizzaEntity.PizzaIngredients.Add(new PizzaIngredientEntity
                {
                    PizzaId = pizzaEntity.Id,
                    IngredientId = ingredient.Id,
                    Quantity = dto.Quantity
                });
            }
            
            foreach (var priceDto in updatePizzaDto.Prices)
            {
                pizzaEntity.Prices.Add(new PizzaPriceEntity
                {
                    PizzaId = pizzaEntity.Id,
                    Size = priceDto.Size.HasValue ? priceDto.Size.Value : 0,
                    OriginalPrice = priceDto.OriginalPrice.HasValue ? priceDto.OriginalPrice.Value : 0,
                    DiscountPrice = priceDto.DiscountPrice.HasValue ? priceDto.DiscountPrice.Value : 0,
                });
            }
            
            await _pizzaRepository.UpdateAsync([pizzaEntity]);

            return _mapper.Map<PizzaDto>(pizzaEntity);
        });
    }

    public async Task<bool> DeletePizzaAsync(string pizzaId)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _pizzaRepository.DeleteAsync(pizzaId);
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}