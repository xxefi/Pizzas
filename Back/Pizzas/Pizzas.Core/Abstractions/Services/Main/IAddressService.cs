using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IAddressService
{
    Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
    Task<AddressDto?> GetAddressByIdAsync(string id);
    Task<AddressDto> CreateAddressAsync(CreateAddressDto createAddressDto);
    Task<AddressDto> UpdateAddressAsync(string id, UpdateAddressDto updateAddressDto);
    Task<bool> DeleteAddressAsync(string id);
    Task<bool> SetDefaultAddressAsync(string addressId);
    Task<AddressDto?> GetDefaultAddressAsync();
    Task<PaginatedResponse<AddressDto>> GetAddressesPageAsync(int pageNumber, int pageSize);
}