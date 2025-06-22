using MediatR;

namespace Pizzas.RequestPipeline.Commands.Address;

public record DeleteAddressCommand(string Id) : IRequest<bool>;