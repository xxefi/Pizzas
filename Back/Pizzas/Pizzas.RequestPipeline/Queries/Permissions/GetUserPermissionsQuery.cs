using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record GetUserPermissionsQuery : IRequest<IEnumerable<PermissionDto>>;