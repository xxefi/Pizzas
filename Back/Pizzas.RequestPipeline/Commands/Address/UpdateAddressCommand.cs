using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Address;

public record UpdateAddressCommand(string Id, UpdateAddressDto Address) : IRequest<AddressDto>;