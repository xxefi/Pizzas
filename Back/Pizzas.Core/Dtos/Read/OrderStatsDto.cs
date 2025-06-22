namespace Pizzas.Core.Dtos.Read;

public class OrderStatsDto
{
    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int CompletedOrders { get; set; }
    public int CancelledOrders { get; set; }
    public decimal AveragePreparationTime { get; set; }
    public decimal AverageDeliveryTime { get; set; }
}
