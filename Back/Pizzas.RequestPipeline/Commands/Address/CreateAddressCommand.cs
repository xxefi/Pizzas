using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Address;

public record CreateAddressCommand(CreateAddressDto Address) : IRequest<AddressDto>;