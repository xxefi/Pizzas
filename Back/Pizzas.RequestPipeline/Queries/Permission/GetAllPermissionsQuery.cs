using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Permission;

public record GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>;