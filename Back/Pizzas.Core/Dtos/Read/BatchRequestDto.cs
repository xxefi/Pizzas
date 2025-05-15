namespace Pizzas.Core.Dtos.Read;

public class BatchRequestDto
{
    public List<UniversalRequestDto<object>> Requests { get; set; } = [];
}