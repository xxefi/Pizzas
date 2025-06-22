namespace Pizzas.Core.Dtos.Read;

public class CustomerStatsDto
{
    public int TotalCustomers { get; set; }
    public int NewCustomers { get; set; }
    public int ReturningCustomers { get; set; }
    public decimal AverageCustomerValue { get; set; }
    public ICollection<CustomerSegmentDto> Segments { get; set; } = [];
}