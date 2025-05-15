using AutoMapper;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;
using Pizzas.Core.Enums;

namespace Pizzas.Application.Services.Main;

public class PizzaPriceService : IPizzaPriceService
{
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IPizzaPriceRepository _pizzaPriceRepository;
    private readonly ICurrencyService _currencyService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PizzaPriceService(
        IPizzaRepository pizzaRepository,
        IPizzaPriceRepository pizzaPriceRepository,
        ICurrencyService currencyService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _pizzaRepository = pizzaRepository;
        _pizzaPriceRepository = pizzaPriceRepository;
        _currencyService = currencyService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<(Dictionary<PizzaSize, decimal> ConvertedOriginalPrices, Dictionary<PizzaSize, decimal> ConvertedDiscountPrices, string Currency)>
        GetConvertedPricesAsync(string pizzaId, string targetCurrency)
    {
        var pizza = await _pizzaRepository.GetByIdAsync(pizzaId);

        if (pizza == null || pizza.Prices == null || !pizza.Prices.Any())
            throw new Exception("Pizza or prices not found");

        var originalPrices = new Dictionary<PizzaSize, decimal>();
        var discountPrices = new Dictionary<PizzaSize, decimal>();

        decimal rate = 1;
        string currency = "USD";

        if (!string.IsNullOrEmpty(targetCurrency) && targetCurrency != "USD")
        {
            rate = await _currencyService.GetExchangeRateAsync("USD", targetCurrency);
            currency = targetCurrency;
        }

        foreach (var price in pizza.Prices)
        {
            originalPrices[price.Size] = price.OriginalPrice * rate;
            discountPrices[price.Size] = price.DiscountPrice > 0 ? price.DiscountPrice * rate : 0;
        }

        return (originalPrices, discountPrices, currency);
    }

    public async Task<PizzaPriceDto> UpdatePricesAsync(string id, IEnumerable<UpdatePizzaPriceDto> prices)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            PizzaPriceEntity? lastUpdated = null;

            foreach (var dto in prices)
            {
                var existingPrice = (await _pizzaPriceRepository.FindAsync(p =>
                    p.PizzaId == id && p.Size == dto.Size)).FirstOrDefault()
                    .EnsureFound("PriceNotFound");

                existingPrice.OriginalPrice = dto.OriginalPrice.Value;
                existingPrice.DiscountPrice = dto.DiscountPrice.Value;

                await _pizzaPriceRepository.UpdateAsync(new[] { existingPrice });

                lastUpdated = existingPrice;
            }

            return _mapper.Map<PizzaPriceDto>(lastUpdated);
        });
    }

    
    public async Task<PizzaPriceDto> AddPriceAsync(CreatePizzaPriceDto dto)
    {
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var entity = _mapper.Map<PizzaPriceEntity>(dto);

            await _pizzaPriceRepository.AddAsync(entity);
            return _mapper.Map<PizzaPriceDto>(entity);
        });
    }

    public async Task<IEnumerable<PizzaPriceDto>> GetPriceAsync(string pizzaId, PizzaSize size, string currency)
    {
        var prices = await _pizzaPriceRepository.FindAsync(p => p.PizzaId == pizzaId
                                                                && p.Size == size);
        if (!prices.Any())
            throw new PizzasException(ExceptionType.NotFound,"NoPricesFound");

        return _mapper.Map<IEnumerable<PizzaPriceDto>>(prices);
    }
}