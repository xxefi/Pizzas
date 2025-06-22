namespace Pizzas.Core.Dtos.Read;

public class PaymentMethodStatsDto
{
    public string Method { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int Count { get; set; }
    public decimal Percentage { get; set; }
}