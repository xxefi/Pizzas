using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Role;

public record GetRolePermissionsQuery(string RoleId) : IRequest<IEnumerable<PermissionDto>>;