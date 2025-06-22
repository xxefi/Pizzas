using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IFavoriteService
{
    Task<FavoriteDto> AddToFavoritesAsync(string pizzaId, string targetCurrency); 
    Task<FavoriteDto> RemoveFromFavoritesAsync(string pizzaId);
    Task<IEnumerable<FavoriteDto>> GetFavoritesAsync(string targetCurrency); 
    Task<int> GetFavoritesCountAsync(); 
    Task<PaginatedResponse<FavoriteDto>> GetFavoritesPageAsync(int pageNumber, int pageSize, string targetCurrency); 
}