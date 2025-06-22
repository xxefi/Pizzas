namespace Pizzas.Core.Dtos.Read;

public class SalesStatsDto
{
    public decimal TotalSales { get; set; }
    public int OrderCount { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal RefundAmount { get; set; }
    public ICollection<PaymentMethodStatsDto> PaymentMethods { get; set; } = [];
}