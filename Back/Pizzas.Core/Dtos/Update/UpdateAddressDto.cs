namespace Pizzas.Core.Dtos.Update;

public class UpdateAddressDto
{
    public string? Street { get; set; } = null!;
    public string? City { get; set; } = null!;
    public string? State { get; set; } = null!;
    public string? Country { get; set; } = null!;
    public string? PostalCode { get; set; } = null!;
    public bool? IsDefault { get; set; }
}