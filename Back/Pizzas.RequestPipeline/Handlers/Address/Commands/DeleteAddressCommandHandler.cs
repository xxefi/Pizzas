using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Address;

namespace Pizzas.RequestPipeline.Handlers.Address.Commands;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly IAddressService _addressService;

    public DeleteAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        return await _addressService.DeleteAddressAsync(request.Id);
    }
}