using MediatR;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record HasAllPermissionsQuery(IEnumerable<string> PermissionNames) : IRequest<bool>;