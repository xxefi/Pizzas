using MediatR;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record HasPermissionQuery(string PermissionName) : IRequest<bool>;