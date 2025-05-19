using CurrencyExchange.Domain.Abstractions.Repositories;
using CurrencyExchange.Domain.Abstractions.Services;
using CurrencyExchange.Domain.Entities;

namespace CurrencyExchange.Application.Services;

public class CurrencyExchangeRateService : ICurrencyExchangeRateService
{
    private readonly ICurrencyExchangeRateRepository _repository;

    public CurrencyExchangeRateService(ICurrencyExchangeRateRepository repository)
        => _repository = repository;
    
    public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
    {
        var exchangeRateEntity = await _repository.GetAsync(fromCurrency, toCurrency);
        if (exchangeRateEntity != null)
        {
            return exchangeRateEntity.ExchangeRate;
        }

        return 0; 
    }

    public async Task AddOrUpdateExchangeRateAsync(string fromCurrency, string toCurrency, decimal exchangeRate)
    {
        var exchangeRateEntity = await _repository.GetAsync(fromCurrency, toCurrency);

        if (exchangeRateEntity != null)
        {
            exchangeRateEntity.ExchangeRate = exchangeRate;
            exchangeRateEntity.LastUpdated = DateTime.UtcNow;
            await _repository.UpdateAsync(exchangeRateEntity);
        }
        else
        {
            var newExchangeRate = new CurrencyExchangeRateEntity
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                ExchangeRate = exchangeRate,
                LastUpdated = DateTime.UtcNow
            };

            await _repository.AddAsync(newExchangeRate);
        }
    }
}