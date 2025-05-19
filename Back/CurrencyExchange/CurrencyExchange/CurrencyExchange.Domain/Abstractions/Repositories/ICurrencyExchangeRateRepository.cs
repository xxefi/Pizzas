using CurrencyExchange.Domain.Entities;

namespace CurrencyExchange.Domain.Abstractions.Repositories;

public interface ICurrencyExchangeRateRepository
{
    Task<CurrencyExchangeRateEntity> GetAsync(string fromCurrency, string toCurrency);
    Task<IEnumerable<CurrencyExchangeRateEntity>> GetAllAsync();
    Task AddAsync(CurrencyExchangeRateEntity entity);
    Task UpdateAsync(CurrencyExchangeRateEntity entity);
}