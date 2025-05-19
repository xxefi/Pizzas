namespace Pizzas.Core.Dtos.Create;

public class CreateReviewDto
{
    public string PizzaId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
}