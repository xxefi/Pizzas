namespace CurrencyExchange.Domain.Abstractions.Services;

public interface ICurrencyExchangeRateService
{
    Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);
    Task AddOrUpdateExchangeRateAsync(string fromCurrency, string toCurrency, decimal exchangeRate);
}