namespace Pizzas.Core.Dtos.Read;

public class UserOrdersStatsDto
{
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
    public int CompletedOrders { get; set; }
    public DateTime LastOrderDate { get; set; }
}