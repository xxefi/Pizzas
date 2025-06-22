namespace Pizzas.Core.Dtos.Read;

public class TopProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int StockLevel { get; set; }
}