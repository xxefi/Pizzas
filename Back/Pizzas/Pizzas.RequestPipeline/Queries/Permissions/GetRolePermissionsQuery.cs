using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record GetRolePermissionsQuery(string RoleId) : IRequest<IEnumerable<PermissionDto>>;