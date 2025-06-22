using System.Globalization;
using Pizzas.Core.Abstractions.Services.Main;

namespace Pizzas.Application.Services.Main;

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
    {
        var url = $"http://localhost:5295/api/CurrencyExchange/rate?fromCurrency={fromCurrency}&toCurrency={toCurrency}";

        var response = await _httpClient.GetStringAsync(url);
        
        if (decimal.TryParse(response.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var exchangeRate))
            return exchangeRate;
            
        return decimal.Zero;
    }
}
