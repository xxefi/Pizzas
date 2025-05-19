using MediatR;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record HasAnyPermissionQuery(IEnumerable<string> PermissionNames) : IRequest<bool>;