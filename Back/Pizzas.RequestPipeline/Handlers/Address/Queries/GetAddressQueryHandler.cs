using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Queries;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, AddressDto>
{
    private readonly IAddressService _addressService;

    public GetAddressQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<AddressDto> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        return await _addressService.GetAddressByIdAsync(request.Id);
    }
}