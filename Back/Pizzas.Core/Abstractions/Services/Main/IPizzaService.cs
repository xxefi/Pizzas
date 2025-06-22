using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IPizzaService
{
    Task<IEnumerable<PizzaDto>> GetAllPizzasAsync(string targetCurrency);
    Task<PizzaDto> GetPizzaByIdAsync(string pizzaId, string targetCurrency);
    Task<IEnumerable<PizzaDto>> GetPizzasByIngredientsAsync(IEnumerable<string> ingredients, string targetCurrency);
    Task<decimal> GetPizzaPriceAsync(string pizzaId, PizzaSize size, string targetCurrency);
    Task<PaginatedResponse<PizzaDto>> GetPizzasPageAsync(int pageNumber, int pageSize, string targetCurrency);
    Task<PaginatedResponse<PizzaDto>> GetPizzasByCategoryAsync(int pageNumber, int pageSize, string categoryName, string targetCurrency);
    Task<IEnumerable<PizzaDto>> GetTopRatedPizzasAsync(int count, string targetCurrency);
    Task<IEnumerable<PizzaDto>> GetRecommendedPizzasAsync(string targetCurrency);
    Task<IEnumerable<PizzaDto>> GetPopularPizzasAsync(string targetCurrency);
    Task<IEnumerable<PizzaDto>> GetNewReleasesAsync(string targetCurrency);
    Task<IEnumerable<PizzaDto>> SearchPizzasAsync(string searchTerm, string targetCurrency);
    Task<PizzaDto> CreatePizzaAsync(CreatePizzaDto createPizzaDto);
    Task<PizzaDto> UpdatePizzaAsync(string id, UpdatePizzaDto updatePizzaDto);
    Task<bool> DeletePizzaAsync(string pizzaId);
}