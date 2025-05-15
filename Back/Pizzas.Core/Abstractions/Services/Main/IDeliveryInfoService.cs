using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Main;

public interface IDeliveryInfoService
{
    Task<DeliveryInfoDto> GetByOrderIdAsync(string orderId);
    Task<DeliveryInfoDto> CreateAsync(CreateDeliveryInfoDto createDto);
    Task<DeliveryInfoDto> UpdateAsync(UpdateDeliveryInfoDto updateDto);
    Task<bool> ValidateDeliveryAddressAsync(string city, string address, string postalCode);
}