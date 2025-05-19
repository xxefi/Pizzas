namespace Pizzas.Core.Common;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Data { get; set; } = [];
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}