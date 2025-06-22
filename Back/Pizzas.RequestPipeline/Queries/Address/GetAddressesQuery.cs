using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Address;

public record GetAddressesQuery : IRequest<IEnumerable<AddressDto>>;