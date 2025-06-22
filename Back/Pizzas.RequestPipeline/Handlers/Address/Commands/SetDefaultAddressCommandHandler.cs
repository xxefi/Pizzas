using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Commands;

public class SetDefaultAddressCommandHandler : IRequestHandler<SetDefaultAddressCommand, bool>
{
    private readonly IAddressService _addressService;

    public SetDefaultAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<bool> Handle(SetDefaultAddressCommand request, CancellationToken cancellationToken)
    {
        return await _addressService.SetDefaultAddressAsync(request.AddressId);
    }
}