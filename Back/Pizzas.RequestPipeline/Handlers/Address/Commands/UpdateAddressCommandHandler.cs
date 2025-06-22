using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Commands;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressDto>
{
    private readonly IAddressService _addressService;

    public UpdateAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<AddressDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        return await _addressService.UpdateAddressAsync(request.Id, request.Address);
    }
}