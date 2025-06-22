namespace Pizzas.Core.Dtos.Read;

public class SalesChartDto
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
    public int Customers { get; set; }
    public int OrderCount { get; set; }
}
