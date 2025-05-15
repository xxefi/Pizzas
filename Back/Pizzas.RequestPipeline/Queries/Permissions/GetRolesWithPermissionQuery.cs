using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record GetRolesWithPermissionQuery(string PermissionName) : IRequest<IEnumerable<RoleDto>>;