namespace Pizzas.Core.Dtos.Read;

public class UniversalRequestDto<T>
{
    public string Action { get; set; } = string.Empty;
    public T Parameters { get; set; }
}