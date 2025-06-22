using MediatR;

namespace Pizzas.RequestPipeline.Commands.Address;

public record SetDefaultAddressCommand(string AddressId) : IRequest<bool>;