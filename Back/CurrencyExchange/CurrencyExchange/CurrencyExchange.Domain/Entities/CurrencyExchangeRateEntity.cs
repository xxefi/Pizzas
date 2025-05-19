namespace CurrencyExchange.Domain.Entities;

public class CurrencyExchangeRateEntity
{
    public Guid Id { get; set; }
    public string FromCurrency { get; set; } = string.Empty;
    public string ToCurrency { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
    public DateTime LastUpdated { get; set; }
}