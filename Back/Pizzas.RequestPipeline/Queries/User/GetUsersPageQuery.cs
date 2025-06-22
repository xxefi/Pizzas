using MediatR;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.User;

public record GetUsersPageQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponse<UserDto>>;