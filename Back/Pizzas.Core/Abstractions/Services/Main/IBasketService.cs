using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Entities;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IBasketService
{
    Task<BasketDto> GetBasketAsync(string targetCurrency);
    Task<BasketDto> AddItemAsync(string pizzaId, int quantity, string targetCurrency, PizzaSize size);
    Task<BasketDto> UpdateItemQuantityAsync(string basketItemId, int quantity, string targetCurrency);
    Task<BasketDto> RemoveItemAsync(string basketItemId, string targetCurrency);
    Task<bool> ClearBasketAsync(string targetCurrency);
    Task<int> GetItemsCountAsync();
    Task<decimal> GetTotalAsync(string targetCurrency);
}