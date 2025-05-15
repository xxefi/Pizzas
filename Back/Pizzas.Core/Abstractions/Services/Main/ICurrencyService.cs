namespace Pizzas.Core.Abstractions.Services.Main;

public interface ICurrencyService
{
    Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);
}