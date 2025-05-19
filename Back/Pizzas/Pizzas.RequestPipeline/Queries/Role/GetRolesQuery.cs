using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Role;

public record GetRolesQuery : IRequest<IEnumerable<RoleDto>>;