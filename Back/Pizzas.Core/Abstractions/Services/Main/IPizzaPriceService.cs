using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;
using Pizzas.Core.Enums;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IPizzaPriceService
{
    Task<(Dictionary<PizzaSize, decimal> ConvertedOriginalPrices, Dictionary<PizzaSize, decimal> ConvertedDiscountPrices,
            string Currency)>
        GetConvertedPricesAsync(string pizzaId, string targetCurrency);

    Task<PizzaPriceDto> UpdatePricesAsync(string id, IEnumerable<UpdatePizzaPriceDto> prices);
    Task<PizzaPriceDto> AddPriceAsync(CreatePizzaPriceDto price);
    Task<IEnumerable<PizzaPriceDto>> GetPriceAsync(string pizzaId, PizzaSize size, string currency);
}