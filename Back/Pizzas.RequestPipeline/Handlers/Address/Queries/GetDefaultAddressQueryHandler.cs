using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Queries;

public class GetDefaultAddressQueryHandler : IRequestHandler<GetDefaultAddressQuery, AddressDto>
{
    private readonly IAddressService _addressService;

    public GetDefaultAddressQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<AddressDto> Handle(GetDefaultAddressQuery request, CancellationToken cancellationToken)
    {
        return await _addressService.GetDefaultAddressAsync();
    }
}