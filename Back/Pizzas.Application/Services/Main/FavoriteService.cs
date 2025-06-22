using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Main;

public class FavoriteService : IFavoriteService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IPizzaPriceService _pizzaPriceService;
    private readonly IUnitOfWork _unitOfWork;

    public FavoriteService(IMapper mapper, IHttpContextAccessor httpContextAccessor, 
        IFavoriteRepository favoriteRepository, IPizzaRepository pizzaRepository,
        IPizzaPriceService pizzaPriceService, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _favoriteRepository = favoriteRepository;
        _pizzaRepository = pizzaRepository;
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


    public async Task<FavoriteDto> AddToFavoritesAsync(string pizzaId, string targetCurrency)
    {
        var userId = GetUserId();
        var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);
        
        var existingFavorite = await _favoriteRepository.FindAsync(f =>
            f.UserId == userId && f.PizzaId == pizzaId);

        if (existingFavorite.Any())
            throw new PizzasException(ExceptionType.Conflict, "AlreadyInFavorites");
        
        var (originalPrices, discountPrices, _) =
            await _pizzaPriceService.GetConvertedPricesAsync(pizzaId, targetCurrency);

        var priceIndex = 0;
        
        foreach (var price in pizza.Prices)
        {
            if (originalPrices.Count > priceIndex)
                price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
            if (discountPrices.Count > priceIndex)
                price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

            priceIndex++;
        }
        var favorite = _mapper.Map<FavoriteEntity>(pizza);
        favorite.UserId = userId;
        favorite.PizzaId = pizzaId;

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _favoriteRepository.AddAsync(favorite);

            return _mapper.Map<FavoriteDto>(favorite);
        });
    }

    public async Task<FavoriteDto> RemoveFromFavoritesAsync(string pizzaId)
    {
        var userId = GetUserId();
        var favorite = await _favoriteRepository.FindAsync(f
            => f.UserId == userId && f.PizzaId == pizzaId);
        
        if (!favorite.Any())
            throw new PizzasException(ExceptionType.NotFound, "NotInFavorites");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _favoriteRepository.DeleteAsync(favorite.First().Id);
            return _mapper.Map<FavoriteDto>(favorite.First());
        });
    }

    public async Task<IEnumerable<FavoriteDto>> GetFavoritesAsync(string targetCurrency)
    {
        var userId = GetUserId();
        var favorites = await _favoriteRepository.FindAsync(f =>
            f.UserId == userId);
        var favoriteDtos = new List<FavoriteDto>();

        foreach (var favorite in favorites)
        {
            var (originalPrices, discountPrices, _) =
                await _pizzaPriceService.GetConvertedPricesAsync(favorite.PizzaId, targetCurrency);
            
            var favoriteDto = _mapper.Map<FavoriteDto>(favorite);
            var pizzaDto = favoriteDto.Pizza;
            
            var priceIndex = 0;
            foreach (var price in pizzaDto.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }

            favoriteDtos.Add(favoriteDto);
        }
        return favoriteDtos;
    }

    public async Task<int> GetFavoritesCountAsync()
    {
        var userId = GetUserId();
        var favorite = await _favoriteRepository.FindAsync(f => f.UserId == userId);
        
        return favorite.Count;
    }

    public async Task<PaginatedResponse<FavoriteDto>> GetFavoritesPageAsync(int pageNumber, int pageSize, string targetCurrency)
    {
        var userId = GetUserId();
        
        var totalFavorites = await _favoriteRepository.FindAsync(f => f.UserId == userId);

        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");

        int totalItems = totalFavorites.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        
        var pagedFavorites = totalFavorites
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var favoriteDtos = _mapper.Map<IEnumerable<FavoriteDto>>(pagedFavorites);
        
        foreach (var favoriteDto in favoriteDtos)
        {
            var (originalPrices, discountPrices, _) = await _pizzaPriceService.GetConvertedPricesAsync(favoriteDto.Pizza.Id, targetCurrency);
        
            var priceIndex = 0;
            foreach (var price in favoriteDto.Pizza.Prices)
            {
                if (originalPrices.Count > priceIndex)
                    price.OriginalPrice = originalPrices.ElementAt(priceIndex).Value;
                if (discountPrices.Count > priceIndex)
                    price.DiscountPrice = discountPrices.ElementAt(priceIndex).Value;

                priceIndex++;
            }
        }
        
        return new PaginatedResponse<FavoriteDto>
        {
            Data = favoriteDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }
}