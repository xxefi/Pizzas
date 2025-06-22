using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;
using Pizzas.Core.Enums;

namespace Pizzas.Application.Services.Main;

public class BasketService : IBasketService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBasketRepository _basketRepository;
    private readonly IBasketItemRepository _basketItemRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IValidator<CreateBasketDto> _createBasketValidator;
    private readonly IValidator<UpdateBasketDto> _updateBasketValidator;
    private readonly ICurrencyService _currencyService;
    private readonly IPizzaPriceService _pizzaPriceService;
    private readonly IUnitOfWork _unitOfWork;

    public BasketService(IMapper mapper, IHttpContextAccessor httpContextAccessor, 
        IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, 
        IPizzaRepository pizzaRepository,
        IValidator<CreateBasketDto> createBasketValidator,
        IValidator<UpdateBasketDto> updateBasketValidator,
        ICurrencyService currencyService,
        IPizzaPriceService pizzaPriceService, 
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _basketRepository = basketRepository;
        _basketItemRepository = basketItemRepository;
        _pizzaRepository = pizzaRepository;
        _createBasketValidator = createBasketValidator;
        _updateBasketValidator = updateBasketValidator;
        _currencyService = currencyService;
        _pizzaPriceService = pizzaPriceService;
        _unitOfWork = unitOfWork;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    
    public async Task<BasketDto> GetBasketAsync(string targetCurrency)
    {
        var userId = GetUserId();
        var basket = (await _basketRepository.FindAsync(c => c.UserId == userId)).FirstOrDefault();

        if (basket is null)
        {
            basket = new BasketEntity { UserId = userId };
            await _basketRepository.AddAsync(basket);
        }

        var defaultPizzaSize = PizzaSize.Small;

        if (basket.Items is not null && basket.Items.Count > 0)
        {
            foreach (var item in basket.Items)
            {
                var (originalPrices, discountPrices, _) =
                    await _pizzaPriceService.GetConvertedPricesAsync(item.PizzaId, targetCurrency);

                var originalPrice = originalPrices.GetValueOrDefault(defaultPizzaSize, 0);
                var discountPrice = discountPrices.GetValueOrDefault(defaultPizzaSize, 0);

                item.Price = discountPrice > 0 ? discountPrice : originalPrice;
            }
        }

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> AddItemAsync(string pizzaId, int quantity, string targetCurrency, PizzaSize size)
    {
        var userId = GetUserId();
        
        var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);
        
        if (!pizza.Stock)
            throw new PizzasException(ExceptionType.InvalidRequest, "PizzaNotInStock");

        var existingItem = await _basketItemRepository.FindAsync(ci =>
            ci.PizzaId == pizzaId && ci.Size == size && ci.Basket.UserId == userId);
        if (existingItem.Any())
            throw new PizzasException(ExceptionType.InvalidRequest, "PizzaAlreadyInBasket");
        
        
        var (originalPrices, discountPrices, _) 
            = await _pizzaPriceService.GetConvertedPricesAsync(pizzaId, targetCurrency);
        
      
        var originalPrice = originalPrices.GetValueOrDefault(size, 0);
        var discountPrice = discountPrices.GetValueOrDefault(size, 0);
        var price = discountPrice > 0 ? discountPrice : originalPrice;
        
        var basket = (await _basketRepository.FindAsync(c => c.UserId == userId))
            .FirstOrDefault();

        if (basket is null)
        {
            basket = new BasketEntity { UserId = userId };
            await _basketRepository.AddAsync(basket);
        }
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var basketItem = new BasketItemEntity
            {
                BasketId = basket.Id,
                PizzaId = pizza.Id,
                Quantity = quantity,
                Price = price,
                Size = size
            };
            
            await _basketItemRepository.AddAsync(basketItem);
            return _mapper.Map<BasketDto>(basket);
        });
    }

    public async Task<BasketDto> UpdateItemQuantityAsync(string basketItemId, int quantity, string targetCurrency)
    {
        var userId = GetUserId();
        var basketItem = await _basketItemRepository.GetByIdAsync(basketItemId);
        

        var (originalPrices, discountPrices, _) =
            await _pizzaPriceService.GetConvertedPricesAsync(basketItem.PizzaId, targetCurrency);
        
        var defaultPizzaSize = PizzaSize.Small; 
        var originalPrice = originalPrices.GetValueOrDefault(defaultPizzaSize, 0);
        var discountPrice = discountPrices.GetValueOrDefault(defaultPizzaSize, 0);
        
        var price = discountPrice > 0 ? discountPrice : originalPrice;

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            basketItem.Quantity = quantity;
            basketItem.Price = price;
            await _basketItemRepository.UpdateAsync(new[] { basketItem });

            var basket = (await _basketRepository.FindAsync(c => c.UserId == userId)).FirstOrDefault();
            basket.Items = basket.Items.OrderBy(item => item.CreatedAt).ToList();

            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<BasketDto>(basket);
        });
    }

    public async Task<BasketDto> RemoveItemAsync(string basketItemId, string targetCurrency)
    {
        var userId = GetUserId();
        await _basketItemRepository.FindAsync(
            b => b.Id == basketItemId && b.Basket.UserId == userId)
            .EnsureFound("BasketItemNotFound");
        
        var basket = (await _basketRepository.FindAsync(c => c.UserId == userId))
            .FirstOrDefault()
            .EnsureFound("BasketNotFound");
            
        foreach (var item in basket.Items)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(item.PizzaId, targetCurrency);
            var defaultPizzaSize = PizzaSize.Small;
            var originalPrice = originalPrices.GetValueOrDefault(defaultPizzaSize, 0);
            var discountPrice = discountPrices.GetValueOrDefault(defaultPizzaSize, 0);
        
            item.Price = discountPrice > 0 ? discountPrice : originalPrice;
        }
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _basketItemRepository.DeleteAsync(basketItemId);
            return _mapper.Map<BasketDto>(basket);
        });
    }

    public async Task<bool> ClearBasketAsync(string targetCurrency)
    {
        var userId = GetUserId();
        var basket = (await _basketRepository.FindAsync(c => c.UserId == userId)).FirstOrDefault();

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            foreach (var item in basket.Items)
            {
                var (originalPrices, discountPrices, _) =
                    await _pizzaPriceService.GetConvertedPricesAsync(item.PizzaId, targetCurrency);
                
                var defaultPizzaSize = PizzaSize.Small; 
                var originalPrice = originalPrices.GetValueOrDefault(defaultPizzaSize, 0);
                var discountPrice = discountPrices.GetValueOrDefault(defaultPizzaSize, 0);
            
                item.Price = discountPrice > 0 ? discountPrice : originalPrice;
                await _basketItemRepository.DeleteAsync(item.Id);
            }
            
            return true;
        });
    }

    public async Task<int> GetItemsCountAsync()
    {
        var userId = GetUserId();
        var basket = (await _basketRepository.FindAsync(c => c.UserId == userId))
            .FirstOrDefault();
        return basket.Items.Count;
    }

    public async Task<decimal> GetTotalAsync(string targetCurrency)
    {
        var userId = GetUserId();
        var basket = (await _basketRepository.FindAsync(c => c.UserId == userId))
            .FirstOrDefault();

        decimal totalPrice = 0;
        foreach (var item in basket.Items)
        {
            var (originalPrices, discountPrices, _) = 
                await _pizzaPriceService.GetConvertedPricesAsync(item.PizzaId, targetCurrency);
        
            var defaultPizzaSize = PizzaSize.Small; 
            var originalPrice = originalPrices.GetValueOrDefault(defaultPizzaSize, 0);
            var discountPrice = discountPrices.GetValueOrDefault(defaultPizzaSize, 0);

            var price = discountPrice > 0 ? discountPrice : originalPrice;
            totalPrice += price * item.Quantity;
        }

        return totalPrice;
    }
}