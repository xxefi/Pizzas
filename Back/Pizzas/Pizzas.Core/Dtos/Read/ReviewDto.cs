namespace Pizzas.Core.Dtos.Read;

public class ReviewDto
{
    public string Id { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public PublicUserDto User { get; set; } = null!;
}