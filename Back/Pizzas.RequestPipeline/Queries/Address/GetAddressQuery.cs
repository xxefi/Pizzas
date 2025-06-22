using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Address;

public record GetAddressQuery(string Id) : IRequest<AddressDto>;