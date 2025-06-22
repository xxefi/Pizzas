using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Queries;

public class GetAddressesPageQueryHandler : IRequestHandler<GetAddressesPageQuery, PaginatedResponse<AddressDto>>
{
    private readonly IAddressService _addressService;

    public GetAddressesPageQueryHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<PaginatedResponse<AddressDto>> Handle(GetAddressesPageQuery request, CancellationToken cancellationToken)
    {
        return await _addressService.GetAddressesPageAsync(request.PageNumber, request.PageSize);
    }
}