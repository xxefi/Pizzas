namespace Pizzas.Core.Dtos.Read;

public class UniversalRequestDto<T>
{
    public string Operation { get; set; } = string.Empty;
    public T Parameters { get; set; }
}