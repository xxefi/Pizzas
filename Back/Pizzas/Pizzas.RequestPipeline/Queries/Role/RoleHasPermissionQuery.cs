using MediatR;

namespace Pizzas.RequestPipeline.Queries.Role;

public record RoleHasPermissionQuery(string RoleId, string PermissionName) : IRequest<bool>;