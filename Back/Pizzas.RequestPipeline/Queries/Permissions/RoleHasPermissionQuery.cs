using MediatR;

namespace Pizzas.RequestPipeline.Queries.Permissions;

public record RoleHasPermissionQuery(string RoleId, string PermissionName) : IRequest<bool>;