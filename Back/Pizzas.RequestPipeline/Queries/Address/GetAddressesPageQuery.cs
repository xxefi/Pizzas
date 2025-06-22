using MediatR;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Address;

public record GetAddressesPageQuery(int PageNumber, int PageSize) :
    IRequest<PaginatedResponse<AddressDto>>;