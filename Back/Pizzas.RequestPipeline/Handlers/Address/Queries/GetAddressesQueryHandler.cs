using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Queries;

public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, IEnumerable<AddressDto>>
{
    private readonly IAddressService _addressService;

    public GetAddressesQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<IEnumerable<AddressDto>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
    {
        return await _addressService.GetAllAddressesAsync();
    }
}