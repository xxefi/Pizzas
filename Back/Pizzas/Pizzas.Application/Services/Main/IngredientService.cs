using AutoMapper;
using FluentValidation;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Main;

public class IngredientService : IIngredientService
{
    private readonly IValidator<CreateIngredientDto> _createIngredientValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IMapper _mapper;

    public IngredientService(
        IValidator<CreateIngredientDto> createIngredientValidator,
        IUnitOfWork unitOfWork,
        IIngredientRepository ingredientRepository,
        IPizzaRepository pizzaRepository,
        IMapper mapper)
    {
        _createIngredientValidator = createIngredientValidator;
        _unitOfWork = unitOfWork;
        _ingredientRepository = ingredientRepository;
        _pizzaRepository = pizzaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
    {
        var ingredients = await _ingredientRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
    }

    public async Task<IEnumerable<IngredientDto>> GetPizzaIngredientsAsync(string pizzaId)
    {
        var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);
        var ingredients = pizza.PizzaIngredients; 

        return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
    }

    public async Task<IngredientDto> CreateIngredientAsync(CreateIngredientDto ingredientDto)
    {
        await _createIngredientValidator.ValidateAndThrowAsync(ingredientDto);
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var ingredientEntity = _mapper.Map<IngredientEntity>(ingredientDto);
            await _ingredientRepository.AddAsync(ingredientEntity);

            return _mapper.Map<IngredientDto>(ingredientEntity);
        });
    }

}