using CurrencyExchange.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyExchangeController : ControllerBase
{
    private readonly ICurrencyExchangeRateService _currencyExchangeRateService;

    public CurrencyExchangeController(ICurrencyExchangeRateService currencyExchangeRateService)
    {
        _currencyExchangeRateService = currencyExchangeRateService;
    }

    [HttpGet("rate")]
    public async Task<IActionResult> GetExchangeRate(string fromCurrency, string toCurrency)
    {
        var rate = await _currencyExchangeRateService.GetExchangeRateAsync(fromCurrency, toCurrency);
        if (rate == 0)
        {
            return NotFound("Exchange rate not found.");
        }

        return Ok(rate);
    }

    [HttpPost("rate")]
    public async Task<IActionResult> AddOrUpdateExchangeRate([FromBody] CurrencyExchangeRateDto exchangeRateDto)
    {
        await _currencyExchangeRateService.AddOrUpdateExchangeRateAsync(exchangeRateDto.FromCurrency, exchangeRateDto.ToCurrency, exchangeRateDto.ExchangeRate);
        return Ok();
    }
}

public class CurrencyExchangeRateDto
{
    public string FromCurrency { get; set; } = string.Empty;
    public string ToCurrency { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
}